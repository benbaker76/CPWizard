// Copyright (c) 2015, Ben Baker
// All rights reserved.
// 
// This source code is licensed under the BSD-style license found in the
// LICENSE file in the root directory of this source tree. 

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;

namespace CPWizard
{
	public class Util
	{
		private const string UserModes = "awiorOs";
		private const string ChannelModes = "OohvaimnqpsrtklbeI";

		private const string REGEX_SPECIAL = "\\[\\]\\`_\\^\\{\\|\\}";
		private const string REGEX_NICK = "[" + REGEX_SPECIAL + "a-zA-Z][\\w\\-" + REGEX_SPECIAL + "]{0,8}";
		private const string REGEX_USER = "(" + REGEX_NICK + ")!([\\~\\w]+)@([\\w\\.\\-]+)";
		private const string REGEX_NAMESPLITTER = "[!@]";

		private static Regex userRegex;
		private static Regex nickRegex;
		private static Regex nameSplitterRegex;

		static Util()
		{
			userRegex = new Regex(REGEX_USER);
			nickRegex = new Regex(REGEX_NICK);
			nameSplitterRegex = new Regex(REGEX_NAMESPLITTER, RegexOptions.Compiled | RegexOptions.Singleline);
		}

		public static string GetNickWithoutPrefix(string nick)
		{
			if (nick.StartsWith("~")) // Channel Owner
				return nick.Substring(1);
			if (nick.StartsWith("&")) // Channel Admin
				return nick.Substring(1);
			if (nick.StartsWith("@")) // Channel Operator
				return nick.Substring(1);
			if (nick.StartsWith("%")) // Half Operator (Halfop)
				return nick.Substring(1);
			if (nick.StartsWith("+")) // Voice
				return nick.Substring(1);

			return nick;
		}

		public static IRCManager.StatsQuery CharToStatsQuery(char queryType)
		{
			byte b = System.Convert.ToByte(queryType, CultureInfo.InvariantCulture);
			return (IRCManager.StatsQuery)Enum.Parse(typeof(IRCManager.StatsQuery), b.ToString(CultureInfo.InvariantCulture), false);
		}

		public static bool IsValidModeChar(char c, string validList)
		{
			return validList.IndexOf(c) != -1;
		}

		public static bool ContainsSpace(string text)
		{
			return text.IndexOf(' ', 0, text.Length) != -1;
		}

		public static bool IsValidNick(string nick)
		{
			if (nick == null || nick.Trim().Length == 0)
			{
				return false;
			}
			if (ContainsSpace(nick))
			{
				return false;
			}
			if (nickRegex.IsMatch(nick))
			{
				return true;
			}
			return false;
		}

		public static bool IsValidChannelName(string channel)
		{
			if (channel == null || channel.Trim().Length == 0)
			{
				return false;
			}

			if (ContainsSpace(channel))
			{
				return false;
			}
			if ("#!+&".IndexOf(channel[0]) != -1)
			{
				if (channel.Length <= 50)
				{
					return true;
				}
			}
			return false;
		}

		public static string[] ParseUserInfoLine(string fullUserName)
		{
			if (fullUserName == null || fullUserName.Trim().Length == 0)
			{
				return null;
			}
			Match match = nameSplitterRegex.Match(fullUserName);
			if (match.Success)
			{
				string[] parts = nameSplitterRegex.Split(fullUserName);
				return parts;
			}
			else
			{
				return new string[] { fullUserName, "", "" };
			}
		}

		public static UserInfo UserInfoFromString(string fullUserName)
		{
			string[] parts = ParseUserInfoLine(fullUserName);
			if (parts == null)
			{
				return UserInfo.Empty;
			}
			else
			{
				return new UserInfo(parts[0], parts[1], parts[2]);
			}
		}

		public static IRCManager.ModeAction CharToModeAction(char action)
		{
			byte b = System.Convert.ToByte(action, CultureInfo.InvariantCulture);
			return (IRCManager.ModeAction)Enum.Parse(typeof(IRCManager.ModeAction), b.ToString(CultureInfo.InvariantCulture), false);
		}

		public static IRCManager.ChannelMode CharToChannelMode(char mode)
		{
			byte b = System.Convert.ToByte(mode, CultureInfo.InvariantCulture);
			return (IRCManager.ChannelMode)Enum.Parse(typeof(IRCManager.ChannelMode), b.ToString(CultureInfo.InvariantCulture), false);
		}

		public static IRCManager.UserMode CharToUserMode(char mode)
		{
			byte b = System.Convert.ToByte(mode, CultureInfo.InvariantCulture);
			return (IRCManager.UserMode)Enum.Parse(typeof(IRCManager.UserMode), b.ToString(CultureInfo.InvariantCulture), false);
		}

		public static IRCManager.UserMode[] UserModesToArray(string modes)
		{
			List<IRCManager.UserMode> list = new List<IRCManager.UserMode>();
			for (int i = 0; i < modes.Length; i++)
			{
				if (IsValidModeChar(modes[i], UserModes))
				{
					list.Add(CharToUserMode(modes[i]));
				}
			}
			return (IRCManager.UserMode[])list.ToArray();
		}
	}

	public class UserInfo : IComparable
	{
		public string NickName = null;
		public string UserName = null;
		public string RealName = null;
		public string HostName = null;
		public bool IsInvisible = true;
		private static readonly UserInfo EmptyInstance = new UserInfo();

		public bool Owner = false;
		public bool Operator = false;
		public bool HalfOp = false;
		public bool Voice = false;

		public UserInfo()
		{
		}

		public UserInfo(string nick)
		{
			NickName = nick;

			CheckNickPrefix();
		}

		public UserInfo(string nickName, string userName, string realName, bool isinvisible)
		{
			NickName = nickName;
			UserName = userName;
			RealName = realName;
			IsInvisible = isinvisible;

			CheckNickPrefix();
		}

		public UserInfo(string nick, string user, string host)
		{
			NickName = nick;
			UserName = user;
			HostName = host;

			CheckNickPrefix();
		}

		private void CheckNickPrefix()
		{
			if (NickName.StartsWith("~")) // Owner
				Owner = true;
			if (NickName.StartsWith("@")) // Operator
				Operator = true;
			if (NickName.StartsWith("%")) // HalfOp
				HalfOp = true;
			if (NickName.StartsWith("+")) // Voice
				Voice = true;
		}

		private void CreateNickName()
		{
			NickName = Util.GetNickWithoutPrefix(NickName);

			if (Owner)
			{
				NickName = "~" + NickName;
				return;
			}
			if (Operator)
			{
				NickName = "@" + NickName;
				return;
			}
			if (HalfOp)
			{
				NickName = "%" + NickName;
				return;
			}
			if (Voice)
			{
				NickName = "+" + NickName;
				return;
			}
		}

		public void AddPrefix(string mode)
		{
			switch (mode)
			{
				case "~": // Owner
					Owner = true;
					break;
				case "@": // Operator
					Operator = true;
					break;
				case "%": // HalfOp
					HalfOp = true;
					break;
				case "+": // Voice
					Voice = true;
					break;
			}

			CreateNickName();
		}

		public void RemovePrefix(string mode)
		{
			switch (mode)
			{
				case "~": // Owner
					Owner = false;
					break;
				case "@": // Operator
					Operator = false;
					break;
				case "%": // HalfOp
					HalfOp = false;
					break;
				case "+": // Voice
					Voice = false;
					break;
			}

			CreateNickName();
		}

		public int CompareTo(object obj)
		{
			if (obj is UserInfo)
			{
				UserInfo temp = (UserInfo)obj;
				return this.NickName.CompareTo(temp.NickName);
			}
			else
				throw new ArgumentException("Object is not a User.");
		}

		public static UserInfo Empty
		{
			get { return EmptyInstance; }
		}
	}

	public class ChannelInfo
	{
		public string Name = null;
		public List<UserInfo> UserList = null;

		public ChannelInfo(string name)
		{
			Name = name;
			UserList = new List<UserInfo>();
		}

		public void SortUserList()
		{
			UserList.Sort();
		}

		public bool ContainsUser(string NickName, out UserInfo User)
		{
			User = null;

			if (String.IsNullOrEmpty(NickName))
				return false;

			foreach (UserInfo user in UserList)
			{
				if (Util.GetNickWithoutPrefix(user.NickName.ToLower()) == Util.GetNickWithoutPrefix(NickName.ToLower()))
				{
					User = user;
					return true;
				}
			}

			return false;
		}

		public UserInfo GetUser(string NickName)
		{
			if (String.IsNullOrEmpty(NickName))
				return null;

			foreach (UserInfo user in UserList)
				if (Util.GetNickWithoutPrefix(user.NickName.ToLower()) == Util.GetNickWithoutPrefix(NickName.ToLower()))
					return user;

			return null;
		}

		public bool ContainsUser(string NickName)
		{
			if (String.IsNullOrEmpty(NickName))
				return false;

			foreach (UserInfo user in UserList)
				if (Util.GetNickWithoutPrefix(user.NickName.ToLower()) == Util.GetNickWithoutPrefix(NickName.ToLower()))
					return true;

			return false;
		}

		public void AddUser(string NickName)
		{
			if (String.IsNullOrEmpty(NickName))
				return;

			if (!ContainsUser(NickName))
				UserList.Add(new UserInfo(NickName));
		}

		public void AddUser(UserInfo User)
		{
			if (User == null)
				return;

			if (String.IsNullOrEmpty(User.NickName))
				return;

			UserInfo userInfo = null;
			if (ContainsUser(User.NickName, out userInfo))
			{
				userInfo.UserName = User.UserName;
				userInfo.RealName = User.RealName;
				userInfo.HostName = User.HostName;
				userInfo.IsInvisible = User.IsInvisible;
			}
			else
				UserList.Add(User);
		}

		public bool RemoveUser(string NickName)
		{
			if (String.IsNullOrEmpty(NickName))
				return false;

			UserInfo User = null;

			if (ContainsUser(NickName, out User))
			{
				UserList.Remove(User);

				return true;
			}

			return false;
		}
	}

	public class ChannelModeInfo
	{
		public IRCManager.ModeAction Action;
		public IRCManager.ChannelMode Mode;
		public string Parameter;

		public override string ToString()
		{
			return string.Format("Action={0} Mode={1} Parameter={2}", Action, Mode, Parameter);
		}

		public string NickPrefix
		{
			get
			{
				switch (Mode)
				{
					case IRCManager.ChannelMode.ChannelCreator:
						return "~";
					case IRCManager.ChannelMode.ChannelOperator:
						return "@";
					case IRCManager.ChannelMode.HalfChannelOperator:
						return "%";
					case IRCManager.ChannelMode.Voice:
						return "+";
				}

				return null;
			}
		}

		internal static ChannelModeInfo[] ParseModes(string[] tokens, int start)
		{
			List<ChannelModeInfo> modeInfoArray = new List<ChannelModeInfo>();
			int i = start;
			while (i < tokens.Length)
			{
				ChannelModeInfo modeInfo = new ChannelModeInfo();
				int parmIndex = i + 1;
				for (int j = 0; j < tokens[i].Length; j++)
				{

					while (j < tokens[i].Length && tokens[i][j] == '+')
					{
						modeInfo.Action = IRCManager.ModeAction.Add;
						j++;
					}
					while (j < tokens[i].Length && tokens[i][j] == '-')
					{
						modeInfo.Action = IRCManager.ModeAction.Remove;
						j++;
					}
					if (j == 0)
					{
						throw new Exception();
					}
					else if (j < tokens[i].Length)
					{
						switch (tokens[i][j])
						{
							case 'o': // Operator
							case 'h': // Half-op
							case 'v': // Voice
							case 'b': // Ban
							case 'e': // Event
							case 'I': // Invite only
							case 'k': // Key lock (password)
							case 'O': // Owner
								modeInfo.Mode = Util.CharToChannelMode(tokens[i][j]);
								modeInfo.Parameter = tokens[parmIndex++];
								break;
							case 'l': // Limit
								modeInfo.Mode = Util.CharToChannelMode(tokens[i][j]);
								if (modeInfo.Action == IRCManager.ModeAction.Add)
								{
									modeInfo.Parameter = tokens[parmIndex++];
								}
								else
								{
									modeInfo.Parameter = "";
								}
								break;
							default:
								modeInfo.Mode = Util.CharToChannelMode(tokens[i][j]);
								modeInfo.Parameter = "";
								break;
						}

					}
					modeInfoArray.Add((ChannelModeInfo)modeInfo.MemberwiseClone());
				}
				i = parmIndex;
			}

			return (ChannelModeInfo[])modeInfoArray.ToArray();
		}
	}

	public class WhoisInfo
	{
		public UserInfo UserInfo;
		public string RealName;
		public string[] Channels;
		public string IrcServer;
		public string ServerDescription;
		public long IdleTime;
		public bool IsOperator;

		public WhoisInfo()
		{
			IsOperator = false;
		}

		public void SetChannels(string[] channels)
		{
			Channels = channels;
		}

		public string[] GetChannels()
		{
			return Channels;
		}

	}

	public class IRCManager : IDisposable
	{
		#region ReplyCode
		public enum ReplyCode : int
		{
			RPL_WELCOME = 001,
			RPL_YOURHOST = 002,
			RPL_CREATED = 003,
			RPL_MYINFO = 004,
			RPL_BOUNCE = 005,
			RPL_USERHOST = 302,
			RPL_ISON = 303,
			RPL_AWAY = 301,
			RPL_UNAWAY = 305,
			RPL_NOWAWAY = 306,
			RPL_WHOISUSER = 311,
			RPL_WHOISSERVER = 312,
			RPL_WHOISOPERATOR = 313,
			RPL_WHOISIDLE = 317,
			RPL_ENDOFWHOIS = 318,
			RPL_WHOISCHANNELS = 319,
			RPL_WHOWASUSER = 314,
			RPL_ENDOFWHOWAS = 369,
			RPL_LISTSTART = 321,
			RPL_LIST = 322,
			RPL_LISTEND = 323,
			RPL_UNIQOPIS = 325,
			RPL_CHANNELMODEIS = 324,
			RPL_NOTOPIC = 331,
			RPL_TOPIC = 332,
			RPL_INVITING = 341,
			RPL_SUMMONING = 342,
			RPL_INVITELIST = 346,
			RPL_ENDOFINVITELIST = 347,
			RPL_EXCEPTLIST = 348,
			RPL_ENDOFEXCEPTLIST = 349,
			RPL_VERSION = 351,
			RPL_WHOREPLY = 352,
			RPL_ENDOFWHO = 315,
			RPL_NAMREPLY = 353,
			RPL_ENDOFNAMES = 366,
			RPL_LINKS = 364,
			RPL_ENDOFLINKS = 365,
			RPL_BANLIST = 367,
			RPL_ENDOFBANLIST = 368,
			RPL_INFO = 371,
			RPL_ENDOFINFO = 374,
			RPL_MOTDSTART = 375,
			RPL_MOTD = 372,
			RPL_ENDOFMOTD = 376,
			RPL_YOUREOPER = 381,
			RPL_REHASHING = 382,
			RPL_YOURESERVICE = 383,
			RPL_TIME = 391,
			RPL_USERSSTART = 392,
			RPL_USERS = 393,
			RPL_ENDOFUSERS = 394,
			RPL_NOUSERS = 395,
			RPL_TRACELINK = 200,
			RPL_TRACECONNECTING = 201,
			RPL_TRACEHANDSHAKE = 202,
			RPL_TRACEUNKNOWN = 203,
			RPL_TRACEOPERATOR = 204,
			RPL_TRACEUSER = 205,
			RPL_TRACESERVER = 206,
			RPL_TRACESERVICE = 207,
			RPL_TRACENEWTYPE = 208,
			RPL_TRACECLASS = 209,
			RPL_TRACERECONNECT = 210,
			RPL_TRACELOG = 261,
			RPL_TRACEEND = 262,
			RPL_STATSLINKINFO = 211,
			RPL_STATSCOMMANDS = 212,
			RPL_ENDOFSTATS = 219,
			RPL_STATSUPTIME = 242,
			RPL_STATSOLINE = 243,
			RPL_UMODEIS = 221,
			RPL_SERVLIST = 234,
			RPL_SERVLISTEND = 235,
			RPL_LUSERCLIENT = 251,
			RPL_LUSEROP = 252,
			RPL_LUSERUNKNOWN = 253,
			RPL_LUSERCHANNELS = 254,
			RPL_LUSERME = 255,
			RPL_ADMINME = 256,
			RPL_ADMINLOC1 = 257,
			RPL_ADMINLOC2 = 258,
			RPL_ADMINEMAIL = 259,
			RPL_TRYAGAIN = 263,
			ERR_NOSUCHNICK = 401,
			ERR_NOSUCHSERVER = 402,
			ERR_NOSUCHCHANNEL = 403,
			ERR_CANNOTSENDTOCHAN = 404,
			ERR_TOOMANYCHANNELS = 405,
			ERR_WASNOSUCHNICK = 406,
			ERR_TOOMANYTARGETS = 407,
			ERR_NOSUCHSERVICE = 408,
			ERR_NOORIGIN = 409,
			ERR_NORECIPIENT = 411,
			ERR_NOTEXTTOSEND = 412,
			ERR_NOTOPLEVEL = 413,
			ERR_WILDTOPLEVEL = 414,
			ERR_BADMASK = 415,
			ERR_TOOMANYLINES = 416,
			ERR_UNKNOWNCOMMAND = 421,
			ERR_NOMOTD = 422,
			ERR_NOADMININFO = 423,
			ERR_FILEERROR = 424,
			ERR_NONICKNAMEGIVEN = 431,
			ERR_ERRONEUSNICKNAME = 432,
			ERR_NICKNAMEINUSE = 433,
			ERR_NICKCOLLISION = 436,
			ERR_UNAVAILRESOURCE = 437,
			ERR_USERNOTINCHANNEL = 441,
			ERR_NOTONCHANNEL = 442,
			ERR_USERONCHANNEL = 443,
			ERR_NOLOGIN = 444,
			ERR_SUMMONDISABLED = 445,
			ERR_USERSDISABLED = 446,
			ERR_NOTREGISTERED = 451,
			ERR_NEEDMOREPARAMS = 461,
			ERR_ALREADYREGISTRED = 462,
			ERR_NOPERMFORHOST = 463,
			ERR_PASSWDMISMATCH = 464,
			ERR_YOUREBANNEDCREEP = 465,
			ERR_YOUWILLBEBANNED = 466,
			ERR_KEYSET = 467,
			ERR_CHANNELISFULL = 471,
			ERR_UNKNOWNMODE = 472,
			ERR_INVITEONLYCHAN = 473,
			ERR_BANNEDFROMCHAN = 474,
			ERR_BADCHANNELKEY = 475,
			ERR_BADCHANMASK = 476,
			ERR_NOCHANMODES = 477,
			ERR_BANLISTFULL = 478,
			ERR_NOPRIVILEGES = 481,
			ERR_CHANOPRIVSNEEDED = 482,
			ERR_CANTKILLSERVER = 483,
			ERR_RESTRICTED = 484,
			ERR_UNIQOPPRIVSNEEDED = 485,
			ERR_NOOPERHOST = 491,
			ERR_UMODEUNKNOWNFLAG = 501,
			ERR_USERSDONTMATCH = 502,
			ConnectionFailed = 1000,
			IrcServerError = 1001,
			BadDccEndpoint = 1002,
			UnparseableMessage = 1003,
			UnableToResume = 1004,
			UnknownEncryptionProtocol = 1005,
			BadDccAcceptValue = 1006,
			BadResumePosition = 1007,
			DccConnectionRefused = 1008
		}

		#endregion

		public enum ModeAction : int
		{
			Add = 43,                   // +
			Remove = 45                 // -
		};

		public enum UserMode : int
		{
			Away = 97,                  // a
			Wallops = 119,              // w
			Invisible = 105,            // i
			Operator = 111,             // o
			Restricted = 114,           // r
			LocalOperator = 79,         // O
			ServerNotices = 115         // s
		};

		public enum ChannelMode : int
		{
			ChannelCreator = 79,        // O
			ChannelOperator = 111,      // o
			HalfChannelOperator = 104,  // h
			Voice = 118,                // v
			Anonymous = 97,             // a
			InviteOnly = 105,           // i
			Moderated = 109,            // m
			NoOutside = 110,            // n
			Quiet = 113,                // q
			Private = 112,              // p
			Secret = 115,               // s
			ServerReop = 114,           // r
			TopicSettable = 116,        // t
			Password = 107,             // k
			UserLimit = 108,            // l
			Ban = 98,                   // b
			Exception = 101,            // e
			Invitation = 73             // I
		};

		public enum StatsQuery : int
		{
			Connections = 108,          //l
			CommandUsage = 109,         //m
			Operators = 111,            //o
			Uptime = 117,               //u
		};

		public delegate void ReplyHandler(ReplyCode code, string message);
		public delegate void PingHandler(string message);
		public delegate void PublicNoticeHandler(UserInfo user, string channel, string notice);
		public delegate void PrivateNoticeHandler(UserInfo user, string channel, string notice);
		public delegate void JoinHandler(UserInfo user, string channel);
		public delegate void ActionHandler(UserInfo user, string channel, string description);
		public delegate void PrivateActionHandler(UserInfo user, string description);
		public delegate void PublicMessageHandler(UserInfo user, string channel, string message);
		public delegate void PrivateMessageHandler(UserInfo user, string message);
		public delegate void NickHandler(UserInfo user, string newNick);
		public delegate void TopicHandler(UserInfo user, string channel, string newTopic);
		public delegate void PartHandler(UserInfo user, string channel, string reason);
		public delegate void QuitHandler(UserInfo user, string reason);
		public delegate void InviteHandler(UserInfo user, string channel);
		public delegate void KickHandler(UserInfo user, string channel, string kickee, string reason);
		public delegate void ChannelModeChangeHandler(UserInfo who, string channel, ChannelModeInfo[] modes);
		public delegate void UserModeChangeHandler(ModeAction action, UserMode mode);
		public delegate void KillHandler(UserInfo user, string nick, string reason);
		public delegate void RegisteredHandler();
		public delegate void MotdHandler(string message, bool last);
		public delegate void IsonHandler(string nicks);
		public delegate void NamesHandler(string channel, string[] nicks, bool last);
		public delegate void ListHandler(string channel, int visibleNickCount, string topic, bool last);
		public delegate void TopicRequestHandler(string channel, string topic);
		public delegate void InviteSentHandler(string nick, string channel);
		public delegate void AwayHandler(string nick, string awayMessage);
		public delegate void WhoHandler(UserInfo user, string channel, string ircServer, string mask, int hopCount, string realName, bool last);
		public delegate void WhoisHandler(WhoisInfo whoisInfo);
		public delegate void WhowasHandler(UserInfo user, string realName, bool last);
		public delegate void UserModeRequestHandler(UserMode[] modes);
		public delegate void ChannelModeRequestHandler(string channel, ChannelModeInfo[] modes);
		public delegate void ChannelListHandler(string channel, ChannelMode mode, string item, UserInfo who, long whenSet, bool last);
		public delegate void VersionHandler(string versionInfo);
		public delegate void TimeHandler(string time);
		public delegate void InfoHandler(string message, bool last);
		public delegate void AdminHandler(string message);
		public delegate void LusersHandler(string message);
		public delegate void LinksHandler(string mask, string hostname, int hopCount, string serverInfo, bool done);
		public delegate void StatsHandler(StatsQuery queryType, string message, bool done);
		public delegate void NickErrorHandler(string badNick, string reason);
		public delegate void ErrorHandler(ReplyCode code, string message);

		private const string PING = "PING";
		private const string ERROR = "ERROR";
		private const string NOTICE = "NOTICE";
		private const string JOIN = "JOIN";
		private const string PRIVMSG = "PRIVMSG";
		private const string NICK = "NICK";
		private const string TOPIC = "TOPIC";
		private const string PART = "PART";
		private const string QUIT = "QUIT";
		private const string INVITE = "INVITE";
		private const string KICK = "KICK";
		private const string MODE = "MODE";
		private const string KILL = "KILL";
		private const string ACTION = "\u0001ACTION";
		private const string CHAR_COLOR = "\u0003";
		private const string CHAR_BOLD = "\u0002";
		private const string CHAR_CTCP = "\u0001";
		private const string CHAR_REVERSE = "\u0016";
		private const string CHAR_UNDERLINE = "\u001f";
		private const string REGEX_COLOR = "(" + CHAR_COLOR + ")(\\d+)(,\\d+)?";
		private const string REGEX_BOLD = "(" + CHAR_BOLD + "+)?";
		private const string REGEX_SPECIAL = "\\[\\]\\`_\\^\\{\\|\\}";
		private const string REGEX_USERPATTERN = "([\\w\\-" + REGEX_SPECIAL + "]+![\\~\\w]+@[\\w\\.\\-]+)";
		private const string REGEX_CHANNELPATTERN = "([#!+&]\\w+)";
		private const string REGEX_REPLY = "^:([^\\s]*) ([\\d]{3}) ([^\\s]*) (.*)";

		private Regex userPattern;
		private Regex channelPattern;
		private Regex replyRegex;

		private Dictionary<string, WhoisInfo> whoisInfos;

		private string m_ircServer = null;
		private int m_ircPort = 6667;
		private UserInfo m_user = null;

		private Thread m_thread = null;

		private TcpClient m_ircConnection = null;
		private NetworkStream m_ircStream = null;

		private StreamReader m_ircReader = null;
		private StreamWriter m_ircWriter = null;

		public event ReplyHandler OnReply;
		public event PingHandler OnPing;
		public event PublicNoticeHandler OnPublicNotice;
		public event PrivateNoticeHandler OnPrivateNotice;
		public event JoinHandler OnJoin;
		public event ActionHandler OnAction;
		public event PrivateActionHandler OnPrivateAction;
		public event PrivateMessageHandler OnPrivate;
		public event PublicMessageHandler OnPublic;
		public event NickHandler OnNick;
		public event TopicHandler OnTopicChanged;
		public event PartHandler OnPart;
		public event QuitHandler OnQuit;
		public event InviteHandler OnInvite;
		public event KickHandler OnKick;
		public event ChannelModeChangeHandler OnChannelModeChange;
		public event UserModeChangeHandler OnUserModeChange;
		public event KillHandler OnKill;
		public event RegisteredHandler OnRegistered;
		public event MotdHandler OnMotd;
		public event IsonHandler OnIson;
		public event NamesHandler OnNames;
		public event ListHandler OnList;
		public event TopicRequestHandler OnTopicRequest;
		public event InviteSentHandler OnInviteSent;
		public event AwayHandler OnAway;
		public event WhoHandler OnWho;
		public event WhoisHandler OnWhois;
		public event WhowasHandler OnWhowas;
		public event UserModeRequestHandler OnUserModeRequest;
		public event ChannelModeRequestHandler OnChannelModeRequest;
		public event ChannelListHandler OnChannelList;
		public event VersionHandler OnVersion;
		public event TimeHandler OnTime;
		public event InfoHandler OnInfo;
		public event AdminHandler OnAdmin;
		public event LusersHandler OnLusers;
		public event LinksHandler OnLinks;
		public event StatsHandler OnStats;
		public event ErrorHandler OnError;
		public event NickErrorHandler OnNickError;

		public static Color[] WindowsColors =
        {
            Color.White,
            Color.Black,
            Color.DarkBlue,
            Color.DarkGreen,
            Color.Red,
            Color.Maroon,
            Color.Purple,
            Color.Orange,
            Color.Yellow,
            Color.LightGreen,
            Color.CadetBlue,
            Color.Aqua,
            Color.Blue,
            Color.Pink,
            Color.DarkGray,
            Color.Gray,
        };

		public enum IrcColors
		{
			White = 0,
			Black,
			DarkBlue,
			DarkGreen,
			Red,
			Maroon,
			Purple,
			Orange,
			Yellow,
			Green,
			BlueGreen,
			Aqua,
			Blue,
			Pink,
			DarkGray,
			Gray,
		}

		public string[] IRCColorNames =
        {
            "White",
            "Black",
            "DarkBlue",
            "DarkGreen",
            "Red",
            "Maroon",
            "Purple",
            "Orange",
            "Yellow",
            "Green",
            "BlueGreen",
            "Aqua",
            "Blue",
            "Pink",
            "DarkGray",
            "Gray"
        };

		public string[] WindowsColorNames =
        {
            "White",
            "Black",
            "DarkBlue",
            "DarkGreen",
            "Red",
            "Maroon",
            "Purple",
            "Orange",
            "Yellow",
            "LightGreen",
            "CadetBlue",
            "Aqua",
            "Blue",
            "Pink",
            "DarkGray",
            "Gray"
        };

		public IRCManager(string NickName, string UserName, string RealName, bool IsInvisible)
		{
			userPattern = new Regex(REGEX_USERPATTERN, RegexOptions.Compiled | RegexOptions.Singleline);
			channelPattern = new Regex(REGEX_CHANNELPATTERN, RegexOptions.Compiled | RegexOptions.Singleline);
			replyRegex = new Regex(REGEX_REPLY, RegexOptions.Compiled | RegexOptions.Singleline);

			m_user = new UserInfo(NickName, UserName, RealName, IsInvisible);
		}

		public void Connect(string IrcServer, int IrcPort)
		{
			try
			{
				m_ircServer = IrcServer;
				m_ircPort = IrcPort;

				// Connect with the IRC server.
				m_ircConnection = new TcpClient(m_ircServer, m_ircPort);
				m_ircStream = m_ircConnection.GetStream();
				m_ircReader = new StreamReader(m_ircStream);
				m_ircWriter = new StreamWriter(m_ircStream);

				Authenticate();

				Thread m_thread = new Thread(new ThreadStart(ReadStreamThread));
				m_thread.IsBackground = true;
				m_thread.Start();
			}
			catch
			{
				Disconnect();
			}
		}

		public void Disconnect()
		{
			if (m_thread != null)
			{
				m_thread.Abort();
				m_thread = null;
			}
			if (m_ircReader != null)
			{
				m_ircReader.Close();
				m_ircReader.Dispose();
				m_ircReader = null;
			}
			if (m_ircWriter != null)
			{
				m_ircWriter.Flush();
				m_ircWriter.Close();
				m_ircWriter.Dispose();
				m_ircWriter = null;
			}
			if (m_ircStream != null)
			{
				m_ircStream.Flush();
				m_ircStream.Close();
				m_ircStream.Dispose();
				m_ircStream = null;
			}
			if (m_ircConnection != null)
			{
				m_ircConnection.Close();
				m_ircConnection = null;
			}
		}

		private void ParseIrcLine(string ircLine)
		{
			if (ircLine == null)
				return;

			string[] ircSplit = ircLine.Split(new char[] { ' ' });

			if (ircSplit[0] == PING)
			{
				if (OnPing != null)
				{
					ircSplit[1] = RemoveLeadingColon(ircSplit[1]);

					OnPing(CondenseStrings(ircSplit, 1));
				}
			}
			else if (ircSplit[0] == NOTICE)
			{
				if (OnPrivateNotice != null)
				{
					ircSplit[2] = RemoveLeadingColon(ircSplit[2]);
					OnPrivateNotice(UserInfo.Empty, null, CondenseStrings(ircSplit, 2));
				}
			}
			else if (ircSplit[0] == ERROR)
			{
				ircSplit[1] = RemoveLeadingColon(ircSplit[1]);
				Error(ReplyCode.IrcServerError, CondenseStrings(ircSplit, 1));
			}
			else if (replyRegex.IsMatch(ircLine))
			{
				ParseIrcReply(ircSplit);
			}
			else
			{
				ParseIrcCommand(ircSplit);
			}
		}

		private void ParseIrcCommand(string[] ircSplit)
		{
			//Remove colon user info string
			ircSplit[0] = RemoveLeadingColon(ircSplit[0]);
			switch (ircSplit[1])
			{
				case NOTICE:
					ircSplit[3] = RemoveLeadingColon(ircSplit[3]);
					if (Util.IsValidChannelName(ircSplit[2]))
					{
						if (OnPublicNotice != null)
						{
							OnPublicNotice(Util.UserInfoFromString(ircSplit[0]), ircSplit[2], CondenseStrings(ircSplit, 3));
						}
					}
					else
					{
						if (OnPrivateNotice != null)
						{
							OnPrivateNotice(Util.UserInfoFromString(ircSplit[0]), null, CondenseStrings(ircSplit, 3));
						}
					}
					break;
				case JOIN:
					if (OnJoin != null)
					{
						OnJoin(Util.UserInfoFromString(ircSplit[0]), RemoveLeadingColon(ircSplit[2]));
					}
					break;
				case PRIVMSG:
					ircSplit[3] = RemoveLeadingColon(ircSplit[3]);
					if (ircSplit[3] == ACTION)
					{
						if (Util.IsValidChannelName(ircSplit[2]))
						{
							if (OnAction != null)
							{
								int last = ircSplit.Length - 1;
								ircSplit[last] = RemoveTrailingQuote(ircSplit[last]);
								OnAction(Util.UserInfoFromString(ircSplit[0]), ircSplit[2], CondenseStrings(ircSplit, 4));
							}
						}
						else
						{
							if (OnPrivateAction != null)
							{
								int last = ircSplit.Length - 1;
								ircSplit[last] = RemoveTrailingQuote(ircSplit[last]);
								OnPrivateAction(Util.UserInfoFromString(ircSplit[0]), CondenseStrings(ircSplit, 4));
							}
						}
					}
					else if (channelPattern.IsMatch(ircSplit[2]))
					{
						if (OnPublic != null)
						{
							OnPublic(Util.UserInfoFromString(ircSplit[0]), ircSplit[2], CondenseStrings(ircSplit, 3));
						}
					}
					else
					{
						if (OnPrivate != null)
						{
							OnPrivate(Util.UserInfoFromString(ircSplit[0]), CondenseStrings(ircSplit, 3));
						}
					}
					break;
				case NICK:
					if (OnNick != null)
					{
						OnNick(Util.UserInfoFromString(ircSplit[0]), RemoveLeadingColon(ircSplit[2]));
					}
					break;
				case TOPIC:
					if (OnTopicChanged != null)
					{
						ircSplit[3] = RemoveLeadingColon(ircSplit[3]);
						OnTopicChanged(Util.UserInfoFromString(ircSplit[0]), ircSplit[2], CondenseStrings(ircSplit, 3));
					}
					break;
				case PART:
					if (OnPart != null)
					{
						OnPart(Util.UserInfoFromString(ircSplit[0]), ircSplit[2], ircSplit.Length >= 4 ? RemoveLeadingColon(CondenseStrings(ircSplit, 3)) : "");
					}
					break;
				case QUIT:
					if (OnQuit != null)
					{
						ircSplit[2] = RemoveLeadingColon(ircSplit[2]);
						OnQuit(Util.UserInfoFromString(ircSplit[0]), CondenseStrings(ircSplit, 2));
					}
					break;
				case INVITE:
					if (OnInvite != null)
					{
						OnInvite(Util.UserInfoFromString(ircSplit[0]), RemoveLeadingColon(ircSplit[3]));
					}
					break;
				case KICK:
					if (OnKick != null)
					{
						ircSplit[4] = RemoveLeadingColon(ircSplit[4]);
						OnKick(Util.UserInfoFromString(ircSplit[0]), ircSplit[2], ircSplit[3], CondenseStrings(ircSplit, 4));
					}
					break;
				case MODE:
					if (channelPattern.IsMatch(ircSplit[2]))
					{
						if (OnChannelModeChange != null)
						{
							UserInfo who = Util.UserInfoFromString(ircSplit[0]);
							try
							{
								ChannelModeInfo[] modes = ChannelModeInfo.ParseModes(ircSplit, 3);
								OnChannelModeChange(who, ircSplit[2], modes);
							}
							catch (Exception)
							{
								if (OnError != null)
								{
									OnError(ReplyCode.UnparseableMessage, CondenseStrings(ircSplit, 0));
								}
								//System.Diagnostics.Debug.WriteLineIf(IrcTrace.TraceWarning, "[" + Thread.CurrentThread.Name + "] Listener::ParseCommand() Bad IRC MODE string=" + ircSplit[0]);
							}
						}
					}
					else
					{
						if (OnUserModeChange != null)
						{
							ircSplit[3] = RemoveLeadingColon(ircSplit[3]);
							OnUserModeChange(Util.CharToModeAction(ircSplit[3][0]), Util.CharToUserMode(ircSplit[3][1]));
						}
					}
					break;
				case KILL:
					if (OnKill != null)
					{
						string reason = "";
						if (ircSplit.Length >= 4)
						{
							ircSplit[3] = RemoveLeadingColon(ircSplit[3]);
							reason = CondenseStrings(ircSplit, 3);
						}
						OnKill(Util.UserInfoFromString(ircSplit[0]), ircSplit[2], reason);
					}
					break;
				default:
					if (OnError != null)
					{
						OnError(ReplyCode.UnparseableMessage, CondenseStrings(ircSplit, 0));
					}
					//System.Diagnostics.Debug.WriteLineIf(IrcTrace.TraceWarning, "[" + Thread.CurrentThread.Name + "] Listener::ParseCommand() Unknown IRC command=" + ircSplit[1]);
					break;
			}
		}
		private void ParseIrcReply(string[] ircSplit)
		{
			ReplyCode code = (ReplyCode)int.Parse(ircSplit[1], CultureInfo.InvariantCulture);
			ircSplit[3] = RemoveLeadingColon(ircSplit[3]);
			switch (code)
			{
				//Messages sent upon successful registration 
				case ReplyCode.RPL_WELCOME:
				case ReplyCode.RPL_YOURESERVICE:
					if (OnRegistered != null)
					{
						OnRegistered();
					}
					break;
				case ReplyCode.RPL_MOTDSTART:
				case ReplyCode.RPL_MOTD:
					if (OnMotd != null)
					{
						OnMotd(CondenseStrings(ircSplit, 3), false);
					}
					break;
				case ReplyCode.RPL_ENDOFMOTD:
					if (OnMotd != null)
					{
						OnMotd(CondenseStrings(ircSplit, 3), true);
					}
					break;
				case ReplyCode.RPL_ISON:
					if (OnIson != null)
					{
						OnIson(ircSplit[3]);
					}
					break;
				case ReplyCode.RPL_NAMREPLY:
					if (OnNames != null)
					{
						ircSplit[5] = RemoveLeadingColon(ircSplit[5]);
						int numberOfUsers = ircSplit.Length - 5;
						string[] users = new string[numberOfUsers];
						Array.Copy(ircSplit, 5, users, 0, numberOfUsers);
						OnNames(ircSplit[4], users, false);
					}
					break;
				case ReplyCode.RPL_ENDOFNAMES:
					if (OnNames != null)
					{
						OnNames(ircSplit[3], new string[0], true);
					}
					break;
				case ReplyCode.RPL_LIST:
					if (OnList != null)
					{
						ircSplit[5] = RemoveLeadingColon(ircSplit[5]);
						OnList(ircSplit[3], int.Parse(ircSplit[4], CultureInfo.InvariantCulture), CondenseStrings(ircSplit, 5),
							false);
					}
					break;
				case ReplyCode.RPL_LISTEND:
					if (OnList != null)
					{
						OnList("", 0, "", true);
					}
					break;
				case ReplyCode.ERR_NICKNAMEINUSE:
				case ReplyCode.ERR_NICKCOLLISION:
					if (OnNickError != null)
					{
						ircSplit[4] = RemoveLeadingColon(ircSplit[4]);
						OnNickError(ircSplit[3], CondenseStrings(ircSplit, 4));
					}
					break;
				case ReplyCode.RPL_NOTOPIC:
					if (OnError != null)
					{
						OnError(code, CondenseStrings(ircSplit, 3));
					}
					break;
				case ReplyCode.RPL_TOPIC:
					if (OnTopicRequest != null)
					{
						ircSplit[4] = RemoveLeadingColon(ircSplit[4]);
						OnTopicRequest(ircSplit[3], CondenseStrings(ircSplit, 4));
					}
					break;
				case ReplyCode.RPL_INVITING:
					if (OnInviteSent != null)
					{
						OnInviteSent(ircSplit[3], ircSplit[4]);
					}
					break;
				case ReplyCode.RPL_AWAY:
					if (OnAway != null)
					{
						OnAway(ircSplit[3], RemoveLeadingColon(CondenseStrings(ircSplit, 4)));
					}
					break;
				case ReplyCode.RPL_WHOREPLY:
					if (OnWho != null)
					{
						UserInfo user = new UserInfo(ircSplit[7], ircSplit[4], ircSplit[5]);
						OnWho(user, ircSplit[3], ircSplit[6], ircSplit[8], int.Parse(RemoveLeadingColon(ircSplit[9]), CultureInfo.InvariantCulture), ircSplit[10], false);
					}
					break;
				case ReplyCode.RPL_ENDOFWHO:
					if (OnWho != null)
					{
						OnWho(UserInfo.Empty, "", "", "", 0, "", true);
					}
					break;
				case ReplyCode.RPL_WHOISUSER:
					UserInfo whoUser = new UserInfo(ircSplit[3], ircSplit[4], ircSplit[5]);
					WhoisInfo whoisInfo = LookupInfo(whoUser.NickName);
					whoisInfo.UserInfo = whoUser;
					ircSplit[7] = RemoveLeadingColon(ircSplit[7]);
					whoisInfo.RealName = CondenseStrings(ircSplit, 7);
					break;
				case ReplyCode.RPL_WHOISCHANNELS:
					WhoisInfo whoisChannelInfo = LookupInfo(ircSplit[3]);
					ircSplit[4] = RemoveLeadingColon(ircSplit[4]);
					int numberOfChannels = ircSplit.Length - 4;
					string[] channels = new String[numberOfChannels];
					Array.Copy(ircSplit, 4, channels, 0, numberOfChannels);
					whoisChannelInfo.SetChannels(channels);
					break;
				case ReplyCode.RPL_WHOISSERVER:
					WhoisInfo whoisServerInfo = LookupInfo(ircSplit[3]);
					whoisServerInfo.IrcServer = ircSplit[4];
					ircSplit[5] = RemoveLeadingColon(ircSplit[5]);
					whoisServerInfo.ServerDescription = CondenseStrings(ircSplit, 5);
					break;
				case ReplyCode.RPL_WHOISOPERATOR:
					WhoisInfo whoisOpInfo = LookupInfo(ircSplit[3]);
					whoisOpInfo.IsOperator = true;
					break;
				case ReplyCode.RPL_WHOISIDLE:
					WhoisInfo whoisIdleInfo = LookupInfo(ircSplit[3]);
					whoisIdleInfo.IdleTime = long.Parse(ircSplit[5], CultureInfo.InvariantCulture);
					break;
				case ReplyCode.RPL_ENDOFWHOIS:
					string nick = ircSplit[3];
					WhoisInfo whoisEndInfo = LookupInfo(nick);
					if (OnWhois != null)
					{
						OnWhois(whoisEndInfo);
					}
					whoisInfos.Remove(nick);
					break;
				case ReplyCode.RPL_WHOWASUSER:
					if (OnWhowas != null)
					{
						UserInfo whoWasUser = new UserInfo(ircSplit[3], ircSplit[4], ircSplit[5]);
						ircSplit[7] = RemoveLeadingColon(ircSplit[7]);
						OnWhowas(whoWasUser, CondenseStrings(ircSplit, 7), false);
					}
					break;
				case ReplyCode.RPL_ENDOFWHOWAS:
					if (OnWhowas != null)
					{
						OnWhowas(UserInfo.Empty, "", true);
					}
					break;
				case ReplyCode.RPL_UMODEIS:
					if (OnUserModeRequest != null)
					{
						//First drop the '+'
						string chars = ircSplit[3].Substring(1);
						UserMode[] modes = Util.UserModesToArray(chars);
						OnUserModeRequest(modes);
					}
					break;
				case ReplyCode.RPL_CHANNELMODEIS:
					if (OnChannelModeRequest != null)
					{
						try
						{
							ChannelModeInfo[] modes = ChannelModeInfo.ParseModes(ircSplit, 4);
							OnChannelModeRequest(ircSplit[3], modes);
						}
						catch (Exception)
						{
							if (OnError != null)
							{
								OnError(ReplyCode.UnparseableMessage, CondenseStrings(ircSplit, 0));
							}
							//System.Diagnostics.Debug.WriteLineIf(IrcTrace.TraceWarning, "[" + Thread.CurrentThread.Name + "] Listener::ParseReply() Bad IRC MODE string=" + ircSplit[0]);
						}
					}
					break;
				case ReplyCode.RPL_BANLIST:
					if (OnChannelList != null)
					{
						OnChannelList(ircSplit[3], ChannelMode.Ban, ircSplit[4], Util.UserInfoFromString(ircSplit[5]), System.Convert.ToInt64(ircSplit[6], CultureInfo.InvariantCulture), false);
					}
					break;
				case ReplyCode.RPL_ENDOFBANLIST:
					if (OnChannelList != null)
					{
						OnChannelList(ircSplit[3], ChannelMode.Ban, "", UserInfo.Empty, 0, true);
					}
					break;
				case ReplyCode.RPL_INVITELIST:
					if (OnChannelList != null)
					{
						OnChannelList(ircSplit[3], ChannelMode.Invitation, ircSplit[4], Util.UserInfoFromString(ircSplit[5]), System.Convert.ToInt64(ircSplit[6]), false);
					}
					break;
				case ReplyCode.RPL_ENDOFINVITELIST:
					if (OnChannelList != null)
					{
						OnChannelList(ircSplit[3], ChannelMode.Invitation, "", UserInfo.Empty, 0, true);
					}
					break;
				case ReplyCode.RPL_EXCEPTLIST:
					if (OnChannelList != null)
					{
						OnChannelList(ircSplit[3], ChannelMode.Exception, ircSplit[4], Util.UserInfoFromString(ircSplit[5]), System.Convert.ToInt64(ircSplit[6]), false);
					}
					break;
				case ReplyCode.RPL_ENDOFEXCEPTLIST:
					if (OnChannelList != null)
					{
						OnChannelList(ircSplit[3], ChannelMode.Exception, "", UserInfo.Empty, 0, true);
					}
					break;
				case ReplyCode.RPL_UNIQOPIS:
					if (OnChannelList != null)
					{
						OnChannelList(ircSplit[3], ChannelMode.ChannelCreator, ircSplit[4], UserInfo.Empty, 0, true);
					}
					break;
				case ReplyCode.RPL_VERSION:
					if (OnVersion != null)
					{
						OnVersion(CondenseStrings(ircSplit, 3));
					}
					break;
				case ReplyCode.RPL_TIME:
					if (OnTime != null)
					{
						OnTime(CondenseStrings(ircSplit, 3));
					}
					break;
				case ReplyCode.RPL_INFO:
					if (OnInfo != null)
					{
						OnInfo(CondenseStrings(ircSplit, 3), false);
					}
					break;
				case ReplyCode.RPL_ENDOFINFO:
					if (OnInfo != null)
					{
						OnInfo(CondenseStrings(ircSplit, 3), true);
					}
					break;
				case ReplyCode.RPL_ADMINME:
				case ReplyCode.RPL_ADMINLOC1:
				case ReplyCode.RPL_ADMINLOC2:
				case ReplyCode.RPL_ADMINEMAIL:
					if (OnAdmin != null)
					{
						OnAdmin(RemoveLeadingColon(CondenseStrings(ircSplit, 3)));
					}
					break;
				case ReplyCode.RPL_LUSERCLIENT:
				case ReplyCode.RPL_LUSEROP:
				case ReplyCode.RPL_LUSERUNKNOWN:
				case ReplyCode.RPL_LUSERCHANNELS:
				case ReplyCode.RPL_LUSERME:
					if (OnLusers != null)
					{
						OnLusers(RemoveLeadingColon(CondenseStrings(ircSplit, 3)));
					}
					break;
				case ReplyCode.RPL_LINKS:
					if (OnLinks != null)
					{
						OnLinks(ircSplit[3], //mask
									ircSplit[4], //hostname
									int.Parse(RemoveLeadingColon(ircSplit[5]), CultureInfo.InvariantCulture), //hopcount
									CondenseStrings(ircSplit, 6), false);
					}
					break;
				case ReplyCode.RPL_ENDOFLINKS:
					if (OnLinks != null)
					{
						OnLinks(String.Empty, String.Empty, -1, String.Empty, true);
					}
					break;
				case ReplyCode.RPL_STATSLINKINFO:
				case ReplyCode.RPL_STATSCOMMANDS:
				case ReplyCode.RPL_STATSUPTIME:
				case ReplyCode.RPL_STATSOLINE:
					if (OnStats != null)
					{
						OnStats(GetQueryType(code), RemoveLeadingColon(CondenseStrings(ircSplit, 3)), false);
					}
					break;
				case ReplyCode.RPL_ENDOFSTATS:
					if (OnStats != null)
					{
						OnStats(Util.CharToStatsQuery(ircSplit[3][0]), RemoveLeadingColon(CondenseStrings(ircSplit, 4)), true);
					}
					break;
				default:
					HandleDefaultReply(code, ircSplit);
					break;
			}
		}

		public void SendLine(string line)
		{
			WriteToStream(line);
		}

		public void Pong(string message)
		{
			WriteToStream("PONG " + message);
		}

		//:HeadKaze!headkaze@vw12891.iinet.net.au PRIVMSG #IRCTest :This is a test!!!
		public string SendPrivMsg(string NickName, string Msg)
		{
			if (Msg == String.Empty)
				return null;

			if (Msg.Length + NickName.Length + 10 > 468)
				Msg = Msg.Substring(0, 468 - NickName.Length - 10);

			if (!WriteToStream(String.Format("PRIVMSG {0} :{1}", NickName, Msg)))
				return null;

			return Msg;
		}

		//:HeadKaze!headkaze@vw12891.iinet.net.au PART #IRCTest
		public void PartChannel(string Channel)
		{
			WriteToStream(String.Format("PART {0}", Channel));
		}

		public void Authenticate()
		{
			// Authenticate our user
			string isInvisible = m_user.IsInvisible ? "8" : "0";

			WriteToStream(String.Format("USER {0} {1} * :{2}", m_user.UserName, isInvisible, m_user.RealName));
			WriteToStream(String.Format("NICK {0}", m_user.NickName));
		}

		public void JoinChannel(string channel)
		{
			WriteToStream(String.Format("JOIN {0}", channel));
		}

		public void GetColorCodes(ref string str, out int foreColor, out int backColor)
		{
			int i = 0;
			string foreColorStr = "";
			string backColorStr = "";

			foreColor = -1;
			backColor = -1;

			for (i = 0; i < str.Length; i++)
			{
				if (Char.IsNumber(str[i]))
					foreColorStr += str[i];
				else
					break;
			}

			if (str[i] == ',')
			{
				for (i = i + 1; i < str.Length; i++)
				{
					if (Char.IsNumber(str[i]))
						backColorStr += str[i];
					else
						break;
				}
			}

			str = str.Substring(i);

			if (foreColorStr.Length > 0)
				foreColor = System.Convert.ToInt32(foreColorStr);

			if (backColorStr.Length > 0)
				backColor = System.Convert.ToInt32(backColorStr);

			//System.Diagnostics.Debug.WriteLine(str + ":" + foreColor + ":" + backColor);
		}

		public string ProcessLine(string line)
		{
			string[] strSplit = StringTools.SplitString(line, new string[] { CHAR_COLOR, CHAR_BOLD, CHAR_CTCP, CHAR_REVERSE, CHAR_UNDERLINE });

			for (int i = 0; i < strSplit.Length; i++)
			{
				if (strSplit[i] == CHAR_COLOR)
				{
					int foreColor = -1, backColor = -1;
					if (i + 1 < strSplit.Length)
						GetColorCodes(ref strSplit[i + 1], out foreColor, out backColor);

					if (foreColor >= 0 && foreColor < 16)
						strSplit[i] = "[" + GetWindowsColor((IrcColors)foreColor).Name.ToUpper() + "]";

					if (backColor >= 0 && backColor < 16)
						strSplit[i] = "[" + GetWindowsColor((IrcColors)backColor).Name.ToLower() + "]";
				}
				if (strSplit[i] == CHAR_BOLD)
					strSplit[i] = "[BOLD]";
				if (strSplit[i] == CHAR_CTCP)
					strSplit[i] = "[CTCP]";
				if (strSplit[i] == CHAR_REVERSE)
					strSplit[i] = "[REVERSE]";
				if (strSplit[i] == CHAR_UNDERLINE)
					strSplit[i] = "[UNDERLINE]";
			}

			return String.Join("", strSplit);
		}

		public Color GetWindowsColor(IrcColors ircColor)
		{
			return WindowsColors[(int)ircColor];
		}

		public IrcColors GetIrcColor(Color clr)
		{
			switch (clr.ToKnownColor())
			{
				case KnownColor.Aqua:
					return IrcColors.Aqua;
				case KnownColor.Black:
					return IrcColors.Black;
				case KnownColor.Blue:
					return IrcColors.Blue;
				case KnownColor.CadetBlue:
					return IrcColors.BlueGreen;
				case KnownColor.DarkBlue:
					return IrcColors.DarkBlue;
				case KnownColor.DarkGray:
					return IrcColors.DarkGray;
				case KnownColor.DarkGreen:
					return IrcColors.DarkGreen;
				case KnownColor.Gray:
					return IrcColors.Gray;
				case KnownColor.LightGreen:
					return IrcColors.Green;
				case KnownColor.Maroon:
					return IrcColors.Maroon;
				case KnownColor.Orange:
					return IrcColors.Orange;
				case KnownColor.Pink:
					return IrcColors.Pink;
				case KnownColor.Purple:
					return IrcColors.Purple;
				case KnownColor.Red:
					return IrcColors.Red;
				case KnownColor.White:
					return IrcColors.White;
				case KnownColor.Yellow:
					return IrcColors.Yellow;
				default:
					return IrcColors.White;
			}
		}

		private string ProcessColorCodes(string line)
		{
			for (int i = 0; i < WindowsColorNames.Length; i++)
				line = line.Replace("[" + WindowsColorNames[i].ToUpper() + "]", CHAR_COLOR + String.Format("{0:D2}", i));

			return line;
		}

		private bool WriteToStream(string line)
		{
			if (m_ircWriter == null)
				return false;

			try
			{
				System.Diagnostics.Debug.WriteLine(line);
				line = ProcessColorCodes(line);
				m_ircWriter.WriteLine(line);
				m_ircWriter.Flush();
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("WriteToStream", "IRCManager", ex.Message, ex.StackTrace);

				return false;
			}

			return true;
		}

		private void ReadStreamThread()
		{
			try
			{
				while (true)
				{

					if (m_ircReader.Peek() != 0)
					{
						string Line = m_ircReader.ReadLine();

						if (Line != null)
							ParseIrcLine(Line);
					}

					System.Threading.Thread.Sleep(100);
				}
			}
			catch (Exception)
			{
				//LogFile.WriteLine("ReadStreamThread", "IRCManager", ex.Message, ex.StackTrace);
			}
		}

		private void Error(ReplyCode code, string message)
		{
			if (OnError != null)
			{
				OnError(code, message);
			}
		}

		private void HandleDefaultReply(ReplyCode code, string[] tokens)
		{
			if (code >= ReplyCode.ERR_NOSUCHNICK && code <= ReplyCode.ERR_USERSDONTMATCH)
			{
				if (OnError != null)
				{
					OnError(code, CondenseStrings(tokens, 3));
				}
			}
			else if (OnReply != null)
			{
				OnReply(code, CondenseStrings(tokens, 3));
			}
		}

		private WhoisInfo LookupInfo(string nick)
		{
			if (whoisInfos == null)
			{
				whoisInfos = new Dictionary<string, WhoisInfo>();
			}
			WhoisInfo info = (WhoisInfo)whoisInfos[nick];
			if (info == null)
			{
				info = new WhoisInfo();
				whoisInfos[nick] = info;
			}
			return info;
		}

		private string CondenseStrings(string[] strings, int start)
		{
			if (strings.Length == start + 1)
			{
				return strings[start];
			}
			else
			{
				return String.Join(" ", strings, start, (strings.Length - start));
			}
		}
		private string RemoveLeadingColon(string text)
		{
			if (text[0] == ':')
			{
				return text.Substring(1);
			}
			return text;
		}

		private string RemoveTrailingQuote(string text)
		{
			return text.Substring(0, text.Length - 1);
		}

		private StatsQuery GetQueryType(ReplyCode code)
		{
			switch (code)
			{
				case ReplyCode.RPL_STATSLINKINFO:
					return StatsQuery.Connections;
				case ReplyCode.RPL_STATSCOMMANDS:
					return StatsQuery.CommandUsage;
				case ReplyCode.RPL_STATSUPTIME:
					return StatsQuery.Uptime;
				case ReplyCode.RPL_STATSOLINE:
					return StatsQuery.Operators;
				//Should never get here
				default:
					return StatsQuery.CommandUsage;
			}
		}

		public string IrcServer
		{
			get { return m_ircServer; }
			set { m_ircServer = value; }
		}

		public int IrcPort
		{
			get { return m_ircPort; }
			set { m_ircPort = value; }
		}

		public UserInfo User
		{
			get { return m_user; }
			set { m_user = value; }
		}

		public TcpClient IrcConnection
		{
			get { return m_ircConnection; }
			set { m_ircConnection = value; }
		}

		#region IDisposable Members

		public void Dispose()
		{
			if (m_thread != null)
			{
				m_thread.Abort();
				m_thread = null;
			}
		}

		#endregion
	}
}
