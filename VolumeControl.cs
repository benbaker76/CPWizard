// Copyright (c) 2015, Ben Baker
// All rights reserved.
// 
// This source code is licensed under the BSD-style license found in the
// LICENSE file in the root directory of this source tree. 

using System;
using System.Runtime.InteropServices;

namespace CPWizard
{
	partial class Win32
	{
		public const int MMSYSERR_NOERROR = 0;
		public const int MAXPNAMELEN = 32;
		public const int MIXER_LONG_NAME_CHARS = 64;
		public const int MIXER_SHORT_NAME_CHARS = 16;
		public const int MIXER_GETLINEINFOF_COMPONENTTYPE = 0x3;
		public const int MIXER_GETCONTROLDETAILSF_VALUE = 0x0;
		public const int MIXER_GETLINECONTROLSF_ONEBYTYPE = 0x2;
		public const int MIXER_SETCONTROLDETAILSF_VALUE = 0x0;
		public const int MIXERLINE_COMPONENTTYPE_DST_FIRST = 0x0;
		public const int MIXERLINE_COMPONENTTYPE_SRC_FIRST = 0x1000;
		public const int MIXERLINE_COMPONENTTYPE_DST_SPEAKERS = (MIXERLINE_COMPONENTTYPE_DST_FIRST + 4);
		public const int MIXERLINE_COMPONENTTYPE_SRC_MICROPHONE = (MIXERLINE_COMPONENTTYPE_SRC_FIRST + 3);
		public const int MIXERLINE_COMPONENTTYPE_SRC_LINE = (MIXERLINE_COMPONENTTYPE_SRC_FIRST + 2);
		public const int MIXERCONTROL_CT_CLASS_FADER = 0x50000000;
		public const int MIXERCONTROL_CT_UNITS_UNSIGNED = 0x30000;
		public const int MIXERCONTROL_CONTROLTYPE_FADER = (MIXERCONTROL_CT_CLASS_FADER | MIXERCONTROL_CT_UNITS_UNSIGNED);
		public const int MIXERCONTROL_CONTROLTYPE_VOLUME = (MIXERCONTROL_CONTROLTYPE_FADER + 1);

		[DllImport("winmm.dll", CharSet = CharSet.Ansi)]
		public static extern int mixerClose(int hmx);
		[DllImport("winmm.dll", CharSet = CharSet.Ansi)]
		public static extern int mixerGetControlDetailsA(int hmxobj, ref MIXERCONTROLDETAILS pmxcd, int fdwDetails);
		[DllImport("winmm.dll", CharSet = CharSet.Ansi)]
		public static extern int mixerGetDevCapsA(int uMxId, MIXERCAPS pmxcaps, int cbmxcaps);
		[DllImport("winmm.dll", CharSet = CharSet.Ansi)]
		public static extern int mixerGetID(int hmxobj, int pumxID, int fdwId);
		[DllImport("winmm.dll", CharSet = CharSet.Ansi)]
		public static extern int mixerGetLineControlsA(int hmxobj, ref MIXERLINECONTROLS pmxlc, int fdwControls);
		[DllImport("winmm.dll", CharSet = CharSet.Ansi)]
		public static extern int mixerGetLineInfoA(int hmxobj, ref MIXERLINE pmxl, int fdwInfo);
		[DllImport("winmm.dll", CharSet = CharSet.Ansi)]
		public static extern int mixerGetNumDevs();
		[DllImport("winmm.dll", CharSet = CharSet.Ansi)]
		public static extern int mixerMessage(int hmx, int uMsg, int dwParam1, int dwParam2);
		[DllImport("winmm.dll", CharSet = CharSet.Ansi)]
		public static extern int mixerOpen(out int phmx, int uMxId, int dwCallback, int dwInstance, int fdwOpen);
		[DllImport("winmm.dll", CharSet = CharSet.Ansi)]
		public static extern int mixerSetControlDetails(int hmxobj, ref MIXERCONTROLDETAILS pmxcd, int fdwDetails);

		public struct MIXERCAPS
		{
			public int wMid;
			public int wPid;
			public int vDriverVersion;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAXPNAMELEN)]
			public string szPname;
			public int fdwSupport;
			public int cDestinations;
		}

		public struct MIXERCONTROL
		{
			public int cbStruct;
			public int dwControlID;
			public int dwControlType;
			public int fdwControl;
			public int cMultipleItems;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = MIXER_SHORT_NAME_CHARS)]
			public string szShortName;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = MIXER_LONG_NAME_CHARS)]
			public string szName;
			public int lMinimum;
			public int lMaximum;
			[MarshalAs(UnmanagedType.U4, SizeConst = 10)]
			public int reserved;
		}

		public struct MIXERCONTROLDETAILS
		{
			public int cbStruct;
			public int dwControlID;
			public int cChannels;
			public int item;
			public int cbDetails;
			public IntPtr paDetails;
		}

		public struct MIXERCONTROLDETAILS_UNSIGNED
		{
			public int dwValue;
		}

		public struct MIXERLINE
		{
			public int cbStruct;
			public int dwDestination;
			public int dwSource;
			public int dwLineID;
			public int fdwLine;
			public int dwUser;
			public int dwComponentType;
			public int cChannels;
			public int cConnections;
			public int cControls;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = MIXER_SHORT_NAME_CHARS)]
			public string szShortName;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = MIXER_LONG_NAME_CHARS)]
			public string szName;
			public int dwType;
			public int dwDeviceID;
			public int wMid;
			public int wPid;
			public int vDriverVersion;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAXPNAMELEN)]
			public string szPname;
		}

		public struct MIXERLINECONTROLS
		{
			public int cbStruct;
			public int dwLineID;
			public int dwControl;
			public int cControls;
			public int cbmxctrl;
			public IntPtr pamxctrl;
		}
	}

	public class VolumeControl
	{
		private static bool GetVolumeControl(int hmixer, int componentType, int ctrlType, out Win32.MIXERCONTROL mxc, out int vCurrentVol)
		{
			bool retValue = false;

			mxc = new Win32.MIXERCONTROL();
			vCurrentVol = -1;

			try
			{
				// This function attempts to obtain a mixer control.
				// Returns True if successful.
				Win32.MIXERLINECONTROLS mxlc = new Win32.MIXERLINECONTROLS();
				Win32.MIXERLINE mxl = new Win32.MIXERLINE();
				Win32.MIXERCONTROLDETAILS pmxcd = new Win32.MIXERCONTROLDETAILS();
				Win32.MIXERCONTROLDETAILS_UNSIGNED du = new Win32.MIXERCONTROLDETAILS_UNSIGNED();

				int rc;

				mxl.cbStruct = Marshal.SizeOf(mxl);
				mxl.dwComponentType = componentType;

				rc = Win32.mixerGetLineInfoA(hmixer, ref mxl, Win32.MIXER_GETLINEINFOF_COMPONENTTYPE);

				if (Win32.MMSYSERR_NOERROR == rc)
				{
					int sizeofMIXERCONTROL = 152;
					int ctrl = Marshal.SizeOf(typeof(Win32.MIXERCONTROL));
					IntPtr ptrMIXERCONTROL = Marshal.AllocCoTaskMem(sizeofMIXERCONTROL);
					mxlc.pamxctrl = ptrMIXERCONTROL;
					mxlc.cbStruct = Marshal.SizeOf(mxlc);
					mxlc.dwLineID = mxl.dwLineID;
					mxlc.dwControl = ctrlType;
					mxlc.cControls = 1;
					mxlc.cbmxctrl = sizeofMIXERCONTROL;

					// Allocate a buffer for the control
					mxc.cbStruct = sizeofMIXERCONTROL;

					// Get the control
					rc = Win32.mixerGetLineControlsA(hmixer, ref mxlc, Win32.MIXER_GETLINECONTROLSF_ONEBYTYPE);

					if (Win32.MMSYSERR_NOERROR == rc)
					{
						retValue = true;

						// Copy the control into the destination structure
						mxc = (Win32.MIXERCONTROL)Marshal.PtrToStructure(mxlc.pamxctrl, typeof(Win32.MIXERCONTROL));
					}
					else
					{
						retValue = false;
					}
					int sizeofMIXERCONTROLDETAILS = Marshal.SizeOf(typeof(Win32.MIXERCONTROLDETAILS));
					int sizeofMIXERCONTROLDETAILS_UNSIGNED = Marshal.SizeOf(typeof(Win32.MIXERCONTROLDETAILS_UNSIGNED));
					pmxcd.cbStruct = sizeofMIXERCONTROLDETAILS;
					pmxcd.dwControlID = mxc.dwControlID;
					IntPtr ptrMIXERCONTROLDETAILS_UNSIGNED = Marshal.AllocCoTaskMem(sizeofMIXERCONTROLDETAILS_UNSIGNED);
					pmxcd.paDetails = ptrMIXERCONTROLDETAILS_UNSIGNED;
					pmxcd.cChannels = 1;
					pmxcd.item = 0;
					pmxcd.cbDetails = sizeofMIXERCONTROLDETAILS_UNSIGNED;

					rc = Win32.mixerGetControlDetailsA(hmixer, ref pmxcd, Win32.MIXER_GETCONTROLDETAILSF_VALUE);

					du = (Win32.MIXERCONTROLDETAILS_UNSIGNED)Marshal.PtrToStructure(pmxcd.paDetails, typeof(Win32.MIXERCONTROLDETAILS_UNSIGNED));

					vCurrentVol = du.dwValue;

					Marshal.FreeCoTaskMem(ptrMIXERCONTROL);
					Marshal.FreeCoTaskMem(ptrMIXERCONTROLDETAILS_UNSIGNED);

					return retValue;
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("GetVolumeControl", "VolumeControl", ex.Message, ex.StackTrace);
			}

			return retValue;
		}

		private static bool SetVolumeControl(int hmixer, Win32.MIXERCONTROL mxc, int volume)
		{
			try
			{
				// This function sets the value for a volume control.
				// Returns True if successful

				Win32.MIXERCONTROLDETAILS mxcd = new Win32.MIXERCONTROLDETAILS();
				Win32.MIXERCONTROLDETAILS_UNSIGNED vol = new
				Win32.MIXERCONTROLDETAILS_UNSIGNED();

				mxcd.item = 0;
				mxcd.dwControlID = mxc.dwControlID;
				mxcd.cbStruct = Marshal.SizeOf(mxcd);
				mxcd.cbDetails = Marshal.SizeOf(vol);

				// Allocate a buffer for the control value buffer
				mxcd.cChannels = 1;
				vol.dwValue = volume;

				// Copy the data into the control value buffer
				IntPtr ptrMIXERCONTROLDETAILS_UNSIGNED = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(Win32.MIXERCONTROLDETAILS_UNSIGNED)));
				mxcd.paDetails = ptrMIXERCONTROLDETAILS_UNSIGNED;
				Marshal.StructureToPtr(vol, mxcd.paDetails, false);

				// Set the control value
				int retValue = Win32.mixerSetControlDetails(hmixer, ref mxcd, Win32.MIXER_SETCONTROLDETAILSF_VALUE);

				Marshal.FreeCoTaskMem(ptrMIXERCONTROLDETAILS_UNSIGNED);

				if (retValue == Win32.MMSYSERR_NOERROR)
					return true;
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("SetVolumeControl", "VolumeControl", ex.Message, ex.StackTrace);
			}

			return false;
		}

		public static int GetVolume()
		{
			try
			{
				int mixer;
				Win32.MIXERCONTROL volCtrl = new Win32.MIXERCONTROL();
				int currentVol;
				Win32.mixerOpen(out mixer, 0, 0, 0, 0);
				int type = Win32.MIXERCONTROL_CONTROLTYPE_VOLUME;
				GetVolumeControl(mixer, Win32.MIXERLINE_COMPONENTTYPE_DST_SPEAKERS, type, out volCtrl, out currentVol);
				Win32.mixerClose(mixer);

				return (int)Math.Round((((float)currentVol / (float)volCtrl.lMaximum) * 100f));
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("GetVolume", "VolumeControl", ex.Message, ex.StackTrace);
			}

			return -1;
		}

		public static void SetVolume(int vVolume)
		{
			try
			{
				int mixer;
				Win32.MIXERCONTROL volCtrl = new Win32.MIXERCONTROL();
				int currentVol;
				Win32.mixerOpen(out mixer, 0, 0, 0, 0);
				int type = Win32.MIXERCONTROL_CONTROLTYPE_VOLUME;
				GetVolumeControl(mixer, Win32.MIXERLINE_COMPONENTTYPE_DST_SPEAKERS, type, out volCtrl, out currentVol);

				vVolume = (int)Math.Round((((float)vVolume / 100f) * (float)volCtrl.lMaximum));

				SetVolumeControl(mixer, volCtrl, vVolume);
				GetVolumeControl(mixer, Win32.MIXERLINE_COMPONENTTYPE_DST_SPEAKERS, type, out volCtrl, out currentVol);

				Win32.mixerClose(mixer);
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("SetVolume", "VolumeControl", ex.Message, ex.StackTrace);
			}
		}
	}
}