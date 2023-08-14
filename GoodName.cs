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
using System.Text.RegularExpressions;

namespace CPWizard
{
	public enum SpecialCodeType
	{
		None,
		Gameboy,
		SuperNintendo,
		Atari,
		Genesis,
		ThomsonMO5,
		Coleco,
		Nintendo,
		NeoGeoPocket
	}

	class CodeNode
	{
		public string Name = null;
		public string Description = null;
		public Regex RegEx = null;
		public Bitmap Icon = null;
		public SpecialCodeType SpecialCode = SpecialCodeType.None;

		public CodeNode(string name, string description, string iconFileName)
		{
			Name = name;
			Description = description;
			Icon = FileIO.LoadImage(iconFileName);
		}

		public CodeNode(string name, string regex, string description, string iconFileName)
		{
			Name = name;
			RegEx = new Regex(regex);
			Description = description;
			Icon = FileIO.LoadImage(iconFileName);
		}

		public CodeNode(string name, string description, string iconFileName, SpecialCodeType specialCode)
		{
			Name = name;
			Description = description;
			SpecialCode = specialCode;
			Icon = FileIO.LoadImage(iconFileName);
		}

		public bool IsMatch(string name)
		{
			if (RegEx != null)
				return RegEx.IsMatch(name);

			if (Name == name)
				return true;

			return false;
		}
	}

	class GoodName
	{
		private CodeNode[] StandardCodes =
        {
            new CodeNode("[!]", "Verified Good Dump", Path.Combine(Settings.Folders.Icons, "[!].png")),
            new CodeNode("[a]", "Alternate to the original dump often done by another dumping group.", Path.Combine(Settings.Folders.Icons, "[a].png")),
            new CodeNode("[o]", "Overdump of a rom which has too much space allocated for it making it too big.", Path.Combine(Settings.Folders.Icons, "[o].png")),
            new CodeNode("[p]", "Pirate rom released without consent or stolen from the original dumping group.", Path.Combine(Settings.Folders.Icons, "[p].png")),
            new CodeNode("[b]", "Bad Dump of a rom that probably doesn't work properly", Path.Combine(Settings.Folders.Icons, "[b].png")),
            new CodeNode("[f]", "Fixed to run better on emulators or bug fixed.", Path.Combine(Settings.Folders.Icons, "[f].png")),
            new CodeNode("[h]", "Hack of a rom that has possibly an intro added or graphics changed.", Path.Combine(Settings.Folders.Icons, "[h].png")),
            new CodeNode("[t]", "Trained rom that has a small bit of code that lets you cheat in the game.", Path.Combine(Settings.Folders.Icons, "[t].png")),
            new CodeNode("[T]", "Translation of a rom from one language to another.", Path.Combine(Settings.Folders.Icons, "[Tr].png")),
            new CodeNode("(-)", "Unknown Year", Path.Combine(Settings.Folders.Icons, "(-).png")),
            new CodeNode("(M#)", @"\(M[0-9]\)", "Multilanguage (# of Languages)", Path.Combine(Settings.Folders.Icons, ".png")),
            new CodeNode("(###)", @"\([0-9]\)", "Checksum", Path.Combine(Settings.Folders.Icons, ".png")),
            new CodeNode("(??k)", @"\([0-9]k\)", "ROM Size", Path.Combine(Settings.Folders.Icons, ".png")),
            new CodeNode("ZZZ_", "Unclassified", Path.Combine(Settings.Folders.Icons, ".png")),
            new CodeNode("(Unl)", "Unlicensed", Path.Combine(Settings.Folders.Icons, ".png")),
            new CodeNode("(-)", "Unknown Year", Path.Combine(Settings.Folders.Icons, ".png")),
        };

		private CodeNode[] CountryCodes =
        {
            new CodeNode("(1)", "Japan & Korea", Path.Combine(Settings.Folders.Flags, "(1).png")),
            new CodeNode("(4)", "USA & Brazil NTSC", Path.Combine(Settings.Folders.Flags, "(4).png")),
            new CodeNode("(A)", "Australia", Path.Combine(Settings.Folders.Flags, "(A).png")),
            new CodeNode("(B)", "non USA (GoodGen Only)", Path.Combine(Settings.Folders.Flags, "(B).png")),
            new CodeNode("(C)", "China", Path.Combine(Settings.Folders.Flags, "(C).png")),
            new CodeNode("(E)", "Europe", Path.Combine(Settings.Folders.Flags, "(E).png")),
            new CodeNode("(F)", "France", Path.Combine(Settings.Folders.Flags, "(F).png")),
            new CodeNode("(F)", "World (GoodGen Only)", Path.Combine(Settings.Folders.Flags, "(F).png")),
            new CodeNode("(FC)", "French Canadian", Path.Combine(Settings.Folders.Flags, "(FC).png")),
            new CodeNode("(FN)", "Finland", Path.Combine(Settings.Folders.Flags, "(FN).png")),
            new CodeNode("(G)", "Germany", Path.Combine(Settings.Folders.Flags, "(G).png")),
            new CodeNode("(GR)", "Greece", Path.Combine(Settings.Folders.Flags, "(GR).png")),
            new CodeNode("(H)", "Holland", Path.Combine(Settings.Folders.Flags, "(H).png")),
            new CodeNode("(HK)", "Hong Kong", Path.Combine(Settings.Folders.Flags, "(HK).png")),
            new CodeNode("(I)", "Italy", Path.Combine(Settings.Folders.Flags, "(I).png")),
            new CodeNode("(J)", "Japan", Path.Combine(Settings.Folders.Flags, "(J).png")),
            new CodeNode("(K)", "Korea", Path.Combine(Settings.Folders.Flags, "(K).png")),
            new CodeNode("(NL)", "Netherlands", Path.Combine(Settings.Folders.Flags, "(NL).png")),
            new CodeNode("(PD)", "Public Domain", Path.Combine(Settings.Folders.Flags, "(PD).png")),
            new CodeNode("(S)", "Spain", Path.Combine(Settings.Folders.Flags, "(S).png")),
            new CodeNode("(SW)", "Sweden", Path.Combine(Settings.Folders.Flags, "(SW).png")),
            new CodeNode("(U)", "USA", Path.Combine(Settings.Folders.Flags, "(U).png")),
            new CodeNode("(UK)", "England", Path.Combine(Settings.Folders.Flags, "(UK).png")),
            new CodeNode("(Unk)", "Unknown Country", Path.Combine(Settings.Folders.Flags, "(Unk).png")),
            new CodeNode("(Unl)", "Unlicensed", Path.Combine(Settings.Folders.Flags, "(Unl).png"))
        };

		private CodeNode[] SpecialCodes =
        {
            new CodeNode("(C)", "Gameboy Colour Version", Path.Combine(Settings.Folders.Icons, "(C).png"), SpecialCodeType.Gameboy),
            new CodeNode("(S)", "Super Gameboy Enhanced Version", Path.Combine(Settings.Folders.Icons, "(S).png"), SpecialCodeType.Gameboy),
            new CodeNode("(BF)", "Fixed to work on programmable Gameboy cartridges released by a company called Bung", Path.Combine(Settings.Folders.Icons, "(BF).png"), SpecialCodeType.Gameboy),
            new CodeNode("(BS)", "Broadcast Satellaview game roms.", Path.Combine(Settings.Folders.Icons, "(BS).png"), SpecialCodeType.SuperNintendo),
            new CodeNode("(ST)", "Sufami Turbo interface compatible roms.", Path.Combine(Settings.Folders.Icons, "(ST).png"), SpecialCodeType.SuperNintendo),
            new CodeNode("(NP)", "Nintendo Power subscriber only games.", Path.Combine(Settings.Folders.Icons, "(NP).png"), SpecialCodeType.SuperNintendo),
            new CodeNode("(PAL)", "Euro Version", Path.Combine(Settings.Folders.Icons, "(PAL).png"), SpecialCodeType.Atari),
            new CodeNode("(1)", "Japan & Korean Game", Path.Combine(Settings.Folders.Icons, "(1).png"), SpecialCodeType.Genesis),
            new CodeNode("(4)", "USA & Brazil Only Game", Path.Combine(Settings.Folders.Icons, "(4).png"), SpecialCodeType.Genesis),
            new CodeNode("(5)", "NTSC Only Game", Path.Combine(Settings.Folders.Icons, "(5).png"), SpecialCodeType.Genesis),
            new CodeNode("(8)", "PAL Only Game", Path.Combine(Settings.Folders.Icons, "(8).png"), SpecialCodeType.Genesis),
            new CodeNode("(B)", "Runs on any non USA Genesis", Path.Combine(Settings.Folders.Icons, "(B).png"), SpecialCodeType.Genesis),
            new CodeNode("[c]", "Faulty checksum routine", Path.Combine(Settings.Folders.Icons, "[c].png"), SpecialCodeType.Genesis),
            new CodeNode("[x]", "Bad Checksum", Path.Combine(Settings.Folders.Icons, "[x].png"), SpecialCodeType.Genesis),
            new CodeNode("[R-]", "Countries for use in.", Path.Combine(Settings.Folders.Icons, "[R-].png"), SpecialCodeType.Genesis),
            new CodeNode("(Y)", "Year unknown", Path.Combine(Settings.Folders.Icons, "(Y).png"), SpecialCodeType.ThomsonMO5),
            new CodeNode("(Adam)", "ADAM computer version", Path.Combine(Settings.Folders.Icons, "(Adam).png"), SpecialCodeType.Coleco),
            new CodeNode("(PC10)", "PlayChoice 10 arcade system rom (Mame supports these)", Path.Combine(Settings.Folders.Icons, "(PC10).png"), SpecialCodeType.Nintendo),
            new CodeNode("(VS)", "Versus system arcade rom (Mame supports these)", Path.Combine(Settings.Folders.Icons, "(VS).png"), SpecialCodeType.Nintendo),
            new CodeNode("[M]", "Mono Only", Path.Combine(Settings.Folders.Icons, "[M].png"), SpecialCodeType.NeoGeoPocket)
        };
	}

	class TOSEC
	{
		private CodeNode[] CountryCodes =
        {
            new CodeNode("(Eu)", "European", Path.Combine(Settings.Folders.Flags, "(Eu).png")),
            new CodeNode("(Au)", "Australia", Path.Combine(Settings.Folders.Flags, "(Au).png")),
            new CodeNode("(Br)", "Brazil", Path.Combine(Settings.Folders.Flags, "(Br).png")),
            new CodeNode("(Jp)", "Japan / Japanese", Path.Combine(Settings.Folders.Flags, "(Jp).png")),
            new CodeNode("(HK)", "Hong Kong", Path.Combine(Settings.Folders.Flags, "(HK).png")),
            new CodeNode("(UK)", "United Kingdom (English)", Path.Combine(Settings.Folders.Flags, "(UK).png")),
            new CodeNode("(US)", "United States (English)", Path.Combine(Settings.Folders.Flags, "(US).png")),
            new CodeNode("(En)", "English (non-country specific)", Path.Combine(Settings.Folders.Flags, "(En).png")),
            new CodeNode("(Cz)", "Czech Republic / Czechoslovakian", Path.Combine(Settings.Folders.Flags, "(Cz).png")),
            new CodeNode("(De)", "Germany / German", Path.Combine(Settings.Folders.Flags, "(De).png")),
            new CodeNode("(Es)", "Spain / Spanish", Path.Combine(Settings.Folders.Flags, "(Es).png")),
            new CodeNode("(Fr)", "France / French", Path.Combine(Settings.Folders.Flags, "(Fr).png")),
            new CodeNode("(It)", "Italy / Italian", Path.Combine(Settings.Folders.Flags, "(It).png")),
            new CodeNode("(Nl)", "Netherlands / Dutch", Path.Combine(Settings.Folders.Flags, "(Nl).png")),
            new CodeNode("(Se)", "Sweden / Swedish", Path.Combine(Settings.Folders.Flags, "(Se).png")),
            new CodeNode("(Pl)", "Poland", Path.Combine(Settings.Folders.Flags, "(Pl).png")),
            new CodeNode("(Tr)", "Turkey / Turkish", Path.Combine(Settings.Folders.Flags, "(Tr).png"))
        };

		private CodeNode[] DumpInformationFlags =
        {
            new CodeNode("[a]", "Alternate version", Path.Combine(Settings.Folders.Icons, "[a].png")),
            new CodeNode("[b]", "Bad dump", Path.Combine(Settings.Folders.Icons, "[b].png")),
            new CodeNode("[cr]", "Cracked", Path.Combine(Settings.Folders.Icons, "[cr].png")),
            new CodeNode("[cr Crack Group]", @"\[cr *\]", "Cracked by Crack Group", Path.Combine(Settings.Folders.Icons, "[cr].png")),
            new CodeNode("[f]", "Fixed", Path.Combine(Settings.Folders.Icons, "[f].png")),
            new CodeNode("[f save]", "Fixed to allow saving", Path.Combine(Settings.Folders.Icons, "[f].png")),
            new CodeNode("[f copier]", "Fixed for use on a copier", Path.Combine(Settings.Folders.Icons, "[f].png")),
            new CodeNode("[f NTSC]", "Fixed for NTSC systems", Path.Combine(Settings.Folders.Icons, "[f].png")),
            new CodeNode("[h]", "Hacked", Path.Combine(Settings.Folders.Icons, "[h].png")),
            new CodeNode("[h Hacker Group]", @"\[h *\]", "Hacked by Hacker Group", Path.Combine(Settings.Folders.Icons, "[h].png")),
            new CodeNode("[m]", "Modified", Path.Combine(Settings.Folders.Icons, "[m].png")),
            new CodeNode("[m Modifier]", @"\[m *\]", "Modified by Modifier", Path.Combine(Settings.Folders.Icons, "[m].png")),
            new CodeNode("[o]", "Overdump", Path.Combine(Settings.Folders.Icons, "[o].png")),
            new CodeNode("[p]", "Pirate (non-licensed)", Path.Combine(Settings.Folders.Icons, "[p].png")),
            new CodeNode("[t]", "Trained", Path.Combine(Settings.Folders.Icons, "[t].png")),
            new CodeNode("[t Trainer Group]", @"\[t *\]", "Trained by Trainer Group", Path.Combine(Settings.Folders.Icons, "[t].png")),
            new CodeNode("[t +5]", "Plus five trainer", Path.Combine(Settings.Folders.Icons, "[t].png")),
            new CodeNode("[tr]", "Translation", Path.Combine(Settings.Folders.Icons, "[Tr].png")),
            new CodeNode("[tr Fr]", "Translated to French", Path.Combine(Settings.Folders.Icons, "[Tr].png")),
            new CodeNode("[tr En]", "Translated to English", Path.Combine(Settings.Folders.Icons, "[Tr].png")),
            new CodeNode("[u]", "Underdump", Path.Combine(Settings.Folders.Icons, "[u].png")),
            new CodeNode("[!]", "Verified Good dump", Path.Combine(Settings.Folders.Icons, "[!].png"))
        };
	}
}
