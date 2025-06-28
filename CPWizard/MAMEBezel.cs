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
using System.Xml;

namespace CPWizard
{
	public class MAMEBezel
	{
		public enum BezelType
		{
			Layout,
			GameInfo,
			LayoutAndGameInfo
		}

		private enum StarType
		{
			Black,
			Bronze,
			Silver,
			Gold
		}

		public Bitmap GameInfoBak = null;
		public Bitmap BezelBitmap = null;

		public Font Font = new Font("Lucida Console", 14, FontStyle.Regular);
		public Bitmap[] StarBitmapArray = null;

		public MAMEBezel()
		{
			try
			{
				//BezelBitmap = FileIO.LoadImage(Path.Combine(Settings.Folders.Media, "Bezel.png"));
				GameInfoBak = FileIO.LoadImage(Path.Combine(Settings.Folders.Media, "BezelBak.png"));

				StarBitmapArray = new Bitmap[4];

				StarBitmapArray[(int)StarType.Black] = FileIO.LoadImage(Path.Combine(Settings.Folders.Media, "StarBlack.png"));
				StarBitmapArray[(int)StarType.Bronze] = FileIO.LoadImage(Path.Combine(Settings.Folders.Media, "StarBronze.png"));
				StarBitmapArray[(int)StarType.Silver] = FileIO.LoadImage(Path.Combine(Settings.Folders.Media, "StarSilver.png"));
				StarBitmapArray[(int)StarType.Gold] = FileIO.LoadImage(Path.Combine(Settings.Folders.Media, "StarGold.png"));
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("Bezel", "Bezel", ex.Message, ex.StackTrace);
			}
		}

		private float StringToPower(string str)
		{
			switch (str)
			{
				case "preliminary":
					return 0.2f;
				case "imperfect":
					return 0.5f;
				case "good":
					return 1f;
			}

			return 0f;
		}

		private string GetWeightedAverageName(float weightedaverage)
		{
			if (weightedaverage <= 20)
				return "Rubbish";
			else if (weightedaverage <= 70)
				return "Average";
			else if (weightedaverage <= 100)
				return "Perfect";

			return "Unknown";
		}

		private int GetStarType(int count, float weightedaverage)
		{
			if (count * 10 <= weightedaverage)
			{
				if (weightedaverage <= 20)
					return 1;
				else if (weightedaverage <= 70)
					return 2;
				else if (weightedaverage <= 100)
					return 3;
			}

			return 0;
		}

		// Bezel V1 (Pre MAME 0.107 format) 
		public void GenerateBezelV1(Bitmap bmp, string outputFolder, string name, string description, string year, string manufacturer, string category, string gameType, float weightedAverage, bool vertical, BezelType bezelType)
		{
			try
			{
				string TempFolder = Path.Combine(Settings.Folders.Temp, "Bezel");
				string MaskFile = Path.Combine(TempFolder, "Mask.png");
				string LayoutFile = Path.Combine(TempFolder, name + ".art");
				string CPFile = Path.Combine(TempFolder, name + ".png");
				string ZipFile = Path.Combine(outputFolder, name + ".zip");

				using (Bitmap BezelBitmap = new Bitmap(640, 480))
				{
					using (Graphics g = Graphics.FromImage(BezelBitmap))
					{
						if (!Directory.Exists(TempFolder))
							Directory.CreateDirectory(TempFolder);

						g.Clear(Color.Black);

						g.FillRectangle(new SolidBrush(Color.White), new Rectangle(0, 480 - 48, 640, 48));

						if (vertical)
						{
							g.FillRectangle(new SolidBrush(Color.White), new Rectangle(0, 0, 140, 480));
							g.FillRectangle(new SolidBrush(Color.White), new Rectangle(500, 0, 140, 480));
							BezelBitmap.RotateFlip(RotateFlipType.Rotate90FlipNone);
						}

						BezelBitmap.Save(MaskFile, System.Drawing.Imaging.ImageFormat.Png);

						if (vertical)
							BezelBitmap.RotateFlip(RotateFlipType.Rotate270FlipNone);

						g.Clear(Color.FromArgb(255, 0, 0, 0));


						if (bezelType == BezelType.Layout || bezelType == BezelType.LayoutAndGameInfo)
							g.DrawImage(bmp, 0, 0, 640, 480);

						if (vertical)
							BezelBitmap.RotateFlip(RotateFlipType.Rotate90FlipNone);

						BezelBitmap.Save(CPFile, System.Drawing.Imaging.ImageFormat.Png);
					}
				}

				if (vertical)
					WriteBezelArt(LayoutFile, Path.GetFileName(MaskFile), Path.GetFileName(CPFile), new RectangleF(-0.0f, -0.384f, 1.0f, 1.384f));
				else
					WriteBezelArt(LayoutFile, Path.GetFileName(MaskFile), Path.GetFileName(CPFile), new RectangleF(0f, 0f, 1.0f, 1.0f));

				Zip.ZipFile(TempFolder, ZipFile);

				if (File.Exists(LayoutFile))
					File.Delete(LayoutFile);

				if (File.Exists(MaskFile))
					File.Delete(MaskFile);

				if (File.Exists(CPFile))
					File.Delete(CPFile);

				if (Directory.Exists(TempFolder))
					Directory.Delete(TempFolder);
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("GenerateBezelV1", "Bezel", ex.Message, ex.StackTrace);
			}
		}

		// Bezel V2 (MAME 0.107+ format) 
		public void GenerateBezelV2(Bitmap bmp, string outputFolder, Size screenSize, Rectangle screenRect, Rectangle bezelRect, string name, string description, string year, string manufacturer, string category, string gameType, float weightedAverage, bool includeBezel, BezelType bezelType)
		{
			try
			{
				string TempFolder = Path.Combine(Settings.Folders.Temp, "Bezel");
				string LayoutFile = Path.Combine(TempFolder, name + ".lay");
				string CPFile = Path.Combine(TempFolder, name + ".png");
				string BezelFile = Path.Combine(Settings.Folders.Media, "Bezel.png");
				string DestBezelFile = Path.Combine(TempFolder, "Bezel.png");
				string ZipFile = Path.Combine(outputFolder, name + ".zip");

				using (Bitmap BezelBmp = new Bitmap(screenSize.Width, screenSize.Height))
				{
					using (Graphics g = Graphics.FromImage(BezelBmp))
					{
						if (!Directory.Exists(TempFolder))
							Directory.CreateDirectory(TempFolder);

						if (bezelType == BezelType.Layout || bezelType == BezelType.LayoutAndGameInfo)
							g.DrawImage(bmp, 0, 0, screenSize.Width, screenSize.Height);

						g.Flush();

						BezelBmp.Save(CPFile, System.Drawing.Imaging.ImageFormat.Png);
					}
				}

				if (includeBezel)
					File.Copy(BezelFile, DestBezelFile);

				WriteBezelXml(LayoutFile, Path.GetFileName(CPFile), Path.GetFileName(BezelFile), screenSize, screenRect, bezelRect, includeBezel);

				Zip.ZipFile(TempFolder, ZipFile);

				if (File.Exists(LayoutFile))
					File.Delete(LayoutFile);

				if (File.Exists(CPFile))
					File.Delete(CPFile);

				if (includeBezel)
					if (File.Exists(DestBezelFile))
						File.Delete(DestBezelFile);

				if (Directory.Exists(TempFolder))
					Directory.Delete(TempFolder);
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("GenerateBezelV2", "Bezel", ex.Message, ex.StackTrace);
			}
		}

		public void PaintMiniInfo(Graphics g, int x, int y, int width, int height, MAMEMachineNode gameNode)
		{
			string category = null;
			string type = null;
			float weightedAverage = 0f;
			bool rotate = false;

			if (gameNode == null)
				return;

			if (gameNode.CatVer != null)
				category = gameNode.CatVer.Category;

			if (gameNode.NPlayers != null)
				type = gameNode.NPlayers.Type;

			if (gameNode.HallOfFame != null)
				weightedAverage = gameNode.HallOfFame.WeightedAverage;

			if (gameNode.DisplayList != null)
			{
				if (gameNode.DisplayList.Count > 0)
					rotate = (gameNode.DisplayList[0].Rotate == 270);
			}

			PaintMiniInfo(g, x, y, width, height, gameNode.Name, gameNode.ROMOf, gameNode.Description, gameNode.Year, gameNode.Manufacturer, category, type, weightedAverage);
		}

		public void PaintMiniInfo(Graphics g, int x, int y, int width, int height, string name, string romOf, string description, string year, string manufacturer, string category, string gameType, float weightedAverage)
		{
			try
			{
				Color[] gameInfoColorArray = new Color[3] { Color.Yellow, Color.Red, Color.White };
				string[] gameInfoStringArray = new string[3];
				string[] textReplaceArray = new string[] { "[]" };

				gameInfoStringArray[0] = String.Format("{0,-50}", StringTools.SubString(description, 50));
				gameInfoStringArray[1] = String.Format("{0,-20}{1,30}", StringTools.SubString(year + " (" + manufacturer + ")", 20), StringTools.SubString(category, 30));
				gameInfoStringArray[2] = String.Format("{0,-30}{1,20}", String.Format("[][][][][][][][][][] ({0})", GetWeightedAverageName(weightedAverage)), gameType);

				SizeF fontSize = g.MeasureString(gameInfoStringArray[0], Font, 1024, StringFormat.GenericTypographic);
				//Size fontSize = m_textMeasure.MeasureString(GameInfoString[0]);

				for (int i = 0; i < gameInfoStringArray.Length; i++)
				{
					SizeF maxFontSize = g.MeasureString(gameInfoStringArray[i], Font, 1024, StringFormat.GenericTypographic);
					//Size maxFontSize = m_textMeasure.MeasureString(GameInfoString[i]);

					if (maxFontSize.Width > fontSize.Width)
						fontSize.Width = maxFontSize.Width;

					if (maxFontSize.Height > fontSize.Height)
						fontSize.Height = maxFontSize.Height;
				}

				Size paddingSize = new Size((int)(fontSize.Width * 0.03f), (int)((fontSize.Height * gameInfoStringArray.Length) * 0.03f));
				SizeF backSize = new SizeF(fontSize.Width + (paddingSize.Width * 2), (int)(fontSize.Height * gameInfoStringArray.Length) + (paddingSize.Height * 2));

				using (Bitmap cmdBmp = new Bitmap((int)backSize.Width, (int)backSize.Height))
				{
					using (Graphics gSrc = Graphics.FromImage(cmdBmp))
					{
						g.DrawImage(GameInfoBak, new Rectangle(x, y, width, height), 0, 0, GameInfoBak.Width, GameInfoBak.Height, GraphicsUnit.Pixel);

						int xPos = paddingSize.Width;
						int yPos = paddingSize.Height;

						Bitmap Icon = Globals.MAMEManager.GetIcon(name, romOf);
						Bitmap Marquee = Globals.MAMEManager.GetMarquee(name, romOf);

						if (Icon != null)
						{
							g.DrawImage(Icon, new Rectangle(x, y, (int)(width * 0.1f), height), 0, 0, Icon.Width, Icon.Height, GraphicsUnit.Pixel);
							Icon.Dispose();
							Icon = null;
						}

						if (Marquee != null)
						{
							g.DrawImage(Marquee, new Rectangle(x + width - (int)(width * 0.3f), y, (int)(width * 0.3f), height), 0, 0, Marquee.Width, Marquee.Height, GraphicsUnit.Pixel);
							Marquee.Dispose();
							Marquee = null;
						}

						for (int i = 0; i < gameInfoStringArray.Length; i++)
						{
							string[] strSplit = StringTools.SplitString(gameInfoStringArray[i], textReplaceArray);
							xPos = paddingSize.Width;
							int count = 0;

							for (int k = 0; k < strSplit.Length; k++)
							{
								SizeF strSize = g.MeasureString(strSplit[k].Replace(' ', '*'), Font, 0, StringFormat.GenericTypographic);
								//Size strSize = m_textMeasure.MeasureString(strSplit[k]);

								if (strSplit[k] == "[]")
								{
									Bitmap starBmp = StarBitmapArray[GetStarType(count, weightedAverage)];
									gSrc.DrawImage(starBmp, new Rectangle(xPos, yPos, (int)strSize.Width, (int)strSize.Height), 0, 0, starBmp.Width, starBmp.Height, GraphicsUnit.Pixel);
									count += 1;
								}
								else
								{
									using (Brush solidBrush = new SolidBrush(Color.Black))
										gSrc.DrawString(strSplit[k], Font, solidBrush, xPos + 1, yPos + 1, StringFormat.GenericTypographic);

									using (Brush solidBrush = new SolidBrush(gameInfoColorArray[i]))
										gSrc.DrawString(strSplit[k], Font, solidBrush, xPos, yPos, StringFormat.GenericTypographic);
								}

								xPos += (int)strSize.Width;
							}

							yPos += (int)fontSize.Height;
						}

						g.DrawImage(cmdBmp, new Rectangle((int)(x + width * 0.1f), y, width - (int)(width * 0.4f), height), 0, 0, backSize.Width, backSize.Height, GraphicsUnit.Pixel);
					}
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("PaintMiniInfo", "Bezel", ex.Message, ex.StackTrace);
			}
		}

		public void WriteBezelArt(string fileName, string maskFile, string imageFile, RectangleF bezelRect)
		{
			TextWriter textWriter = null;

			try
			{
				textWriter = new StreamWriter(fileName);

				char Tab = (char)9;

				textWriter.WriteLine("bezel:");
				textWriter.WriteLine(Tab + "file" + Tab + Tab + String.Format(" = {0}", imageFile));
				textWriter.WriteLine(Tab + "alphafile" + Tab + String.Format(" = {0}", maskFile));
				textWriter.WriteLine(Tab + "layer" + Tab + Tab + String.Format(" = bezel"));
				textWriter.WriteLine(Tab + "priority" + Tab + Tab + String.Format(" = {0}", 0));
				textWriter.WriteLine(Tab + "visible" + Tab + Tab + String.Format(" = {0}", 1));
				textWriter.WriteLine(Tab + "position" + Tab + String.Format(" = {0},{1},{2},{3}", bezelRect.X, bezelRect.Y, bezelRect.Width, bezelRect.Height));
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("WriteBezelArt", "Bezel", ex.Message, ex.StackTrace);
			}

			if (textWriter != null)
			{
				textWriter.Flush();
				textWriter.Close();
			}
		}

		public void WriteBezelXml(string fileName, string cpFile, string bezelFile, Size screenSize, Rectangle screenRect, Rectangle bezelRect, bool includeBezel)
		{
			XmlTextWriter xmlTextWriter = null;

			try
			{
				xmlTextWriter = new XmlTextWriter(fileName, null);

				xmlTextWriter.Formatting = Formatting.Indented;

				xmlTextWriter.WriteStartDocument();
				xmlTextWriter.WriteStartElement("mamelayout");
				xmlTextWriter.WriteAttributeString("version", "2");

				xmlTextWriter.WriteStartElement("element");
				xmlTextWriter.WriteAttributeString("name", "CP");
				xmlTextWriter.WriteStartElement("image");
				xmlTextWriter.WriteAttributeString("file", cpFile);
				xmlTextWriter.WriteEndElement();
				xmlTextWriter.WriteEndElement();

				if (includeBezel)
				{
					xmlTextWriter.WriteStartElement("element");
					xmlTextWriter.WriteAttributeString("name", "Bezel");
					xmlTextWriter.WriteStartElement("image");
					xmlTextWriter.WriteAttributeString("file", bezelFile);
					xmlTextWriter.WriteEndElement();
					xmlTextWriter.WriteEndElement();
				}

				xmlTextWriter.WriteStartElement("view");
				xmlTextWriter.WriteAttributeString("name", "Hide CP");

				xmlTextWriter.WriteStartElement("screen");
				xmlTextWriter.WriteAttributeString("index", "0");
				xmlTextWriter.WriteStartElement("bounds");
				xmlTextWriter.WriteAttributeString("left", screenRect.Left.ToString());
				xmlTextWriter.WriteAttributeString("top", screenRect.Top.ToString());
				xmlTextWriter.WriteAttributeString("right", screenRect.Right.ToString());
				xmlTextWriter.WriteAttributeString("bottom", screenRect.Bottom.ToString());
				xmlTextWriter.WriteEndElement();
				xmlTextWriter.WriteEndElement();

				if (includeBezel)
				{
					xmlTextWriter.WriteStartElement("bezel");
					xmlTextWriter.WriteAttributeString("element", "Bezel");
					xmlTextWriter.WriteStartElement("bounds");
					xmlTextWriter.WriteAttributeString("left", bezelRect.Left.ToString());
					xmlTextWriter.WriteAttributeString("top", bezelRect.Top.ToString());
					xmlTextWriter.WriteAttributeString("right", bezelRect.Right.ToString());
					xmlTextWriter.WriteAttributeString("bottom", bezelRect.Bottom.ToString());
					xmlTextWriter.WriteEndElement();
					xmlTextWriter.WriteEndElement();
				}

				xmlTextWriter.WriteEndElement();

				xmlTextWriter.WriteStartElement("view");
				xmlTextWriter.WriteAttributeString("name", "Show CP");

				xmlTextWriter.WriteStartElement("screen");
				xmlTextWriter.WriteAttributeString("index", "0");
				xmlTextWriter.WriteStartElement("bounds");
				xmlTextWriter.WriteAttributeString("left", screenRect.Left.ToString());
				xmlTextWriter.WriteAttributeString("top", screenRect.Top.ToString());
				xmlTextWriter.WriteAttributeString("right", screenRect.Right.ToString());
				xmlTextWriter.WriteAttributeString("bottom", screenRect.Bottom.ToString());
				xmlTextWriter.WriteEndElement();
				xmlTextWriter.WriteEndElement();

				if (includeBezel)
				{
					xmlTextWriter.WriteStartElement("bezel");
					xmlTextWriter.WriteAttributeString("element", "Bezel");
					xmlTextWriter.WriteStartElement("bounds");
					xmlTextWriter.WriteAttributeString("left", bezelRect.Left.ToString());
					xmlTextWriter.WriteAttributeString("top", bezelRect.Top.ToString());
					xmlTextWriter.WriteAttributeString("right", bezelRect.Right.ToString());
					xmlTextWriter.WriteAttributeString("bottom", bezelRect.Bottom.ToString());
					xmlTextWriter.WriteEndElement();
					xmlTextWriter.WriteEndElement();
				}

				xmlTextWriter.WriteStartElement("bezel");
				xmlTextWriter.WriteAttributeString("element", "CP");
				xmlTextWriter.WriteStartElement("bounds");
				xmlTextWriter.WriteAttributeString("left", "0");
				xmlTextWriter.WriteAttributeString("top", "0");
				xmlTextWriter.WriteAttributeString("right", screenSize.Width.ToString());
				xmlTextWriter.WriteAttributeString("bottom", screenSize.Height.ToString());
				xmlTextWriter.WriteEndElement();
				xmlTextWriter.WriteEndElement();

				xmlTextWriter.WriteEndElement();

				xmlTextWriter.WriteEndDocument();
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("WriteBezelXml", "Bezel", ex.Message, ex.StackTrace);
			}
			finally
			{
				if (xmlTextWriter != null)
				{
					xmlTextWriter.Flush();
					xmlTextWriter.Close();
				}
			}
		}

		#region "IDisposable Members"

		public void Dispose()
		{
		}

		#endregion
	}
}