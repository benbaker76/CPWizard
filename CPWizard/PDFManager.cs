// Copyright (c) 2015, Ben Baker
// All rights reserved.
// 
// This source code is licensed under the BSD-style license found in the
// LICENSE file in the root directory of this source tree. 

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.IO;

namespace CPWizard
{
	class PDFManager : RenderObject, IDisposable
	{
		private int m_pageNumber = 0;
		private int m_pageCount = 0;
		private int m_pageOffset = 0;
		private string m_fileName = null;
		private Bitmap m_PDFBitmap = null;
		private bool m_PDFLoading = false;
		private bool m_zoom = true;

		private string m_tempFileName = null;

		public PDFManager()
		{
			m_tempFileName = Path.GetTempFileName();
		}

		private void OnInputEvent(object sender, InputEventArgs e)
		{
			try
			{
				if (Globals.InputManager.CheckInput((uint)e.InputCode, Settings.Input.SelectKey))
				{
					e.Handled = true;

					if (!e.IsDown)
					{
						m_zoom = !m_zoom;

						EventManager.UpdateDisplay();
					}
				}

				if (Globals.InputManager.CheckInput((uint)e.InputCode, Settings.Input.MenuLeft))
				{
					e.Handled = true;

					if (!e.IsDown)
					{
						if (!m_PDFLoading)
						{
							if (m_pageNumber > 0)
								m_pageNumber--;

							CreatePDFPage();
						}
					}
				}

				if (Globals.InputManager.CheckInput((uint)e.InputCode, Settings.Input.MenuRight))
				{
					e.Handled = true;

					if (!e.IsDown)
					{
						if (!m_PDFLoading)
						{
							if (m_pageNumber < m_pageCount - 1)
								m_pageNumber++;

							CreatePDFPage();
						}
					}
				}

				if (Globals.InputManager.CheckInput((uint)e.InputCode, Settings.Input.MenuUp))
				{
					e.Handled = true;

					if (e.IsDown)
					{
						if (m_pageOffset > 0)
							m_pageOffset -= 64;

						EventManager.UpdateDisplay();
					}
				}

				if (Globals.InputManager.CheckInput((uint)e.InputCode, Settings.Input.MenuDown))
				{
					e.Handled = true;

					if (e.IsDown)
					{
						m_pageOffset += 64;

						EventManager.UpdateDisplay();
					}
				}

				if (Globals.InputManager.CheckInput((uint)e.InputCode, Settings.Input.BackKey))
				{
					e.Handled = true;

					if (!e.IsDown)
					{
						Hide();

						if (ExitToMenu)
							Globals.MainMenu.Show();
						else
							Globals.ProgramManager.Hide();
					}
				}

				if (Settings.Input.EnableExitKey && Globals.InputManager.CheckInput((uint)e.InputCode, Settings.Input.ExitKey))
				{
					e.Handled = true;

					if (!e.IsDown)
					{
						Hide();

						Globals.ProgramManager.Hide();
					}
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("OnGlobalKeyEvent", "PDFManager", ex.Message, ex.StackTrace);
			}
		}

		public int GetPDFPageCount()
		{
			string Output = null;

			try
			{
				if (String.IsNullOrEmpty(m_fileName) || String.IsNullOrEmpty(Settings.Files.GSExe))
					return 0;

				if (!File.Exists(m_fileName) || !File.Exists(Settings.Files.GSExe))
					return 0;

				Process GSProcess = new Process();

				GSProcess.StartInfo.CreateNoWindow = true;
				GSProcess.StartInfo.UseShellExecute = false;
				GSProcess.StartInfo.RedirectStandardOutput = true;
				GSProcess.StartInfo.FileName = Settings.Files.GSExe;
				GSProcess.StartInfo.Arguments = String.Format("-q -sPDFname=\"{0}\" \"{1}\"", m_fileName, Path.Combine(Settings.Folders.Data, "PDFPageCount.ps"));
				GSProcess.Start();
				Output = GSProcess.StandardOutput.ReadToEnd();
				GSProcess.WaitForExit();
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("GetPDFPageCount", "PDFManager", ex.Message, ex.StackTrace);
			}

			string[] SplitString = Output.Split(':');

			if (SplitString.Length == 2)
				return StringTools.FromString<int>(SplitString[1].Trim());

			return 0;
		}

		private void CreatePDFPage()
		{
			try
			{
				if (String.IsNullOrEmpty(m_fileName) || String.IsNullOrEmpty(Settings.Files.GSExe))
					return;

				if (!File.Exists(m_fileName) || !File.Exists(Settings.Files.GSExe))
					return;

				string Args = String.Format("-dFirstPage={0} -dLastPage={1} -dGraphicsAlphaBits=4 -dTextAlphaBits=4 -dQUIET -r150 -dNOPAUSE -dBATCH -sDEVICE=png16m -sOutputFile=\"{2}\" \"{3}\"", m_pageNumber + 1, m_pageNumber + 1, m_tempFileName, m_fileName);

				LaunchProcess GSProcess = new LaunchProcess(Settings.Files.GSExe, Args);
				GSProcess.OnExited += new LaunchProcess.ProcessExitedHandler(GSProcess_Exited);
				GSProcess.Start();

				m_PDFLoading = true;
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("CreatePDFPage", "PDFManager", ex.Message, ex.StackTrace);
			}
		}

		void GSProcess_Exited()
		{
			try
			{
				m_PDFLoading = false;

				if (File.Exists(m_tempFileName))
				{
					if (m_PDFBitmap != null)
					{
						m_PDFBitmap.Dispose();
						m_PDFBitmap = null;
					}

					m_PDFBitmap = FileIO.LoadImage(m_tempFileName);

					//m_pageOffset = 0;
					EventManager.UpdateDisplay();
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("GSProcess_Exited", "PDFManager", ex.Message, ex.StackTrace);
			}
		}

		public Rectangle ResizePicture(Size oldSize, Size newSize)
		{
			Rectangle retRect = Rectangle.Empty;

			if ((float)oldSize.Width / (float)newSize.Width == (float)oldSize.Height / (float)newSize.Height)
			{
				retRect = new Rectangle(0, 0, newSize.Width, newSize.Height);
			}
			else if ((float)oldSize.Width / (float)newSize.Width > (float)oldSize.Height / (float)newSize.Height)
			{
				retRect = new Rectangle(0, (int)((float)newSize.Height / 2f - (oldSize.Height * ((float)newSize.Width / (float)oldSize.Width)) / 2f), (int)((float)newSize.Width), (int)(oldSize.Height * ((float)newSize.Width / (float)oldSize.Width)));
			}
			else if ((float)oldSize.Width / (float)newSize.Width < (float)oldSize.Height / (float)newSize.Height)
			{
				retRect = new Rectangle((int)((float)newSize.Width / 2f - (oldSize.Width * ((float)newSize.Height / (float)oldSize.Height)) / 2f), 0, (int)(oldSize.Width * ((float)newSize.Height / (float)oldSize.Height)), newSize.Height);
			}

			return retRect;
		}

		private void GetNewPDF()
		{
			try
			{
				if (File.Exists(m_tempFileName))
					File.Delete(m_tempFileName);

				if (m_PDFBitmap != null)
				{
					m_PDFBitmap.Dispose();
					m_PDFBitmap = null;
				}

				m_pageNumber = 0;
				m_pageOffset = 0;
				m_pageCount = GetPDFPageCount();

				CreatePDFPage();
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("GetNewPDF", "PDFManager", ex.Message, ex.StackTrace);
			}
		}

		public override void Paint(Graphics g, int x, int y, int width, int height)
		{
			try
			{
				if (m_PDFBitmap != null)
				{
					if (m_zoom)
					{
						Rectangle newRect = ResizePicture(new Size(m_PDFBitmap.Width, m_PDFBitmap.Height), new Size(width, height));
						g.DrawImage(m_PDFBitmap, newRect, 0, 0, m_PDFBitmap.Width, m_PDFBitmap.Height, GraphicsUnit.Pixel);
					}
					else
					{
						if (m_PDFBitmap.Height <= height)
							g.DrawImage(m_PDFBitmap, new Rectangle(x, y, width, height), 0, m_pageOffset, m_PDFBitmap.Width, m_PDFBitmap.Height, GraphicsUnit.Pixel);
						else
							g.DrawImage(m_PDFBitmap, new Rectangle(x, y, width, height), 0, m_pageOffset, m_PDFBitmap.Width, height, GraphicsUnit.Pixel);
					}
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("Paint", "PDFManager", ex.Message, ex.StackTrace);
			}
		}

		public override bool CheckEnabled()
		{
			try
			{
				if (String.IsNullOrEmpty(m_fileName) || String.IsNullOrEmpty(Settings.Files.GSExe))
					return false;

				if (!File.Exists(m_fileName) || !File.Exists(Settings.Files.GSExe))
					return false;

				return true;
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("CheckEnabled", "PDFManager", ex.Message, ex.StackTrace);
			}

			return false;
		}

		public override void Reset(EmulatorMode mode)
		{
			m_pageNumber = 0;
			m_pageCount = 0;
			m_pageOffset = 0;
			m_zoom = true;

			GetNewPDF();
		}

		public override void AddEventHandlers()
		{
			Globals.InputManager.InputEvent += new InputManager.InputEventHandler(OnInputEvent);
		}

		public override void RemoveEventHandlers()
		{
			Globals.InputManager.InputEvent -= new InputManager.InputEventHandler(OnInputEvent);
		}

		public string FileName
		{
			set { m_fileName = value; }
		}

		#region IDisposable Members

		public override void Dispose()
		{
			base.Dispose();

			try
			{
				if (File.Exists(m_tempFileName))
					File.Delete(m_tempFileName);
			}
			catch
			{
			}
		}

		#endregion
	}
}
