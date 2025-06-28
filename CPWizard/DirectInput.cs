// Copyright (c) 2015, Ben Baker
// All rights reserved.
// 
// This source code is licensed under the BSD-style license found in the
// LICENSE file in the root directory of this source tree. 

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Timers;
using System.Windows.Forms;
using System.Diagnostics;

using SlimDX;
using SlimDX.DirectInput;

namespace CPWizard
{
	/// <summary>
	/// Summary description for 
	/// </summary>
	public class DirectInput : IDisposable
	{
		public enum ForceType
		{
			VeryBriefJolt,
			BriefJolt,
			LowRumble,
			HardRumble
		};

		private bool m_isDisposed = false;

		private const int MAX_JOYSTICKS = 8;
		private const int MAX_KEYS = 256;
		private const int MAX_BUTTONS = 128;

		private const int AXIS_MIN = -32768;
		private const int AXIS_NONE = 0;
		private const int AXIS_DEADZONE = 16384;
		private const int AXIS_MAX = 32767;

		private const int POV_NONE = 65535;
		private const int POV_UP = 0;
		private const int POV_RIGHT = 9000;
		private const int POV_DOWN = 18000;
		private const int POV_LEFT = 27000;

		private const int TIMER_INTERVAL = 33;

		private JoyNode[] m_joyArray = null;

		private const int JOY_AXIS_THRESHOLD = (((AXIS_MAX) - (AXIS_MIN)) / 100);

		private enum Type { BUTTON, AXIS, HAT };

		private Control m_control = null;

		private object m_tag = null;

		private SlimDX.DirectInput.DirectInput m_directInput = null;
		private Keyboard m_keyDevice = null;
		private SlimDX.DirectInput.Joystick[] m_joyDevice = null;
		private int m_joyCount = 0;
		private System.Timers.Timer m_timer = null;

		private Dictionary<ForceType, Effect>[] m_forcesDictionary = new Dictionary<ForceType, Effect>[MAX_JOYSTICKS];

		public event EventHandler<KeyEventArgs> KeyEvent;
		public event EventHandler<JoyEventArgs> JoyEvent;
		public event EventHandler<EventArgs> UpdateEvent;

		public DirectInput(Control control, object tag)
		{
			m_control = control;
			m_tag = tag;
		}

		~DirectInput()
		{
			Dispose(false);
		}

		public void ResourceLoad()
		{
			Debug.Assert(m_directInput == null);

			m_directInput = new SlimDX.DirectInput.DirectInput();
			IList<DeviceInstance> gameControllerList = m_directInput.GetDevices(DeviceClass.GameController, DeviceEnumerationFlags.AllDevices);

			m_joyCount = 0;
			m_joyArray = new JoyNode[gameControllerList.Count];
			m_joyDevice = new SlimDX.DirectInput.Joystick[gameControllerList.Count];

			for (int i = 0; i < m_joyArray.Length; i++)
				m_joyArray[i] = new JoyNode(MAX_BUTTONS, -JoyEventArgs.JoyRepeatDelay);

			foreach (DeviceInstance deviceInstance in gameControllerList)
			{
				m_joyDevice[m_joyCount] = new SlimDX.DirectInput.Joystick(m_directInput, deviceInstance.InstanceGuid);
				//m_joyDevice[m_joyCount].SetCooperativeLevel(GetDesktopWindow(), CooperativeLevel.Background | CooperativeLevel.Nonexclusive);
				m_joyDevice[m_joyCount].SetCooperativeLevel(m_control, CooperativeLevel.Background | CooperativeLevel.Nonexclusive);

				m_joyDevice[m_joyCount].Properties.AxisMode = DeviceAxisMode.Absolute;
				//m_joyDevice[m_joyCount].Properties.AutoCenter = true;

				SetAxisRange(m_joyDevice[m_joyCount], AXIS_MIN, AXIS_MAX);
				//SetForceFeedback(m_joyCount, m_joyDevice[m_joyCount]);

				Result result = m_joyDevice[m_joyCount].Acquire();

				//Console.WriteLine(String.Format("Joystick {0} Aquired {1} ({2})", m_joyCount + 1, result.IsSuccess ? "Success" : "Failure", m_joyDevice[m_joyCount].Information.ProductName));

				m_joyCount++;
			}

			//m_keyDevice = new Keyboard(m_directInput);
			//m_keyDevice.SetCooperativeLevel(control.Handle, CooperativeLevel.Background | CooperativeLevel.Nonexclusive);
			//m_keyDevice.Acquire();

			m_timer = new System.Timers.Timer(TIMER_INTERVAL);
			m_timer.Elapsed += new ElapsedEventHandler(OnTimerElapsed);
			m_timer.SynchronizingObject = m_control;
			m_timer.Start();
		}

		public void ResourceUnload()
		{
			if (m_timer != null)
			{
				m_timer.Stop();
				m_timer.Dispose();
				m_timer = null;
			}

			DisposeForces();

			for (int i = 0; i < m_joyCount; i++)
			{
				if (m_joyDevice[i] == null)
					continue;

				m_joyDevice[i].Unacquire();
				m_joyDevice[i].Dispose();
				m_joyDevice[i] = null;
			}

			if (m_keyDevice != null)
			{
				m_keyDevice.Unacquire();
				m_keyDevice.Dispose();
				m_keyDevice = null;
			}

			if (m_directInput != null)
			{
				m_directInput.Dispose();
				m_directInput = null;
			}
		}

		private void OnTimerElapsed(object sender, ElapsedEventArgs e)
		{
			if (m_timer == null)
				return;

			//TryPollKeyboard();
			if (TryPollJoystick((float)m_timer.Interval / 1000.0f))
			{
				if (UpdateEvent != null)
					UpdateEvent(this, EventArgs.Empty);
			}
		}

		public bool TryPollKeyboard()
		{
			if (m_keyDevice == null)
				return false;

			try
			{
				KeyboardState keyboardState = m_keyDevice.GetCurrentState();

				for (int i = 0; i < MAX_KEYS; i++)
				{
					Key key = (Key)i;

					if (keyboardState.IsPressed(key))
					{
						if (KeyEvent != null)
							KeyEvent(this, new KeyEventArgs(key, true, keyboardState, m_tag));

						return true;
					}
					else if (keyboardState.IsReleased(key))
					{
						if (KeyEvent != null)
							KeyEvent(this, new KeyEventArgs(key, false, keyboardState, m_tag));

						return true;
					}
				}
			}
			catch
			{
			}

			return false;
		}

		/* private bool CheckJoyRepeatDown(int state, int repeatCount)
		{
			if (state < repeatCount)
			{
				state++;
				return true;
			}

			return false;
		}

		private bool CheckJoyRepeatUp(int state, int repeatCount)
		{
			return (state == repeatCount);
		} */

		public bool TryPollJoystick(float elapsedTime)
		{
			if (JoyEvent == null)
				return false;

			bool retVal = false;
			int joyDelay = (int)Math.Max(elapsedTime * 1000.0f, 1);

			try
			{
				for (byte i = 0; i < m_joyCount; i++)
				{
					if (m_joyDevice[i] == null)
						continue;

					JoystickState joyState = null;
					bool[] buttonArray = null;
					JoyNode joy = m_joyArray[i];

					try
					{
						Result result = m_joyDevice[i].Poll();

						if (result.IsFailure)
							continue;

						joyState = m_joyDevice[i].GetCurrentState();
						buttonArray = joyState.GetButtons();
					}
					catch
					{
						if (m_joyDevice[i] != null)
						{
							m_joyDevice[i].Unacquire();
							m_joyDevice[i].Acquire();
						}

						continue;
					}

					joy.AxisX = joyState.X;
					joy.AxisY = joyState.Y;
					joy.AxisZ = joyState.Z;
					joy.AxisRx = joyState.RotationX;
					joy.AxisRy = joyState.RotationY;
					joy.AxisRz = joyState.RotationZ;
					joy.Pov = (ushort)joyState.GetPointOfViewControllers()[0];
					joy.ButtonCount = buttonArray.Length;

					Array.Copy(buttonArray, joy.ButtonArray, buttonArray.Length);

					retVal = JoyEventArgs.TryProcessJoyInput(this, i, joy, joyDelay, m_tag, JoyEvent);
				}
			}
			catch
			{
			}

			return retVal;
		}

		private void SetAxisRange(SlimDX.DirectInput.Joystick joyDevice, int lowerRange, int upperRange)
		{
			// Enumerate any axes
			foreach (DeviceObjectInstance deviceObjectInstance in joyDevice.GetObjects())
			{
				if ((deviceObjectInstance.ObjectType & ObjectDeviceType.Axis) != 0)
					joyDevice.Properties.SetRange(lowerRange, upperRange);
			}
		}

		private void SetForceFeedback(int joyId, Device joyDevice)
		{
			DisposeForces();

			int[] axis = null;

			// Enumerate any axes
			foreach (DeviceObjectInstance doi in joyDevice.GetObjects())
			{
				int[] temp;

				// Get info about first two FF axii on the device
				if ((doi.Aspect & ObjectAspect.ForceFeedbackActuator) != 0)
				{
					if (axis != null)
					{
						temp = new int[axis.Length + 1];
						axis.CopyTo(temp, 0);
						axis = temp;
					}
					else
						axis = new int[1];

					// Store the offset of each axis.
					axis[axis.Length - 1] = doi.Offset;
					if (axis.Length == 2)
					{
						break;
					}
				}
			}

			try
			{
				if (axis != null)
				{
					m_forcesDictionary[joyId] = new Dictionary<ForceType, Effect>();

					m_forcesDictionary[joyId].Add(ForceType.VeryBriefJolt, InitializeForce(joyDevice, EffectType.ConstantForce, axis, 6000, EffectFlags.ObjectOffsets | EffectFlags.Spherical, 150000));
					m_forcesDictionary[joyId].Add(ForceType.BriefJolt, InitializeForce(joyDevice, EffectType.ConstantForce, axis, 10000, EffectFlags.ObjectOffsets | EffectFlags.Spherical, 250000));
					m_forcesDictionary[joyId].Add(ForceType.LowRumble, InitializeForce(joyDevice, EffectType.ConstantForce, axis, 2000, EffectFlags.ObjectOffsets | EffectFlags.Cartesian, 900000));
					m_forcesDictionary[joyId].Add(ForceType.HardRumble, InitializeForce(joyDevice, EffectType.ConstantForce, axis, 10000, EffectFlags.ObjectOffsets | EffectFlags.Spherical, 2000000));
				}
			}
			catch
			{
			}
		}

		public static string[] GetJoystickArray()
		{
			List<string> joystickList = new List<string>();

			using (SlimDX.DirectInput.DirectInput directInput = new SlimDX.DirectInput.DirectInput())
			{
				foreach (DeviceInstance instance in directInput.GetDevices(DeviceClass.GameController, DeviceEnumerationFlags.AttachedOnly))
					joystickList.Add(instance.InstanceName);
			}

			return joystickList.ToArray();
		}

		#region IDisposable Members

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (m_isDisposed)
				return;

			if (disposing)
			{
				// Dispose managed resources
			}

			// Dispose unmanaged resources
			ResourceUnload();

			m_isDisposed = true;
		}

		#endregion

		#region DisposeForces
		public void DisposeForces()
		{
			for (int i = 0; i < m_forcesDictionary.Length; i++)
			{
				Dictionary<ForceType, Effect> forcesDictionary = m_forcesDictionary[i];

				if (forcesDictionary != null)
				{
					foreach (Effect effect in forcesDictionary.Values)
						effect.Dispose();

					forcesDictionary.Clear();
					forcesDictionary = null;
				}
			}
		}
		#endregion

		#region SendForce
		public void SendForce(ForceType type, int joyId)
		{
			Dictionary<ForceType, Effect> forcesDict = m_forcesDictionary[joyId];

			if (m_forcesDictionary == null)
				return;

			if (!m_forcesDictionary[joyId].ContainsKey(type))
				return;

			Effect force = m_forcesDictionary[joyId][type];
			force.Start(1);
		}
		#endregion

		#region InitializeForce
		public Effect InitializeForce(Device joyDevice, EffectType type, int[] axis, int magnitude, EffectFlags flags, int duration)
		{
			Effect effect = null;
			EffectParameters effectParameters;

			foreach (EffectInfo effectInfo in joyDevice.GetEffects(EffectType.All))
			{
				if (effectInfo.Type == type)
				{
					ConstantForce constantForce = new ConstantForce();
					constantForce.Magnitude = magnitude;
					constantForce.AsConditionSet();

					effectParameters = new EffectParameters();
					effectParameters.SetAxes(new int[1], new int[axis.Length]);
					effectParameters.Duration = duration;
					effectParameters.Gain = 10000;
					effectParameters.Parameters = constantForce;
					effectParameters.SamplePeriod = 0;
					effectParameters.TriggerButton = -1;            // No Trigger
					effectParameters.TriggerRepeatInterval = -1;    // Infinite
					effectParameters.Flags = flags;
					effectParameters.Envelope = new Envelope();

					// Create the effect, using the passed in guid.
					effect = new Effect(joyDevice, effectInfo.Guid, effectParameters);
				}
			}

			return effect;
		}
		#endregion
	}

	public class JoyNode
	{
		private const int AXIS_NONE = 0;
		private const int POV_NONE = 65535;

		public bool[] ButtonArray = null;
		public int AxisX = AXIS_NONE;
		public int AxisY = AXIS_NONE;
		public int AxisZ = AXIS_NONE;
		public int AxisRx = AXIS_NONE;
		public int AxisRy = AXIS_NONE;
		public int AxisRz = AXIS_NONE;
		public int Pov = POV_NONE;
		public int ButtonCount = 0;

		public int AxisX_Left = 0;
		public int AxisX_Right = 0;

		public int AxisY_Up = 0;
		public int AxisY_Down = 0;

		public int AxisZ_Neg = 0;
		public int AxisZ_Pos = 0;

		public int AxisRx_Neg = 0;
		public int AxisRx_Pos = 0;

		public int AxisRy_Neg = 0;
		public int AxisRy_Pos = 0;

		public int AxisRz_Neg = 0;
		public int AxisRz_Pos = 0;

		public int Pov_Up = 0;
		public int Pov_Right = 0;
		public int Pov_Down = 0;
		public int Pov_Left = 0;

		public int[] Buttons = null;

		public JoyNode(int maxButtons, int resetValue)
		{
			ButtonArray = new bool[maxButtons];
			Buttons = new int[maxButtons];

			Reset(resetValue);
		}

		public void Reset(int value)
		{
			AxisX_Left = value;
			AxisX_Right = value;

			AxisY_Up = value;
			AxisY_Down = value;

			AxisZ_Neg = value;
			AxisZ_Pos = value;

			AxisRx_Neg = value;
			AxisRx_Pos = value;

			AxisRy_Neg = value;
			AxisRy_Pos = value;

			AxisRz_Neg = value;
			AxisRz_Pos = value;

			Pov_Up = value;
			Pov_Right = value;
			Pov_Down = value;
			Pov_Left = value;

			for (int i = 0; i < Buttons.Length; i++)
				Buttons[i] = value;
		}
	}

	public enum JoyType
	{
		None = -1,
		AxisX = 0,
		AxisY,
		AxisZ,
		AxisRx,
		AxisRy,
		AxisRz,
		DPad,
		Button
	};

	public enum JoyDirection
	{
		None = -1,
		Left = 0,
		Right,
		Up,
		Down,
		Negative,
		Positive
	};

	public class JoyEventArgs : EventArgs
	{
		public const int AXIS_MIN = -32768;
		public const int AXIS_NONE = 0;
		public const int AXIS_DEADZONE = 16384;
		public const int AXIS_MAX = 32767;

		public const int POV_NONE = 65535;
		public const int POV_UP = 0;
		public const int POV_UP_RIGHT = 4500;
		public const int POV_RIGHT = 9000;
		public const int POV_DOWN_RIGHT = 13500;
		public const int POV_DOWN = 18000;
		public const int POV_DOWN_LEFT = 22500;
		public const int POV_LEFT = 27000;
		public const int POV_UP_LEFT = 31500;

		private string[] JoyTypeName = { "XAXIS", "YAXIS", "ZAXIS", "RXAXIS", "RYAXIS", "RZAXIS", "DPAD", "BUTTON" };
		private string[] JoyDirectionName = { "LEFT", "RIGHT", "UP", "DOWN", "NEG", "POS" };

		private static int m_joyRepeatDelay = 0;
		private static int m_joyRepeatSpeed = 0;

		public string Name;
		public int JoyIndex;
		public int ButtonIndex;
		public JoyType JoyType;
		public JoyDirection JoyDirection;
		public int JoyValue;
		public bool IsDown;
		public bool IsRepeat;
		public bool[] ButtonArray;
		public bool Handled = false;
		public object Tag;

		static JoyEventArgs()
		{
			m_joyRepeatDelay = GetKeyboardDelay();
			m_joyRepeatSpeed = GetKeyboardSpeed();
		}

		public JoyEventArgs(int joyIndex, int buttonIndex, JoyType joyType, JoyDirection joyDirection, int joyValue, bool isDown, bool isRepeat, bool[] buttonArray, object tag)
		{
			this.Name = GetJoyString(joyIndex, buttonIndex, joyType, joyDirection);
			this.JoyIndex = joyIndex;
			this.ButtonIndex = buttonIndex;
			this.JoyType = joyType;
			this.JoyDirection = joyDirection;
			this.JoyValue = joyValue;
			this.IsDown = isDown;
			this.IsRepeat = isRepeat;
			this.ButtonArray = buttonArray;
			this.Tag = tag;

			//Console.WriteLine("{0} (IsDown {1} IsRepeat: {2})", this.Name, isDown, isRepeat);
		}

		public string GetJoyTypeString(JoyType joyType)
		{
			return (joyType == JoyType.None ? "[NONE]" : JoyTypeName[(int)joyType]);
		}

		public string GetJoyDirectionString(JoyDirection joyDirection)
		{
			return (joyDirection == JoyDirection.None ? "[NONE]" : JoyDirectionName[(int)joyDirection]);
		}

		public string GetJoyString(int joyIndex, int buttonIndex, JoyType joyType, JoyDirection joyDirection)
		{
			string joyTypeString = GetJoyTypeString(joyType);
			string joyDirectionString = GetJoyDirectionString(joyDirection);

			switch (joyType)
			{
				case JoyType.None:
				case JoyType.AxisX:
				case JoyType.AxisY:
				case JoyType.AxisZ:
				case JoyType.AxisRx:
				case JoyType.AxisRy:
				case JoyType.AxisRz:
					return String.Format("[JOYCODE_{0}_{1}_{2}_SWITCH]", joyIndex + 1, joyTypeString, joyDirectionString);
				case JoyType.DPad:
					return String.Format("[JOYCODE_{0}_{1}{2}]", joyIndex + 1, joyTypeString, joyDirectionString);
				case JoyType.Button:
					return String.Format("[JOYCODE_{0}_{1}{2}]", joyIndex + 1, joyTypeString, buttonIndex + 1);
			}

			return null;
		}

		public static bool TryProcessJoyInput(object sender, int index, JoyNode joy, int joyDelay, object tag, EventHandler<JoyEventArgs> joyEvent)
		{
			bool retVal = false;

			if (joyEvent == null)
				return false;

			// Up
			if (joy.AxisX >= AXIS_NONE - AXIS_DEADZONE && joy.AxisX <= AXIS_NONE + AXIS_DEADZONE && joy.AxisX_Left > -m_joyRepeatDelay) { joyEvent(sender, new JoyEventArgs(index, -1, JoyType.AxisX, JoyDirection.Left, joy.AxisX, false, false, joy.ButtonArray, tag)); joy.AxisX_Left = -m_joyRepeatDelay; retVal = true; }
			if (joy.AxisX >= AXIS_NONE - AXIS_DEADZONE && joy.AxisX <= AXIS_NONE + AXIS_DEADZONE && joy.AxisX_Right > -m_joyRepeatDelay) { joyEvent(sender, new JoyEventArgs(index, -1, JoyType.AxisX, JoyDirection.Right, joy.AxisX, false, false, joy.ButtonArray, tag)); joy.AxisX_Right = -m_joyRepeatDelay; retVal = true; }
			if (joy.AxisY >= AXIS_NONE - AXIS_DEADZONE && joy.AxisY <= AXIS_NONE + AXIS_DEADZONE && joy.AxisY_Up > -m_joyRepeatDelay) { joyEvent(sender, new JoyEventArgs(index, -1, JoyType.AxisY, JoyDirection.Up, joy.AxisY, false, false, joy.ButtonArray, tag)); joy.AxisY_Up = -m_joyRepeatDelay; retVal = true; }
			if (joy.AxisY >= AXIS_NONE - AXIS_DEADZONE && joy.AxisY <= AXIS_NONE + AXIS_DEADZONE && joy.AxisY_Down > -m_joyRepeatDelay) { joyEvent(sender, new JoyEventArgs(index, -1, JoyType.AxisY, JoyDirection.Down, joy.AxisY, false, false, joy.ButtonArray, tag)); joy.AxisY_Down = -m_joyRepeatDelay; retVal = true; }
			if (joy.AxisZ >= AXIS_NONE - AXIS_DEADZONE && joy.AxisZ <= AXIS_NONE + AXIS_DEADZONE && joy.AxisZ_Neg > -m_joyRepeatDelay) { joyEvent(sender, new JoyEventArgs(index, -1, JoyType.AxisZ, JoyDirection.Negative, joy.AxisZ, false, false, joy.ButtonArray, tag)); joy.AxisZ_Neg = -m_joyRepeatDelay; retVal = true; }
			if (joy.AxisZ >= AXIS_NONE - AXIS_DEADZONE && joy.AxisZ <= AXIS_NONE + AXIS_DEADZONE && joy.AxisZ_Pos > -m_joyRepeatDelay) { joyEvent(sender, new JoyEventArgs(index, -1, JoyType.AxisZ, JoyDirection.Positive, joy.AxisZ, false, false, joy.ButtonArray, tag)); joy.AxisZ_Pos = -m_joyRepeatDelay; retVal = true; }
			if (joy.AxisRx >= AXIS_NONE - AXIS_DEADZONE && joy.AxisRx <= AXIS_NONE + AXIS_DEADZONE && joy.AxisRx_Neg > -m_joyRepeatDelay) { joyEvent(sender, new JoyEventArgs(index, -1, JoyType.AxisRx, JoyDirection.Negative, joy.AxisRx, false, false, joy.ButtonArray, tag)); joy.AxisRx_Neg = -m_joyRepeatDelay; retVal = true; }
			if (joy.AxisRx >= AXIS_NONE - AXIS_DEADZONE && joy.AxisRx <= AXIS_NONE + AXIS_DEADZONE && joy.AxisRx_Pos > -m_joyRepeatDelay) { joyEvent(sender, new JoyEventArgs(index, -1, JoyType.AxisRx, JoyDirection.Positive, joy.AxisRx, false, false, joy.ButtonArray, tag)); joy.AxisRx_Pos = -m_joyRepeatDelay; retVal = true; }
			if (joy.AxisRy >= AXIS_NONE - AXIS_DEADZONE && joy.AxisRy <= AXIS_NONE + AXIS_DEADZONE && joy.AxisRy_Neg > -m_joyRepeatDelay) { joyEvent(sender, new JoyEventArgs(index, -1, JoyType.AxisRy, JoyDirection.Negative, joy.AxisRy, false, false, joy.ButtonArray, tag)); joy.AxisRy_Neg = -m_joyRepeatDelay; retVal = true; }
			if (joy.AxisRy >= AXIS_NONE - AXIS_DEADZONE && joy.AxisRy <= AXIS_NONE + AXIS_DEADZONE && joy.AxisRy_Pos > -m_joyRepeatDelay) { joyEvent(sender, new JoyEventArgs(index, -1, JoyType.AxisRy, JoyDirection.Positive, joy.AxisRy, false, false, joy.ButtonArray, tag)); joy.AxisRy_Pos = -m_joyRepeatDelay; retVal = true; }
			if (joy.AxisRz >= AXIS_NONE - AXIS_DEADZONE && joy.AxisRz <= AXIS_NONE + AXIS_DEADZONE && joy.AxisRz_Neg > -m_joyRepeatDelay) { joyEvent(sender, new JoyEventArgs(index, -1, JoyType.AxisRz, JoyDirection.Negative, joy.AxisRz, false, false, joy.ButtonArray, tag)); joy.AxisRz_Neg = -m_joyRepeatDelay; retVal = true; }
			if (joy.AxisRz >= AXIS_NONE - AXIS_DEADZONE && joy.AxisRz <= AXIS_NONE + AXIS_DEADZONE && joy.AxisRz_Pos > -m_joyRepeatDelay) { joyEvent(sender, new JoyEventArgs(index, -1, JoyType.AxisRz, JoyDirection.Positive, joy.AxisRz, false, false, joy.ButtonArray, tag)); joy.AxisRz_Pos = -m_joyRepeatDelay; retVal = true; }

			// Down
			if (joy.AxisX <= AXIS_MIN + AXIS_DEADZONE) if (joy.AxisX_Left == -m_joyRepeatDelay || joy.AxisX_Left >= m_joyRepeatSpeed) { joyEvent(sender, new JoyEventArgs(index, -1, JoyType.AxisX, JoyDirection.Left, joy.AxisX, true, joy.AxisX_Left >= m_joyRepeatSpeed, joy.ButtonArray, tag)); joy.AxisX_Left = (joy.AxisX_Left == -m_joyRepeatDelay ? -m_joyRepeatDelay + joyDelay : 0); retVal = true; } else joy.AxisX_Left += joyDelay;
			if (joy.AxisX >= AXIS_MAX - AXIS_DEADZONE) if (joy.AxisX_Right == -m_joyRepeatDelay || joy.AxisX_Right >= m_joyRepeatSpeed) { joyEvent(sender, new JoyEventArgs(index, -1, JoyType.AxisX, JoyDirection.Right, joy.AxisX, true, joy.AxisX_Right >= m_joyRepeatSpeed, joy.ButtonArray, tag)); joy.AxisX_Right = (joy.AxisX_Right == -m_joyRepeatDelay ? -m_joyRepeatDelay + joyDelay : 0); retVal = true; } else joy.AxisX_Right += joyDelay;
			if (joy.AxisY <= AXIS_MIN + AXIS_DEADZONE) if (joy.AxisY_Up == -m_joyRepeatDelay || joy.AxisY_Up >= m_joyRepeatSpeed) { joyEvent(sender, new JoyEventArgs(index, -1, JoyType.AxisY, JoyDirection.Up, joy.AxisY, true, joy.AxisY_Up >= m_joyRepeatSpeed, joy.ButtonArray, tag)); joy.AxisY_Up = (joy.AxisY_Up == -m_joyRepeatDelay ? -m_joyRepeatDelay + joyDelay : 0); retVal = true; } else joy.AxisY_Up += joyDelay;
			if (joy.AxisY >= AXIS_MAX - AXIS_DEADZONE) if (joy.AxisY_Down == -m_joyRepeatDelay || joy.AxisY_Down >= m_joyRepeatSpeed) { joyEvent(sender, new JoyEventArgs(index, -1, JoyType.AxisY, JoyDirection.Down, joy.AxisY, true, joy.AxisY_Down >= m_joyRepeatSpeed, joy.ButtonArray, tag)); joy.AxisY_Down = (joy.AxisY_Down == -m_joyRepeatDelay ? -m_joyRepeatDelay + joyDelay : 0); retVal = true; } else joy.AxisY_Down += joyDelay;
			if (joy.AxisZ <= AXIS_MIN + AXIS_DEADZONE) if (joy.AxisZ_Neg == -m_joyRepeatDelay || joy.AxisZ_Neg >= m_joyRepeatSpeed) { joyEvent(sender, new JoyEventArgs(index, -1, JoyType.AxisZ, JoyDirection.Negative, joy.AxisZ, true, joy.AxisZ_Neg >= m_joyRepeatSpeed, joy.ButtonArray, tag)); joy.AxisZ_Neg = (joy.AxisZ_Neg == -m_joyRepeatDelay ? -m_joyRepeatDelay + joyDelay : 0); retVal = true; } else joy.AxisZ_Neg += joyDelay;
			if (joy.AxisZ >= AXIS_MAX - AXIS_DEADZONE) if (joy.AxisZ_Pos == -m_joyRepeatDelay || joy.AxisZ_Pos >= m_joyRepeatSpeed) { joyEvent(sender, new JoyEventArgs(index, -1, JoyType.AxisZ, JoyDirection.Positive, joy.AxisZ, true, joy.AxisZ_Pos >= m_joyRepeatSpeed, joy.ButtonArray, tag)); joy.AxisZ_Pos = (joy.AxisZ_Pos == -m_joyRepeatDelay ? -m_joyRepeatDelay + joyDelay : 0); retVal = true; } else joy.AxisZ_Pos += joyDelay;
			if (joy.AxisRx <= AXIS_MIN + AXIS_DEADZONE) if (joy.AxisRx_Neg == -m_joyRepeatDelay || joy.AxisRx_Neg >= m_joyRepeatSpeed) { joyEvent(sender, new JoyEventArgs(index, -1, JoyType.AxisRx, JoyDirection.Negative, joy.AxisRx, true, joy.AxisRx_Neg >= m_joyRepeatSpeed, joy.ButtonArray, tag)); joy.AxisRx_Neg = (joy.AxisRx_Neg == -m_joyRepeatDelay ? -m_joyRepeatDelay + joyDelay : 0); retVal = true; } else joy.AxisRx_Neg += joyDelay;
			if (joy.AxisRx >= AXIS_MAX - AXIS_DEADZONE) if (joy.AxisRx_Pos == -m_joyRepeatDelay || joy.AxisRx_Pos >= m_joyRepeatSpeed) { joyEvent(sender, new JoyEventArgs(index, -1, JoyType.AxisRx, JoyDirection.Positive, joy.AxisRx, true, joy.AxisRx_Pos >= m_joyRepeatSpeed, joy.ButtonArray, tag)); joy.AxisRx_Pos = (joy.AxisRx_Pos == -m_joyRepeatDelay ? -m_joyRepeatDelay + joyDelay : 0); retVal = true; } else joy.AxisRx_Pos += joyDelay;
			if (joy.AxisRy <= AXIS_MIN + AXIS_DEADZONE) if (joy.AxisRy_Neg == -m_joyRepeatDelay || joy.AxisRy_Neg >= m_joyRepeatSpeed) { joyEvent(sender, new JoyEventArgs(index, -1, JoyType.AxisRy, JoyDirection.Negative, joy.AxisRy, true, joy.AxisRy_Neg >= m_joyRepeatSpeed, joy.ButtonArray, tag)); joy.AxisRy_Neg = (joy.AxisRy_Neg == -m_joyRepeatDelay ? -m_joyRepeatDelay + joyDelay : 0); retVal = true; } else joy.AxisRy_Neg += joyDelay;
			if (joy.AxisRy >= AXIS_MAX - AXIS_DEADZONE) if (joy.AxisRy_Pos == -m_joyRepeatDelay || joy.AxisRy_Pos >= m_joyRepeatSpeed) { joyEvent(sender, new JoyEventArgs(index, -1, JoyType.AxisRy, JoyDirection.Positive, joy.AxisRy, true, joy.AxisRy_Pos >= m_joyRepeatSpeed, joy.ButtonArray, tag)); joy.AxisRy_Pos = (joy.AxisRy_Pos == -m_joyRepeatDelay ? -m_joyRepeatDelay + joyDelay : 0); retVal = true; } else joy.AxisRy_Pos += joyDelay;
			if (joy.AxisRz <= AXIS_MIN + AXIS_DEADZONE) if (joy.AxisRz_Neg == -m_joyRepeatDelay || joy.AxisRz_Neg >= m_joyRepeatSpeed) { joyEvent(sender, new JoyEventArgs(index, -1, JoyType.AxisRz, JoyDirection.Negative, joy.AxisRz, true, joy.AxisRz_Neg >= m_joyRepeatSpeed, joy.ButtonArray, tag)); joy.AxisRz_Neg = (joy.AxisRz_Neg == -m_joyRepeatDelay ? -m_joyRepeatDelay + joyDelay : 0); retVal = true; } else joy.AxisRz_Neg += joyDelay;
			if (joy.AxisRz >= AXIS_MAX - AXIS_DEADZONE) if (joy.AxisRz_Pos == -m_joyRepeatDelay || joy.AxisRz_Pos >= m_joyRepeatSpeed) { joyEvent(sender, new JoyEventArgs(index, -1, JoyType.AxisRz, JoyDirection.Positive, joy.AxisRz, true, joy.AxisRz_Pos >= m_joyRepeatSpeed, joy.ButtonArray, tag)); joy.AxisRz_Pos = (joy.AxisRz_Pos == -m_joyRepeatDelay ? -m_joyRepeatDelay + joyDelay : 0); retVal = true; } else joy.AxisRz_Pos += joyDelay;

			// Up
			if (joy.Pov == POV_NONE && joy.Pov_Up > -m_joyRepeatDelay) { joyEvent(sender, new JoyEventArgs(index, -1, JoyType.DPad, JoyDirection.Up, POV_UP, false, false, joy.ButtonArray, tag)); joy.Pov_Up = -m_joyRepeatDelay; retVal = true; }
			if (joy.Pov == POV_NONE && joy.Pov_Right > -m_joyRepeatDelay) { joyEvent(sender, new JoyEventArgs(index, -1, JoyType.DPad, JoyDirection.Right, POV_RIGHT, false, false, joy.ButtonArray, tag)); joy.Pov_Right = -m_joyRepeatDelay; retVal = true; }
			if (joy.Pov == POV_NONE && joy.Pov_Down > -m_joyRepeatDelay) { joyEvent(sender, new JoyEventArgs(index, -1, JoyType.DPad, JoyDirection.Down, POV_DOWN, false, false, joy.ButtonArray, tag)); joy.Pov_Down = -m_joyRepeatDelay; retVal = true; }
			if (joy.Pov == POV_NONE && joy.Pov_Left > -m_joyRepeatDelay) { joyEvent(sender, new JoyEventArgs(index, -1, JoyType.DPad, JoyDirection.Left, POV_LEFT, false, false, joy.ButtonArray, tag)); joy.Pov_Left = -m_joyRepeatDelay; retVal = true; }

			// Down
			if (joy.Pov == POV_UP || joy.Pov == POV_UP_LEFT || joy.Pov == POV_UP_RIGHT) if (joy.Pov_Up == -m_joyRepeatDelay || joy.Pov_Up >= m_joyRepeatSpeed) { joyEvent(sender, new JoyEventArgs(index, -1, JoyType.DPad, JoyDirection.Up, POV_UP, true, joy.Pov_Up >= m_joyRepeatSpeed, joy.ButtonArray, tag)); joy.Pov_Up = (joy.Pov_Up == -m_joyRepeatDelay ? -m_joyRepeatDelay + joyDelay : 0); retVal = true; } else joy.Pov_Up += joyDelay;
			if (joy.Pov == POV_RIGHT || joy.Pov == POV_UP_RIGHT || joy.Pov == POV_DOWN_RIGHT) if (joy.Pov_Right == -m_joyRepeatDelay || joy.Pov_Right >= m_joyRepeatSpeed) { joyEvent(sender, new JoyEventArgs(index, -1, JoyType.DPad, JoyDirection.Right, POV_RIGHT, true, joy.Pov_Right >= m_joyRepeatSpeed, joy.ButtonArray, tag)); joy.Pov_Right = (joy.Pov_Right == -m_joyRepeatDelay ? -m_joyRepeatDelay + joyDelay : 0); retVal = true; } else joy.Pov_Right += joyDelay;
			if (joy.Pov == POV_DOWN || joy.Pov == POV_DOWN_LEFT || joy.Pov == POV_DOWN_RIGHT) if (joy.Pov_Down == -m_joyRepeatDelay || joy.Pov_Down >= m_joyRepeatSpeed) { joyEvent(sender, new JoyEventArgs(index, -1, JoyType.DPad, JoyDirection.Down, POV_DOWN, true, joy.Pov_Down >= m_joyRepeatSpeed, joy.ButtonArray, tag)); joy.Pov_Down = (joy.Pov_Down == -m_joyRepeatDelay ? -m_joyRepeatDelay + joyDelay : 0); retVal = true; } else joy.Pov_Down += joyDelay;
			if (joy.Pov == POV_LEFT || joy.Pov == POV_UP_LEFT || joy.Pov == POV_DOWN_LEFT) if (joy.Pov_Left == -m_joyRepeatDelay || joy.Pov_Left >= m_joyRepeatSpeed) { joyEvent(sender, new JoyEventArgs(index, -1, JoyType.DPad, JoyDirection.Left, POV_LEFT, true, joy.Pov_Left >= m_joyRepeatSpeed, joy.ButtonArray, tag)); joy.Pov_Left = (joy.Pov_Left == -m_joyRepeatDelay ? -m_joyRepeatDelay + joyDelay : 0); retVal = true; } else joy.Pov_Left += joyDelay;

			for (int j = 0; j < joy.ButtonCount; j++)
			{
				// Up
				if (!joy.ButtonArray[j] && joy.Buttons[j] > -m_joyRepeatDelay) { joyEvent(sender, new JoyEventArgs(index, j, JoyType.Button, JoyDirection.None, joy.ButtonArray[j] ? 1 : 0, false, false, joy.ButtonArray, tag)); joy.Buttons[j] = -m_joyRepeatDelay; retVal = true; }

				// Down
				if (joy.ButtonArray[j]) if (joy.Buttons[j] == -m_joyRepeatDelay || joy.Buttons[j] >= m_joyRepeatSpeed) { joyEvent(sender, new JoyEventArgs(index, j, JoyType.Button, JoyDirection.None, joy.ButtonArray[j] ? 1 : 0, true, joy.Buttons[j] >= m_joyRepeatSpeed, joy.ButtonArray, tag)); joy.Buttons[j] = (joy.Buttons[j] == -m_joyRepeatDelay ? -m_joyRepeatDelay + joyDelay : 0); retVal = true; } else joy.Buttons[j] += joyDelay;
			}

			return retVal;
		}

		private static int GetKeyboardDelay()
		{
			int keyboardDelay = SystemInformation.KeyboardDelay;

			// SPI_GETKEYBOARDDELAY 0,1,2,3 correspond to 250,500,750,1000ms 
			if (keyboardDelay < 0 || keyboardDelay > 3)
				keyboardDelay = 0;

			return (keyboardDelay + 1) * 250;
		}

		private static int GetKeyboardSpeed()
		{
			int keyboardSpeed = SystemInformation.KeyboardSpeed;

			// SPI_GETKEYBOARDSPEED 0,...,31 correspond to 1000/2.5=400,...,1000/30 ms
			if (keyboardSpeed < 0 || keyboardSpeed > 31)
				keyboardSpeed = 31;

			return (31 - keyboardSpeed) * (400 - 1000 / 30) / 31 + 1000 / 30;
		}

		public static int JoyRepeatDelay
		{
			get { return m_joyRepeatDelay; }
			set { m_joyRepeatDelay = value; }
		}

		public static int JoyRepeaSpeed
		{
			get { return m_joyRepeatDelay; }
			set { m_joyRepeatDelay = value; }
		}
	}

	public class KeyEventArgs : EventArgs
	{
		private Keys m_keys;

		public string Name;
		public KeyboardState KeyboardState;
		public Key DIKey;
		public Char Ch;
		public bool IsDown;
		public bool IsCapsLockDown;
		public bool Handled = false;
		public object Tag;

		public KeyEventArgs(Keys keys, Char ch, bool isDown, bool isCapsLockDown, object tag)
		{
			m_keys = keys;
			DIKey = (Key)KeyCodes.VKKeyToDIKey((uint)VKKey);
			Ch = ch;
			IsDown = isDown;
			IsCapsLockDown = isCapsLockDown;
			Tag = tag;
			Name = KeyCodes.VKKeyToString(VKKey);
		}

		public KeyEventArgs(Keys keys, bool isDown, object tag)
		{
			m_keys = keys;
			DIKey = (Key)KeyCodes.VKKeyToDIKey((uint)VKKey);
			IsDown = isDown;
			Tag = tag;
			Ch = (Char)KeyCodes.VKKeyToAscii((uint)VKKey);
			Name = KeyCodes.VKKeyToString(VKKey);
		}

		public KeyEventArgs(Key diKey, bool isDown, KeyboardState keyboardState, object tag)
		{
			DIKey = diKey;
			m_keys = KeyCodes.DIKeyToVKKey((int)diKey);
			IsDown = isDown;
			KeyboardState = keyboardState;
			Tag = tag;
			Ch = (Char)KeyCodes.VKKeyToAscii((uint)VKKey);
			Name = KeyCodes.DIKeyToString((uint)DIKey);
		}

		public Keys Keys
		{
			get { return m_keys; }
		}

		public Keys VKKey
		{
			get { return (Keys)((int)m_keys & ~((int)Keys.Alt | (int)Keys.Control | (int)Keys.Shift)); }
		}

		public bool IsAltDown
		{
			get { return ((m_keys & Keys.Alt) != 0); }
		}

		public bool IsControlDown
		{
			get { return ((m_keys & Keys.Control) != 0); }
		}

		public bool IsShiftDown
		{
			get { return ((m_keys & Keys.Shift) != 0); }
		}
	}

	public partial class Win32
	{
		[DllImport("user32.dll", SetLastError = false)]
		public static extern IntPtr GetDesktopWindow();
	}
}
