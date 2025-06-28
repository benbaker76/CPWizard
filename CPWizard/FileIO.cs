// Copyright (c) 2015, Ben Baker
// All rights reserved.
// 
// This source code is licensed under the BSD-style license found in the
// LICENSE file in the root directory of this source tree. 

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace CPWizard
{
	class FileIO
	{
		public static bool LaunchFileCaptureOutput(string fileName, string arguments, string outputFileName)
		{
			IntPtr hFile = IntPtr.Zero;
			Win32.PROCESS_INFORMATION processInformation = new Win32.PROCESS_INFORMATION();
			Win32.STARTUPINFO startupInfo = new Win32.STARTUPINFO();
			Win32.SECURITY_ATTRIBUTES securityAttributes = new Win32.SECURITY_ATTRIBUTES();

			try
			{
				bool retValue = false;
				securityAttributes.nLength = Marshal.SizeOf(securityAttributes);
				securityAttributes.bInheritHandle = 1;
				securityAttributes.lpSecurityDescriptor = IntPtr.Zero;

				hFile = Win32.CreateFile(outputFileName, Win32.GENERIC_READ | Win32.GENERIC_WRITE, 0, ref securityAttributes, Win32.CREATE_ALWAYS, 0, IntPtr.Zero);

				if (hFile == IntPtr.Zero)
					return false;

				startupInfo.cb = Marshal.SizeOf(startupInfo);
				startupInfo.hStdOutput = hFile;
				startupInfo.dwFlags = Win32.STARTF_USESTDHANDLES;

				retValue = Win32.CreateProcess(fileName, String.Format(" {0}", arguments), ref securityAttributes, ref securityAttributes, true, Win32.NORMAL_PRIORITY_CLASS | Win32.CREATE_NO_WINDOW, IntPtr.Zero, null, ref startupInfo, out processInformation);

				//WaitForSingleObject(pi.hProcess, INFINITE);

				if (retValue)
				{
					while (Win32.WaitForSingleObject(processInformation.hProcess, 1000) != Win32.WAIT_OBJECT_0)
						System.Windows.Forms.Application.DoEvents();
				}

				return retValue;
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("LaunchFileCaptureOutput", "FileIO", ex.Message, ex.StackTrace);
			}
			finally
			{
				Win32.CloseHandle(hFile);
				Win32.CloseHandle(processInformation.hProcess);
				Win32.CloseHandle(processInformation.hThread);
			}

			return false;
		}

		private static FileDialogExtender fileDialogExt = null;

		static FileIO()
		{
			fileDialogExt = new FileDialogExtender();
			EventManager.OnWndProc += new EventManager.WndProcHandler(EventHandler_OnWndProc);
		}

		static void EventHandler_OnWndProc(ref Message msg)
		{
			fileDialogExt.WndProc(ref msg);
		}

		public static bool TryOpenLabel(Form owner, string initialDirectory, string label, out string fileName)
		{
			fileName = null;

			try
			{
				fileDialogExt.Enabled = false;

				OpenFileDialog openFileDialog = new OpenFileDialog();

				openFileDialog.Title = "Open Label File";
				openFileDialog.InitialDirectory = initialDirectory;
				openFileDialog.FileName = label;
				openFileDialog.Filter = "Label Files (*.ini)|*.ini|All Files (*.*)|*.*";
				openFileDialog.RestoreDirectory = true;
				openFileDialog.CheckFileExists = true;

				if (openFileDialog.ShowDialog(owner) == DialogResult.OK)
				{
					fileName = openFileDialog.FileName;

					return true;
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("TryOpenLabel", "FileIO", ex.Message, ex.StackTrace);
			}

			return false;
		}

		public static bool TryOpenLayout(Form owner, string initialDirectory, string layout, out string fileName)
		{
			fileName = null;

			try
			{
				fileDialogExt.Enabled = false;

				OpenFileDialog openFileDialog = new OpenFileDialog();

				openFileDialog.Title = "Open Layout File";
				openFileDialog.InitialDirectory = initialDirectory;
				openFileDialog.FileName = layout;
				openFileDialog.Filter = "Layout Files (*.xml)|*.xml|All Files (*.*)|*.*";
				openFileDialog.RestoreDirectory = true;
				openFileDialog.CheckFileExists = true;

				if (openFileDialog.ShowDialog(owner) == DialogResult.OK)
				{
					fileName = openFileDialog.FileName;

					return true;
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("TryOpenLayout", "FileIO", ex.Message, ex.StackTrace);
			}

			return false;
		}

		public static bool TryOpenDatabase(Form owner, string initialDirectory, string database, out string fileName)
		{
			fileName = null;

			try
			{
				fileDialogExt.Enabled = false;

				OpenFileDialog openFileDialog = new OpenFileDialog();

				openFileDialog.Title = "Open Database File";
				openFileDialog.InitialDirectory = initialDirectory;
				openFileDialog.FileName = database;
				openFileDialog.Filter = "Database Files (*.mdb)|*.mdb|All Files (*.*)|*.*";
				openFileDialog.RestoreDirectory = true;
				openFileDialog.CheckFileExists = true;

				if (openFileDialog.ShowDialog(owner) == DialogResult.OK)
				{
					fileName = openFileDialog.FileName;

					return true;
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("TryOpenDatabase", "FileIO", ex.Message, ex.StackTrace);
			}

			return false;
		}

		public static bool TryOpenImage(Form owner, string initialDirectory, out string fileName)
		{
			fileName = null;

			try
			{
				fileDialogExt.Enabled = true;
				fileDialogExt.DialogViewType = FileDialogExtender.DialogViewTypes.LargeIcons;

				OpenFileDialog openFileDialog = new OpenFileDialog();

				openFileDialog.Title = "Open Image File";
				openFileDialog.InitialDirectory = initialDirectory;
				openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
				openFileDialog.RestoreDirectory = true;
				openFileDialog.CheckFileExists = true;

				if (openFileDialog.ShowDialog(owner) == DialogResult.OK)
				{
					fileName = openFileDialog.FileName;

					return true;
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("TryOpenImage", "FileIO", ex.Message, ex.StackTrace);
			}

			return false;
		}

		public static bool TryOpenExe(Form owner, string initialDirectory, out string fileName)
		{
			fileName = null;

			try
			{
				fileDialogExt.Enabled = false;

				OpenFileDialog openFileDialog = new OpenFileDialog();

				openFileDialog.Title = "Open Exe File";
				openFileDialog.InitialDirectory = initialDirectory;
				openFileDialog.Filter = "Exe Files (*.exe)|*.exe|All Files (*.*)|*.*";
				openFileDialog.RestoreDirectory = true;
				openFileDialog.CheckFileExists = true;

				if (openFileDialog.ShowDialog(owner) == DialogResult.OK)
				{
					fileName = openFileDialog.FileName;

					return true;
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("TryOpenExe", "FileIO", ex.Message, ex.StackTrace);
			}

			return false;
		}

		public static bool TryOpenWav(Form owner, string initialDirectory, out string fileName)
		{
			fileName = null;

			try
			{
				fileDialogExt.Enabled = false;

				OpenFileDialog openFileDialog = new OpenFileDialog();

				openFileDialog.Title = "Open Wav File";
				openFileDialog.InitialDirectory = initialDirectory;
				openFileDialog.Filter = "Wav Files (*.wav)|*.wav|All Files (*.*)|*.*";
				openFileDialog.RestoreDirectory = true;
				openFileDialog.CheckFileExists = true;

				if (openFileDialog.ShowDialog(owner) == DialogResult.OK)
				{
					fileName = Path.GetFileName(openFileDialog.FileName);
					return true;
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("TryOpenWav", "FileIO", ex.Message, ex.StackTrace);
			}

			return false;
		}

		public static bool TryOpenFolder(Form owner, string selectedPath, out string folder)
		{
			folder = null;

			try
			{
				FolderBrowserDialog fileBrowserDialog = new FolderBrowserDialog();

				fileBrowserDialog.SelectedPath = selectedPath;
				fileBrowserDialog.ShowNewFolderButton = true;

				if (fileBrowserDialog.ShowDialog(owner) == DialogResult.OK)
				{
					folder = fileBrowserDialog.SelectedPath;

					return true;
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("TryOpenFolder", "FileIO", ex.Message, ex.StackTrace);
			}

			return false;
		}

		public static Bitmap LoadImage(string fileName)
		{
			Bitmap bmp = null;

			try
			{
				if (!File.Exists(fileName))
					return null;

				using (Bitmap bmpTemp = (Bitmap)Bitmap.FromFile(fileName))
					bmp = new Bitmap(bmpTemp);
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("LoadImage", "FileIO", ex.Message, ex.StackTrace);
			}

			return bmp;
		}

		public static bool TrySaveLayout(Control parent, out string fileName)
		{
			fileName = null;

			try
			{
				SaveFileDialog saveFileDialog = new SaveFileDialog();

				saveFileDialog.Title = "Save Layout";
				saveFileDialog.InitialDirectory = Settings.Folders.Layout;
				saveFileDialog.FileName = Settings.Layout.Name + ".xml";
				saveFileDialog.Filter = "Layout Files (*.xml)|*.xml|All Files (*.*)|*.*";
				saveFileDialog.OverwritePrompt = true;
				saveFileDialog.RestoreDirectory = true;

				if (saveFileDialog.ShowDialog(parent) == DialogResult.OK)
				{
					fileName = saveFileDialog.FileName;

					return true;
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("TrySaveLayout", "FileIO", ex.Message, ex.StackTrace);
			}

			return false;
		}

		public static bool TrySaveImage(Control parent, out string fileName)
		{
			fileName = null;

			try
			{
				SaveFileDialog saveFileDialog = new SaveFileDialog();

				saveFileDialog.Title = "Save Image";
				saveFileDialog.InitialDirectory = Settings.Folders.Layout;
				saveFileDialog.FileName = Settings.Layout.Name + ".png";
				saveFileDialog.Filter = "Image Files (*.png)|*.png|All Files (*.*)|*.*";
				saveFileDialog.OverwritePrompt = true;
				saveFileDialog.RestoreDirectory = true;

				if (saveFileDialog.ShowDialog(parent) == DialogResult.OK)
				{
					fileName = saveFileDialog.FileName;

					return true;
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("TrySaveImage", "FileIO", ex.Message, ex.StackTrace);
			}

			return false;
		}

		public static bool TrySaveText(Control parent, out string fileName)
		{
			fileName = null;

			try
			{
				SaveFileDialog saveFileDialog = new SaveFileDialog();

				saveFileDialog.Title = "Save Text";
				saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
				saveFileDialog.OverwritePrompt = true;
				saveFileDialog.RestoreDirectory = true;

				if (saveFileDialog.ShowDialog(parent) == DialogResult.OK)
				{
					fileName = saveFileDialog.FileName;

					return true;
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("TrySaveText", "FileIO", ex.Message, ex.StackTrace);
			}

			return false;
		}

		public static string GetRelativeFolder(string Folder, string Filename, bool removeExtension)
		{
			string NewFilename = Filename.Replace(Folder, "");

			if (NewFilename.StartsWith(Path.DirectorySeparatorChar.ToString()))
				NewFilename = NewFilename.Substring(1);

			if (removeExtension)
				if (NewFilename.Contains("."))
					NewFilename = NewFilename.Substring(0, NewFilename.LastIndexOf("."));

			return NewFilename;
		}

		private static void WndProc(ref Message m)
		{
			fileDialogExt.WndProc(ref m);
		}

		public static bool TryLaunchFileCaptureOutput(string fileName, string arguments, out string outputString)
		{
			outputString = null;

			if (!File.Exists(fileName))
				return false;

			try
			{
				Process process = new Process();

				process.StartInfo.CreateNoWindow = true;
				process.StartInfo.UseShellExecute = false;
				process.StartInfo.RedirectStandardOutput = true;
				process.StartInfo.FileName = fileName;
				process.StartInfo.Arguments = arguments;
				process.Start();

				outputString = process.StandardOutput.ReadToEnd();

				process.WaitForExit();
				process.Close();
				process.Dispose();

				return true;
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("TryLaunchFileCaptureOutput", "FileIO", ex.Message, ex.StackTrace);
			}

			return false;
		}
	}
	public partial class Win32
	{
		public static uint INFINITE = 0xFFFFFFFF;
		public static int WAIT_OBJECT_0 = 0;
		//public const int WAIT_TIMEOUT = 0x102;

		public const Int32 CREATE_NO_WINDOW = 0x08000000;
		public const Int32 NORMAL_PRIORITY_CLASS = 0x0020;
		public const Int32 STARTF_USESTDHANDLES = 0x00000100;
		public const Int32 STARTF_USESHOWWINDOW = 0x00000001;

		public const uint CREATE_ALWAYS = 0x0002;
		public const int STD_OUTPUT_HANDLE = -11;

		public const uint GENERIC_READ = 0x80000000;
		public const uint GENERIC_WRITE = 0x40000000;
		public const uint FILE_READ_ACCESS = 0x00000001;
		public const uint FILE_WRITE_ACCESS = 0x00000002;
		public const uint FILE_SHARE_READ = 0x00000001;
		public const uint FILE_SHARE_WRITE = 0x00000002;
		public const uint OPEN_EXISTING = 3;

		public const uint FILE_ATTRIBUTE_ARCHIVE = 0x20;
		public const uint FILE_ATTRIBUTE_DIRECTORY = 0x10;
		public const uint FILE_ATTRIBUTE_HIDDEN = 0x2;
		public const uint FILE_ATTRIBUTE_NORMAL = 0x80;
		public const uint FILE_ATTRIBUTE_READONLY = 0x1;
		public const uint FILE_ATTRIBUTE_SYSTEM = 0x4;
		public const uint FILE_ATTRIBUTE_TEMPORARY = 0x100;

		//public static IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);

		public const int ERROR_NO_MORE_FILES = 18;

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		public struct STARTUPINFO
		{
			public Int32 cb;
			public string lpReserved;
			public string lpDesktop;
			public string lpTitle;
			public Int32 dwX;
			public Int32 dwY;
			public Int32 dwXSize;
			public Int32 dwYSize;
			public Int32 dwXCountChars;
			public Int32 dwYCountChars;
			public Int32 dwFillAttribute;
			public Int32 dwFlags;
			public Int16 wShowWindow;
			public Int16 cbReserved2;
			public IntPtr lpReserved2;
			public IntPtr hStdInput;
			public IntPtr hStdOutput;
			public IntPtr hStdError;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct PROCESS_INFORMATION
		{
			public IntPtr hProcess;
			public IntPtr hThread;
			public int dwProcessId;
			public int dwThreadId;
		}

		public enum GET_FILEEX_INFO_LEVELS
		{
			GetFileExInfoStandard,
			GetFileExMaxInfoLevel
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct SECURITY_ATTRIBUTES
		{
			public int nLength;
			public IntPtr lpSecurityDescriptor;
			public int bInheritHandle;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct WIN32_FILE_ATTRIBUTE_DATA
		{
			public FileAttributes dwFileAttributes;
			public FILETIME ftCreationTime;
			public FILETIME ftLastAccessTime;
			public FILETIME ftLastWriteTime;
			public uint nFileSizeHigh;
			public uint nFileSizeLow;
		}

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		public struct FILETIME
		{
			public UInt32 dwLowDateTime;
			public UInt32 dwHighDateTime;

			public DateTime DateTime
			{
				get { return DateTime.FromFileTime((((long)dwHighDateTime) << 32) | ((uint)dwLowDateTime)); }
			}

			public DateTime DateTimeUtc
			{
				get { return DateTime.FromFileTimeUtc((((long)dwHighDateTime) << 32) | ((uint)dwLowDateTime)); }
			}
		};

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public struct WIN32_FIND_DATA
		{
			public uint dwFileAttributes;
			public FILETIME ftCreationTime;
			public FILETIME ftLastAccessTime;
			public FILETIME ftLastWriteTime;
			public uint nFileSizeHigh;
			public uint nFileSizeLow;
			public uint dwReserved0;
			public uint dwReserved1;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			public string cFileName;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)]
			public string cAlternateFileName;

			public long FileSize
			{
				get { return ((((long)nFileSizeHigh) << 32) | nFileSizeLow); }
			}
		}

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool Wow64DisableWow64FsRedirection(ref IntPtr ptr);
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool Wow64RevertWow64FsRedirection(IntPtr ptr);

		[DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern IntPtr FindFirstFile(string lpFileName, out WIN32_FIND_DATA lpFindFileData);
		[DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool FindNextFile(IntPtr hFindFile, out WIN32_FIND_DATA lpFindFileData);
		[DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool FindClose(IntPtr hFindFile);

		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern FileAttributes GetFileAttributes(string lpFileName);

		[DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
		public static extern IntPtr CreateFile(string lpFileName, uint dwDesiredAccess, uint dwShareMode, [In] ref SECURITY_ATTRIBUTES lpSecurityAttributes, uint dwCreationDisposition, uint dwFlagsAndAttributes, IntPtr hTemplateFile);

		[DllImport("kernel32.dll")]
		public static extern bool CreateProcess(string lpApplicationName, string lpCommandLine, ref SECURITY_ATTRIBUTES lpProcessAttributes, ref SECURITY_ATTRIBUTES lpThreadAttributes, bool bInheritHandles, uint dwCreationFlags, IntPtr lpEnvironment, string lpCurrentDirectory, [In] ref STARTUPINFO lpStartupInfo, out PROCESS_INFORMATION lpProcessInformation);

		[DllImport("kernel32", SetLastError = true, ExactSpelling = true)]
		public static extern Int32 WaitForSingleObject(IntPtr handle, uint milliseconds);

		[DllImport("kernel32")]
		public static extern bool CloseHandle(IntPtr hObject);

		[DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool GetFileAttributesEx(string lpFileName, GET_FILEEX_INFO_LEVELS fInfoLevelId, out WIN32_FILE_ATTRIBUTE_DATA fileData);
	}
}
