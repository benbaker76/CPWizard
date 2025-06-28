// Copyright (c) 2015, Ben Baker
// All rights reserved.
// 
// This source code is licensed under the BSD-style license found in the
// LICENSE file in the root directory of this source tree. 

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Collections;

namespace CPWizard
{
	public enum MAMEMessageType : int
	{
		Pause = 0
	};

	public class MAMEInterop : IDisposable
	{
		[DllImport("MAME32.dll", EntryPoint = "init_mame", CallingConvention = CallingConvention.StdCall)]
		static extern int init_mame32(int clientid, string name, [MarshalAs(UnmanagedType.FunctionPtr)] MAME_START start, [MarshalAs(UnmanagedType.FunctionPtr)] MAME_STOP stop, [MarshalAs(UnmanagedType.FunctionPtr)] MAME_COPYDATA copydata, [MarshalAs(UnmanagedType.FunctionPtr)] MAME_OUTPUT output, bool useNetworkOutput);

		[DllImport("MAME32.dll", EntryPoint = "close_mame", CallingConvention = CallingConvention.StdCall)]
		static extern int close_mame32();

		[DllImport("MAME32.dll", EntryPoint = "message_mame", CallingConvention = CallingConvention.StdCall)]
		static extern int message_mame32(int id, int value);

		[DllImport("MAME64.dll", EntryPoint = "init_mame", CallingConvention = CallingConvention.StdCall)]
		static extern int init_mame64(int clientid, string name, [MarshalAs(UnmanagedType.FunctionPtr)] MAME_START start, [MarshalAs(UnmanagedType.FunctionPtr)] MAME_STOP stop, [MarshalAs(UnmanagedType.FunctionPtr)] MAME_COPYDATA copydata, [MarshalAs(UnmanagedType.FunctionPtr)] MAME_OUTPUT output, bool useNetworkOutput);

		[DllImport("MAME64.dll", EntryPoint = "close_mame", CallingConvention = CallingConvention.StdCall)]
		static extern int close_mame64();

		[DllImport("MAME64.dll", EntryPoint = "message_mame", CallingConvention = CallingConvention.StdCall)]
		static extern int message_mame64(int id, int value);

		private delegate int MAME_START(IntPtr hwnd);
		private delegate int MAME_STOP();
		private delegate int MAME_COPYDATA(int id, IntPtr name);
		private delegate int MAME_OUTPUT(IntPtr name, int value);

		[MarshalAs(UnmanagedType.FunctionPtr)]
		private MAME_START startPtr = null;

		[MarshalAs(UnmanagedType.FunctionPtr)]
		private MAME_STOP stopPtr = null;

		[MarshalAs(UnmanagedType.FunctionPtr)]
		private MAME_COPYDATA copydataPtr = null;

		[MarshalAs(UnmanagedType.FunctionPtr)]
		private MAME_OUTPUT outputPtr = null;

		private Control m_control = null;
		private IntPtr m_hWndOutputWindow = IntPtr.Zero;

		public event EventHandler<MAMEEventArgs> MAMEStart = null;
		public event EventHandler<EventArgs> MAMEStop = null;
		public event EventHandler<MAMEOutputEventArgs> MAMEOutput = null;

		private bool m_isRunning = false;

		private bool m_is64Bit = false;

		private bool m_disposed = false;

		public MAMEInterop(Control control)
		{
			m_control = control;
			m_is64Bit = Is64Bit();
		}

		public void Initialize(int clientId, string name, bool useNetworkOutput)
		{
			m_is64Bit = Is64Bit();

			startPtr = new MAME_START(mame_start);
			stopPtr = new MAME_STOP(mame_stop);
			copydataPtr = new MAME_COPYDATA(mame_copydata);
			outputPtr = new MAME_OUTPUT(mame_output);

			if (m_is64Bit)
				init_mame64(clientId, name, startPtr, stopPtr, copydataPtr, outputPtr, useNetworkOutput);
			else
				init_mame32(clientId, name, startPtr, stopPtr, copydataPtr, outputPtr, useNetworkOutput);

			m_isRunning = true;
		}

		public void Shutdown()
		{
			if (m_is64Bit)
				close_mame64();
			else
				close_mame32();

			m_isRunning = false;
		}

		private int mame_start(IntPtr hWnd)
		{
			m_hWndOutputWindow = hWnd;

			return 1;
		}

		private int mame_stop()
		{
			m_hWndOutputWindow = IntPtr.Zero;

			if (MAMEStop != null)
				m_control.BeginInvoke(MAMEStop, this, EventArgs.Empty);

			return 1;
		}

		private int mame_copydata(int id, IntPtr namePtr)
		{
			string name = Marshal.PtrToStringAnsi(namePtr);

			if (id == 0)
			{
				if (MAMEStart != null)
					m_control.BeginInvoke(MAMEStart, this, new MAMEEventArgs(name));
			}

			return 1;
		}

		private int mame_output(IntPtr namePtr, int state)
		{
			string name = Marshal.PtrToStringAnsi(namePtr);

			if (MAMEOutput != null)
				m_control.BeginInvoke(MAMEOutput, this, new MAMEOutputEventArgs(name, state));

			return 1;
		}

		public int MessageMAME(int id, int value)
		{
			int retVal = 0;

			if (m_is64Bit)
				retVal = message_mame64(id, value);
			else
				retVal = message_mame32(id, value);

			return retVal;
		}

		public int PauseMAME(int pauseValue)
		{
			return MessageMAME((int)MAMEMessageType.Pause, pauseValue);
		}

		public bool IsRunning
		{
			get { return m_isRunning; }
		}

		#region IDisposable Members

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this); // remove this from gc finalizer list
		}

		private void Dispose(bool disposing)
		{
			if (!this.m_disposed) // dispose once only
			{
				if (disposing) // called from Dispose
				{
					// Dispose managed resources.
				}

				// Clean up unmanaged resources here.
			}

			m_disposed = true;
		}

		#endregion

		private bool Is64Bit()
		{
			return Marshal.SizeOf(typeof(IntPtr)) == 8;
		}
	}

	public class MAMEEventArgs : EventArgs
	{
		public string ROMName;

		public MAMEEventArgs(string romName)
		{
			ROMName = romName;
		}
	}

	public class MAMEOutputEventArgs : EventArgs
	{
		public string Name;
		public int State;

		public MAMEOutputEventArgs(string name, int state)
		{
			Name = name;
			State = state;
		}
	}
}