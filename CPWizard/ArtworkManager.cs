// Copyright (c) 2015, Ben Baker
// All rights reserved.
// 
// This source code is licensed under the BSD-style license found in the
// LICENSE file in the root directory of this source tree. 

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;

namespace CPWizard
{
	class ArtworkManager : RenderObject, IDisposable
	{
		private int m_artworkPos = 0;
		private List<string> m_artworkFolders = null;
		private string m_gameName = null;
		private string m_parentName = null;

		private string[] m_imageExt = { ".png", ".jpg", ".gif" };

		public ArtworkManager()
		{
			m_artworkFolders = new List<string>();
		}

		private void OnInputEvent(object sender, InputEventArgs e)
		{
			try
			{
				if (Globals.InputManager.CheckInput((uint)e.InputCode, Settings.Input.MenuLeft))
				{
					e.Handled = true;

					if (!e.IsDown)
					{
						if (m_artworkPos > 0)
							m_artworkPos--;
						else
							m_artworkPos = m_artworkFolders.Count - 1;

						GetPreviousImage();

						EventManager.UpdateDisplay();
					}
				}

				if (Globals.InputManager.CheckInput((uint)e.InputCode, Settings.Input.MenuRight))
				{
					e.Handled = true;

					if (!e.IsDown)
					{
						if (m_artworkPos < m_artworkFolders.Count - 1)
							m_artworkPos++;
						else
							m_artworkPos = 0;

						GetNextImage();

						EventManager.UpdateDisplay();
					}
				}

				if (Globals.InputManager.CheckInput((uint)e.InputCode, Settings.Input.BackKey) ||
					Globals.InputManager.CheckInput((uint)e.InputCode, Settings.Input.SelectKey))
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
				LogFile.WriteLine("OnGlobalKeyEvent", "ArtworkManager", ex.Message, ex.StackTrace);
			}
		}

		private bool TryGetArtwork(int pos, out string FileName)
		{
			FileName = null;

			try
			{
				if (String.IsNullOrEmpty(m_artworkFolders[pos]))
					return false;

				for (int i = 0; i < m_imageExt.Length; i++)
				{
					FileName = Path.Combine(m_artworkFolders[pos], m_gameName + m_imageExt[i]);

					if (File.Exists(FileName))
						return true;

					if (m_parentName != null)
					{
						FileName = Path.Combine(m_artworkFolders[pos], m_parentName + m_imageExt[i]);

						if (File.Exists(FileName))
							return true;
					}
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("TryGetArtwork", "ArtworkManager", ex.Message, ex.StackTrace);
			}

			return false;
		}

		public Rectangle ResizePicture(Size oldSize, Size newSize)
		{
			Rectangle retRect = Rectangle.Empty;

			try
			{
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
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("ResizePicture", "ArtworkManager", ex.Message, ex.StackTrace);
			}

			return retRect;
		}

		private void GetPreviousImage()
		{
			try
			{
				for (int i = 0; i < m_artworkFolders.Count; i++)
				{
					string picFilename = null;

					if (TryGetArtwork(m_artworkPos, out picFilename))
						return;
					else
					{
						if (m_artworkPos > 0)
							m_artworkPos--;
						else
							m_artworkPos = m_artworkFolders.Count - 1;
					}
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("GetPreviousImage", "ArtworkManager", ex.Message, ex.StackTrace);
			}
		}

		private void GetNextImage()
		{
			try
			{
				for (int i = 0; i < m_artworkFolders.Count; i++)
				{
					string picFilename = null;

					if (TryGetArtwork(m_artworkPos, out picFilename))
						return;
					else
					{
						if (m_artworkPos < m_artworkFolders.Count - 1)
							m_artworkPos++;
						else
							m_artworkPos = 0;
					}
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("GetNextImage", "ArtworkManager", ex.Message, ex.StackTrace);
			}
		}

		private Bitmap GetPictureBitmap()
		{
			try
			{
				string picFilename = null;

				if (!TryGetArtwork(m_artworkPos, out picFilename))
				{
					GetNextImage();
					TryGetArtwork(m_artworkPos, out picFilename);
				}

				return FileIO.LoadImage(picFilename);
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("GetPictureBitmap", "ArtworkManager", ex.Message, ex.StackTrace);
			}

			return null;
		}

		public override void Paint(Graphics g, int x, int y, int width, int height)
		{
			try
			{
				using (Bitmap pictureBmp = GetPictureBitmap())
				{
					if (pictureBmp != null)
					{
						Size paddingSize = new Size((int)(width * 0.02f), (int)(height * 0.04f));
						Rectangle pictureRect = ResizePicture(pictureBmp.Size, new Size(width, height));

						g.DrawImage(pictureBmp, new Rectangle(pictureRect.X, pictureRect.Y, pictureRect.Width, pictureRect.Height), 0, 0, pictureBmp.Width, pictureBmp.Height, GraphicsUnit.Pixel);
					}
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("Paint", "ArtworkManager", ex.Message, ex.StackTrace);
			}
		}

		public override bool CheckEnabled()
		{
			try
			{
				for (int i = 0; i < m_artworkFolders.Count; i++)
				{
					string picFilename = null;

					if (TryGetArtwork(i, out picFilename))
						return true;
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("CheckEnabled", "ArtworkManager", ex.Message, ex.StackTrace);
			}

			return false;
		}

		public override void Reset(EmulatorMode mode)
		{
			m_artworkPos = 0;

			m_artworkFolders.Clear();

			switch (mode)
			{
				case EmulatorMode.MAME:
					m_gameName = Settings.MAME.GameName;
					m_parentName = Settings.MAME.GetParent();

					m_artworkFolders.Add(Settings.Folders.MAME.Cabinets);
					m_artworkFolders.Add(Settings.Folders.MAME.CPanel);
					m_artworkFolders.Add(Settings.Folders.MAME.Flyers);
					m_artworkFolders.Add(Settings.Folders.MAME.Marquees);
					m_artworkFolders.Add(Settings.Folders.MAME.Snap);
					m_artworkFolders.Add(Settings.Folders.MAME.Titles);
					m_artworkFolders.Add(Settings.Folders.MAME.Previews);
					m_artworkFolders.Add(Settings.Folders.MAME.Select);
					m_artworkFolders.Add(Settings.Folders.MAME.PCB);
					break;
				case EmulatorMode.Emulator:
					m_gameName = Settings.Emulator.GameName;
					m_parentName = null;

					if (Settings.Emulator.Profile != null)
					{
						m_artworkFolders.Add(Settings.Emulator.Profile.Snaps);
						m_artworkFolders.Add(Settings.Emulator.Profile.Titles);
						m_artworkFolders.Add(Settings.Emulator.Profile.Carts);
						m_artworkFolders.Add(Settings.Emulator.Profile.Boxes);
					}
					break;
			}
		}

		public override void AddEventHandlers()
		{
			Globals.InputManager.InputEvent += new InputManager.InputEventHandler(OnInputEvent);
		}

		public override void RemoveEventHandlers()
		{
			Globals.InputManager.InputEvent -= new InputManager.InputEventHandler(OnInputEvent);
		}
	}
}
