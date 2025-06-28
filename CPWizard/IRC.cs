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
	class Smilie
	{
		public string Name = null;
		public string Source = null;
		public Bitmap Bitmap = null;

		public Smilie(string name, string source)
		{
			Name = name;
			Source = source;
			Bitmap = FileIO.LoadImage(Path.Combine(Settings.Folders.Smilies, Source));
		}
	}

	class IRCWord
	{
		public string Text = null;
		public Rectangle Rectangle = Rectangle.Empty;
		public Font Font = null;
		public Color ForeColor = Color.Empty;
		public Color BackColor = Color.Empty;
		public bool IsSmilie = false;

		public IRCWord(string text, Rectangle rect, Font font, Color foreColor, Color backColor)
		{
			Text = text;
			Rectangle = rect;
			Font = font;
			ForeColor = foreColor;
			BackColor = backColor;
		}

		public IRCWord(string text, Rectangle rect)
		{
			Text = text;
			Rectangle = rect;
			IsSmilie = true;
		}
	}

	class IRCLine
	{
		public List<IRCWord> Words = new List<IRCWord>();
		public int LineCount = 0;

		public IRCLine()
		{
		}
	}

	class IRC : RenderObject, IDisposable
	{
		private const int MAX_LINES = 100;
		private const int MAX_CHARS = 400;
		private const int MAX_VISIBLE_CHARS = 140;

		private enum IrcFontStyle
		{
			None,
			Bold,
			Underline,
			Reverse
		}

		public static bool[] ForeColorSwitch = new bool[16];
		public static bool[] BackColorSwitch = new bool[16];

		private IRCManager m_ircManager = null;
		public Bitmap IRCBak = null;
		public Font RegularFont = new Font("Lucida Console", 14, FontStyle.Regular);
		public Font BoldFont = new Font("Lucida Console", 14, FontStyle.Bold);
		public Font UnderlineFont = new Font("Lucida Console", 14, FontStyle.Underline);
		private Color ForeColor = Color.White;
		private Color BackColor = Color.Empty;
		private Color ShadowColor = Color.Black;
		private List<IRCLine> IRCLines;
		private RectangleF MainRect;
		private RectangleF UserListRect;
		private RectangleF UserInputRect;
		private SizeF BackSize;
		private SizeF FontSize;
		private string UserInput = "";

		//private int CharOffset = 0;

		private static bool Busy = false;

		private KeyboardManager m_ircKeyboard = null;
		private MenuManager m_ircMenu = null;

		private List<ChannelInfo> m_channelArray = null;
		private Dictionary<string, ChannelInfo> m_channelHash = null;

		private const int LINE_COUNT = 30;
		private const int MAX_LINE_COLS = 80;

		private string[] FontStyleNames = { "[BOLD]", "[UNDERLINE]", "[REVERSE]" };

		private Smilie[] Smilies =
        {
            new Smilie(">:)", "evil.gif"),
            new Smilie("}:)", "8ball.gif"),
            new Smilie(":(!", "angry.gif"),
            new Smilie(":)", "smile.gif"),
            new Smilie(":-)", "smile.gif"),
            new Smilie(":(", "sad.gif"),
            new Smilie(":-(", "sad.gif"),
            new Smilie(":D", "big.gif"),
            new Smilie("8)", "shy.gif"),
            new Smilie("8D", "cool.gif"),
            new Smilie(":O", "shock.gif"),
            new Smilie(":I", "blush.gif"),
            new Smilie(":P", "tongue.gif"),
            new Smilie("xx(", "dead.gif"),
            new Smilie("|)", "sleepy.gif"),
            new Smilie(";)", "wink.gif"),
            new Smilie(":X", "kisses.gif"),
            new Smilie(":x", "kisses2.gif"),
            new Smilie(":o)", "clown.gif"),
            new Smilie("(^)", "approve.gif"),
            new Smilie("B)", "blackeye.gif"),
            new Smilie("(V)", "dissapprove.gif"),
            new Smilie("(8)", "8ball.gif"),
            new Smilie("(?)", "question.gif")
        };


		public enum StateID
		{
			Reset,
			Enabled,
			Showing,
			ShowingKeyboard,
			ShowingMenu,
		}

		public IRC()
		{
			try
			{
				IRCBak = FileIO.LoadImage(Path.Combine(Settings.Folders.Media, "IRCBak.png"));

				IRCLines = new List<IRCLine>();

				GetSize();

				MainRect = new RectangleF(0.03f * BackSize.Width, 0.04f * BackSize.Height, 0.75f * BackSize.Width, 0.8f * BackSize.Height);
				UserListRect = new RectangleF(0.82f * BackSize.Width, 0.04f * BackSize.Height, 0.2f * BackSize.Width, 0.8f * BackSize.Height);
				UserInputRect = new RectangleF(0.03f * BackSize.Width, 0.9f * BackSize.Height, 0.93f * BackSize.Width, 0.08f * BackSize.Height);

				m_ircManager = new IRCManager(Settings.IRC.NickName, Settings.IRC.UserName, Settings.IRC.RealName, Settings.IRC.IsInvisible);

				m_ircManager.OnReply += new IRCManager.ReplyHandler(OnReply);
				m_ircManager.OnPing += new IRCManager.PingHandler(OnPing);
				m_ircManager.OnPublicNotice += new IRCManager.PublicNoticeHandler(OnPublicNotice);
				m_ircManager.OnPrivateNotice += new IRCManager.PrivateNoticeHandler(OnPrivateNotice);
				m_ircManager.OnJoin += new IRCManager.JoinHandler(OnJoin);
				m_ircManager.OnAction += new IRCManager.ActionHandler(OnAction);
				m_ircManager.OnPrivateAction += new IRCManager.PrivateActionHandler(OnPrivateAction);
				m_ircManager.OnPublic += new IRCManager.PublicMessageHandler(OnPublic);
				m_ircManager.OnPrivate += new IRCManager.PrivateMessageHandler(OnPrivate);
				m_ircManager.OnNick += new IRCManager.NickHandler(OnNick);
				m_ircManager.OnTopicChanged += new IRCManager.TopicHandler(OnTopic);
				m_ircManager.OnPart += new IRCManager.PartHandler(OnPart);
				m_ircManager.OnQuit += new IRCManager.QuitHandler(OnQuit);
				m_ircManager.OnInvite += new IRCManager.InviteHandler(OnInvite);
				m_ircManager.OnKick += new IRCManager.KickHandler(OnKick);
				m_ircManager.OnChannelModeChange += new IRCManager.ChannelModeChangeHandler(OnChannelModeChange);
				m_ircManager.OnUserModeChange += new IRCManager.UserModeChangeHandler(OnUserModeChange);
				m_ircManager.OnKill += new IRCManager.KillHandler(OnKill);
				m_ircManager.OnRegistered += new IRCManager.RegisteredHandler(OnRegistered);
				m_ircManager.OnMotd += new IRCManager.MotdHandler(OnMotd);
				m_ircManager.OnIson += new IRCManager.IsonHandler(OnIson);
				m_ircManager.OnNames += new IRCManager.NamesHandler(OnNames);
				m_ircManager.OnList += new IRCManager.ListHandler(OnList);
				m_ircManager.OnTopicRequest += new IRCManager.TopicRequestHandler(OnTopicRequest);
				m_ircManager.OnInviteSent += new IRCManager.InviteSentHandler(OnInviteSent);
				m_ircManager.OnAway += new IRCManager.AwayHandler(OnAway);
				m_ircManager.OnWho += new IRCManager.WhoHandler(OnWho);
				m_ircManager.OnWhois += new IRCManager.WhoisHandler(OnWhois);
				m_ircManager.OnWhowas += new IRCManager.WhowasHandler(OnWhowas);
				m_ircManager.OnUserModeRequest += new IRCManager.UserModeRequestHandler(OnUserModeRequest);
				m_ircManager.OnChannelModeRequest += new IRCManager.ChannelModeRequestHandler(OnChannelModeRequest);
				m_ircManager.OnChannelList += new IRCManager.ChannelListHandler(OnChannelList);
				m_ircManager.OnVersion += new IRCManager.VersionHandler(OnVersion);
				m_ircManager.OnTime += new IRCManager.TimeHandler(OnTime);
				m_ircManager.OnInfo += new IRCManager.InfoHandler(OnInfo);
				m_ircManager.OnAdmin += new IRCManager.AdminHandler(OnAdmin);
				m_ircManager.OnLusers += new IRCManager.LusersHandler(OnLusers);
				m_ircManager.OnLinks += new IRCManager.LinksHandler(OnLinks);
				m_ircManager.OnStats += new IRCManager.StatsHandler(OnStats);
				m_ircManager.OnNickError += new IRCManager.NickErrorHandler(OnNickError);
				m_ircManager.OnError += new IRCManager.ErrorHandler(OnError);

				m_channelArray = new List<ChannelInfo>();
				m_channelHash = new Dictionary<string, ChannelInfo>();

				m_ircMenu = new MenuManager();
				m_ircMenu.Items.Add("Log In");
				m_ircMenu.Items.Add("Join");
				m_ircMenu.Items.Add("Part");
				m_ircMenu.Items.Add("Keyboard");
				m_ircMenu.Items.Add("Log Off");
				m_ircMenu.Items.Add("Exit");

				m_ircKeyboard = new KeyboardManager();

				//Events.Add((int)StateID.Reset, new StateMachineEvent(StateMachineEvent.StateType.SuperState, new StateMachineEvent.EnterEventHandler(OnReset), null, null));
				//Events.Add((int)StateID.Enabled, new StateMachineEvent(StateMachineEvent.StateType.SuperState, new StateMachineEvent.EnterEventHandler(OnEnabled), null, null));
				//Events.Add((int)StateID.Showing, new StateMachineEvent(StateMachineEvent.StateType.SuperState, new StateMachineEvent.EnterEventHandler(OnShow), new StateMachineEvent.ExitEventHandler(OnHide), null));
				//Events.Add((int)StateID.ShowingKeyboard, new StateMachineEvent(StateMachineEvent.StateType.SubState, new StateMachineEvent.EnterEventHandler(OnShowKeyboard), new StateMachineEvent.ExitEventHandler(OnHideKeyboard), null));
				//Events.Add((int)StateID.ShowingMenu, new StateMachineEvent(StateMachineEvent.StateType.SubState, new StateMachineEvent.EnterEventHandler(OnShowMenu), new StateMachineEvent.ExitEventHandler(OnHideMenu), null));
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("IRC", "IRC", ex.Message, ex.StackTrace);
			}
		}

		void OnMenuItemSelect(string item)
		{
			switch (item)
			{
				case "Log In":
					m_ircMenu.Hide();
					m_ircKeyboard.Hide();
					m_ircManager.Connect(Settings.IRC.Server, Settings.IRC.Port);
					EventManager.UpdateDisplay();
					break;
				case "Join":
					m_ircMenu.Hide();
					m_ircKeyboard.Hide();
					m_ircManager.JoinChannel(Settings.IRC.Channel);
					EventManager.UpdateDisplay();
					break;
				case "Part":
					m_ircMenu.Hide();
					m_ircKeyboard.Hide();
					m_ircManager.PartChannel(Settings.IRC.Channel);
					EventManager.UpdateDisplay();
					break;
				case "Keyboard":
					m_ircMenu.Hide();
					m_ircKeyboard.Hide();
					m_ircKeyboard.Show();

					EventManager.UpdateDisplay();
					break;
				case "Log Off":
					m_ircMenu.Hide();
					m_ircKeyboard.Hide();
					m_ircManager.Disconnect();
					SendTextColor("Disconnected.", Color.Red);
					break;
				case "Exit":
					m_ircMenu.Hide();
					m_ircKeyboard.Hide();
					Hide();
					if (ExitToMenu)
						Globals.MainMenu.Show();
					else
						Globals.ProgramManager.Hide();
					break;
				case "!":
					m_ircMenu.Hide();
					m_ircKeyboard.Hide();
					EventManager.UpdateDisplay();
					break;
			}
		}

		void OnScreenKeyDown(string key)
		{
			switch (key)
			{
				case "BkSp":
					if (UserInput.Length > 0)
						UserInput = UserInput.Remove(UserInput.Length - 1);
					break;
				case "Tab":
					break;
				case "Enter":
					string Msg = m_ircManager.SendPrivMsg(Settings.IRC.Channel, UserInput);
					if (Msg != null)
					{
						SendText(String.Format("<[YELLOW]{0}[YELLOW]> {1}", m_ircManager.User.NickName, Msg));
						UserInput = "";
					}
					break;
				case "Space":
					if (UserInput.Length < MAX_CHARS)
						UserInput += " ";
					break;
				case "Ins":
					break;
				case "Del":
					break;
				default:
					if (UserInput.Length < MAX_CHARS)
						UserInput += key;
					break;
			}

			EventManager.UpdateDisplay();
		}

		private void OnInputEvent(object sender, InputEventArgs e)
		{
			try
			{
				if (m_ircMenu.Visible || m_ircKeyboard.Visible)
					return;

				if (Globals.InputManager.CheckInput((uint)e.InputCode, Settings.Input.MenuLeft))
					e.Handled = true;

				if (Globals.InputManager.CheckInput((uint)e.InputCode, Settings.Input.MenuRight))
					e.Handled = true;

				if (Globals.InputManager.CheckInput((uint)e.InputCode, Settings.Input.MenuUp))
					e.Handled = true;

				if (Globals.InputManager.CheckInput((uint)e.InputCode, Settings.Input.MenuDown))
					e.Handled = true;

				if (Globals.InputManager.CheckInput((uint)e.InputCode, Settings.Input.SelectKey))
					e.Handled = true;

				if (Globals.InputManager.CheckInput((uint)e.InputCode, Settings.Input.BackKey))
				{
					e.Handled = true;

					if (!e.IsDown)
					{
						m_ircMenu.Show();

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

				if (e.InputCode == (uint)System.Windows.Forms.Keys.Space)
				{
					e.Handled = true;

					if (e.IsDown)
					{
						if (UserInput.Length < MAX_CHARS)
							UserInput += " ";

						EventManager.UpdateDisplay();
					}
				}

				if (e.InputCode == (uint)System.Windows.Forms.Keys.Enter)
				{
					e.Handled = true;

					if (!e.IsDown)
					{
						string Msg = m_ircManager.SendPrivMsg(Settings.IRC.Channel, UserInput);

						if (Msg != null)
						{
							SendText(String.Format("<[YELLOW]{0}[YELLOW]> {1}", m_ircManager.User.NickName, Msg));
							UserInput = "";
						}

						EventManager.UpdateDisplay();
					}
				}

				if (e.InputCode == (uint)System.Windows.Forms.Keys.Back)
				{
					e.Handled = true;

					if (e.IsDown)
					{
						if (UserInput.Length > 0)
							UserInput = UserInput.Substring(0, UserInput.Length - 1);

						EventManager.UpdateDisplay();
					}
				}

				if (e.KeyEventArgs != null)
				{
					KeyEventArgs keyEventArgs = e.KeyEventArgs;

					if ((int)keyEventArgs.Ch > 32 && (int)keyEventArgs.Ch < 127)
					{
						e.Handled = true;

						if (keyEventArgs.IsDown)
						{
							if (UserInput.Length < MAX_CHARS)
								UserInput += keyEventArgs.Ch;

							EventManager.UpdateDisplay();
						}
					}
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("OnGlobalKeyEvent", "IRC", ex.Message, ex.StackTrace);
			}
		}

		public void GetSize()
		{
			try
			{
				using (Bitmap backBmp = new Bitmap(1, 1))
				{
					using (Graphics g = Graphics.FromImage(backBmp))
					{
						FontSize = g.MeasureString(StringTools.StrFillChar('*', MAX_LINE_COLS), RegularFont, 1024, StringFormat.GenericTypographic);
						BackSize = new SizeF(FontSize.Width, FontSize.Height * LINE_COUNT);
					}
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("GetSize", "IRC", ex.Message, ex.StackTrace);
			}
		}

		private void DrawShadow(Graphics g, string text, Font font, RectangleF rect)
		{
			try
			{
				Brush b = new SolidBrush(ShadowColor);
				StringFormat sf = StringFormat.GenericTypographic;

				g.DrawString(text, font, b, new RectangleF(rect.X + 1, rect.Y + 1, rect.Width, rect.Height), StringFormat.GenericTypographic);

				b.Dispose();
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("DrawShadow", "IRC", ex.Message, ex.StackTrace);
			}
		}

		private void DrawString(Graphics g, string text, Font font, Color color, RectangleF rect)
		{
			try
			{
				SolidBrush b = new SolidBrush(color);
				StringFormat sf = StringFormat.GenericTypographic;

				g.DrawString(text, font, b, rect, StringFormat.GenericTypographic);

				b.Dispose();
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("DrawString", "IRC", ex.Message, ex.StackTrace);
			}
		}

		public void DrawUserList(Graphics g)
		{
			try
			{
				ChannelInfo channelInfo = null;

				if (m_channelHash.TryGetValue(Settings.IRC.Channel, out channelInfo))
				{
					channelInfo.SortUserList();

					for (int i = 0; i < channelInfo.UserList.Count && i < LINE_COUNT; i++)
					{
						DrawShadow(g, channelInfo.UserList[i].NickName, RegularFont, new RectangleF(UserListRect.X, UserListRect.Y + (FontSize.Height * i), UserListRect.Width, UserListRect.Height));

						if (channelInfo.UserList[i].Owner || channelInfo.UserList[i].Operator || channelInfo.UserList[i].HalfOp)
							DrawString(g, channelInfo.UserList[i].NickName, RegularFont, Color.Red, new RectangleF(UserListRect.X, UserListRect.Y + (FontSize.Height * i), UserListRect.Width, UserListRect.Height));
						else
							DrawString(g, channelInfo.UserList[i].NickName, RegularFont, Color.Yellow, new RectangleF(UserListRect.X, UserListRect.Y + (FontSize.Height * i), UserListRect.Width, UserListRect.Height));
					}
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("DrawUserList", "IRC", ex.Message, ex.StackTrace);
			}
		}

		public void DrawUserInput(Graphics g)
		{
			try
			{
				string userInput = null;
				if (UserInput.Length > MAX_VISIBLE_CHARS)
					userInput = UserInput.Substring(UserInput.Length - MAX_VISIBLE_CHARS, MAX_VISIBLE_CHARS);
				else
					userInput = UserInput;

				DrawShadow(g, userInput, RegularFont, UserInputRect);
				DrawString(g, userInput + "_", RegularFont, Color.White, UserInputRect);
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("DrawUserInput", "IRC", ex.Message, ex.StackTrace);
			}
		}

		private bool IsSmilie(string text)
		{
			for (int i = 0; i < Smilies.Length; i++)
				if (text == Smilies[i].Name)
					return true;

			return false;
		}

		private IrcFontStyle IsFontStyle(string text)
		{
			switch (text)
			{
				case "[BOLD]":
					return IrcFontStyle.Bold;
				case "[UNDERLINE]":
					return IrcFontStyle.Underline;
				case "[REVERSE]":
					return IrcFontStyle.Reverse;
				default:
					return IrcFontStyle.None;
			}
		}

		private int IsColor(string text, ref Color foreColor, ref Color backColor)
		{
			foreColor = Color.Empty;
			backColor = Color.Empty;

			try
			{
				for (int i = 0; i < IRCManager.WindowsColors.Length; i++)
				{
					if (text == "[" + IRCManager.WindowsColors[i].Name.ToUpper() + "]")
					{
						foreColor = IRCManager.WindowsColors[i];

						return (int)m_ircManager.GetIrcColor(IRCManager.WindowsColors[i]);
					}
					if (text == "[" + IRCManager.WindowsColors[i].Name.ToLower() + "]")
					{
						backColor = IRCManager.WindowsColors[i];

						return (int)m_ircManager.GetIrcColor(IRCManager.WindowsColors[i]);
					}
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("IsColor", "IRC", ex.Message, ex.StackTrace);
			}

			return -1;
		}


		private void DrawSmilie(Graphics g, string text, Rectangle rect)
		{
			try
			{
				for (int i = 0; i < Smilies.Length; i++)
				{
					if (text == Smilies[i].Name)
					{
						g.DrawImage(Smilies[i].Bitmap, rect, 0, 0, Smilies[i].Bitmap.Width, Smilies[i].Bitmap.Height, GraphicsUnit.Pixel);

						return;
					}
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("DrawSmilie", "IRC", ex.Message, ex.StackTrace);
			}
		}

		private void DrawWords(Graphics g, int x, int y, List<IRCWord> words)
		{
			try
			{
				foreach (IRCWord word in words)
				{
					Rectangle rect = new Rectangle(x + word.Rectangle.X, y + word.Rectangle.Y, word.Rectangle.Width, word.Rectangle.Height);

					if (word.IsSmilie)
					{
						DrawSmilie(g, word.Text, rect);
					}
					else
					{
						if (word.BackColor != Color.Empty)
						{
							SolidBrush backBrush = new SolidBrush(word.BackColor);
							g.FillRectangle(backBrush, rect);

							backBrush.Dispose();
						}

						Rectangle rectText = new Rectangle(x + word.Rectangle.X, y + word.Rectangle.Y, 0, 0);

						DrawShadow(g, word.Text, word.Font, rectText);
						DrawString(g, word.Text, word.Font, word.ForeColor, rectText);
					}

					//g.DrawRectangle(Pens.Red, rect);
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("DrawWords", "IRC", ex.Message, ex.StackTrace);
			}
		}

		private void DrawInfo(Graphics g)
		{
			try
			{
				string ircInfo = Settings.IRC.Channel + " / " + Settings.IRC.Server;
				SizeF textSize = g.MeasureString(ircInfo, RegularFont, 0, StringFormat.GenericTypographic);

				DrawString(g, ircInfo, RegularFont, Color.White, new RectangleF((BackSize.Width / 2) - (textSize.Width / 2), BackSize.Height - textSize.Height - 2, 0, 0));
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("DrawInfo", "IRC", ex.Message, ex.StackTrace);
			}
		}

		private void DrawLines(Graphics g)
		{
			try
			{
				Bitmap bmpText = new Bitmap((int)MainRect.Width, (int)MainRect.Height);
				Graphics gText = Graphics.FromImage(bmpText);

				Globals.DisplayManager.SetGraphicsQuality(gText, DisplayManager.UserGraphicsQuality());

				int xOffset = 0;
				int yOffset = (int)MainRect.Height;
				int LineCount = 1;

				for (int i = IRCLines.Count - 1; i >= 0; i--)
				{
					LineCount += IRCLines[i].LineCount;
					yOffset -= (int)(IRCLines[i].LineCount * FontSize.Height);

					DrawWords(gText, xOffset, yOffset, IRCLines[i].Words);

					if (yOffset < 0)
						break;
				}

				g.DrawImage(bmpText, MainRect.X, MainRect.Y);

				bmpText.Dispose();
				gText.Dispose();
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("DrawLines", "IRC", ex.Message, ex.StackTrace);
			}
		}

		private void AddLine(string text)
		{
			IRCLines.Add(GetLine(text));

			if (IRCLines.Count > LINE_COUNT)
				IRCLines.Remove(IRCLines[0]);
		}

		private IRCLine GetLine(string text)
		{
			try
			{
				IRCLine line = new IRCLine();
				int LineCount = 1;

				using (Bitmap bmp = new Bitmap(1, 1))
				{
					using (Graphics g = Graphics.FromImage(bmp))
					{
						List<string> splitNames = new List<string>();
						SizeF fontSize;
						string[] splitString = null;
						Font font = RegularFont;
						Color foreColor = ForeColor;
						Color backColor = Color.Empty;
						bool bBold = false, bUnderline = false, bReverse = false;
						int xOffset = 0, yOffset = 0;

						splitNames.Add(" ");

						for (int i = 0; i < FontStyleNames.Length; i++)
							splitNames.Add(FontStyleNames[i]);

						for (int i = 0; i < m_ircManager.WindowsColorNames.Length; i++)
							splitNames.Add("[" + m_ircManager.WindowsColorNames[i].ToUpper() + "]");

						for (int i = 0; i < m_ircManager.WindowsColorNames.Length; i++)
							splitNames.Add("[" + m_ircManager.WindowsColorNames[i].ToLower() + "]");

						for (int i = 0; i < Smilies.Length; i++)
							splitNames.Add(Smilies[i].Name);

						for (int i = 0; i < ForeColorSwitch.Length; i++)
							ForeColorSwitch[i] = false;

						for (int i = 0; i < BackColorSwitch.Length; i++)
							BackColorSwitch[i] = false;

						splitString = StringTools.SplitString(text, splitNames.ToArray());

						for (int i = 0; i < splitString.Length; i++)
						{
							//System.Diagnostics.Debug.WriteLine(">" + splitString[i] + "<");

							if (IsSmilie(splitString[i]))
							{
								fontSize = g.MeasureString("**", font, 0, StringFormat.GenericTypographic);
								line.Words.Add(new IRCWord(splitString[i], new Rectangle(xOffset, yOffset, (int)fontSize.Width, (int)fontSize.Height)));

								xOffset += (int)fontSize.Width;

								continue;
							}

							IrcFontStyle fontStyle = IsFontStyle(splitString[i]);

							switch (fontStyle)
							{
								case IrcFontStyle.Bold:
									bBold = !bBold;
									if (bBold)
										font = BoldFont;
									else
										font = RegularFont;
									continue;
								case IrcFontStyle.Underline:
									bUnderline = !bUnderline;
									if (bUnderline)
										font = UnderlineFont;
									else
										font = RegularFont;
									continue;
								case IrcFontStyle.Reverse:
									bReverse = !bReverse;
									if (bReverse)
									{
										foreColor = BackColor;
										backColor = ForeColor;
									}
									else
									{
										foreColor = ForeColor;
										backColor = BackColor;
									}
									continue;
							}

							Color fore = Color.Empty, back = Color.Empty;

							int IrcColor = IsColor(splitString[i], ref fore, ref back);

							if (IrcColor != -1)
							{
								if (fore != Color.Empty)
								{
									ForeColorSwitch[IrcColor] = !ForeColorSwitch[IrcColor];

									if (ForeColorSwitch[IrcColor])
										foreColor = fore;
									else
										foreColor = ForeColor;
								}
								else
									if (back != Color.Empty)
									{
										BackColorSwitch[IrcColor] = !BackColorSwitch[IrcColor];

										if (BackColorSwitch[IrcColor])
											backColor = back;
										else
											backColor = BackColor;
									}

								continue;
							}

							int Characters = 0, Lines = 0;

							g.MeasureString(splitString[i].Replace(' ', '*'), font, new SizeF(MainRect.Width - xOffset, FontSize.Height), StringFormat.GenericTypographic, out Characters, out Lines);
							fontSize = g.MeasureString(splitString[i].Replace(' ', '*'), font, 0, StringFormat.GenericTypographic);

							if (Lines == 0)
							{
								xOffset = 0;
								yOffset += (int)FontSize.Height;
								LineCount++;

								if (splitString[i] == " ")
									continue;
							}

							line.Words.Add(new IRCWord(splitString[i], new Rectangle(xOffset, yOffset, (int)fontSize.Width, (int)fontSize.Height), font, foreColor, backColor));

							xOffset += (int)fontSize.Width;
						}
					}
				}

				line.LineCount = LineCount;

				return line;
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("GetLine", "IRC", ex.Message, ex.StackTrace);
			}

			return null;
		}

		public override void Paint(Graphics g, int x, int y, int width, int height)
		{
			try
			{
				if (IRC.Busy)
					return;

				IRC.Busy = true;

				Bitmap ircBmp = new Bitmap((int)BackSize.Width, (int)(BackSize.Height));
				Graphics gSrc = Graphics.FromImage(ircBmp);

				Globals.DisplayManager.SetGraphicsQuality(g, DisplayManager.UserGraphicsQuality());

				lock (IRCBak)
				{
					gSrc.DrawImage(IRCBak, new Rectangle(0, 0, (int)BackSize.Width, (int)(BackSize.Height)), 0, 0, IRCBak.Width, IRCBak.Height, GraphicsUnit.Pixel);

					DrawInfo(gSrc);
					DrawLines(gSrc);

					DrawUserList(gSrc);
					DrawUserInput(gSrc);

					if (m_ircMenu.Visible)
						m_ircMenu.Paint(gSrc, (int)(BackSize.Width / 4), (int)(BackSize.Height / 4), (int)(BackSize.Width / 2), (int)(BackSize.Height / 2));

					if (m_ircKeyboard.Visible)
						m_ircKeyboard.Paint(gSrc, 0, (int)MainRect.Y, (int)BackSize.Width, (int)BackSize.Height);

					g.DrawImage(ircBmp, new Rectangle(x, y, width, height), 0, 0, ircBmp.Width, ircBmp.Height, GraphicsUnit.Pixel);
				}
				gSrc.Dispose();
				ircBmp.Dispose();

				IRC.Busy = false;
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("Paint", "IRC", ex.Message, ex.StackTrace);
			}
		}

		private void SendTextColor(string IrcLine, Color Color)
		{
			string ColorName = Color.Name.ToUpper();
			SendText("[" + ColorName + "]" + IrcLine + "[" + ColorName + "]");
		}

		private void SendText(string IrcLine)
		{
			AddLine(IrcLine);

			EventManager.UpdateDisplay();

			//System.Windows.Forms.Application.DoEvents();
		}

		void OnReply(IRCManager.ReplyCode code, string message)
		{
			//rtbOutput.AppendText(String.Format("OnReply: {0},{1}{2}", code.ToString(), message, Environment.NewLine));
			SendText(m_ircManager.ProcessLine(message));
		}

		void OnPing(string message)
		{
			//rtbOutput.AppendText(String.Format("OnPing: {0}{1}", message, Environment.NewLine)); // Ping? Pong!
			//rtbOutput.AppendText("Ping? Pong!" + Environment.NewLine);
			// m_ircManager.Pong(message);
			SendTextColor("Ping? Pong!", Color.LightGreen);
			m_ircManager.Pong(message);
		}

		void OnPublicNotice(UserInfo user, string channel, string notice)
		{
			//rtbOutput.AppendText("OnPublicNotice" + Environment.NewLine);
			SendTextColor(notice, Color.Red);
		}

		void OnPrivateNotice(UserInfo user, string channel, string notice)
		{
			//rtbOutput.AppendText(String.Format("OnPrivateNotice: {0} {1} {2}{3}", user.ToString(), channel, notice, Environment.NewLine));
			//rtbOutput.AppendText(notice + Environment.NewLine);
			SendTextColor(notice, Color.Red);
		}

		void OnJoin(UserInfo user, string channel)
		{
			//rtbOutput.AppendText("OnJoin" + Environment.NewLine);
			ChannelInfo channelInfo = null;
			if (m_channelHash.TryGetValue(channel, out channelInfo))
			{
				channelInfo.AddUser(user);
			}
			else
			{
				channelInfo = new ChannelInfo(channel);
				channelInfo.AddUser(user);
				m_channelArray.Add(channelInfo);
				m_channelHash.Add(channel, channelInfo);
			}

			SendTextColor(String.Format("* {0} ({1}) has joined {2}", user.NickName, user.HostName, channel), Color.LightGreen);

			EventManager.UpdateDisplay();
		}

		void OnAction(UserInfo user, string channel, string description)
		{
			//rtbOutput.AppendText("OnAction" + Environment.NewLine);
		}

		void OnPrivateAction(UserInfo user, string description)
		{
			//rtbOutput.AppendText("OnPrivateAction" + Environment.NewLine);
		}

		void OnPublic(UserInfo user, string channel, string message)
		{
			//rtbOutput.AppendText("OnPublic" + Environment.NewLine);

			if (channel == Settings.IRC.Channel)
			{
				SendText(String.Format("<[YELLOW]{0}[YELLOW]> {1}", user.NickName, m_ircManager.ProcessLine(message)));

				System.Diagnostics.Debug.WriteLine(String.Format("<[YELLOW]{0}[YELLOW]> {1}", user.NickName, message));
			}
		}

		void OnPrivate(UserInfo user, string message)
		{
			//rtbOutput.AppendText("OnPrivate" + Environment.NewLine);

			SendText(String.Format("* [YELLOW]{0}[YELLOW]: {1}", user.NickName, m_ircManager.ProcessLine(message)));

			System.Diagnostics.Debug.WriteLine(String.Format("* [YELLOW]{0}[YELLOW]: {1}", user.NickName, message));
		}

		void OnNick(UserInfo user, string newNick)
		{
			//rtbOutput.AppendText("OnNick" + Environment.NewLine);

			ChannelInfo channelInfo = null;

			if (m_channelHash.TryGetValue(Settings.IRC.Channel, out channelInfo))
			{
				UserInfo userFind = channelInfo.GetUser(user.NickName);

				userFind.NickName = newNick;
			}
		}

		void OnTopic(UserInfo user, string channel, string newTopic)
		{
			//rtbOutput.AppendText("OnTopic" + Environment.NewLine);
		}

		void OnPart(UserInfo user, string channel, string reason)
		{
			//rtbOutput.AppendText("OnPart" + Environment.NewLine);
			ChannelInfo channelInfo = null;

			if (m_channelHash.TryGetValue(channel, out channelInfo))
			{
				channelInfo.RemoveUser(user.NickName);
			}

			EventManager.UpdateDisplay();
		}

		void OnQuit(UserInfo user, string reason)
		{
			//rtbOutput.AppendText("OnQuit" + Environment.NewLine);

			ChannelInfo channelInfo = null;

			if (m_channelHash.TryGetValue(Settings.IRC.Channel, out channelInfo))
			{
				channelInfo.RemoveUser(user.NickName);

				SendTextColor(String.Format("* {0} ({1}) Quit ({2})", user.NickName, user.HostName, reason), Color.Blue);
			}
		}

		void OnInvite(UserInfo user, string channel)
		{
			//rtbOutput.AppendText("OnInvite" + Environment.NewLine);
		}

		void OnKick(UserInfo user, string channel, string kickee, string reason)
		{
			//rtbOutput.AppendText("OnKick" + Environment.NewLine);
		}

		void OnChannelModeChange(UserInfo who, string channel, ChannelModeInfo[] modes)
		{
			//rtbOutput.AppendText("OnChannelModeChange" + Environment.NewLine);
			ChannelInfo channelInfo = null;

			if (m_channelHash.TryGetValue(channel, out channelInfo))
			{
				for (int i = 0; i < modes.Length; i++)
				{
					UserInfo userInfo = null;
					if (channelInfo.ContainsUser(modes[i].Parameter, out userInfo))
					{
						switch (modes[i].Action)
						{
							case IRCManager.ModeAction.Add:
								userInfo.AddPrefix(modes[i].NickPrefix);
								break;
							case IRCManager.ModeAction.Remove:
								userInfo.RemovePrefix(modes[i].NickPrefix);
								break;
						}
					}
				}
			}

			EventManager.UpdateDisplay();
		}

		void OnUserModeChange(IRCManager.ModeAction action, IRCManager.UserMode mode)
		{
			//rtbOutput.AppendText("OnUserModeChange" + Environment.NewLine);
		}

		void OnKill(UserInfo user, string nick, string reason)
		{
			//rtbOutput.AppendText("OnKill" + Environment.NewLine);
		}

		void OnRegistered()
		{
			//rtbOutput.AppendText("OnRegistered" + Environment.NewLine);
			m_ircManager.JoinChannel(Settings.IRC.Channel);
			//m_ircManager.SendPrivMsg(m_currentChannel, "Hi There.");
		}

		void OnMotd(string message, bool last)
		{
			//rtbOutput.AppendText("OnMotd" + Environment.NewLine);
		}

		void OnIson(string nicks)
		{
			//rtbOutput.AppendText("OnIson" + Environment.NewLine);
		}

		void OnNames(string channel, string[] nicks, bool last)
		{
			//rtbOutput.AppendText("OnNames" + Environment.NewLine);
			ChannelInfo channelInfo = null;

			if (m_channelHash.TryGetValue(channel, out channelInfo))
			{
				for (int i = 0; i < nicks.Length; i++)
					channelInfo.AddUser(nicks[i]);
			}

			EventManager.UpdateDisplay();
		}

		void OnList(string channel, int visibleNickCount, string topic, bool last)
		{
			//rtbOutput.AppendText("OnList" + Environment.NewLine);
		}

		void OnTopicRequest(string channel, string topic)
		{
			//rtbOutput.AppendText("OnTopicRequest" + Environment.NewLine);
		}

		void OnInviteSent(string nick, string channel)
		{
			//rtbOutput.AppendText("OnInviteSent" + Environment.NewLine);
		}

		void OnAway(string nick, string awayMessage)
		{
			//rtbOutput.AppendText("OnAway" + Environment.NewLine);
		}

		void OnWho(UserInfo user, string channel, string ircServer, string mask, int hopCount, string realName, bool last)
		{
			//rtbOutput.AppendText("OnWho" + Environment.NewLine);
		}

		void OnWhois(WhoisInfo whoisInfo)
		{
			//rtbOutput.AppendText("OnWhois" + Environment.NewLine);
		}

		void OnWhowas(UserInfo user, string realName, bool last)
		{
			//rtbOutput.AppendText("OnWhowas" + Environment.NewLine);
		}

		void OnUserModeRequest(IRCManager.UserMode[] modes)
		{
			//rtbOutput.AppendText("OnUserModeRequest" + Environment.NewLine);
		}

		void OnChannelModeRequest(string channel, ChannelModeInfo[] modes)
		{
			//rtbOutput.AppendText("OnChannelModeRequest" + Environment.NewLine);
		}

		void OnChannelList(string channel, IRCManager.ChannelMode mode, string item, UserInfo who, long whenSet, bool last)
		{
			//rtbOutput.AppendText("OnChannelList" + Environment.NewLine);
		}

		void OnVersion(string versionInfo)
		{
			//rtbOutput.AppendText("OnVersion" + Environment.NewLine);
		}

		void OnTime(string time)
		{
			//rtbOutput.AppendText("OnTime" + Environment.NewLine);
		}

		void OnInfo(string message, bool last)
		{
			//rtbOutput.AppendText("OnInfo" + Environment.NewLine);
		}

		void OnAdmin(string message)
		{
			//rtbOutput.AppendText("OnAdmin" + Environment.NewLine);
		}

		void OnLusers(string message)
		{
			//rtbOutput.AppendText("OnLusers" + Environment.NewLine);
		}

		void OnLinks(string mask, string hostname, int hopCount, string serverInfo, bool done)
		{
			//rtbOutput.AppendText("OnLinks" + Environment.NewLine);
		}

		void OnStats(IRCManager.StatsQuery queryType, string message, bool done)
		{
			//rtbOutput.AppendText("OnStats" + Environment.NewLine);
		}

		void OnNickError(string badNick, string reason)
		{
			//rtbOutput.AppendText("OnNickError" + Environment.NewLine);
		}

		void OnError(IRCManager.ReplyCode code, string message)
		{
			//rtbOutput.AppendText("OnError" + Environment.NewLine);

			SendTextColor(message, Color.Red);
		}

		public override void Show()
		{
			m_ircMenu.Show();

			base.Show();
		}

		public override void Hide()
		{
			m_ircMenu.Hide();
			m_ircKeyboard.Hide();

			base.Hide();
		}

		public override bool CheckEnabled()
		{
			return true;
		}

		public override void Reset(EmulatorMode mode)
		{
			m_ircMenu.Reset(mode);
			m_ircKeyboard.Reset(mode);
		}

		public override void AddEventHandlers()
		{
			Globals.InputManager.InputEvent += new InputManager.InputEventHandler(OnInputEvent);
			m_ircMenu.MenuItemSelect += new MenuItemSelectHandler(OnMenuItemSelect);
			m_ircKeyboard.OnScreenKeyDown += new OnScreenKeyDownHandler(OnScreenKeyDown);
		}

		public override void RemoveEventHandlers()
		{
			Globals.InputManager.InputEvent -= new InputManager.InputEventHandler(OnInputEvent);
			m_ircMenu.MenuItemSelect -= new MenuItemSelectHandler(OnMenuItemSelect);
			m_ircKeyboard.OnScreenKeyDown -= new OnScreenKeyDownHandler(OnScreenKeyDown);
		}

		#region IDisposable Members

		public override void Dispose()
		{
			base.Dispose();

			if (m_ircManager != null)
			{
				m_ircManager.Dispose();
				m_ircManager = null;
			}

			if (m_ircMenu != null)
			{
				m_ircMenu.Dispose();
				m_ircMenu = null;
			}

			if (m_ircKeyboard != null)
			{
				m_ircKeyboard.Dispose();
				m_ircKeyboard = null;
			}
		}

		#endregion
	}
}
