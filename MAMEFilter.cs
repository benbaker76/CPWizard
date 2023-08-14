// Copyright (c) 2015, Ben Baker
// All rights reserved.
// 
// This source code is licensed under the BSD-style license found in the
// LICENSE file in the root directory of this source tree. 

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;

namespace CPWizard
{
	// Total Machines:  ~16,000
	// Machines with E.M. filtered off: ~12,000
	// Machines with E.M., Bios, Mahjong and Gambling filtered off:  ~9,000
	// Machines with the above filtered off and preliminary (unplayable) drivers:  ~7,000
	// The above filters with clones filtered off:  ~3200
	// 
	// Filtering:
	// 1. Bios and E.M. both have flags ✓
	// 2. Consoles / PCs / Handhelds (MESS)
	// 	- First look for a device named "software_list" and ✓
	// 	- Does the input section mention coins ✓
	// NOTE: This isn't super accurate unless to intended to filter off E.M. games as well. At least by determining if there is a coin slot it accurately removes all non-arcade games.  
	// 3. Mahjong and Hanafuda
	// 	- Look for controls with the type set to "mahjong" or "hanafuda" ✓
	// NOTE: This is 99% accurate, but there are a few mahjong games that use standard joystick controls 
	// 4. Slots
	// 	- Look for the control type "gambling" ✓
	// NOTE: This is maybe 75% accurate as there isn't a huge amount of standardization in those games. 
	// 5. Filter games with emulation status set to "preliminary" because those seldom work. ✓
	// NOTE: This doesn't get rid of all non-working games but it is the best a person can do
	// 6. We can't filter off "imperfect" games because games that use samples are classified as imperfect
	// 7. Bad dumps can't be filtered off either because games with bad dumps often work just fine. It's an inconsequential rom that has the issue.  
	// 8. Clones
	// 	- Look for the "cloneof" flag in the machine entry. If it exists at all it's a clone.  
	// 9. Video poker is hard to filter
	// 	- Search the dip switches for "bet"?
	// 	- Look for hopper?
	// NOTE: Strangely enough some of these machines don't appear to pay out.

	// ========================================================================================================
	// Show Vert/Horiz					ShowHorizVert		Filter the entire Games List for all available lists to just show Horizontal or Vertical Games.
	// Only Working Games				OnlyWorking			Only list MAME games where the driver is known to be fully working. In addition AllowImperfect allows a driver status of IMPERFECT to pass as working. In newer versions of MAME, games such as Galaga havea Driver Status as IMPERFECT although run very well. Set AllowImperfect to True to allow these games not to be filtered when OnlyWorking is set to True. Default for AllowImperfect is True.
	// Allow Imperfect Games			AllowImperfect		Only list MAME games where the driver is known to be fully working. In addition AllowImperfect allows a driver status of IMPERFECT to pass as working. In newer versions of MAME, games such as Galaga havea Driver Status as IMPERFECT although run very well. Set AllowImperfect to True to allow these games not to be filtered when OnlyWorking is set to True. Default for AllowImperfect is True.
	// Verify ROMS						VerifyROMs			NAME?, and don't include if bad. This makes updating thegame list slow.
	// Only Existing ROMs				OnlyROMs			Only list MAME games that you have on your hard disk rom directory.
	// No Bracketed text				NoGameInfo			Dont Display game Version (Bracketed) info in the game list.For example if set to true '720 Degrees (rev 1)' Will appear in the list as '720 Degrees'. This is useful if running at a low resolution, as text won't get truncated. Use in conjuction with NoClones=True, tocreate a very clean looking list.
	// Use Game Filter					GameFilterOn		Apply a filter to the MAME game list. Seperate words to filter on witha semi colan. Any game name descriptions that contain these words will not be shown in the game list if the GameFilterOn setting is enabled.
	// Game Filter						GameFilter			Apply a filter to the MAME game list. Seperate words to filter on witha semi colan. Any game name descriptions that contain these words will not be shown in the game list if the GameFilterOn setting is enabled.
	// Dont Filter these ROMS			DoNotFilterROMs		Never filter the following ROMs (They will always appear in the list) Seperate with a semi colan.
	// No Adult Games					NoAdult				The following option when set to True will NOT add Adult Games to the list, and these games will not be available from the front end after doing an update list. Default is true.
	// No Mahjong Games					NoMahjong			The following option when set to True will NOT add Mahjong Games to the list, and these games will not be available from the front end after doing an update list. Default is true.
	// No Casino Games					NoCasino			The following option when set to True will NOT add Casino Games to the list, and these games will not be available from the front end after doing an update list. Default is true.
	// No Clones						NoClones			Don't add clones to the list if True. Default is false. Use in conjuction with NoGameInfo=True tocreate a very clean looking list.
	// Show All MAME Games				ShowAllMAME			Show a list of All MAME Games.
	// Show MultiPlay Games				ShowMulti			Show a seperate list of simultantaneous Multiplayer MAME Games.
	// Show 4 PLAYER Games				Show4Player			Show a seperate list of simultantaneous 4 Player MAME Games.
	// Show MAME CHD Games				ShowCHD				Show a seperate list of MAME CHD games.
	// Show MAME Horizontal Games		ShowHorizontal		Allow a list of MAME games that initially were on monitors running Horizontal orientation to be selected from the Start Page.
	// Show MAME Vertical Games			ShowVertical		Allow a list of MAME games that initially were on monitors running Vertical orientation to be selected from the Start Page.
	// Show MAME Vector Games			ShowVector			Allow a list of MAME Vector games to be selected from the Start Page, Default is true.
	// Show MAME NEO GEO Games			ShowNeoGeo			Show a seperate list of Neo-Geo games, selectable from the Start Page. Default is true.
	// Show MAME Originals				ShowOriginal		Show a seperate list of just MAME original games (Parent ROMs), selectable from the Start Page. Default is false.
	// Show Adult Games					ShowAdult			Show a seperate list of just MAME Adult Games, selectable from the Start Page. Default is false.
	// Show MAME CPS Games				ShowCPS				Show a seperate list of Capcom, selectable from the Start Page. Default is true.
	// Show MAME Atari Games			ShowAtari			Show a seperate list of games produced/licensed by Atari, selectable from the Start Page. Default is true.
	// Show MAME Golden Era				ShowGolden			Show a seperate list of games produced/licensed from 1980 to 1989, selectable from the Start Page. Default is true.
	// Show LightGun Games				ShowLightGun		Show a seperate list of games that used a lightgun, selectable from the Start Page. Default is true.
	// Show Trackball Games				ShowTrackball		Show a seperate list of games that used a Trackball, selectable from the Start Page. Default is true.
	// Show Dial/Spinner Games			ShowSpinner			Show a seperate list of games that used a Dial or Spinner, selectable from the Start Page. Default is true.
	// Show Not Played Games			ShowNotPlayed		Show a seperate list of MAME games that have not been played.
	// Show Live Online Lists			ShowOnlineLists		Registered users can have online live MAME lists of most played in GameEx and MAWS Hall of Fame.
	// Show Model 2 Games				ShowModel2			Show a seperate list of games that run on Sega Model 2 hardware used in conjuction with the Model 2 Emulator, selectable from the Start Page. Default is true.
	// Excluded ROMs					ExcludedROMs		These ROMS will not display in MAME lists, they are normally added here by pressing delete in GameEx.
	// ========================================================================================================

	[Serializable]
	public class MAMEFilterNode
	{
		public string Name;

		public MAMEFilterNode()
		{
		}

		public MAMEFilterNode(string name)
		{
			Name = name;
		}
	}

	public enum FilterRotationOptions
	{
		ShowAllGames,
		ShowOnlyHorizonalGames,
		ShowOnlyVerticalGames,
		OnlyVerticalCocktailGames,
		AutoBasedOnDisplay
	};

	[Serializable]
	[XmlType("Filter")]
	[XmlRoot("Filter")]
	public class MAMEFilter
	{
		public static string[] FilterRotationArray =
		{
			"Show All Games",
			"Show Only Horizonal Games",
			"Show Only Vertical Games",
			"Only Vertical Cocktail Games",
			"Auto Based On Display"
		};

		public bool NoClones;
		public bool NoClonesUnlessNameDifferent;
		public bool NoBios;
		public bool NoDevice;
		public bool NoAdult;
		public bool NoMahjong;
		public bool NoGambling;
		public bool NoCasino;
		public bool NoMechanical;
		public bool NoReels;
		public bool NoUtilities;
		public bool NoNotClassified;
		public bool RunnableOnly;
		public bool ArcadeOnly;
		public bool NoSystemExceptChd;
		public bool NoPreliminary;
		public bool NoImperfect;

		public FilterRotationOptions FilterRotation;

		[XmlArray("NameIncludes")]
		[XmlArrayItem("Include")]
		public List<MAMEFilterNode> NameIncludeList;

		[XmlArray("DescriptionExcludes")]
		[XmlArrayItem("Exclude")]
		public List<MAMEFilterNode> DescriptionExcludeList;

		public string SetDefaultsButton;

		public MAMEFilter()
		{
		}

		public void SetDefaults()
		{
			NoClones = false;
			NoClonesUnlessNameDifferent = false;
			NoBios = true;
			NoDevice = true;
			NoAdult = true;
			NoMahjong = true;
			NoGambling = true;
			NoCasino = true;
			NoMechanical = true;
			NoReels = true;
			NoUtilities = true;
			NoNotClassified = true;
			RunnableOnly = true;
			ArcadeOnly = true;
			NoSystemExceptChd = true;
			NoPreliminary = true;
			NoImperfect = false;

			FilterRotation = FilterRotationOptions.ShowAllGames;

			NameIncludeList = new List<MAMEFilterNode>()
			{
				new MAMEFilterNode("pacman"),
				new MAMEFilterNode("splatter")
			};

			DescriptionExcludeList = new List<MAMEFilterNode>()
			{
				new MAMEFilterNode("japan"),
				new MAMEFilterNode("korea"),
				new MAMEFilterNode("asia"),
				new MAMEFilterNode("hispanic"),
				new MAMEFilterNode("bootleg"),
				new MAMEFilterNode("french"),
				new MAMEFilterNode("german"),
				new MAMEFilterNode("hack"),
				new MAMEFilterNode("prototype"),
				new MAMEFilterNode("hardware"),
				new MAMEFilterNode("spanish"),
				new MAMEFilterNode("(easy"),
				new MAMEFilterNode("(harder")
			};
		}

		public bool ShouldRemove(MAMEMachineNode machineNode, bool removeBad)
		{
			if (NameIncludeList != null)
				if (NameIncludeList.FindIndex(x => x.Name.Equals(machineNode.Name, StringComparison.OrdinalIgnoreCase)) != -1)
					return false;

			if (DescriptionExcludeList != null)
				if (DescriptionExcludeList.FindIndex(x => StringTools.ContainsString(machineNode.Description, x.Name, true)) != -1)
					return true;

			if (removeBad && machineNode.Status == MAMEStatusType.Bad)
				return true;

			if (NoBios && machineNode.IsBios)
				return true;

			if (NoMechanical && machineNode.IsMechanical)
				return true;

			if (NoDevice && machineNode.IsDevice)
				return true;

			if (RunnableOnly && !machineNode.Runnable)
				return true;

			if (NoPreliminary && machineNode.Driver.Emulation == MAMEDriverStatus.Preliminary)
				return true;

			if (NoImperfect && machineNode.Driver.Emulation == MAMEDriverStatus.Imperfect)
				return true;

			if (ArcadeOnly && !machineNode.IsArcade)
				return true;

			if (NoMahjong && machineNode.IsMahjong)
				return true;

			if (NoGambling && machineNode.IsGambling)
				return true;

			if (NoSystemExceptChd && (machineNode.IsSystem && !machineNode.IsChd))
				return true;

			if (machineNode.CatVer != null)
			{
				string genre = machineNode.CatVer.Genre;
				string category = machineNode.CatVer.Category;
				bool isMature = machineNode.CatVer.IsMature;

				if (genre != null)
				{
					if (NoCasino && genre.Equals("Casino"))
						return true;

					if (NoMechanical && genre.Equals("Electromechanical"))
						return true;

					if (NoUtilities && genre.Equals("Utilities"))
						return true;

					if (NoNotClassified && genre.Equals("Not Classified"))
						return true;
				}

				if (category != null)
				{
					if (NoReels && category.Equals("Reels"))
						return true;

					if (NoBios && category.Equals("BIOS"))
						return true;
				}

				if (NoAdult && isMature)
					return true;
			}

			if (!machineNode.IsParent)
			{
				if (NoClones)
					return true;

				if (NoClonesUnlessNameDifferent)
				{
					if (StringTools.RemoveVersionNumber(StringTools.RemoveBrackets(machineNode.Description)) == StringTools.RemoveVersionNumber(StringTools.RemoveBrackets(machineNode.Parent.Description)))
						return true;
				}
			}

			return false;
		}

		public static bool TryLoadXml(string fileName, out MAMEFilter mameFilter)
		{
			mameFilter = null;

			if (!File.Exists(fileName))
				return false;

			try
			{
				mameFilter = Serializer.Load<MAMEFilter>(fileName);

				return true;
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("TryLoadXml", "MAMEFilter", ex.Message, ex.StackTrace);
			}

			return false;
		}

		public static bool TrySaveXml(string fileName, MAMEFilter mameFilter)
		{
			try
			{
				Serializer.Save<MAMEFilter>(fileName, mameFilter);

				return true;
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("TrySaveXml", "MAMEFilter", ex.Message, ex.StackTrace);
			}

			return false;
		}
	}
}
