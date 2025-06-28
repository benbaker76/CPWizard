// Copyright (c) 2015, Ben Baker
// All rights reserved.
// 
// This source code is licensed under the BSD-style license found in the
// LICENSE file in the root directory of this source tree. 

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Management;
using Microsoft.Win32;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace CPWizard
{
	public class LogFile
	{
		private static string m_fileName = null;

		private static object m_lockObject = null;

		public delegate void LogFileDelegate(string errorString);

		public static LogFileDelegate LogFileEvent = null;

		static LogFile()
		{
			m_lockObject = new object();
		}

		public static void ClearLog()
		{
			if (String.IsNullOrEmpty(m_fileName))
				return;

			try
			{
				lock (m_lockObject)
				{
					string folder = Path.GetDirectoryName(m_fileName);

					if (!Directory.Exists(folder))
						Directory.CreateDirectory(folder);

					using (System.IO.StreamWriter streamWriter = File.CreateText(m_fileName))
						streamWriter.Flush();
				}
			}
			catch (Exception)
			{
				//System.Windows.Forms.MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);
			}
		}

		public static void VerboseWriteLine(string methodName, string className, string errorString)
		{
			if (Settings.General.VerboseLogging)
				WriteLine(String.Format("{0} ({1}) -> {2}", methodName, className, errorString));
		}

		public static void VerboseWriteLine(string errorString)
		{
			if (Settings.General.VerboseLogging)
				WriteLine(errorString);
		}

		public static void WriteLine(string methodName, string className, string errorString)
		{
			WriteLine(String.Format("ERROR @ {0} ({1})", methodName ?? "", className ?? ""));
			WriteLine(errorString ?? "");
		}

		public static void WriteLine(string methodName, string className, string errorString, string stackTrace)
		{
			WriteLine(String.Format("ERROR @ {0} ({1})", (methodName ?? ""), className ?? ""));
			WriteLine(errorString ?? "");
			WriteLine(stackTrace ?? "");
		}

		public static void WriteLine(string errorString, string stackTrace)
		{
			WriteLine(String.Format("{0} : {1}", errorString ?? "", stackTrace ?? ""));
		}

		public static void WriteLine(string format, params object[] args)
		{
			if (String.IsNullOrEmpty(m_fileName))
				return;

			try
			{
				lock (m_lockObject)
				{
					using (System.IO.StreamWriter streamWriter = File.AppendText(m_fileName))
					{
						streamWriter.WriteLine(String.Format("{0}: {1}", DateTime.Now, String.Format(format, args)));
						streamWriter.Flush();
					}
				}

				if (LogFileEvent != null)
					LogFileEvent(String.Format(format, args));
			}
			catch (Exception)
			{
				//System.Windows.Forms.MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);
			}
		}

		public static void OutputSystemConfiguration()
		{
			try
			{

				//WriteLine("CPWizard " + Globals.Version); //moved to main program in case config parse disabled

				ManagementObjectSearcher query = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem");

				foreach (ManagementObject mo in query.Get())
				{
					Double totalRAM = Double.Parse(mo["TotalVisibleMemorySize"].ToString()) / 1024;
					Double freeRAM = Double.Parse(mo["FreePhysicalMemory"].ToString()) / 1024;

					WriteLine("OS: " + mo["Caption"].ToString());
					WriteLine("Version: " + mo["Version"].ToString());
					WriteLine("Build: " + mo["BuildNumber"].ToString());
					WriteLine("RAM Total: " + Math.Ceiling(totalRAM) + " MB");
					WriteLine("RAM Used: " + Math.Ceiling(totalRAM - freeRAM) + " MB");
				}

				query = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");

				foreach (ManagementObject obj in query.Get())
					WriteLine("CPU: " + obj["Name"].ToString().TrimStart());

				query = new ManagementObjectSearcher("SELECT * FROM Win32_VideoController");

				foreach (ManagementObject obj in query.Get())
				{
					WriteLine("Video Card: " + obj["Name"].ToString());
					WriteLine("Video Driver: " + obj["DriverVersion"].ToString());
					if (obj["AdapterRAM"] != null)
					{
						double videoRAM = double.Parse(obj["AdapterRAM"].ToString()) / 1024 / 1024;
						WriteLine("Video RAM: " + videoRAM + " MB");
					}
				}

				query = new ManagementObjectSearcher("SELECT * FROM Win32_SoundDevice");

				foreach (ManagementObject obj in query.Get())
				{
					WriteLine("Sound Card: " + obj["Name"].ToString());
				}

				if (Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\.NETFramework\\policy\\v1.0") != null)
					WriteLine(".NET: .NET Framework 1.0 Installed");
				if (Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\.NETFramework\\policy\\v1.1") != null)
					WriteLine(".NET: .NET Framework 1.1 Installed");
				if (Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\.NETFramework\\policy\\v2.0") != null)
					WriteLine(".NET: .NET Framework 2.0 Installed");
				if (Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\.NETFramework\\policy\\v3.0") != null)
					WriteLine(".NET: .NET Framework 3.0 Installed");

				query.Dispose();
				query = null;
			}
			catch (Exception)
			{
				//MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
			}
		}

		public static string FileName
		{
			get { return m_fileName; }
			set { m_fileName = value; }
		}
	}
}
