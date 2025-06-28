// Copyright (c) 2015, Ben Baker
// All rights reserved.
// 
// This source code is licensed under the BSD-style license found in the
// LICENSE file in the root directory of this source tree. 

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace CPWizard
{
	class ScreenManager
	{
		public static int ScreenNameToNumber(string Name)
		{
			try
			{
				for (int i = 0; i < Screen.AllScreens.Length; i++)
					if (GetScreenName(i) == Name)
						return i;

				for (int i = 0; i < Screen.AllScreens.Length; i++)
					if (Screen.AllScreens[i].Primary)
						return i;
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("ScreenNameToNumber", "ScreenManager", ex.Message, ex.StackTrace);
			}

			return 0;
		}

		public static void ClearScreenshotList()
		{
			if (Globals.ScreenshotList != null)
			{
				for (int i = 0; i < Globals.ScreenshotList.Count; i++)
				{
					if (Globals.ScreenshotList[i] != null)
					{
						Globals.ScreenshotList[i].Dispose();
						Globals.ScreenshotList[i] = null;
					}
				}

				Globals.ScreenshotList.Clear();
			}
			else
				Globals.ScreenshotList = new List<Bitmap>();
		}

		public static List<Bitmap> GetScreenshotList()
		{
			ClearScreenshotList();

			List<Bitmap> bitmapList = new List<Bitmap>();

			try
			{
				//int screenNum = 0;

				foreach (Screen screen in Screen.AllScreens)
				{
					Bitmap bmp = new Bitmap(screen.Bounds.Width, screen.Bounds.Height);

					using (Graphics g = Graphics.FromImage(bmp))
						g.CopyFromScreen(screen.Bounds.X, screen.Bounds.Y, 0, 0, screen.Bounds.Size);

					bitmapList.Add(bmp);
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("ScreenNumberToName", "ScreenManager", ex.Message, ex.StackTrace);
			}

			return bitmapList;
		}

		public static string ScreenNumberToName(int Num)
		{
			try
			{
				if (Num < Screen.AllScreens.Length)
					return GetScreenName(Num);
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("ScreenNumberToName", "ScreenManager", ex.Message, ex.StackTrace);
			}

			return @"\\.\DISPLAY1";
		}

		public static string[] GetScreenArray()
		{
			List<string> ScreenArray = new List<string>();

			try
			{
				for (int i = 0; i < Screen.AllScreens.Length; i++)
					ScreenArray.Add(ScreenManager.GetScreenName(i));
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("GetScreenArray", "ScreenManager", ex.Message, ex.StackTrace);
			}

			return (string[])ScreenArray.ToArray();
		}

		public static string GetScreenName(int num)
		{
			try
			{
				if (num < Screen.AllScreens.Length)
				{
					if (Screen.AllScreens[num].DeviceName.Contains("\0"))
						return Screen.AllScreens[num].DeviceName.Substring(0, Screen.AllScreens[num].DeviceName.IndexOf('\0'));
					else
						return Screen.AllScreens[num].DeviceName;

					//return Screen.AllScreens[num].DeviceName.Substring(0, Screen.AllScreens[num].DeviceName.IndexOf('\0'));
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("GetScreenName", "ScreenManager", ex.Message, ex.StackTrace);
			}

			return @"\\.\DISPLAY1";
		}
	}
}
