// Copyright (c) 2015, Ben Baker
// All rights reserved.
// 
// This source code is licensed under the BSD-style license found in the
// LICENSE file in the root directory of this source tree. 

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Drawing;
using System.Runtime.InteropServices;
using System.IO;

namespace CPWizard
{
	public enum MAMEStatusType
	{
		None,
		Good,
		Bad,
		Best
	};

	[Flags]
	public enum DeviceFeature
	{
		None = 0,
		Protection = (1 << 0),
		Palette = (1 << 1),
		Graphics = (1 << 2),
		Sound = (1 << 3),
		Controls = (1 << 4),
		Keyboard = (1 << 5),
		Mouse = (1 << 6),
		Microphone = (1 << 7),
		Camera = (1 << 8),
		Disk = (1 << 9),
		Print = (1 << 10),
		LAN = (1 << 11),
		WAN = (1 << 12),
		All = 0x1FFF
	};

	public class MAMEMachineNode : IComparable<MAMEMachineNode>
	{
		public MAMEMachineNode Parent = null;
		public MAMEMachineNode Clone = null;
		public MAMEMachineNode ROM = null;
		public MAMEMachineNode Sample = null;
		public string Name = null;
		public string SourceFile = null;
		public string CloneOf = null;
		public string ROMOf = null;
		public string SampleOf = null;
		public bool IsBios = false;
		public bool IsMechanical = false;
		public bool IsDevice = false;
		public bool Runnable = true;
		public string Description = null;
		public string Year = null;
		public string Manufacturer = null;
		public MAMEDriverNode Driver = null;
		public List<MAMEROMNode> ROMList = null;
		public List<MAMEDeviceRefNode> DeviceRefList = null;
		public List<MAMEChipNode> ChipList = null;
		public List<MAMEDisplayNode> DisplayList = null;
		public List<MAMEInputNode> InputList = null;
		public List<MAMEDipSwitchNode> DipSwitchList = null;
		public List<MAMEDeviceNode> DeviceList = null;
		public List<MAMESoftwareListNode> SoftwareList = null;
		public ControlsDatNode ControlsDat = null;
		public CatVerNode CatVer = null;
		public NPlayersNode NPlayers = null;
		public HallOfFameNode HallOfFame = null;
		//public List<HistoryDatBiographyNode> HistoryDatList = null;
		//public MAMEInfoDatNode MAMEInfoDat = null;
		//public CommandDatNode CommandDat = null;
		//public StoryDatNode StoryDat = null;
		//public AAMARating AAMARating = AAMARating.None;
		public MAMEStatusType Status = MAMEStatusType.None;
		//public DeviceFeature UnEmulatedFeatures = DeviceFeature.None;
		//public DeviceFeature ImperfectFeatures = DeviceFeature.None;

		public MAMEMachineNode()
		{
			ROMList = new List<MAMEROMNode>();
			DeviceRefList = new List<MAMEDeviceRefNode>();
			ChipList = new List<MAMEChipNode>();
			DisplayList = new List<MAMEDisplayNode>();
			InputList = new List<MAMEInputNode>();
			DipSwitchList = new List<MAMEDipSwitchNode>();
			DeviceList = new List<MAMEDeviceNode>();
			SoftwareList = new List<MAMESoftwareListNode>();
			/* Driver = new MAMEDriverNode(MAMEDriverStatus.Good, MAMEDriverStatus.Good, MAMEDriverStatus.Good, MAMEDriverStatus.Good, MAMEDriverStatus.Good, MAMEDriverStatus.Good, MAMEDriverStatus.Good, MAMESaveState.None, 0);

			ROMList.Add(new MAMEROMNode("", 0, ""));
			DeviceRefList.Add(new MAMEDeviceRefNode(""));
			ChipList.Add(new MAMEChipNode("", "", ""));
			DisplayList.Add(new MAMEDisplayNode("", 0, 0, 0, 0));
			InputList.Add(new MAMEInputNode(0, 0, 0, false, false));
			DipSwitchList.Add(new MAMEDipSwitchNode(""));
			DeviceList.Add(new MAMEDeviceNode("", "", "", ""));
			SoftwareList.Add(new MAMESoftwareListNode("", "")); */
		}

		public MAMEMachineNode(string name, string sourceFile, string cloneOf, string romOf, string sampleOf, bool isBios, bool isMechanical, bool isDevice, bool runnable)
		{
			Name = name;
			SourceFile = sourceFile;
			CloneOf = cloneOf;
			ROMOf = romOf;
			IsBios = isBios;
			IsMechanical = isMechanical;
			IsDevice = isDevice;
			Runnable = runnable;
			ROMList = new List<MAMEROMNode>();
			DeviceRefList = new List<MAMEDeviceRefNode>();
			ChipList = new List<MAMEChipNode>();
			DisplayList = new List<MAMEDisplayNode>();
			InputList = new List<MAMEInputNode>();
			DipSwitchList = new List<MAMEDipSwitchNode>();
			DeviceList = new List<MAMEDeviceNode>();
			SoftwareList = new List<MAMESoftwareListNode>();
		}

		public string[] GetNameArray()
		{
			List<string> nameList = new List<string>();

			nameList.Add(Name);

			if (CloneOf != null)
				if (!nameList.Contains(CloneOf))
					nameList.Add(CloneOf);

			if (ROMOf != null)
				if (!nameList.Contains(ROMOf))
					nameList.Add(ROMOf);

			if (SampleOf != null)
				if (!nameList.Contains(SampleOf))
					nameList.Add(SampleOf);

			if (Parent != null)
				nameList.Add(Parent.Name);

			return nameList.ToArray();
		}

		public int CompareTo(MAMEMachineNode other)
		{
			if (this.Description == null && other.Description == null)
				return 0;

			if (this.Description == null)
				return -1;

			if (other.Description == null)
				return 1;

			return this.Description.CompareTo(other.Description);
		}

		public bool HasControlsDat { get { return (ControlsDat != null); } }
		public bool IsClone { get { return (CloneOf != null); } }
		public bool IsParent { get { return (Parent == null); } }

		public bool IsVerified
		{
			get
			{
				if (HasControlsDat)
					return (ControlsDat.Verified == 1);
				else
					return false;
			}
		}

		public bool IsVertical
		{
			get
			{
				if (DisplayList.Count > 0)
					return (DisplayList[0].Rotate == 90 || DisplayList[0].Rotate == 270);

				return false;
			}
		}

		public bool IsHorizontal { get { return !IsVertical; } }

		public bool IsSystem
		{
			get
			{
				foreach (MAMEDeviceRefNode deviceRef in DeviceRefList)
					if (deviceRef.Name.Equals("software_list"))
						return true;

				return false;
			}
		}

		public bool IsArcade
		{
			get
			{
				foreach (MAMEInputNode input in InputList)
					if (input.Coins > 0)
						return true;

				return false;
			}
		}

		public bool IsChd
		{
			get
			{
				foreach (MAMEDeviceNode deviceNode in DeviceList)
				{
					foreach (MAMEExtensionNode extensionNode in deviceNode.ExtensionList)
					{
						if (extensionNode.Name.Equals("chd"))
							return true;
					}
				}

				return false;
			}
		}

		public bool IsMahjong
		{
			get
			{
				foreach (MAMEInputNode input in InputList)
					foreach (MAMEControlNode control in input.ControlList)
						if (control.Type.Equals("mahjong") || control.Type.Equals("hanafuda"))
							return true;

				return false;
			}
		}

		public bool IsGambling
		{
			get
			{
				foreach (MAMEInputNode input in InputList)
					foreach (MAMEControlNode control in input.ControlList)
						if (control.Type.Equals("gambling"))
							return true;

				return false;
			}
		}

		public int NumPlayers
		{
			get
			{
				int numPlayers = 0;

				foreach (MAMEInputNode input in InputList)
					numPlayers = Math.Max(numPlayers, input.Players);

				return numPlayers;
			}
		}

		public int NumButtons
		{
			get
			{
				int numButtons = 0;

				foreach (MAMEInputNode input in InputList)
					foreach (MAMEControlNode control in input.ControlList)
						numButtons = Math.Max(numButtons, control.Buttons);

				return numButtons;
			}
		}

		public string GetNameString()
		{
			string retString = String.Empty;

			if (Name != null)
				retString = Name;

			return retString;
		}

		public string GetSourceFileString()
		{
			string retString = String.Empty;

			if (SourceFile != null)
				retString = SourceFile;

			return retString;
		}

		public string GetCloneOfString()
		{
			string retString = String.Empty;

			if (CloneOf != null)
				retString = CloneOf;

			return retString;
		}

		public string GetROMOfString()
		{
			string retString = String.Empty;

			if (ROMOf != null)
				retString = ROMOf;

			return retString;
		}

		public string GetDescriptionString()
		{
			string retString = String.Empty;

			if (Description != null)
				retString = Description;

			return retString;
		}

		public string GetParentString()
		{
			string retString = String.Empty;

			if (Parent != null)
				retString = Parent.Name;

			return retString;
		}

		public string GetOrientationString()
		{
			string retString = String.Empty;

			if (DisplayList.Count > 0)
			{
				if (DisplayList[0].Rotate == 90 || DisplayList[0].Rotate == 270)
					return "Vertical";
				else
					return "Horizontal";
			}

			return retString;
		}

		public string GetRotationString()
		{
			string retString = String.Empty;

			if (DisplayList.Count > 0)
				return DisplayList[0].Rotate.ToString();

			return retString;
		}

		private string[] GetInputConstantsArray()
		{
			List<string> constantList = new List<string>();

			foreach (MAMEInputNode inputNode in InputList)
			{
				foreach (MAMEControlNode controlNode in inputNode.ControlList)
					constantList.Add(controlNode.Constant);
			}

			return constantList.ToArray();
		}

		private string GetInputConstantsString()
		{
			return String.Join(" ", GetInputConstantsArray());
		}

		public string[] GetConstantsArray()
		{
			if (ControlsDat != null)
				return ControlsDat.GetConstantsArray();
			else if (Parent != null)
			{
				if (Parent.ControlsDat != null)
					return Parent.ControlsDat.GetConstantsArray();
				else
					return GetInputConstantsArray();
			}

			return GetInputConstantsArray();
		}

		public string GetConstantsString()
		{
			if (ControlsDat != null)
				return ControlsDat.GetConstantsString();
			else if (Parent != null)
			{
				if (Parent.ControlsDat != null)
					return Parent.ControlsDat.GetConstantsString();
				else
					return GetInputConstantsString();
			}

			return GetInputConstantsString();
		}

		public string[] GetControlsArray()
		{
			if (ControlsDat != null)
				return ControlsDat.GetControlsArray();
			else
			{
				if (Parent != null)
				{
					if (Parent.ControlsDat != null)
						return Parent.ControlsDat.GetControlsArray();
				}
			}

			return null;
		}

		public string GetControlsString()
		{
			if (ControlsDat != null)
				return ControlsDat.GetControlsString();
			else
			{
				if (Parent != null)
				{
					if (Parent.ControlsDat != null)
						return Parent.ControlsDat.GetControlsString();
				}
			}

			return String.Empty;
		}

		public string GetNumPlayersString()
		{
			string retString = String.Empty;

			if (InputList.Count > 0)
				retString = InputList[0].Players.ToString();

			if (ControlsDat != null)
				retString = ControlsDat.NumPlayers.ToString();
			else if (Parent != null)
			{
				if (Parent.ControlsDat != null)
					retString = Parent.ControlsDat.NumPlayers.ToString();
			}

			return retString;
		}

		public string GetNumButtonsString()
		{
			int maxButtons = 0;
			string retString = String.Empty;

			if (InputList.Count > 0)
			{
				foreach (MAMEInputNode mameInfoNode in InputList)
					foreach (MAMEControlNode mameControlNode in mameInfoNode.ControlList)
						maxButtons = Math.Max(maxButtons, mameControlNode.Buttons);

				retString = maxButtons.ToString();
			}
			else if (TryGetNumButtons(ControlsDat, out maxButtons))
				retString = maxButtons.ToString();
			else if (Parent != null)
				if (TryGetNumButtons(Parent.ControlsDat, out maxButtons))
					retString = maxButtons.ToString();

			return retString;
		}

		public bool TryGetNumButtons(ControlsDatNode controlsDatNode, out int maxButtons)
		{
			maxButtons = 0;

			if (controlsDatNode == null)
				return false;

			foreach (ControlsDatPlayerNode controlsDatPlayerNode in controlsDatNode.PlayerList)
				maxButtons = Math.Max(maxButtons, controlsDatPlayerNode.NumButtons);

			return true;
		}

		public string GetAlternatingString()
		{
			string retString = String.Empty;

			if (ControlsDat != null)
				retString = (ControlsDat.Alternating == 1 ? "Yes" : "No");
			else if (Parent != null)
			{
				if (Parent.ControlsDat != null)
					retString = (Parent.ControlsDat.Alternating == 1 ? "Yes" : "No");
			}

			return retString;
		}

		public string GetMirroredString()
		{
			string retString = String.Empty;

			if (ControlsDat != null)
				retString = (ControlsDat.Mirrored == 1 ? "Yes" : "No");
			else if (Parent != null)
			{
				if (Parent.ControlsDat != null)
					retString = (Parent.ControlsDat.Mirrored == 1 ? "Yes" : "No");
			}

			return retString;
		}

		public string GetUsesServiceString()
		{
			string retString = String.Empty;

			if (InputList.Count > 0)
				retString = (InputList[0].Service ? "Yes" : "No");

			if (ControlsDat != null)
				retString = (ControlsDat.UsesService == 1 ? "Yes" : "No");
			else if (Parent != null)
			{
				if (Parent.ControlsDat != null)
					retString = (Parent.ControlsDat.UsesService == 1 ? "Yes" : "No");
			}

			return retString;
		}

		public string GetTiltString()
		{
			string retString = String.Empty;

			if (ControlsDat != null)
				retString = (ControlsDat.Tilt == 1 ? "Yes" : "No");
			else if (Parent != null)
			{
				if (Parent.ControlsDat != null)
					retString = (Parent.ControlsDat.Tilt == 1 ? "Yes" : "No");
			}

			return retString;
		}

		public string GetCocktailString()
		{
			string retString = String.Empty;

			if (ControlsDat != null)
				retString = (ControlsDat.Cocktail == 1 ? "Yes" : "No");
			else if (Parent != null)
			{
				if (Parent.ControlsDat != null)
					retString = (Parent.ControlsDat.Cocktail == 1 ? "Yes" : "No");
			}

			return retString;
		}

		public string GetMiscDetailsString()
		{
			string retString = String.Empty;

			if (ControlsDat != null)
				retString = ControlsDat.MiscDetails;
			else if (Parent != null)
			{
				if (Parent.ControlsDat != null)
					retString = Parent.ControlsDat.MiscDetails;
			}

			return retString;
		}
	}

	public enum MAMEROMStatus
	{
		None,
		BadDump,
		NoDump,
		Good
	};

	public class MAMEROMNode
	{
		public string Name = null;
		public string Merge = null;
		public string Bios = null;
		public ulong Size = 0;
		public ulong Crc = 0;
		public string Sha1 = null;
		public string Region = null;
		public string Offset = null;
		public MAMEROMStatus Status = MAMEROMStatus.None;
		public bool Optional = false;

		public MAMEROMNode(string name, ulong size, ulong crc)
		{
			Name = name;
			Size = size;
			Crc = crc;
		}

		public MAMEROMNode(string name, string merge, string bios, ulong size, ulong crc, string sha1, string region, string offset, MAMEROMStatus status, bool optional)
		{
			Name = name;
			Merge = merge;
			Bios = bios;
			Size = size;
			Crc = crc;
			Sha1 = sha1;
			Region = region;
			Offset = offset;
			Status = status;
			Optional = optional;
		}
	}

	public class MAMEDeviceRefNode
	{
		public string Name = null;

		public MAMEDeviceRefNode(string name)
		{
			Name = name;
		}
	}

	public enum MAMEDriverStatus
	{
		None,
		Good,
		Imperfect,
		Preliminary
	};

	public enum MAMESaveState
	{
		None,
		Supported,
		Unsupported
	};

	public class MAMEDriverNode
	{
		public MAMEDriverStatus Status = MAMEDriverStatus.None;
		public MAMEDriverStatus Emulation = MAMEDriverStatus.None;
		public MAMEDriverStatus Color = MAMEDriverStatus.None;
		public MAMEDriverStatus Sound = MAMEDriverStatus.None;
		public MAMEDriverStatus Graphic = MAMEDriverStatus.None;
		public MAMEDriverStatus Cocktail = MAMEDriverStatus.None;
		public MAMEDriverStatus Protection = MAMEDriverStatus.None;
		public MAMESaveState SaveState = MAMESaveState.None;
		public int PaletteSize = 0;

		public MAMEDriverNode(MAMEDriverStatus status, MAMEDriverStatus emulation, MAMEDriverStatus color, MAMEDriverStatus sound, MAMEDriverStatus graphic, MAMEDriverStatus cocktail, MAMEDriverStatus protection, MAMESaveState saveState, int paletteSize)
		{
			Status = status;
			Emulation = emulation;
			Color = color;
			Sound = sound;
			Graphic = graphic;
			Cocktail = cocktail;
			Protection = protection;
			SaveState = saveState;
			PaletteSize = paletteSize;
		}
	}

	public class MAMEDeviceNode
	{
		public string Type = null;
		public string Tag = null;
		public string Mandatory = null;
		public string Interface = null;
		public MAMEInstanceNode Instance = null;
		public List<MAMEExtensionNode> ExtensionList = null;

		public MAMEDeviceNode(string type, string tag, string mandatory, string _interface)
		{
			Type = type;
			Tag = tag;
			Mandatory = mandatory;
			Interface = _interface;
			ExtensionList = new List<MAMEExtensionNode>();
		}
	}

	public class MAMEInstanceNode
	{
		public string Name = null;
		public string BriefName = null;

		public MAMEInstanceNode(string name, string briefName)
		{
			Name = name;
			BriefName = briefName;
		}
	}

	public class MAMEExtensionNode
	{
		public string Name = null;

		public MAMEExtensionNode(string name)
		{
			Name = name;
		}
	}

	public class MAMEDisplayNode
	{
		public string Type = null;
		public int Rotate = 0;
		public int Width = 0;
		public int Height = 0;
		public float Refresh = 0;

		public MAMEDisplayNode(string type, int rotate, int width, int height, float refresh)
		{
			Type = type;
			Rotate = rotate;
			Width = width;
			Height = height;
			Refresh = refresh;
		}
	}

	public class MAMEChipNode
	{
		public string Type = null;
		public string Name = null;
		public string Clock = null;

		public MAMEChipNode(string type, string name, string clock)
		{
			Type = type;
			Name = name;
			Clock = clock;
		}
	}

	public class MAMEInputNode
	{
		public int Players = 0;
		//public int Buttons = 0;
		public int Coins = 0;
		public bool Service = false;
		public bool Tilt = false;
		public List<MAMEControlNode> ControlList = null;

		//public MAMEInputNode(int players, int buttons, int coins, bool service, bool tilt)
		public MAMEInputNode(int players, int coins, bool service, bool tilt)
		{
			Players = players;
			//Buttons = buttons;
			Coins = coins;
			Service = service;
			Tilt = tilt;
			ControlList = new List<MAMEControlNode>();
		}
	}

	public class MAMEDipSwitchNode
	{
		public string Name = null;
		public List<MAMEDipValueNode> DipValueList = null;

		public MAMEDipSwitchNode(string name)
		{
			Name = name;
			DipValueList = new List<MAMEDipValueNode>();
		}
	}

	public class MAMEDipValueNode
	{
		public string Name = null;
		public string Value = null;
		public string Default = null;

		public MAMEDipValueNode(string name, string value, string _default)
		{
			Name = name;
			Value = value;
			Default = _default;
		}
	}

	public class MAMEControlNode
	{
		public string Type = null;
		public int Player = 0;
		public int Buttons = 0;
		public string Ways = null;
		public string Ways2 = null;
		public string Ways3 = null;
		public int Minimum = 0;
		public int Maximum = 0;
		public int Sensitivity = 0;
		public int KeyDelta = 0;
		public bool Reverse = false;

		public MAMEControlNode(string type, int player, int buttons, string ways, string ways2, string ways3, int minimum, int maximum, int sensitivity, int keyDelta, bool reverse)
		{
			Type = type;
			Player = player;
			Buttons = buttons;
			Ways = ways;
			Ways2 = ways2;
			Ways3 = ways3;
			Minimum = minimum;
			Maximum = maximum;
			Sensitivity = sensitivity;
			KeyDelta = keyDelta;
			Reverse = reverse;
		}

		public static string GetControlConstant(string type, string ways, string ways2, string ways3)
		{
			if (type.EndsWith("joy"))
			{
				string vertical = String.Empty;

				if (ways.Equals("3 (half4)"))
					ways = "4";
				else if (ways.Equals("5 (half8)"))
					ways = "8";
				else if (ways.Equals("vertical2"))
				{ ways = "2"; vertical = "v"; }

				return String.Format("{0}{1}{2}way", vertical, type, ways);
			}

			return type;
		}

		public string Constant
		{
			get { return GetControlConstant(Type, Ways, Ways2, Ways3); }
		}
	}

	public class MAMESoftwareListNode
	{
		public string Name = null;
		public string Status = null;
		public string Filter = null;

		public MAMESoftwareListNode(string name, string status, string filter)
		{
			Name = name;
			Status = status;
			Filter = filter;
		}
	}
}