// Copyright (c) 2015, Ben Baker
// All rights reserved.
// 
// This source code is licensed under the BSD-style license found in the
// LICENSE file in the root directory of this source tree. 

using System;
using System.Collections.Generic;
using System.Text;

namespace CPWizard
{
	/**************************************************
	#
	# CORE INPUT OPTIONS
	#
	-ctrlr               preconfigure for specified controller
	-mouse               enable mouse input
	-joystick            enable joystick input
	-lightgun            enable lightgun input
	-multikeyboard       enable separate input from each keyboard device (if present)
	-multimouse          enable separate input from each mouse device (if present)
	-steadykey           enable steadykey support
	-offscreen_reload    convert lightgun button 2 into offscreen reload
	-joystick_map        explicit joystick map, or auto to auto-select
	-joystick_deadzone   center deadzone range for joystick where change is ignored (0.0 center, 1.0 end)
	-joystick_saturation end of axis saturation range for joystick where change is ignored (0.0 center, 1.0 end)

	#
	# CORE INPUT AUTOMATIC ENABLE OPTIONS
	#
	-paddle_device       enable (keyboard|mouse|joystick) if a paddle control is present
	-adstick_device      enable (keyboard|mouse|joystick) if an analog joystick control is present
	-pedal_device        enable (keyboard|mouse|joystick) if a pedal control is present
	-dial_device         enable (keyboard|mouse|joystick) if a dial control is present
	-trackball_device    enable (keyboard|mouse|joystick) if a trackball control is present
	-lightgun_device     enable (keyboard|mouse|joystick) if a lightgun control is present
	-positional_device   enable (keyboard|mouse|joystick) if a positional control is present
	-mouse_device        enable (keyboard|mouse|joystick) if a mouse control is present
	**************************************************/

	public enum MAMEControlMapType
	{
		Nothing,
		Keyboard,
		Mouse,
		Joystick
	}

	public enum MAMEControlType
	{
		Joystick,
		Paddle,
		AdStick,
		Pedal,
		Dial,
		Trackball,
		Lightgun,
		Positional,
		Mouse,
		Button
	}

	public class MAMEIniOptions : ICloneable
	{
		public bool Mouse = false;
		public bool Joystick = false;
		public bool Lightgun = false;
		public bool MultiKeyboard = false;
		public bool MultiMouse = false;
		public bool SteadyKey = false;
		public bool OffscreenReload = false;

		public MAMEControlMapType Paddle_Device = MAMEControlMapType.Keyboard;
		public MAMEControlMapType AdStick_Device = MAMEControlMapType.Keyboard;
		public MAMEControlMapType Pedal_Device = MAMEControlMapType.Keyboard;
		public MAMEControlMapType Dial_Device = MAMEControlMapType.Keyboard;
		public MAMEControlMapType Trackball_Device = MAMEControlMapType.Keyboard;
		public MAMEControlMapType Lightgun_Device = MAMEControlMapType.Keyboard;
		public MAMEControlMapType Positional_Device = MAMEControlMapType.Keyboard;
		public MAMEControlMapType Mouse_Device = MAMEControlMapType.Mouse;

		#region ICloneable Members

		public object Clone()
		{
			return this.MemberwiseClone();
		}

		#endregion
	}

	public class MAMEControlLabelNode
	{
		public MAMEControlType Type;
		public string Constant = null;
		public string[] InputCodes = null;

		public MAMEControlLabelNode(MAMEControlType type, string constant, string[] inputCodes)
		{
			Type = type;
			Constant = constant;
			InputCodes = inputCodes;
		}
	}

	public class MAMEOptions
	{
		//private Dictionary<string, ControlLabelNode> m_controlLabelHash = null;

		private MAMEControlLabelNode[] m_controlLabelArray =
        {
            new MAMEControlLabelNode(MAMEControlType.Joystick, "joy8way", new string[] { "JOYSTICK_UP", "JOYSTICK_DOWN", "JOYSTICK_LEFT", "JOYSTICK_RIGHT" }),
            new MAMEControlLabelNode(MAMEControlType.Joystick, "joy4way", new string[] { "JOYSTICK_UP", "JOYSTICK_DOWN", "JOYSTICK_LEFT", "JOYSTICK_RIGHT" }),
            new MAMEControlLabelNode(MAMEControlType.Joystick, "vjoy2way", new string[] { "JOYSTICK_UP", "JOYSTICK_DOWN" }),
            new MAMEControlLabelNode(MAMEControlType.Joystick, "joy2way", new string[] { "JOYSTICK_LEFT", "JOYSTICK_RIGHT" }),
            new MAMEControlLabelNode(MAMEControlType.Joystick, "doublejoy8way", new string[] { "JOYSTICKLEFT_UP", "JOYSTICKLEFT_DOWN", "JOYSTICKLEFT_LEFT", "JOYSTICKLEFT_RIGHT", "JOYSTICKRIGHT_UP", "JOYSTICKRIGHT_DOWN", "JOYSTICKRIGHT_LEFT", "JOYSTICKRIGHT_RIGHT" }),
            new MAMEControlLabelNode(MAMEControlType.Joystick, "doublejoy4way", new string[] { "JOYSTICKLEFT_UP", "JOYSTICKLEFT_DOWN", "JOYSTICKLEFT_LEFT", "JOYSTICKLEFT_RIGHT", "JOYSTICKRIGHT_UP", "JOYSTICKRIGHT_DOWN", "JOYSTICKRIGHT_LEFT", "JOYSTICKRIGHT_RIGHT" }),
            new MAMEControlLabelNode(MAMEControlType.Joystick, "vdoublejoy2way", new string[] { "JOYSTICKLEFT_UP", "JOYSTICKLEFT_DOWN" }),
            new MAMEControlLabelNode(MAMEControlType.Joystick, "doublejoy2way", new string[] { "JOYSTICKLEFT_LEFT", "JOYSTICKLEFT_RIGHT" }),
            new MAMEControlLabelNode(MAMEControlType.Joystick, "vdoublejoy2way", new string[] { "JOYSTICKRIGHT_UP", "JOYSTICKRIGHT_DOWN" }),
            new MAMEControlLabelNode(MAMEControlType.Joystick, "doublejoy2way", new string[] { "JOYSTICKRIGHT_LEFT", "JOYSTICKRIGHT_RIGHT" }),
            new MAMEControlLabelNode(MAMEControlType.Pedal, "pedal", new string[] { "PEDAL" }),
            new MAMEControlLabelNode(MAMEControlType.Pedal, "pedal2", new string[] { "PEDAL2" }),
            new MAMEControlLabelNode(MAMEControlType.Paddle, "paddle", new string[] { "PADDLE", "PADDLE_EXT" }),
            new MAMEControlLabelNode(MAMEControlType.Paddle, "vpaddle", new string[] { "PADDLE_V", "PADDLE_V_EXT" }),
            new MAMEControlLabelNode(MAMEControlType.Dial, "dial", new string[] { "DIAL", "DIAL_EXT" }),
            new MAMEControlLabelNode(MAMEControlType.Dial, "vdial", new string[] { "DIAL_V", "DIAL_V_EXT" }),
            new MAMEControlLabelNode(MAMEControlType.Trackball, "trackball", new string[] { "TRACKBALL_X", "TRACKBALL_X_EXT", "TRACKBALL_Y", "TRACKBALL_Y_EXT" }),
            new MAMEControlLabelNode(MAMEControlType.AdStick, "stick", new string[] { "AD_STICK_X", "AD_STICK_X_EXT", "AD_STICK_Y", "AD_STICK_Y_EXT" }),
            new MAMEControlLabelNode(MAMEControlType.AdStick, "stickx", new string[] { "AD_STICK_X", "AD_STICK_X_EXT" }),
            new MAMEControlLabelNode(MAMEControlType.AdStick, "sticky", new string[] { "AD_STICK_Y", "AD_STICK_Y_EXT" }),
            new MAMEControlLabelNode(MAMEControlType.AdStick, "stickz", new string[] { "AD_STICK_Z", "AD_STICK_Z_EXT" }),
            new MAMEControlLabelNode(MAMEControlType.Lightgun, "lightgun", new string[] { "LIGHTGUN_X", "LIGHTGUN_X_EXT", "LIGHTGUN_Y", "LIGHTGUN_Y_EXT" }),
            new MAMEControlLabelNode(MAMEControlType.Button, "button", new string[] { "BUTTON" })
        };

		public MAMEOptions()
		{
			//m_controlLabelHash = new Dictionary<string, ControlLabelNode>();

			//for (int i = 0; i < ControlLabelArray.Length; i++)
			//    ControlLabelHash.Add(ControlLabelArray[i].Constant, ControlLabelArray[i]);
		}

		public void OutputOptions(MAMEIniOptions iniOptions)
		{
			System.Diagnostics.Debug.WriteLine(String.Format("Mouse: {0}", iniOptions.Mouse));
			System.Diagnostics.Debug.WriteLine(String.Format("Joystick: {0}", iniOptions.Joystick));
			System.Diagnostics.Debug.WriteLine(String.Format("Lightgun: {0}", iniOptions.Lightgun));
			System.Diagnostics.Debug.WriteLine(String.Format("MultiKeyboard: {0}", iniOptions.MultiKeyboard));
			System.Diagnostics.Debug.WriteLine(String.Format("MultiMouse: {0}", iniOptions.MultiMouse));
			System.Diagnostics.Debug.WriteLine(String.Format("SteadyKey: {0}", iniOptions.SteadyKey));
			System.Diagnostics.Debug.WriteLine(String.Format("OffscreenReload: {0}", iniOptions.OffscreenReload));

			System.Diagnostics.Debug.WriteLine(String.Format("Paddle_Device: {0}", iniOptions.Paddle_Device));
			System.Diagnostics.Debug.WriteLine(String.Format("AdStick_Device: {0}", iniOptions.AdStick_Device));
			System.Diagnostics.Debug.WriteLine(String.Format("Pedal_Device: {0}", iniOptions.Pedal_Device));
			System.Diagnostics.Debug.WriteLine(String.Format("Dial_Device: {0}", iniOptions.Dial_Device));
			System.Diagnostics.Debug.WriteLine(String.Format("Trackball_Device: {0}", iniOptions.Trackball_Device));
			System.Diagnostics.Debug.WriteLine(String.Format("Lightgun_Device: {0}", iniOptions.Lightgun_Device));
			System.Diagnostics.Debug.WriteLine(String.Format("Positional_Device: {0}", iniOptions.Positional_Device));
			System.Diagnostics.Debug.WriteLine(String.Format("Mouse_Device: {0}", iniOptions.Mouse_Device));
		}

		public void ProcessIniFile(Dictionary<string, string> iniValues, ref MAMEIniOptions iniOptions)
		{
			if (iniValues == null)
				return;

			string value = null;

			if (iniValues.TryGetValue("mouse", out value))
				iniOptions.Mouse = StringTools.FromString<bool>(value);

			if (iniValues.TryGetValue("joystick", out value))
				iniOptions.Joystick = StringTools.FromString<bool>(value);

			if (iniValues.TryGetValue("lightgun", out value))
				iniOptions.Lightgun = StringTools.FromString<bool>(value);

			if (iniValues.TryGetValue("multikeyboard", out value))
				iniOptions.MultiKeyboard = StringTools.FromString<bool>(value);

			if (iniValues.TryGetValue("multimouse", out value))
				iniOptions.MultiMouse = StringTools.FromString<bool>(value);

			if (iniValues.TryGetValue("steadykey", out value))
				iniOptions.SteadyKey = StringTools.FromString<bool>(value);

			if (iniValues.TryGetValue("offscreen_reload", out value))
				iniOptions.OffscreenReload = StringTools.FromString<bool>(value);

			if (iniValues.TryGetValue("paddle_device", out value))
				SetMapType("paddle_device", StrToMapType(value), ref iniOptions);

			if (iniValues.TryGetValue("adstick_device", out value))
				SetMapType("adstick_device", StrToMapType(value), ref iniOptions);

			if (iniValues.TryGetValue("pedal_device", out value))
				SetMapType("pedal_device", StrToMapType(value), ref iniOptions);

			if (iniValues.TryGetValue("dial_device", out value))
				SetMapType("dial_device", StrToMapType(value), ref iniOptions);

			if (iniValues.TryGetValue("trackball_device", out value))
				SetMapType("trackball_device", StrToMapType(value), ref iniOptions);

			if (iniValues.TryGetValue("lightgun_device", out value))
				SetMapType("lightgun_device", StrToMapType(value), ref iniOptions);

			if (iniValues.TryGetValue("positional_device", out value))
				SetMapType("positional_device", StrToMapType(value), ref iniOptions);

			if (iniValues.TryGetValue("mouse_device", out value))
				SetMapType("mouse_device", StrToMapType(value), ref iniOptions);
		}

		private MAMEControlMapType StrToMapType(string value)
		{
			switch (value)
			{
				case "keyboard":
					return MAMEControlMapType.Keyboard;
				case "mouse":
					return MAMEControlMapType.Mouse;
				case "joystick":
					return MAMEControlMapType.Joystick;
				default:
					return MAMEControlMapType.Nothing;
			}
		}

		private void SetMapType(string deviceName, MAMEControlMapType mapType, ref MAMEIniOptions options)
		{
			if (mapType == MAMEControlMapType.Nothing)
				return;

			if (deviceName.StartsWith("-"))
				deviceName = deviceName.Substring(1);

			switch (deviceName)
			{
				case "paddle_device":
					options.Paddle_Device = mapType;
					break;
				case "adstick_device":
					options.AdStick_Device = mapType;
					break;
				case "pedal_device":
					options.Pedal_Device = mapType;
					break;
				case "dial_device":
					options.Dial_Device = mapType;
					break;
				case "trackball_device":
					options.Trackball_Device = mapType;
					break;
				case "lightgun_device":
					options.Lightgun_Device = mapType;
					break;
				case "positional_device":
					options.Positional_Device = mapType;
					break;
				case "mouse_device":
					options.Mouse_Device = mapType;
					break;
			}
		}

		public void ProcessCommandLine(ref MAMEIniOptions iniOptions)
		{
			if (Settings.MAME.CommandLine == null)
				return;

			string[] args = Settings.MAME.CommandLine.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

			for (int i = 0; i < args.Length; i++)
			{
				string value = null;
				MAMEControlMapType deviceType = MAMEControlMapType.Nothing;

				if (i + 1 < args.Length)
				{
					value = args[i + 1];

					deviceType = StrToMapType(value);
				}

				switch (args[i])
				{
					case "-mouse":
						iniOptions.Mouse = true;
						break;
					case "-joystick":
						iniOptions.Joystick = true;
						break;
					case "-lightgun":
						iniOptions.Lightgun = true;
						break;
					case "-multikeyboard":
						iniOptions.MultiKeyboard = true;
						break;
					case "-multimouse":
						iniOptions.MultiMouse = true;
						break;
					case "-steadykey":
						iniOptions.SteadyKey = true;
						break;
					default:
						SetMapType(args[i], deviceType, ref iniOptions);
						break;
				}
			}
		}

		public MAMEControlLabelNode[] GetControlLabelArray(string constant)
		{
			List<MAMEControlLabelNode> controlLabelArray = new List<MAMEControlLabelNode>();

			foreach (MAMEControlLabelNode controlLabel in m_controlLabelArray)
				if (constant.Equals(controlLabel.Constant))
					controlLabelArray.Add(controlLabel);

			return controlLabelArray.ToArray();
		}

		public MAMEControlLabelNode[] ControlLabelArray
		{
			get { return m_controlLabelArray; }
		}
	}
}
