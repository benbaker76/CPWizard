// Copyright (c) 2015, Ben Baker
// All rights reserved.
// 
// This source code is licensed under the BSD-style license found in the
// LICENSE file in the root directory of this source tree. 

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;

namespace CPWizard
{
    class NFOViewer : RenderObject, IDisposable
    {
        private Bitmap m_fontBitmap = null;
        private Bitmap[] m_fontArray = null;

        private int m_pageWidth = 80;
        private int m_visibleLines = 25;
        private int m_pageOffset = 0;
        private int m_totalLines = 0;

        private string[] Lines = null;
        private Size m_charSize;

        private string m_fileName = null;

        public NFOViewer()
        {
            try
            {
                m_fontBitmap = FileIO.LoadImage(Path.Combine(Settings.Folders.Media, "NFOFont.png"));
                m_charSize = new Size(5, 12);

                CreateFontArray();
            }
            catch (Exception ex)
            {
                LogFile.WriteLine("NFOViewer", "NFOViewer", ex.Message, ex.StackTrace);
            }
        }

        private void CreateFontArray()
        {
            try
            {
                if (m_fontBitmap == null)
                    return;

                m_fontArray = new Bitmap[256];

                for (int i = 0; i < 256; i++)
                {
                    m_fontArray[i] = new Bitmap(m_charSize.Width, m_charSize.Height);

                    using(Graphics g = Graphics.FromImage(m_fontArray[i]))
                        g.DrawImage(m_fontBitmap, new Rectangle(0, 0, m_charSize.Width, m_charSize.Height), new Rectangle(i * m_charSize.Width, 0, m_charSize.Width, m_charSize.Height), GraphicsUnit.Pixel);
                }
            }
            catch (Exception ex)
            {
                LogFile.WriteLine("CreateFontArray", "NFOViewer", ex.Message, ex.StackTrace);
            }
        }

		private void OnInputEvent(object sender, InputEventArgs e)
        {
            try
            {
				if (Globals.InputManager.CheckInput((uint)e.InputCode, Settings.Input.MenuUp))
                {
                    e.Handled = true;

                    if (e.IsDown)
                    {
                        if (m_pageOffset > 0)
                            m_pageOffset--;
						//else
						//    if (m_totalLines > m_visibleLines)
						//        m_pageOffset = m_totalLines - m_visibleLines;

						EventManager.UpdateDisplay();
                    }
                }

				if (Globals.InputManager.CheckInput((uint)e.InputCode, Settings.Input.MenuDown))
                {
                    e.Handled = true;

					if (e.IsDown)
                    {
                        if (m_pageOffset + m_visibleLines < m_totalLines)
                            m_pageOffset++;
						//else
						//    m_pageOffset = 0;

						EventManager.UpdateDisplay();
                    }
                }

                if (Globals.InputManager.CheckInput((uint)e.InputCode, Settings.Input.MenuLeft))
                {
                    e.Handled = true;

					if (e.IsDown)
                    {
                        m_pageOffset -= m_visibleLines - 1;

                        if (m_pageOffset < 0)
                            m_pageOffset = 0;

						EventManager.UpdateDisplay();
                    }

                    return;
                }

                if (Globals.InputManager.CheckInput((uint)e.InputCode, Settings.Input.MenuRight))
                {
                    e.Handled = true;

					if (e.IsDown)
                    {
                        m_pageOffset += m_visibleLines - 1;

                        if (m_pageOffset + m_visibleLines > m_totalLines)
                            m_pageOffset = m_totalLines - m_visibleLines;

                        if (m_pageOffset < 0)
                            m_pageOffset = 0;

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
                LogFile.WriteLine("OnGlobalKeyEvent", "NFOViewer", ex.Message, ex.StackTrace);
            }
        }

        private void LoadNFO(string FileName)
        {
            try
            {
                Lines = File.ReadAllLines(FileName, Encoding.UTF7);
                m_totalLines = Lines.Length;
            }
            catch (Exception ex)
            {
                LogFile.WriteLine("LoadNFO", "NFOViewer", ex.Message, ex.StackTrace);
            }
        }

        private void DrawLines(Graphics g, int width, int height)
        {
            try
            {
                Bitmap bmpText = new Bitmap(m_pageWidth * m_charSize.Width, m_visibleLines * m_charSize.Height);
                Graphics gText = Graphics.FromImage(bmpText);

                gText.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

                for (int y = 0; y < m_visibleLines; y++)
                {
                   if(y + m_pageOffset < m_totalLines)
                   {
                       string Line = Lines[y + m_pageOffset];

                        for (int x = 0; x < m_pageWidth; x++)
                        {
                            if(x < Line.Length)
                                gText.DrawImage(m_fontArray[(int)Line[x]], x * m_charSize.Width, y * m_charSize.Height);
                        }
                    }
                }

                g.DrawImage(bmpText, 0, 0, width, height);

                bmpText.Dispose();
                gText.Dispose();
            }
            catch (Exception ex)
            {
                LogFile.WriteLine("DrawLines", "NFOViewer", ex.Message, ex.StackTrace);
            }
        }

        public override void Paint(Graphics g, int x, int y, int width, int height)
        {
            try
            {
                g.Clear(Color.Black);

                DrawLines(g, width, height);
            }
            catch (Exception ex)
            {
                LogFile.WriteLine("Paint", "NFOViewer", ex.Message, ex.StackTrace);
            }
        }

        public override bool CheckEnabled()
        {
            if (Settings.Emulator.Profile == null)
                return false;

            if(File.Exists(m_fileName))
                return true;

            return false;
        }

        public override void Reset(EmulatorMode mode)
        {
            if (Settings.Emulator.Profile == null)
                return;

            m_pageOffset = 0;

            if (!String.IsNullOrEmpty(Settings.Emulator.Profile.Nfo))
                m_fileName = Path.Combine(Settings.Emulator.Profile.Nfo, Settings.Emulator.GameName + ".nfo");

            if(File.Exists(m_fileName))
                LoadNFO(m_fileName);
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
