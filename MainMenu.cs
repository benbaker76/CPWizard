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
	class MainMenu : MenuManager, IDisposable
	{
		public MainMenu()
		{
		}

		private void OnMenuItemSelect(string item)
		{
			Hide();

			switch (item)
			{
				case "Control Panel":
					Globals.LayoutManager.Show();
					break;
				case "Game Info":
					Globals.GameInfo.Show();
					break;
				case "Game History":
					Globals.HistoryDat.Show();
					break;
				case "MAME Info":
					Globals.MAMEInfoDat.Show();
					break;
				case "Control Info":
					Globals.CommandDat.Show();
					break;
				case "High Scores":
					Globals.StoryDat.Show();
					break;
				case "My High Scores":
					Globals.HiToText.Show();
					break;
				case "Artwork":
					Globals.ArtworkManager.Show();
					break;
				case "Manual":
					if (Globals.EmulatorMode == EmulatorMode.MAME)
						Globals.MAMEManual.Show();
					else
						Globals.EmulatorManual.Show();
					break;
				case "Operation Card":
					Globals.EmulatorOpCard.Show();
					break;
				case "NFO":
					Globals.NFOViewer.Show();
					break;
				case "IRC":
					Globals.IRC.Show();
					break;
				case "Exit":
					Globals.ProgramManager.Hide();
					break;
				case "!":
					if (Settings.Input.BackShowsCP)
						Globals.LayoutManager.Show();

					else if (Settings.Input.BackKeyExitMenu)
						Globals.ProgramManager.Hide();

					else
						Show();
					break;
				default:
					Show();
					break;
			}

			EventManager.UpdateDisplay();
		}

		public override void Show()
		{
			Hide();

			Globals.MenuJustShown = true;

			base.Show();
		}

		public override void Hide()
		{
			Globals.LayoutManager.Hide();
			Globals.GameInfo.Hide();
			Globals.HistoryDat.Hide();
			Globals.MAMEInfoDat.Hide();
			Globals.CommandDat.Hide();
			Globals.StoryDat.Hide();
			Globals.HiToText.Hide();
			Globals.ArtworkManager.Hide();
			Globals.MAMEManual.Hide();
			Globals.EmulatorManual.Hide();
			Globals.EmulatorOpCard.Hide();
			Globals.NFOViewer.Hide();
			Globals.IRC.Hide();

			base.Hide();
		}

		public override void Paint(Graphics g, int x, int y, int width, int height)
		{
			base.Paint(g, x, y, width, height);
		}

		public override bool CheckEnabled()
		{
			return true;
		}

		public override void Reset(EmulatorMode mode)
		{
			if (!String.IsNullOrEmpty(Settings.Folders.MAME.Manuals))
				Globals.MAMEManual.FileName = Path.Combine(Settings.Folders.MAME.Manuals, Settings.MAME.GameName + ".pdf");

			if (Settings.Emulator.Profile != null)
			{
				if (!String.IsNullOrEmpty(Settings.Emulator.Profile.Manuals))
					Globals.EmulatorManual.FileName = Path.Combine(Settings.Emulator.Profile.Manuals, Settings.Emulator.GameName + ".pdf");

				if (!String.IsNullOrEmpty(Settings.Emulator.Profile.OpCards))
					Globals.EmulatorOpCard.FileName = Path.Combine(Settings.Emulator.Profile.OpCards, Settings.Emulator.GameName + ".pdf");
			}

			Globals.LayoutManager.Reset(mode);
			Globals.GameInfo.Reset(mode);
			Globals.HistoryDat.Reset(mode);
			Globals.MAMEInfoDat.Reset(mode);
			Globals.CommandDat.Reset(mode);
			Globals.StoryDat.Reset(mode);
			Globals.HiToText.Reset(mode);
			Globals.ArtworkManager.Reset(mode);
			Globals.MAMEManual.Reset(mode);
			Globals.EmulatorManual.Reset(mode);
			Globals.EmulatorOpCard.Reset(mode);
			Globals.NFOViewer.Reset(mode);
			Globals.IRC.Reset(mode);

			base.Reset(mode);

			Items.Clear();

			switch (mode)
			{
				case EmulatorMode.MAME:
					if (Settings.Data.MAME.ControlPanel && Globals.LayoutManager.Enabled)
						Items.Add("Control Panel");

					if (Settings.Data.MAME.GameInfo && Globals.GameInfo.Enabled)
						Items.Add("Game Info");

					if (Settings.Data.MAME.GameHistory && Globals.HistoryDat.Enabled)
						Items.Add("Game History");

					if (Settings.Data.MAME.MAMEInfo && Globals.MAMEInfoDat.Enabled)
						Items.Add("MAME Info");

					if (Settings.Data.MAME.ControlInfo && Globals.CommandDat.Enabled)
						Items.Add("Control Info");

					if (Settings.Data.MAME.HighScore && Globals.StoryDat.Enabled)
						Items.Add("High Scores");

					if (Settings.Data.MAME.MyHighScore && Globals.HiToText.Enabled)
						Items.Add("My High Scores");

					if (Settings.Data.MAME.Artwork && Globals.ArtworkManager.Enabled)
						Items.Add("Artwork");

					if (Settings.Data.MAME.Manual && Globals.MAMEManual.Enabled)
						Items.Add("Manual");

					if (Settings.Data.MAME.IRC && Globals.IRC.Enabled)
						Items.Add("IRC");
					break;
				case EmulatorMode.Emulator:
					if (Settings.Data.Emulator.ControlPanel && Globals.LayoutManager.Enabled)
						Items.Add("Control Panel");

					if (Settings.Data.Emulator.Artwork && Globals.ArtworkManager.Enabled)
						Items.Add("Artwork");

					if (Settings.Data.Emulator.Manual && Globals.EmulatorManual.Enabled)
						Items.Add("Manual");

					if (Settings.Data.Emulator.OperationCard && Globals.EmulatorOpCard.Enabled)
						Items.Add("Operation Card");

					if (Settings.Data.Emulator.NFO && Globals.NFOViewer.Enabled)
						Items.Add("NFO");

					if (Settings.Data.Emulator.IRC && Globals.IRC.Enabled)
						Items.Add("IRC");
					break;
			}

			if (!Settings.Display.HideExitMenu)
				Items.Add("Exit");
					
		}

		public override void AddEventHandlers()
		{
			base.AddEventHandlers();

			MenuItemSelect += new MenuItemSelectHandler(OnMenuItemSelect);
		}

		public override void RemoveEventHandlers()
		{
			base.RemoveEventHandlers();

			MenuItemSelect -= new MenuItemSelectHandler(OnMenuItemSelect);
		}

		#region IDisposable Members

		public override void Dispose()
		{
			base.Dispose();
		}

		#endregion
	}
}
