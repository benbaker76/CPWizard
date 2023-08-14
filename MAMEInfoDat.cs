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
	class MAMEInfoDat : TextViewer, IDisposable
	{
		public class MAMENode
		{
			public List<string> Text = null;

			public MAMENode()
			{
				Text = new List<string>();
			}
		}

		public class MAMEInfoDatNode
		{
			public string Name = null;
			public MAMENode MAME = null;

			public MAMEInfoDatNode(string name, MAMENode mame)
			{
				Name = name;
				MAME = mame;
			}
		}

		private enum MAMEInfoDatElement
		{
			Nothing,
			Comment,
			Info,
			MAME
		}

		public Dictionary<string, MAMEInfoDatNode> MAMEInfoDatHash = null;
		private Font m_font = new Font("Lucida Console", 14, FontStyle.Bold);

		public MAMEInfoDat()
			: base()
		{
			MAMEInfoDatHash = new Dictionary<string, MAMEInfoDatNode>();
		}

		public override void GetLineData()
		{
			MAMEInfoDatNode MAMEInfoDat = null;

			if (Settings.MAME.GameName == null)
				return;

			if (!MAMEInfoDatHash.TryGetValue(Settings.MAME.GameName, out MAMEInfoDat))
			{
				if (Settings.MAME.GetParent() == null)
					return;

				if (!MAMEInfoDatHash.TryGetValue(Settings.MAME.GetParent(), out MAMEInfoDat))
					return;
			}

			Lines.Clear();

			foreach (string line in MAMEInfoDat.MAME.Text)
				AddLine(line);

			base.GetLineData();
		}

		public void ReadMAMEInfoDat(string path)
		{
			ReadMAMEInfoDat(path, null);
		}

		public void ReadMAMEInfoDat(string path, string RomName)
		{
			try
			{
				MAMEInfoDatElement CurrentElement = MAMEInfoDatElement.Nothing;
				List<string> ROMNames = new List<string>();
				MAMEInfoDatNode CurrentMAMEInfoDat = null;
				MAMENode CurrentMAME = null;
				int EmptyLineCount = 0;
				bool Found = false;

				MAMEInfoDatHash.Clear();

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
						CurrentElement = MAMEInfoDatElement.Comment;

						continue;
					}

					if (Line.StartsWith("$info"))
					{
						CurrentElement = MAMEInfoDatElement.Info;

						if (Found)
							return;

						CurrentMAMEInfoDat = null;
						CurrentMAME = new MAMENode();

						string[] Values = Line.Split('=');

						if (Values.Length == 2)
						{
							if (Values[1].Contains(","))
							{
								string[] Names = Values[1].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

								for (int j = 0; j < Names.Length; j++)
								{
									if (!MAMEInfoDatHash.ContainsKey(Names[j]))
									{
										if (RomName != null)
										{
											if (RomName == Names[j])
											{
												MAMEInfoDatHash.Add(Names[j], CurrentMAMEInfoDat = new MAMEInfoDatNode(Names[j], CurrentMAME));
												Found = true;
											}
										}
										else
											MAMEInfoDatHash.Add(Names[j], CurrentMAMEInfoDat = new MAMEInfoDatNode(Names[j], CurrentMAME));
									}
								}
							}
							else
							{
								if (!MAMEInfoDatHash.ContainsKey(Values[1]))
								{
									if (RomName != null)
									{
										if (RomName == Values[1])
										{
											MAMEInfoDatHash.Add(Values[1], CurrentMAMEInfoDat = new MAMEInfoDatNode(Values[1], CurrentMAME));
											Found = true;
										}
									}
									else
										MAMEInfoDatHash.Add(Values[1], CurrentMAMEInfoDat = new MAMEInfoDatNode(Values[1], CurrentMAME));
								}
							}
						}

						continue;
					}

					if (Line.StartsWith("$mame"))
					{
						CurrentElement = MAMEInfoDatElement.MAME;

						continue;
					}


					if (Line.StartsWith("$end"))
					{
						CurrentElement = MAMEInfoDatElement.Nothing;

						continue;
					}

					switch (CurrentElement)
					{
						case MAMEInfoDatElement.MAME:

							if (CurrentMAMEInfoDat != null)
							{
								if (CurrentMAMEInfoDat.MAME != null)
									CurrentMAMEInfoDat.MAME.Text.Add(Line);
							}
							break;
					}

					//System.Windows.Forms.Application.DoEvents();
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("ReadMAMEInfoDat", "MAMEInfoDat", ex.Message, ex.StackTrace);
			}
		}

		public override bool CheckEnabled()
		{
			if (Settings.MAME.GameName == null)
				return false;

			if (!MAMEInfoDatHash.ContainsKey(Settings.MAME.GameName))
			{
				if (Settings.MAME.GetParent() == null)
					return false;

				if (!MAMEInfoDatHash.ContainsKey(Settings.MAME.GetParent()))
					return false;
			}

			return true;
		}

		public override void Reset(EmulatorMode mode)
		{
			base.Reset(mode);
		}
	}
}
