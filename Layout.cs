// Copyright (c) 2015, Ben Baker
// All rights reserved.
// 
// This source code is licensed under the BSD-style license found in the
// LICENSE file in the root directory of this source tree. 

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Xml;
using System.IO;

namespace CPWizard
{
	public class Layout : IDisposable
	{
		public class ColorImageNode
		{
			public string Name = null;
			public string Image = null;

			public ColorImageNode(string name, string image)
			{
				Name = name;
				Image = image;
			}
		}

		private enum XmlElement
		{
			Nothing,
			Background,
			LayoutSub,
			ShowMiniInfo,
			Image,
			ColorImages,
			ColorImage,
			Label,
			Font,
			Text,
			Color,
			Transform,
			Code
		}

		public string Name = null;
		public int Width = 1024;
		public int Height = 768;
		public bool ShowMiniInfo = false;
		public string BackgroundFileName = null;
		public string LayoutSub = null;
		public bool ColorImagesEnabled = false;

		public bool PromptToSave = false;

		public List<LayoutObject> LayoutObjectList = null;
		public Bitmap LayoutBitmap = null;

		private Dictionary<string, ColorImageNode> ColorImageDictionary = null;

		public ColorImageNode[] ColorImageArray =
        {
            new ColorImageNode("Black", "T4ButtonBlack.png"),
            new ColorImageNode("White", "T4ButtonWhite.png"),
            new ColorImageNode("Red", "T4ButtonRed.png"),
            new ColorImageNode("Yellow", "T4ButtonYellow.png"),
            new ColorImageNode("Orange", "T4ButtonOrange.png"),
            new ColorImageNode("Lime", "T4ButtonLime.png"),
            new ColorImageNode("Green", "T4ButtonGreen.png"),
            new ColorImageNode("Cyan", "T4ButtonCyan.png"),
            new ColorImageNode("Blue", "T4ButtonBlue.png"),
            new ColorImageNode("Purple", "T4ButtonPurple.png"),
            new ColorImageNode("Violet", "T4ButtonViolet.png"),
            new ColorImageNode("Magenta", "T4ButtonMagenta.png"),
            new ColorImageNode("Brown", "T4ButtonBrown.png")
        };

		public Layout()
		{
			LayoutObjectList = new List<LayoutObject>();
			ColorImageDictionary = new Dictionary<string, ColorImageNode>();

			foreach (ColorImageNode color in ColorImageArray)
				ColorImageDictionary.Add(color.Name, color);
		}

		public void SortObjectList()
		{
			List<LayoutObject> labelList = new List<LayoutObject>();
			List<LayoutObject> imageList = new List<LayoutObject>();

			foreach (LayoutObject layoutObject in LayoutObjectList)
			{
				if (layoutObject is LabelObject)
					labelList.Add(layoutObject);

				if (layoutObject is ImageObject)
					imageList.Add(layoutObject);
			}

			LayoutObjectList.Clear();
			LayoutObjectList.AddRange(imageList);
			LayoutObjectList.AddRange(labelList);
		}

		public bool TryGetImage(string name, out ImageObject imageObject)
		{
			imageObject = null;

			foreach (LayoutObject layoutObject in LayoutObjectList)
			{
				if (layoutObject is ImageObject)
				{
					if (layoutObject.Name == name)
					{
						imageObject = (ImageObject)layoutObject;
						return true;
					}
				}
			}

			return false;
		}

		public bool TryGetLabel(string name, out LabelObject[] labelArray)
		{
			bool retVal = false;
			List<LabelObject> labelList = new List<LabelObject>();

			foreach (LayoutObject layoutObject in LayoutObjectList)
			{
				if (layoutObject is LabelObject)
				{
					if (layoutObject.Name == name)
					{
						labelList.Add((LabelObject)layoutObject);

						retVal = true;
					}
				}
			}

			labelArray = labelList.ToArray();

			return retVal;
		}

		public bool TryGetLabelLink(string name, out LabelObject[] labelArray)
		{
			bool retVal = false;
			string labelName = name;
			int labelCount = 0;
			int labelCounter = 0;
			List<LabelObject> labelList = new List<LabelObject>();
			Dictionary<string, int> labelCountDictionary = new Dictionary<string, int>();
			int openBracketIndex = name.IndexOf("(");
			int closeBracketIndex = name.IndexOf(")");

			if (openBracketIndex < closeBracketIndex)
			{
				labelCount = StringTools.FromString<int>(name.Substring(openBracketIndex + 1, closeBracketIndex - openBracketIndex - 1));
				labelName = name.Substring(0, openBracketIndex - 1);
			}

			foreach (LayoutObject layoutObject in LayoutObjectList)
			{
				if (layoutObject is LabelObject)
				{
					if (layoutObject.Name == labelName)
					{
						if (labelCountDictionary.TryGetValue(layoutObject.Name, out labelCounter))
							labelCountDictionary[layoutObject.Name] = labelCounter + 1;
						else
							labelCountDictionary.Add(layoutObject.Name, 1);

						if (labelCount == labelCounter)
						{
							labelList.Add((LabelObject)layoutObject);

							retVal = true;
						}
					}
				}
			}

			labelArray = labelList.ToArray();

			return retVal;
		}

		public bool TryGetObject(string name, out LayoutObject outLayoutObject)
		{
			outLayoutObject = null;

			foreach (LayoutObject layoutObject in LayoutObjectList)
			{
				if (layoutObject.Name == name)
				{
					outLayoutObject = layoutObject;
					return true;
				}
			}

			return false;
		}

		public void ReplaceLabel(string name, string value)
		{
			foreach (LayoutObject layoutObject in LayoutObjectList)
			{
				if (layoutObject is LabelObject)
				{
					LabelObject labelObject = (LabelObject)layoutObject;

					if (labelObject.Name == name)
						labelObject.Text = value;
				}
			}
		}

		public void DestoryObjects()
		{
			foreach (LayoutObject layoutObject in LayoutObjectList)
				layoutObject.Dispose();
		}

		public void ClearLabels()
		{
			foreach (LayoutObject layoutObject in LayoutObjectList)
			{
				if (layoutObject is LabelObject)
				{
					LabelObject labelObject = (LabelObject)layoutObject;

					if (StringTools.IsInputCode(labelObject.Name))
						labelObject.Text = "";

					//foreach (LabelCode labelCode in label.Codes)
					//    if (labelObject.Name == labelCode.Value && labelCode.Type == "Custom Text")
					//        labelObject.Text = "";    
				}
			}
		}

		public bool LoadLayoutXml(string fileName)
		{
			object currentControl = null;
			XmlElement currentElement = XmlElement.Nothing;
			Hashtable attribHash = new Hashtable();
			XmlTextReader xmlTextReader = null;

			Name = null;

			try
			{
				xmlTextReader = new XmlTextReader(fileName);

				xmlTextReader.Read();

				while (xmlTextReader.Read())
				{
					switch (xmlTextReader.NodeType)
					{
						case XmlNodeType.Element:
							switch (xmlTextReader.LocalName.ToLower())
							{
								case "background":
									currentElement = XmlElement.Background;
									break;
								case "layoutsub":
									currentElement = XmlElement.LayoutSub;
									break;
								case "showminiinfo":
									currentElement = XmlElement.ShowMiniInfo;
									break;
								case "image":
									currentElement = XmlElement.Image;
									break;
								case "colorimages":
									currentElement = XmlElement.ColorImages;
									break;
								case "colorimage":
									currentElement = XmlElement.ColorImage;
									break;
								case "label":
									currentElement = XmlElement.Label;
									break;
								case "font":
									currentElement = XmlElement.Font;
									break;
								case "text":
									currentElement = XmlElement.Text;
									break;
								case "color":
									currentElement = XmlElement.Color;
									break;
								case "code":
									currentElement = XmlElement.Code;
									break;
								default:
									currentElement = XmlElement.Nothing;
									break;
							}
							if (xmlTextReader.HasAttributes)
							{
								attribHash.Clear();
								while (xmlTextReader.MoveToNextAttribute())
									attribHash.Add(xmlTextReader.Name.ToLower(), xmlTextReader.Value);
							}
							switch (currentElement)
							{
								case XmlElement.Image:
									{
										ImageObject imageObject = new ImageObject((string)attribHash["name"], new Rectangle(StringTools.FromString<int>((string)attribHash["x"]), StringTools.FromString<int>((string)attribHash["y"]), StringTools.FromString<int>((string)attribHash["width"]), StringTools.FromString<int>((string)attribHash["height"])), StringTools.FromString<bool>((string)attribHash["sizeable"]), (string)attribHash["labellink"], StringTools.FromString<bool>((string)attribHash["alphafade"], Settings.Display.AlphaFade), StringTools.FromString<int>((string)attribHash["alphavalue"], Settings.Display.AlphaFadeValue));
										currentControl = imageObject;
										LayoutObjectList.Add(imageObject);

										if (StringTools.IsInputCode(imageObject.LabelLink))
											imageObject.LabelLink = StringTools.FixVariables(imageObject.LabelLink);
										break;
									}
								case XmlElement.ColorImages:
									{
										ColorImagesEnabled = StringTools.FromString<bool>((string)attribHash["enabled"]);
										break;
									}
								case XmlElement.ColorImage:
									{
										string colorName = (string)attribHash["name"];
										
										ColorImageNode colorImage = null;

										if (ColorImageDictionary.TryGetValue(colorName, out colorImage))
											colorImage.Image = (string)attribHash["image"];
										break;
									}
								case XmlElement.Label:
									{
										LabelObject labelObject = new LabelObject((string)attribHash["name"], (string)attribHash["group"], new Rectangle(StringTools.FromString<int>((string)attribHash["x"]), StringTools.FromString<int>((string)attribHash["y"]), StringTools.FromString<int>((string)attribHash["width"]), StringTools.FromString<int>((string)attribHash["height"])), StringTools.FromString<bool>((string)attribHash["sizeable"]), StringTools.FromString<bool>((string)attribHash["arrow"], Settings.Display.LabelArrowShow), StringTools.FromString<bool>((string)attribHash["spot"], Settings.Display.LabelSpotShow));
										currentControl = labelObject;
										LayoutObjectList.Add(labelObject);

										if (StringTools.IsInputCode(labelObject.Name))
										{
											labelObject.Name = StringTools.FixVariables(labelObject.Name);
											labelObject.Text = labelObject.Name;
										}

										labelObject.Group = StringTools.FixVariables(labelObject.Group);
										break;
									}
								case XmlElement.Font:
									{
										if (currentControl is LabelObject)
										{
											LabelObject labelObject = (LabelObject)currentControl;
											string fontName = (string)attribHash["name"];
											Color fontColor = StringTools.FromString<Color>((string)attribHash["color"], Color.White);
											float fontSize = StringTools.FromString<float>((string)attribHash["size"]);
											FontStyle fontStyle = FontStyle.Regular;

											switch ((string)attribHash["fontstyle"])
											{
												case "Regular":
													fontStyle = FontStyle.Regular;
													break;
												case "Bold":
													fontStyle = FontStyle.Bold;
													break;
												case "Italic":
													fontStyle = FontStyle.Italic;
													break;
												case "Strikeout":
													fontStyle = FontStyle.Strikeout;
													break;
												case "Underline":
													fontStyle = FontStyle.Underline;
													break;
												default:
													fontStyle = FontStyle.Regular;
													break;
											}

											labelObject.Font = new Font(fontName, fontSize, fontStyle);
											labelObject.Color = fontColor;
										}
										break;
									}
								case XmlElement.Text:
									{
										if (currentControl is LabelObject)
										{
											LabelObject labelObject = (LabelObject)currentControl;
											labelObject.TextAlign = (string)attribHash["align"];
											labelObject.TextStyle = (string)attribHash["style"];

											switch (labelObject.TextAlign)
											{
												case "Left":
												case "Right":
												case "Center":
													break;
												default:
													labelObject.TextAlign = "Center";
													break;
											}

											switch (labelObject.TextStyle)
											{
												case "Outline":
												case "Shadow":
													break;
												default:
													labelObject.TextStyle = "Outline";
													break;
											}
										}
										break;
									}
								case XmlElement.Color:
									{
										if (currentControl is ImageObject)
										{
											ImageObject imageObject = (ImageObject)currentControl;
											imageObject.Hue = StringTools.FromString<float>((string)attribHash["hue"]);
											imageObject.Saturation = StringTools.FromString<float>((string)attribHash["saturation"]);
											imageObject.Brightness = StringTools.FromString<float>((string)attribHash["brightness"]);
										}
										break;
									}
								case XmlElement.Transform:
									{
										if (currentControl is ImageObject)
										{
											ImageObject imageObject = (ImageObject)currentControl;
											//imageObject.Rotation = StringTools.FromString<int>((string)attribHash["rotation"]);
											//imageObject.HorizontalFlip = StringTools.FromString<bool>((string)attribHash["horizontalflip"]);
											//imageObject.VerticalFlip = StringTools.FromString<bool>((string)attribHash["verticalflip"]);
										}
										break;
									}
								case XmlElement.Code:
									{
										if (currentControl is LabelObject)
										{
											LabelObject labelObject = (LabelObject)currentControl;

											LabelCode labelCode = new LabelCode((string)attribHash["type"], (string)attribHash["value"]);
											labelObject.Codes.Add(labelCode);

											if (StringTools.IsInputCode(labelCode.Value))
												labelCode.Value = StringTools.FixVariables(labelCode.Value);
										}
										break;
									}
								default:
									break;
							}
							xmlTextReader.MoveToElement();
							break;

						case XmlNodeType.Text:
							string text = xmlTextReader.Value.Trim();
							switch (currentElement)
							{
								case XmlElement.Background:
									BackgroundFileName = text;
									break;
								case XmlElement.LayoutSub:
									LayoutSub = text;
									break;
								case XmlElement.ShowMiniInfo:
									ShowMiniInfo = StringTools.FromString<bool>(text);
									break;
								default:
									break;
							}
							break;

						case XmlNodeType.EndElement:
							switch (currentElement)
							{
								case XmlElement.Image:
								case XmlElement.Label:
								case XmlElement.Font:
								case XmlElement.Text:
								case XmlElement.Color:
								case XmlElement.Transform:
								default:
									break;
							}
							break;
					}
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("ReadLayout", "Layout", ex.Message, ex.StackTrace);

				return false;
			}
			finally
			{
				if (xmlTextReader != null)
					xmlTextReader.Close();
			}

			Name = FileIO.GetRelativeFolder(Settings.Folders.Layout, fileName, true);
			LoadImages();

			return true;
		}

		public void WriteLayout(string fileName)
		{
			XmlTextWriter xmlTextWriter = null;

			try
			{
				xmlTextWriter = new XmlTextWriter(fileName, null);

				xmlTextWriter.Formatting = Formatting.Indented;

				xmlTextWriter.WriteStartDocument();

				xmlTextWriter.WriteStartElement("Layout");

				xmlTextWriter.WriteStartElement("Background");
				xmlTextWriter.WriteString(BackgroundFileName);
				xmlTextWriter.WriteEndElement();

				xmlTextWriter.WriteStartElement("LayoutSub");
				xmlTextWriter.WriteString(LayoutSub);
				xmlTextWriter.WriteEndElement();

				xmlTextWriter.WriteStartElement("ShowMiniInfo");
				xmlTextWriter.WriteString(ShowMiniInfo.ToString());
				xmlTextWriter.WriteEndElement();

				xmlTextWriter.WriteStartElement("Images");

				foreach (LayoutObject layoutObject in LayoutObjectList)
				{
					if (layoutObject is ImageObject)
					{
						ImageObject imageObject = (ImageObject)layoutObject;

						xmlTextWriter.WriteStartElement("Image");
						xmlTextWriter.WriteAttributeString("Name", imageObject.Name);
						xmlTextWriter.WriteAttributeString("X", imageObject.Rect.X.ToString());
						xmlTextWriter.WriteAttributeString("Y", imageObject.Rect.Y.ToString());
						xmlTextWriter.WriteAttributeString("Width", imageObject.Rect.Width.ToString());
						xmlTextWriter.WriteAttributeString("Height", imageObject.Rect.Height.ToString());
						xmlTextWriter.WriteAttributeString("Sizeable", imageObject.Sizeable.ToString());
						xmlTextWriter.WriteAttributeString("AlphaFade", imageObject.AlphaFade.ToString());
						xmlTextWriter.WriteAttributeString("AlphaValue", imageObject.AlphaValue.ToString());
						xmlTextWriter.WriteAttributeString("LabelLink", imageObject.LabelLink);

						if (imageObject.Hue != 0 || imageObject.Saturation != 1 || imageObject.Brightness != 1)
						{
							xmlTextWriter.WriteStartElement("Color");
							xmlTextWriter.WriteAttributeString("Hue", imageObject.Hue.ToString());
							xmlTextWriter.WriteAttributeString("Saturation", imageObject.Saturation.ToString());
							xmlTextWriter.WriteAttributeString("Brightness", imageObject.Brightness.ToString());
							xmlTextWriter.WriteEndElement();
						}

						/* if (imageObject.Rotation != 0 || imageObject.HorizontalFlip != false || imageObject.VerticalFlip != false)
						{
							xmlTextWriter.WriteStartElement("Transform");
							xmlTextWriter.WriteAttributeString("Rotation", imageObject.Rotation.ToString());
							xmlTextWriter.WriteAttributeString("HorizontalFlip", imageObject.HorizontalFlip.ToString());
							xmlTextWriter.WriteAttributeString("VerticalFlip", imageObject.VerticalFlip.ToString());
							xmlTextWriter.WriteEndElement();
						} */

						xmlTextWriter.WriteEndElement();
					}
				}

				xmlTextWriter.WriteEndElement();

				xmlTextWriter.WriteStartElement("ColorImages");
				xmlTextWriter.WriteAttributeString("Enabled", ColorImagesEnabled.ToString());

				foreach (ColorImageNode colorImage in ColorImageArray)
				{
					xmlTextWriter.WriteStartElement("ColorImage");
					xmlTextWriter.WriteAttributeString("Name", colorImage.Name);
					xmlTextWriter.WriteAttributeString("Image", colorImage.Image);
					xmlTextWriter.WriteEndElement();
				}

				xmlTextWriter.WriteEndElement();

				xmlTextWriter.WriteStartElement("Labels");

				foreach (LayoutObject layoutObject in LayoutObjectList)
				{
					if (layoutObject is LabelObject)
					{
						LabelObject labelObject = (LabelObject)layoutObject;

						LogFile.WriteLine(labelObject.Name);

						xmlTextWriter.WriteStartElement("Label");
						xmlTextWriter.WriteAttributeString("Name", labelObject.Name);
						xmlTextWriter.WriteAttributeString("Group", labelObject.Group);
						xmlTextWriter.WriteAttributeString("X", labelObject.Rect.X.ToString());
						xmlTextWriter.WriteAttributeString("Y", labelObject.Rect.Y.ToString());
						xmlTextWriter.WriteAttributeString("Width", labelObject.Rect.Width.ToString());
						xmlTextWriter.WriteAttributeString("Height", labelObject.Rect.Height.ToString());
						xmlTextWriter.WriteAttributeString("Sizeable", labelObject.Sizeable.ToString());
						xmlTextWriter.WriteAttributeString("Arrow", labelObject.Arrow.ToString());
						xmlTextWriter.WriteAttributeString("Spot", labelObject.Spot.ToString());

						xmlTextWriter.WriteStartElement("Font");
						xmlTextWriter.WriteAttributeString("Name", labelObject.Font.Name.ToString());
						xmlTextWriter.WriteAttributeString("Size", labelObject.Font.Size.ToString());
						xmlTextWriter.WriteAttributeString("FontStyle", labelObject.Font.Style.ToString());
						xmlTextWriter.WriteAttributeString("Color", StringTools.ToString<Color>(labelObject.Color));
						xmlTextWriter.WriteEndElement();

						xmlTextWriter.WriteStartElement("Text");
						xmlTextWriter.WriteAttributeString("Align", labelObject.TextAlign);
						xmlTextWriter.WriteAttributeString("Style", labelObject.TextStyle);
						xmlTextWriter.WriteEndElement();

						xmlTextWriter.WriteStartElement("Codes");
						foreach (LabelCode code in labelObject.Codes)
						{
							if (String.IsNullOrEmpty(code.Type))
								continue;

							xmlTextWriter.WriteStartElement("Code");
							xmlTextWriter.WriteAttributeString("Type", code.Type);
							xmlTextWriter.WriteAttributeString("Value", code.Value);
							xmlTextWriter.WriteEndElement();
						}
						xmlTextWriter.WriteEndElement();

						xmlTextWriter.WriteEndElement();
					}
				}

				xmlTextWriter.WriteEndElement();

				xmlTextWriter.WriteEndDocument();

				PromptToSave = false;
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("WriteLayout", "Layout", ex.Message, ex.StackTrace);
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

		public void LoadBackground()
		{
			try
			{
				if (LayoutBitmap != null)
				{
					LayoutBitmap.Dispose();
					LayoutBitmap = null;
				}

				if (BackgroundFileName != null)
				{
					if (File.Exists(Path.Combine(Settings.Folders.Media, BackgroundFileName)))
					{
						LayoutBitmap = FileIO.LoadImage(Path.Combine(Settings.Folders.Media, BackgroundFileName));

						if (LayoutBitmap != null)
						{
							Width = LayoutBitmap.Width;
							Height = LayoutBitmap.Height;
						}
					}
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("LoadBackground", "Layout", ex.Message, ex.StackTrace);
			}
		}

		public void LoadImages()
		{
			try
			{
				LoadBackground();

				foreach (LayoutObject layoutObject in LayoutObjectList)
				{
					if (layoutObject is ImageObject)
					{
						ImageObject imageObject = (ImageObject)layoutObject;

						imageObject.Bitmap = FileIO.LoadImage(Path.Combine(Settings.Folders.Media, imageObject.Name));
					}
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("LoadImages", "Layout", ex.Message, ex.StackTrace);
			}
		}

		#region IDisposable Members

		public void Dispose()
		{
			DestoryObjects();
			LayoutObjectList.Clear();
		}

		#endregion
	}
}
