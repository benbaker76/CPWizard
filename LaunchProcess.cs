// Copyright (c) 2015, Ben Baker
// All rights reserved.
// 
// This source code is licensed under the BSD-style license found in the
// LICENSE file in the root directory of this source tree. 

using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace CPWizard
{
	class LaunchProcess
	{
		private Process m_Process;
		private string m_Filename;
		private string m_Args;
		private int m_killTimeout;
		private System.Timers.Timer m_killTimer;

		public delegate void ProcessExitedHandler();
		public event ProcessExitedHandler OnExited;

		public delegate void SendKeysReadyHandler();
		public event SendKeysReadyHandler OnSendKeysReady;

		public LaunchProcess(string filename, string args)
		{
			m_Filename = filename;
			m_Args = args;
			m_killTimeout = 0;
		}

		public LaunchProcess(string filename, string args, int killTimeout)
		{
			m_Filename = filename;
			m_Args = args;
			m_killTimeout = killTimeout;
		}

		public bool Start()
		{
			try
			{
				m_Process = new Process();
				m_Process.Exited += ProcessExitedEvent;
				m_Process.StartInfo.WorkingDirectory = Path.GetDirectoryName(m_Filename);
				m_Process.StartInfo.FileName = m_Filename;
				m_Process.StartInfo.Arguments = m_Args;
				m_Process.StartInfo.CreateNoWindow = true;
				m_Process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
				m_Process.EnableRaisingEvents = true;
				m_Process.Start();

				WaitForInput();
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("Start", "LaunchProcess", ex.Message, ex.StackTrace);

				if (OnExited != null)
					OnExited();

				return false;
			}

			if (m_killTimeout != 0)
			{
				m_killTimer = new System.Timers.Timer(m_killTimeout);
				m_killTimer.Elapsed += KillTimerEvent;
				m_killTimer.Enabled = true;
			}

			return true;
		}

		private void WaitForInput()
		{
			try
			{
				if (OnSendKeysReady != null)
				{
					// Wait for process to be ready to receive input
					m_Process.WaitForInputIdle();

					// Is process responding?
					if (m_Process.Responding)
					{
						// Raise an event to say it's ready for sending keys
						OnSendKeysReady();
					}
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("WaitForInput", "LaunchProcess", ex.Message, ex.StackTrace);
			}
		}

		private void KillTimerEvent(object sender, System.Timers.ElapsedEventArgs e)
		{
			m_killTimer.Enabled = false;

			if (m_Process != null)
			{
				if (!m_Process.HasExited)
					m_Process.Kill();

				if (OnExited != null)
					OnExited();
			}
		}

		private void ProcessExitedEvent(object sender, System.EventArgs e)
		{
			if (OnExited != null)
				OnExited();
		}
	}
}
