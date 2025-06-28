// Copyright (c) 2015, Ben Baker
// All rights reserved.
// 
// This source code is licensed under the BSD-style license found in the
// LICENSE file in the root directory of this source tree. 

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;

namespace CPWizard
{
	class HistoryDat : TextViewer, IDisposable
	{
		public class BioNode
		{
			public List<string> Text = null;

			public BioNode()
			{
				Text = new List<string>();
			}
		}

		public class HistoryDatNode
		{
			public string Name = null;
			public BioNode Bio = null;

			public HistoryDatNode(string name, BioNode bio)
			{
				Name = name;
				Bio = bio;
			}
		}

		private enum HistoryDatElement
		{
			Nothing,
			Comment,
			Info,
			Bio
		}

		public Dictionary<string, HistoryDatNode> HistoryDatHash = null;
		private Font m_font = new Font("Lucida Console", 14, FontStyle.Bold);

		public HistoryDat()
			: base()
		{
			HistoryDatHash = new Dictionary<string, HistoryDatNode>();
		}

		public override void GetLineData()
		{
			try
			{
				HistoryDatNode HistoryDat = null;

				if (Settings.MAME.GameName == null)
					return;

				if (!HistoryDatHash.TryGetValue(Settings.MAME.GameName, out HistoryDat))
					return;

				Lines.Clear();

				//AddLine(HistoryDat.Name);

				foreach (string line in HistoryDat.Bio.Text)
					AddLine(line);

				base.GetLineData();
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("GetLineData", "HistoryDat", ex.Message, ex.StackTrace);
			}
		}

		public void ReadHistoryDat(string path)
		{
			ReadHistoryDat(Settings.Files.HistoryDat, null);
		}

		public void ReadHistoryDat(string path, string RomName)
		{
			try
			{
				HistoryDatElement CurrentElement = HistoryDatElement.Nothing;
				List<string> ROMNames = new List<string>();
				HistoryDatNode CurrentHistoryDat = null;
				BioNode CurrentBio = null;
				int EmptyLineCount = 0;

				HistoryDatHash.Clear();

				string[] Lines = File.ReadAllLines(path, Encoding.ASCII);

				for (int i = 0; i < Lines.Length; i++)
				{
					string Line = Lines[i].Trim();

					if (Line == String.Empty)
					{
						EmptyLineCount++;

						if (EmptyLineCount == 2)
							EmptyLineCount = 0;
						else
							continue;
					}
					else
						EmptyLineCount = 0;

					if (Line.StartsWith("#"))
					{
						CurrentElement = HistoryDatElement.Comment;

						continue;
					}

					if (Line.StartsWith("$info"))
					{
						CurrentElement = HistoryDatElement.Info;

						if (RomName != null && CurrentHistoryDat != null)
							return;

						CurrentHistoryDat = null;
						CurrentBio = new BioNode();

						string[] Values = Line.Split('=');

						if (Values.Length == 2)
						{
							if (Values[1].Contains(","))
							{
								string[] Names = Values[1].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

								for (int j = 0; j < Names.Length; j++)
								{
									if (!HistoryDatHash.ContainsKey(Names[j]))
									{
										if (RomName != null)
										{
											if (RomName == Names[j])
												HistoryDatHash.Add(Names[j], CurrentHistoryDat = new HistoryDatNode(Names[j], CurrentBio));
										}
										else
											HistoryDatHash.Add(Names[j], CurrentHistoryDat = new HistoryDatNode(Names[j], CurrentBio));
									}
								}
							}
							else
							{
								if (!HistoryDatHash.ContainsKey(Values[1]))
								{
									if (RomName != null)
									{
										if (RomName == Values[1])
											HistoryDatHash.Add(Values[1], CurrentHistoryDat = new HistoryDatNode(Values[1], CurrentBio));
									}
									else
										HistoryDatHash.Add(Values[1], CurrentHistoryDat = new HistoryDatNode(Values[1], CurrentBio));
								}
							}
						}

						continue;
					}

					if (Line.StartsWith("$bio"))
					{
						CurrentElement = HistoryDatElement.Bio;

						continue;
					}


					if (Line.StartsWith("$end"))
					{
						CurrentElement = HistoryDatElement.Nothing;

						continue;
					}

					switch (CurrentElement)
					{
						case HistoryDatElement.Bio:

							if (CurrentHistoryDat != null)
							{
								if (CurrentHistoryDat.Bio != null)
									CurrentHistoryDat.Bio.Text.Add(Line);
							}
							break;
					}

					//System.Windows.Forms.Application.DoEvents();
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("ReadHistoryDat", "HistoryDat", ex.Message, ex.StackTrace);
			}
		}

		public override bool CheckEnabled()
		{
			if (Settings.MAME.GameName == null)
				return false;

			if (!HistoryDatHash.ContainsKey(Settings.MAME.GameName))
				return false;

			return true;
		}

		public override void Reset(EmulatorMode mode)
		{
			base.Reset(mode);
		}
	}
}
