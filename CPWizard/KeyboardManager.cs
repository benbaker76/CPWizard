// Copyright (c) 2015, Ben Baker
// All rights reserved.
// 
// This source code is licensed under the BSD-style license found in the
// LICENSE file in the root directory of this source tree. 

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Xml;
using System.IO;

namespace CPWizard
{
	public class KeyNode
	{
		public string Name = null;
		public Rectangle Location = Rectangle.Empty;

		public KeyNode(string name, Rectangle location)
		{
			Name = name;
			Location = location;
		}

		public PointF Center
		{
			get { return new PointF((float)Location.X + ((float)Location.Width / 2f), (float)Location.Y + ((float)Location.Height / 2f)); }
		}

		public PointF PointTop
		{
			get { return new PointF((float)Location.X + ((float)Location.Width / 2f), (float)Location.Y); }
		}

		public PointF PointBottom
		{
			get { return new PointF((float)Location.X + ((float)Location.Width / 2f), (float)Location.Y + (float)Location.Height); }
		}

		public PointF PointRight
		{
			get { return new PointF((float)Location.X + (float)Location.Width, (float)Location.Y + ((float)Location.Height / 2f)); }
		}

		public PointF PointLeft
		{
			get { return new PointF((float)Location.X, (float)Location.Y + ((float)Location.Height / 2f)); }
		}
	}

	public class KeyboardKeyNode : KeyNode
	{
		public string Uppercase = null;

		public KeyboardKeyNode(string name, string uppercase, Rectangle location)
			: base(name, location)
		{
			Uppercase = uppercase;
		}
	}

	public class ColorKeyNode : KeyNode
	{
		public ColorKeyNode(string name, Rectangle location)
			: base(name, location)
		{
		}
	}

	public class SmilieKeyNode : KeyNode
	{
		public SmilieKeyNode(string name, Rectangle location)
			: base(name, location)
		{
		}
	}

	public delegate void OnScreenKeyDownHandler(string key);

	class KeyboardManager : RenderObject, IDisposable
	{
		public Font Font = new Font("Lucida Console", 10, FontStyle.Bold);
		private Bitmap KeyboardBak = null;
		private bool m_shift = false;
		private bool m_capsLock = false;

		private ArrayList m_keyboardKeys = null;

		private KeyNode m_selectedKey = null;

		public event OnScreenKeyDownHandler OnScreenKeyDown = null;

		private enum xmlElement
		{
			Nothing,
			Key,
			Color,
			Smilie
		}

		public KeyboardManager()
		{
			try
			{
				KeyboardBak = FileIO.LoadImage(Path.Combine(Settings.Folders.Media, "Keyboard.png"));

				m_keyboardKeys = new ArrayList();

				ReadKeyboardXml(Path.Combine(Settings.Folders.Data, "Keyboard.xml"));

				if (m_keyboardKeys.Count > 0)
					m_selectedKey = (KeyboardKeyNode)m_keyboardKeys[0];
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("KeyboardManager", "KeyboardManager", ex.Message, ex.StackTrace);
			}
		}

		private void OnInputEvent(object sender, InputEventArgs e)
		{
			try
			{
				if (Globals.InputManager.CheckInput((uint)e.InputCode, Settings.Input.MenuLeft))
				{
					e.Handled = true;

					if (e.IsDown)
					{
						GetKeyLeft();

						EventManager.UpdateDisplay();
					}
				}

				if (Globals.InputManager.CheckInput((uint)e.InputCode, Settings.Input.MenuRight))
				{
					e.Handled = true;

					if (e.IsDown)
					{
						GetKeyRight();

						EventManager.UpdateDisplay();
					}
				}

				if (Globals.InputManager.CheckInput((uint)e.InputCode, Settings.Input.MenuUp))
				{
					e.Handled = true;

					if (e.IsDown)
					{
						GetKeyUp();

						EventManager.UpdateDisplay();
					}
				}

				if (Globals.InputManager.CheckInput((uint)e.InputCode, Settings.Input.MenuDown))
				{
					e.Handled = true;

					if (e.IsDown)
					{
						GetKeyDown();

						EventManager.UpdateDisplay();
					}
				}

				if (Globals.InputManager.CheckInput((uint)e.InputCode, Settings.Input.SelectKey))
				{
					e.Handled = true;

					if (!e.IsDown)
					{
						HandleKeyDown();
					}
				}

				if (Globals.InputManager.CheckInput((uint)e.InputCode, Settings.Input.BackKey))
				{
					e.Handled = true;

					if (!e.IsDown)
					{
						Hide();

						EventManager.UpdateDisplay();
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
				LogFile.WriteLine("OnGlobalKeyEvent", "KeyboardManager", ex.Message, ex.StackTrace);
			}
		}

		public void ReadKeyboardXml(string FileName)
		{
			xmlElement currentElement = xmlElement.Nothing;

			Hashtable attribHash = new Hashtable();

			XmlTextReader reader = null;

			try
			{
				reader = new XmlTextReader(FileName);

				reader.Read();

				while (reader.Read())
				{
					switch (reader.NodeType)
					{
						case XmlNodeType.Element:
							switch (reader.LocalName.ToLower())
							{
								case "key":
									currentElement = xmlElement.Key;
									break;
								case "color":
									currentElement = xmlElement.Color;
									break;
								case "smilie":
									currentElement = xmlElement.Smilie;
									break;
								default:
									currentElement = xmlElement.Nothing;
									break;
							}
							if (reader.HasAttributes)
							{
								attribHash.Clear();
								while (reader.MoveToNextAttribute())
									attribHash.Add(reader.Name.ToLower(), reader.Value);
							}
							switch (currentElement)
							{
								case xmlElement.Key:
									m_keyboardKeys.Add(new KeyboardKeyNode((string)attribHash["name"], (string)attribHash["uppercase"], new Rectangle(StringTools.FromString<int>((string)attribHash["x"]), StringTools.FromString<int>((string)attribHash["y"]), StringTools.FromString<int>((string)attribHash["width"]), StringTools.FromString<int>((string)attribHash["height"]))));
									break;
								case xmlElement.Color:
									m_keyboardKeys.Add(new ColorKeyNode((string)attribHash["name"], new Rectangle(StringTools.FromString<int>((string)attribHash["x"]), StringTools.FromString<int>((string)attribHash["y"]), StringTools.FromString<int>((string)attribHash["width"]), StringTools.FromString<int>((string)attribHash["height"]))));
									break;
								case xmlElement.Smilie:
									m_keyboardKeys.Add(new SmilieKeyNode((string)attribHash["name"], new Rectangle(StringTools.FromString<int>((string)attribHash["x"]), StringTools.FromString<int>((string)attribHash["y"]), StringTools.FromString<int>((string)attribHash["width"]), StringTools.FromString<int>((string)attribHash["height"]))));
									break;
								default:
									break;
							}
							reader.MoveToElement();
							break;

						case XmlNodeType.Text:
							break;
						case XmlNodeType.EndElement:
							break;
					}
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("ReadKeyboardXml", "KeyboardManager", ex.Message, ex.StackTrace);
			}

			if (reader != null)
				reader.Close();
		}

		private void DrawShadow(Graphics g, string text, Font font, RectangleF rect)
		{
			try
			{
				Brush b = new SolidBrush(Color.Black);
				StringFormat stringFormat = new StringFormat();
				stringFormat.Alignment = StringAlignment.Center;

				SizeF fontSize = g.MeasureString(text, Font, (int)rect.Width, stringFormat);

				g.DrawString(text, font, b, new RectangleF(rect.X + 2, rect.Y + 2 + (rect.Height / 2) - (fontSize.Height / 2), rect.Width, rect.Height), stringFormat);

				b.Dispose();
				stringFormat.Dispose();
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("DrawShadow", "KeyboardManager", ex.Message, ex.StackTrace);
			}
		}

		private void DrawString(Graphics g, string text, Font font, Color color, RectangleF rect)
		{
			try
			{
				SolidBrush b = new SolidBrush(color);
				StringFormat stringFormat = new StringFormat();
				stringFormat.Alignment = StringAlignment.Center;

				SizeF fontSize = g.MeasureString(text, Font, (int)rect.Width, stringFormat);

				g.DrawString(text, font, b, new RectangleF(rect.X, rect.Y + (rect.Height / 2) - (fontSize.Height / 2), rect.Width, rect.Height), stringFormat);

				b.Dispose();
				stringFormat.Dispose();
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("DrawString", "KeyboardManager", ex.Message, ex.StackTrace);
			}
		}

		public void DrawKeys(Graphics g)
		{
			try
			{
				foreach (object obj in m_keyboardKeys)
				{
					if (obj is KeyboardKeyNode)
					{
						KeyboardKeyNode key = (KeyboardKeyNode)obj;

						if (m_capsLock || m_shift)
							DrawShadow(g, key.Uppercase, Font, key.Location);
						else
							DrawShadow(g, key.Name, Font, key.Location);

						if (key.Name == "CapsLock")
						{
							if (m_capsLock)
								DrawString(g, key.Name, Font, Color.Red, key.Location);
							else
								DrawString(g, key.Name, Font, Color.White, key.Location);
						}
						else if (key.Name == "Shift")
						{
							if (m_shift)
								DrawString(g, key.Name, Font, Color.Red, key.Location);
							else
								DrawString(g, key.Name, Font, Color.White, key.Location);
						}
						else
						{
							if (m_capsLock || m_shift)
								DrawString(g, key.Uppercase, Font, Color.White, key.Location);
							else
								DrawString(g, key.Name, Font, Color.White, key.Location);
						}
					}
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("DrawKeys", "KeyboardManager", ex.Message, ex.StackTrace);
			}
		}

		public void DrawSelectedKey(Graphics g)
		{
			try
			{
				if (m_selectedKey == null)
					return;

				Pen p = new Pen(Brushes.Yellow, 2);

				g.DrawRectangle(p, m_selectedKey.Location);

				p.Dispose();
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("DrawSelectedKey", "KeyboardManager", ex.Message, ex.StackTrace);
			}
		}

		public override void Paint(Graphics g, int x, int y, int width, int height)
		{
			try
			{
				Bitmap keyboardBmp = new Bitmap(KeyboardBak.Width, KeyboardBak.Height);
				Graphics gSrc = Graphics.FromImage(keyboardBmp);

				Globals.DisplayManager.SetGraphicsQuality(gSrc, DisplayManager.UserGraphicsQuality());

				gSrc.DrawImage(KeyboardBak, new Rectangle(0, 0, KeyboardBak.Width, KeyboardBak.Height), 0, 0, keyboardBmp.Width, keyboardBmp.Height, GraphicsUnit.Pixel);

				DrawKeys(gSrc);

				DrawSelectedKey(gSrc);

				g.DrawImage(keyboardBmp, new Rectangle(x, y, width, (int)(height / 2)), 0, 0, keyboardBmp.Width, keyboardBmp.Height, GraphicsUnit.Pixel);

				gSrc.Dispose();
				keyboardBmp.Dispose();
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("Paint", "KeyboardManager", ex.Message, ex.StackTrace);
			}
		}

		//calculates the euclidean distance between two points
		public int GetDistance(PointF p1, PointF p2)
		{
			return (int)Math.Round(Math.Sqrt((p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y)));
		}

		private void GetKeyLeft()
		{
			if (m_selectedKey == null)
				return;

			KeyNode newKey = null;
			int minDistance = 1024;

			foreach (KeyNode key in m_keyboardKeys)
			{
				if (key == m_selectedKey)
					continue;

				if (m_selectedKey.PointLeft.X > key.PointRight.X)
				{
					int distance = GetDistance(m_selectedKey.PointLeft, key.PointRight);

					if (distance < minDistance)
					{
						minDistance = distance;
						newKey = key;
					}
				}
			}

			if (newKey != null)
				m_selectedKey = newKey;
		}

		private void GetKeyRight()
		{
			if (m_selectedKey == null)
				return;

			KeyNode newKey = null;
			int minDistance = 1024;

			foreach (KeyNode key in m_keyboardKeys)
			{
				if (key == m_selectedKey)
					continue;

				if (m_selectedKey.PointRight.X < key.PointLeft.X)
				{
					int distance = GetDistance(m_selectedKey.PointRight, key.PointLeft);
					if (distance < minDistance)
					{
						minDistance = distance;
						newKey = key;
					}
				}
			}

			if (newKey != null)
				m_selectedKey = newKey;
		}

		private void GetKeyUp()
		{
			if (m_selectedKey == null)
				return;

			KeyNode newKey = null;
			int minDistance = 1024;

			foreach (KeyNode key in m_keyboardKeys)
			{
				if (key == m_selectedKey)
					continue;

				if (m_selectedKey.PointTop.Y > key.PointBottom.Y)
				{
					int distance = GetDistance(m_selectedKey.PointTop, key.PointBottom);
					if (distance < minDistance)
					{
						minDistance = distance;
						newKey = key;
					}
				}
			}

			if (newKey != null)
				m_selectedKey = newKey;
		}

		private void GetKeyDown()
		{
			if (m_selectedKey == null)
				return;

			KeyNode newKey = null;
			int minDistance = 1024;

			foreach (KeyNode key in m_keyboardKeys)
			{
				if (key == m_selectedKey)
					continue;

				if (m_selectedKey.PointBottom.Y < key.PointTop.Y)
				{
					int distance = GetDistance(m_selectedKey.PointBottom, key.PointTop);
					if (distance < minDistance)
					{
						minDistance = distance;
						newKey = key;
					}
				}
			}

			if (newKey != null)
				m_selectedKey = newKey;
		}

		private void HandleKeyDown()
		{
			if (m_selectedKey == null)
				return;

			if (m_selectedKey is KeyboardKeyNode)
			{
				KeyboardKeyNode key = (KeyboardKeyNode)m_selectedKey;

				switch (key.Name)
				{
					case "":
						Hide();
						EventManager.UpdateDisplay();
						break;
					case "CapsLock":
						m_capsLock = !m_capsLock;
						EventManager.UpdateDisplay();
						break;
					case "Shift":
						m_shift = !m_shift;
						EventManager.UpdateDisplay();
						break;
					default:
						if (m_shift)
						{
							if (OnScreenKeyDown != null)
								OnScreenKeyDown(key.Uppercase);

							m_shift = false;

							EventManager.UpdateDisplay();
						}
						else if (m_capsLock)
						{
							if (OnScreenKeyDown != null)
								OnScreenKeyDown(key.Uppercase);
						}
						else
						{
							if (OnScreenKeyDown != null)
								OnScreenKeyDown(key.Name);
						}
						break;
				}
			}
			else
			{
				if (OnScreenKeyDown != null)
					OnScreenKeyDown(m_selectedKey.Name);
			}
		}

		public override void Reset(EmulatorMode mode)
		{
		}

		public override bool CheckEnabled()
		{
			return true;
		}

		public override void AddEventHandlers()
		{
			Globals.InputManager.InputEvent += new InputManager.InputEventHandler(OnInputEvent);
		}

		public override void RemoveEventHandlers()
		{
			Globals.InputManager.InputEvent -= new InputManager.InputEventHandler(OnInputEvent);
		}

		#region IDisposable Members

		public override void Dispose()
		{
			base.Dispose();

			RemoveEventHandlers();
		}

		#endregion
	}
}
