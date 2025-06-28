using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;

namespace CPWizard
{
	public partial class frmInterComm : Form
	{
		public enum InterCommMode
		{
			Server,
			Client
		}

		private Mutex m_mutex = null;
		private IntPtr m_hWnd = IntPtr.Zero;

		private InterCommMode m_interCommMode = InterCommMode.Server;

		public delegate void MessageReceivedDelegate(int id, string str);

		public event MessageReceivedDelegate MessageReceived;

		public frmInterComm()
		{
			InitializeComponent();

			this.Text = "IPC_Server";

			if (!IsFirstInstance())
			{
				m_interCommMode = InterCommMode.Client;
				this.Text = "IPC_Client";
			}

			this.Show();
		}

		public bool IsFirstInstance()
		{
			try
			{
				string m_UniqueIdentifier;
				string assemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName(false).CodeBase;
				m_UniqueIdentifier = assemblyName.Replace("\\", "_").ToLower();

				m_mutex = new Mutex(false, m_UniqueIdentifier);

				if (m_mutex.WaitOne(1, true)) // FirstInstance
				{
					return true;
				}
				else
				{
					m_mutex.Close();
					m_mutex = null;

					return false;
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("IsFirstInstance", "InterComm", ex.Message, ex.StackTrace);
			}

			return true;
		}

		private IntPtr GetTargethWnd()
		{
			switch (m_interCommMode)
			{
				case InterCommMode.Server:
					return Win32.FindWindow(IntPtr.Zero, "IPC_Client");
				case InterCommMode.Client:
					return Win32.FindWindow(IntPtr.Zero, "IPC_Server");
			}

			return IntPtr.Zero;
		}

		public bool SendMessage(int id, string str)
		{
			IntPtr hWnd = GetTargethWnd();

			if (hWnd == IntPtr.Zero)
			{
				LogFile.VerboseWriteLine("SendMessage", "InterComm", String.Format("{0} Failed To Send Message Id:{1} Str:{2}", this.Text, Globals.DataModeString[id], str));

				return false;
			}

			Win32.CopyDataStruct cds = new Win32.CopyDataStruct();

			try
			{
				cds.cbData = (str.Length + 1) * 2;
				cds.lpData = Win32.LocalAlloc(0x40, cds.cbData);
				Marshal.Copy(str.ToCharArray(), 0, cds.lpData, str.Length);
				cds.dwData = new IntPtr(1);

				Win32.SendMessage(hWnd, Win32.WM_COPYDATA, new IntPtr(id), ref cds);

				LogFile.VerboseWriteLine("SendMessage", "InterComm", String.Format("{0} Succeeded To Send Message Id:{1} Str:{2}", this.Text, Globals.DataModeString[id], str));
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("SendMessage", "InterComm", ex.Message, ex.StackTrace);
			}
			finally
			{
				cds.Dispose();
			}

			return true;
		}

		protected override bool ShowWithoutActivation
		{
			get { return true; }
		}

		protected override void WndProc(ref Message m)
		{
			switch (m.Msg)
			{
				case Win32.WM_COPYDATA:
					Win32.CopyDataStruct copyData = (Win32.CopyDataStruct)Marshal.PtrToStructure(m.LParam, typeof(Win32.CopyDataStruct));

					string strData = Marshal.PtrToStringUni(copyData.lpData);

					LogFile.VerboseWriteLine("SendMessage", "InterComm", String.Format("{0} Succeeded To Receive Message Id:{1} Str:{2}", this.Text, Globals.DataModeString[m.WParam.ToInt32()], strData));

					if (MessageReceived != null)
						MessageReceived(m.WParam.ToInt32(), strData);
					break;

				case Win32.WM_WINDOWPOSCHANGING:
					Win32.WINDOWPOS windowPos = (Win32.WINDOWPOS)Marshal.PtrToStructure(m.LParam, typeof(Win32.WINDOWPOS));

					windowPos.flags &= unchecked((uint)~Win32.SWP_SHOWWINDOW);
					Marshal.StructureToPtr(windowPos, m.LParam, true);
					m.Result = IntPtr.Zero;
					break;
			}

			base.WndProc(ref m);
		}


		#region IDisposable Members

		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}

			if (m_mutex != null)
			{
				m_mutex.Close();
				m_mutex = null;
			}

			base.Dispose(disposing);
		}

		#endregion

		public InterCommMode CommMode
		{
			get { return m_interCommMode; }
		}
	}

	partial class Win32
	{
		[StructLayout(LayoutKind.Sequential)]
		public struct WINDOWPOS
		{
			public IntPtr hwnd;
			public IntPtr hwndAfter;
			public int x;
			public int y;
			public int cx;
			public int cy;
			public uint flags;
		}

		public struct CopyDataStruct : IDisposable
		{
			public IntPtr dwData;
			public int cbData;
			public IntPtr lpData;

			public void Dispose()
			{
				if (this.lpData != IntPtr.Zero)
				{
					LocalFree(this.lpData);
					this.lpData = IntPtr.Zero;
				}
			}
		}

		[DllImport("User32.dll")]
		public static extern IntPtr FindWindow(IntPtr strClassName, string strWindowName);

		[DllImport("User32.dll")]
		public static extern Int32 SendMessage(int hWnd, int Msg, int wParam, [MarshalAs(UnmanagedType.LPStr)] string lParam);

		[DllImport("user32.dll")]
		public static extern int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, ref CopyDataStruct lParam);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern IntPtr LocalAlloc(int flag, int size);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern IntPtr LocalFree(IntPtr p);

		public const int ERROR_CLASS_ALREADY_EXISTS = 1410;
		public const int WM_COPYDATA = 0x004A;
		public const int WM_WINDOWPOSCHANGING = 0x0046;
	}
}