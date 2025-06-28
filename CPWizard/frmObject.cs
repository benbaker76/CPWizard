using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace CPWizard
{
	public partial class frmObject : Form
	{
		public frmObject()
		{
			InitializeComponent();
		}

		private void ObjectForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			Globals.ObjectForm = null;
		}

		private void ObjectForm_Load(object sender, EventArgs e)
		{
			GetSelectedObjectInfo();
		}

		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);
			EventManager.WndProc(ref m);
		}

		public void GetSelectedObjectInfo()
		{
			try
			{
				if (Globals.SelectedObjectList.Count == 0)
				{
					tabControl1.TabPages.Clear();

					return;
				}

				RemoveEventHandlers();

				foreach (LayoutObject layoutObject in Globals.SelectedObjectList)
				{
					if (layoutObject is ImageObject)
					{
						ImageObject imageObject = (ImageObject)layoutObject;
						GetSelectedImageInfo(imageObject);
						break;
					}
					else if (layoutObject is LabelObject)
					{
						LabelObject labelObject = (LabelObject)layoutObject;
						GetSelectedLabelInfo(labelObject);
						break;
					}
				}

				AddEventHandlers();

				UpdateDisplay();
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("GetSelectedObjectInfo", "ObjectForm", ex.Message, ex.StackTrace);
			}
		}

		private void UpdateDisplay()
		{
			picLabelPreview.Invalidate();
			picImagePreview.Invalidate();
			Globals.MainForm.UpdateDisplay();
		}

		private void AddEventHandlers()
		{
			this.picImagePreview.Paint += new System.Windows.Forms.PaintEventHandler(this.picImagePreview_Paint);
			this.cboImageName.SelectedIndexChanged += new System.EventHandler(this.cboDynamicImage_SelectedIndexChanged);
			this.butImageBrowse.Click += new System.EventHandler(this.butImageBrowse_Click);
			this.trkHue.ValueChanged += new System.EventHandler(this.trkHue_ValueChanged);
			this.trkSaturation.ValueChanged += new System.EventHandler(this.trkSaturation_ValueChanged);
			this.trkBrightness.ValueChanged += new System.EventHandler(this.trkBrightness_ValueChanged);
			this.chkImageSizeable.CheckedChanged += new System.EventHandler(this.chkImageSizeable_CheckedChanged);
			this.cboLabelLink.SelectedIndexChanged += new System.EventHandler(this.cboLabelLink_SelectedIndexChanged);

			this.picLabelPreview.Paint += new System.Windows.Forms.PaintEventHandler(this.picLabelPreview_Paint);
			this.butInputCodeNew.Click += new System.EventHandler(this.butInputCodeNew_Click);
			this.butInputCodeDelete.Click += new System.EventHandler(this.butInputCodeDelete_Click);
			this.butLabelColor.Click += new System.EventHandler(this.butLabelColor_Click);
			this.butLabelFont.Click += new System.EventHandler(this.butLabelFont_Click);
			this.cboTextAlign.SelectedIndexChanged += new System.EventHandler(this.cboTextAlign_SelectedIndexChanged);
			this.rdoOutlineStyle.CheckedChanged += new System.EventHandler(this.rdoTextStyle_CheckedChanged);
			this.rdoShadowStyle.CheckedChanged += new System.EventHandler(this.rdoTextStyle_CheckedChanged);
			this.chkLabelSizeable.CheckedChanged += new System.EventHandler(this.chkLabelSizeable_CheckedChanged);
			this.cboLabelGroup.SelectedIndexChanged += new System.EventHandler(this.cboLabelGroup_SelectedIndexChanged);

			this.chkAlphaFade.CheckedChanged += new System.EventHandler(this.chkAlphaFade_CheckedChanged);
			this.nudAlphaFadeValue.ValueChanged += new System.EventHandler(this.nudAlphaFadeValue_ValueChanged);

			this.chkLabelArrow.CheckedChanged += new System.EventHandler(this.chkLabelArrow_CheckedChanged);
			this.chkLabelSpot.CheckedChanged += new System.EventHandler(this.chkLabelSpot_CheckedChanged);

			this.lvwInputCodes.ItemChanged += new ListViewEx.ItemChangedHandler(lvwInputCodes_ItemChanged);
			this.lvwInputCodes.ItemChecked += new ItemCheckedEventHandler(lvwInputCodes_ItemChecked);
		}

		private void RemoveEventHandlers()
		{
			this.picImagePreview.Paint -= new System.Windows.Forms.PaintEventHandler(this.picImagePreview_Paint);
			this.cboImageName.SelectedIndexChanged -= new System.EventHandler(this.cboDynamicImage_SelectedIndexChanged);
			this.butImageBrowse.Click -= new System.EventHandler(this.butImageBrowse_Click);
			this.trkHue.ValueChanged -= new System.EventHandler(this.trkHue_ValueChanged);
			this.trkSaturation.ValueChanged -= new System.EventHandler(this.trkSaturation_ValueChanged);
			this.trkBrightness.ValueChanged -= new System.EventHandler(this.trkBrightness_ValueChanged);
			this.chkImageSizeable.CheckedChanged -= new System.EventHandler(this.chkImageSizeable_CheckedChanged);
			this.cboLabelLink.SelectedIndexChanged -= new System.EventHandler(this.cboLabelLink_SelectedIndexChanged);

			this.picLabelPreview.Paint -= new System.Windows.Forms.PaintEventHandler(this.picLabelPreview_Paint);
			this.butInputCodeNew.Click -= new System.EventHandler(this.butInputCodeNew_Click);
			this.butInputCodeDelete.Click -= new System.EventHandler(this.butInputCodeDelete_Click);
			this.butLabelColor.Click -= new System.EventHandler(this.butLabelColor_Click);
			this.butLabelFont.Click -= new System.EventHandler(this.butLabelFont_Click);
			this.cboTextAlign.SelectedIndexChanged -= new System.EventHandler(this.cboTextAlign_SelectedIndexChanged);
			this.rdoOutlineStyle.CheckedChanged -= new System.EventHandler(this.rdoTextStyle_CheckedChanged);
			this.rdoShadowStyle.CheckedChanged -= new System.EventHandler(this.rdoTextStyle_CheckedChanged);
			this.chkLabelSizeable.CheckedChanged -= new System.EventHandler(this.chkLabelSizeable_CheckedChanged);
			this.cboLabelGroup.SelectedIndexChanged -= new System.EventHandler(this.cboLabelGroup_SelectedIndexChanged);

			this.chkAlphaFade.CheckedChanged -= new System.EventHandler(this.chkAlphaFade_CheckedChanged);
			this.nudAlphaFadeValue.ValueChanged -= new System.EventHandler(this.nudAlphaFadeValue_ValueChanged);

			this.chkLabelArrow.CheckedChanged -= new System.EventHandler(this.chkLabelArrow_CheckedChanged);
			this.chkLabelSpot.CheckedChanged -= new System.EventHandler(this.chkLabelSpot_CheckedChanged);

			this.lvwInputCodes.ItemChanged -= new ListViewEx.ItemChangedHandler(lvwInputCodes_ItemChanged);
			this.lvwInputCodes.ItemChecked -= new ItemCheckedEventHandler(lvwInputCodes_ItemChecked);
		}
		
#region Label

		private string[] CodeTypeNames =
        {
            "Player Code",
            "Key Code",
            "Joystick Code",
            "Mouse Code",
            "Misc Code",
            "Emulator Code",
            "Group Code",
            "Custom Text"
        };

		private bool TryGetSelectedLabel(out List<LabelObject> labelList)
		{
			labelList = new List<LabelObject>();

			foreach (LayoutObject layoutObject in Globals.SelectedObjectList)
			{
				if (layoutObject is LabelObject)
				{
					LabelObject labelObject = (LabelObject)layoutObject;
					labelList.Add(labelObject);
				}
			}

			if (labelList.Count > 0)
				return true;

			return false;
		}

		private void GetSelectedLabelInfo(LabelObject label)
		{
			try
			{
				//RemoveEventHandlers();

				tabControl1.TabPages.Clear();
				tabControl1.TabPages.Add(tabLabel);

				tabControl1.SelectedTab = tabLabel;

				butLabelColor.BackColor = label.Color;
				txtLabelFont.Text = label.Font.Name;
				chkLabelSizeable.Checked = label.Sizeable;

				chkLabelArrow.Checked = label.Arrow;
				chkLabelSpot.Checked = label.Spot;

				cboTextAlign.Text = label.TextAlign;

				if (label.TextStyle == "Shadow")
					rdoShadowStyle.Checked = true;
				else
					rdoOutlineStyle.Checked = true;

				PopulateInputCodesListView(label);

				ShowCheckedLabelCode(label.Name);

				cboLabelGroup.Items.Clear();
				cboLabelGroup.Items.Add("");

				cboLabelGroup.Items.AddRange(Globals.InputCodes.GroupCodes);

				cboLabelGroup.SelectedItem = label.Group;

				if (label.Name.StartsWith("[GROUP_"))
					grpLabelGroup.Enabled = false;
				else
					grpLabelGroup.Enabled = true;

				//AddEventHandlers();
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("GetSelectedLabelInfo", "ObjectForm", ex.Message, ex.StackTrace);
			}
		}

		private void picLabelPreview_Paint(object sender, PaintEventArgs e)
		{
			try
			{
				e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;

				List<LabelObject> labelArray = null;

				if (TryGetSelectedLabel(out labelArray))
				{
					Rectangle OldLocation = labelArray[0].Rect;

					labelArray[0].Rect.X = 0;
					labelArray[0].Rect.Y = 0;

					labelArray[0].Render(e.Graphics, true);

					labelArray[0].Rect = OldLocation;
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("picLabelPreview_Paint", "ObjectForm", ex.Message, ex.StackTrace);
			}
		}

		private void PopulateInputCodesListView(LabelObject label)
		{
			try
			{
				this.lvwInputCodes.ItemChanged -= new ListViewEx.ItemChangedHandler(lvwInputCodes_ItemChanged);
				this.lvwInputCodes.ItemChecked -= new ItemCheckedEventHandler(lvwInputCodes_ItemChecked);

				this.lvwInputCodes.Items.Clear();

				foreach (LabelCode code in label.Codes)
				{
					this.lvwInputCodes.Items.Add(code.Type);
					this.lvwInputCodes.Items[this.lvwInputCodes.Items.Count - 1].SubItems.Clear();
					this.lvwInputCodes.Items[this.lvwInputCodes.Items.Count - 1].SubItems.AddRange(new string[] { code.Type, code.Value });

					this.lvwInputCodes.AddComboBoxCell(-1, 1, CodeTypeNames);
					ChangeCodeValue(code.Type, this.lvwInputCodes.Items.Count - 1);
				}

				this.lvwInputCodes.ItemChanged += new ListViewEx.ItemChangedHandler(lvwInputCodes_ItemChanged);
				this.lvwInputCodes.ItemChecked += new ItemCheckedEventHandler(lvwInputCodes_ItemChecked);
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("PopulateInputCodesListView", "ObjectForm", ex.Message, ex.StackTrace);
			}
		}

		private void ChangeCodeValue(string CodeName, int row)
		{
			try
			{
				grpLabelGroup.Enabled = true;

				switch (CodeName)
				{
					case "Player Code":
						this.lvwInputCodes.AddComboBoxCell(row, 2, Globals.InputCodes.PlayerCodes);
						break;
					case "Key Code":
						this.lvwInputCodes.AddComboBoxCell(row, 2, Globals.InputCodes.KeyCodes);
						break;
					case "Joystick Code":
						this.lvwInputCodes.AddComboBoxCell(row, 2, Globals.InputCodes.JoyCodes);
						break;
					case "Mouse Code":
						this.lvwInputCodes.AddComboBoxCell(row, 2, Globals.InputCodes.MouseCodes);
						break;
					case "Misc Code":
						this.lvwInputCodes.AddComboBoxCell(row, 2, Globals.InputCodes.MiscCodes);
						break;
					case "Emulator Code":
						this.lvwInputCodes.AddComboBoxCell(row, 2, Globals.InputCodes.EmuCodes);
						break;
					case "Group Code":
						this.lvwInputCodes.AddComboBoxCell(row, 2, Globals.InputCodes.GroupCodes);
						grpLabelGroup.Enabled = false;
						break;
					case "Custom Text":
						this.lvwInputCodes.AddTextBoxCell(row, 2);
						break;
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("ChangeCodeValue", "ObjectForm", ex.Message, ex.StackTrace);
			}
		}

		private void ShowCheckedLabelCode(string CodeValue)
		{
			try
			{
				List<LabelObject> labelList = null;

				if (TryGetSelectedLabel(out labelList))
				{
					for (int i = 0; i < this.lvwInputCodes.Items.Count; i++)
					{
						if (this.lvwInputCodes.Items[i].SubItems[2].Text == CodeValue &&
							this.lvwInputCodes.Items[i].SubItems[2].Text != String.Empty)
						{
							labelList[0].Name = CodeValue;
							labelList[0].Text = CodeValue;

							this.lvwInputCodes.Items[i].Checked = true;
						}
						else
							this.lvwInputCodes.Items[i].Checked = false;
					}

					UpdateDisplay();
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("ShowCheckedLabelCode", "ObjectForm", ex.Message, ex.StackTrace);
			}
		}

		private void lvwInputCodes_ItemChanged(int row, int col, string oldText, string newText)
		{
			try
			{
				List<LabelObject> labelList = null;

				if (TryGetSelectedLabel(out labelList))
				{
					foreach (LabelObject labelObject in labelList)
					{
						switch (col)
						{
							case 1: // Code Type
								ChangeCodeValue(newText, row);

								labelObject.Codes[row].Type = newText;
								break;
							case 2: // Code Value
								ShowCheckedLabelCode(newText);

								labelObject.Codes[row].Value = newText;
								break;
						}
					}

					if (Globals.Layout != null)
						Globals.Layout.PromptToSave = true;
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("lvwInputCodes_ItemChanged", "ObjectForm", ex.Message, ex.StackTrace);
			}
		}

		private void lvwInputCodes_ItemChecked(object sender, ItemCheckedEventArgs e)
		{
			try
			{
				if (e.Item.Checked == true)
				{
					ShowCheckedLabelCode(e.Item.SubItems[2].Text);

					if (Globals.Layout != null)
						Globals.Layout.PromptToSave = true;
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("lvwInputCodes_ItemChecked", "ObjectForm", ex.Message, ex.StackTrace);
			}
		}

		private void butInputCodeNew_Click(object sender, EventArgs e)
		{
			try
			{
				List<LabelObject> labelList = null;
				string labelType = String.Empty;
				string labelValue = String.Empty;

				using (frmInput inputForm = new frmInput())
				{
					if (inputForm.ShowDialog(this) == DialogResult.OK)
					{
						if (frmInput.InputType == frmInput.InputTypes.Keyboard)
						{
							labelType = "Key Code";
							labelValue = frmInput.InputName;
						}

						if (frmInput.InputType == frmInput.InputTypes.Joystick)
						{
							labelType = "Joystick Code";
							labelValue = frmInput.InputName;
						}
					}
				}

				if (TryGetSelectedLabel(out labelList))
				{
					foreach (LabelObject labelObject in labelList)
					{
						labelObject.Codes.Add(new LabelCode(labelType, labelValue));

						this.lvwInputCodes.Items.Add(String.Empty);
						//this.lvwInputCodes.Items[this.lvwInputCodes.Items.Count - 1].Checked = false;
						this.lvwInputCodes.Items[this.lvwInputCodes.Items.Count - 1].SubItems.Clear();
						this.lvwInputCodes.Items[this.lvwInputCodes.Items.Count - 1].SubItems.AddRange(new string[] { labelType, labelValue });

						this.lvwInputCodes.AddComboBoxCell(-1, 1, CodeTypeNames);
						ChangeCodeValue(labelType, this.lvwInputCodes.Items.Count - 1);
					}

					if (Globals.Layout != null)
						Globals.Layout.PromptToSave = true;
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("butInputCodeNew_Click", "ObjectForm", ex.Message, ex.StackTrace);
			}
		}

		private void butInputCodeDelete_Click(object sender, EventArgs e)
		{
			try
			{
				if (lvwInputCodes.SelectedItems.Count == 0)
					return;

				int index = lvwInputCodes.SelectedItems[0].Index;

				List<LabelObject> labelList = null;

				if (TryGetSelectedLabel(out labelList))
				{
					foreach (LabelObject labelObject in labelList)
					{
						if (index < labelObject.Codes.Count)
							labelObject.Codes.RemoveAt(index);

						this.lvwInputCodes.Items[index].Remove();
					}

					if (Globals.Layout != null)
						Globals.Layout.PromptToSave = true;
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("butInputCodeDelete_Click", "ObjectForm", ex.Message, ex.StackTrace);
			}
		}

		private void butLabelColor_Click(object sender, EventArgs e)
		{
			try
			{
				ColorDialog colorDialog = new ColorDialog();

				colorDialog.Color = butLabelColor.BackColor;

				if (colorDialog.ShowDialog(this) == DialogResult.OK)
				{
					butLabelColor.BackColor = colorDialog.Color;

					List<LabelObject> labelList = null;

					if (TryGetSelectedLabel(out labelList))
					{
						foreach (LabelObject labelObject in labelList)
							labelObject.Color = colorDialog.Color;

						if (Globals.Layout != null)
							Globals.Layout.PromptToSave = true;

						UpdateDisplay();
					}
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("butLabelColor_Click", "ObjectForm", ex.Message, ex.StackTrace);
			}
		}

		private void butLabelFont_Click(object sender, EventArgs e)
		{
			try
			{
				List<LabelObject> labelList = null;
				FontDialog fontDialog = new FontDialog();

				fontDialog.ShowColor = false;

				if (TryGetSelectedLabel(out labelList))
				{
					foreach (LabelObject labelObject in labelList)
						fontDialog.Font = labelObject.Font;
				}

				if (fontDialog.ShowDialog(this) == DialogResult.OK)
				{
					txtLabelFont.Text = fontDialog.Font.Name;

					if (TryGetSelectedLabel(out labelList))
					{
						foreach (LabelObject labelObject in labelList)
							labelObject.Font = fontDialog.Font;

						if (Globals.Layout != null)
							Globals.Layout.PromptToSave = true;

						UpdateDisplay();
					}
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("butLabelFont_Click", "ObjectForm", ex.Message, ex.StackTrace);
			}
		}

		private void cboTextAlign_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				List<LabelObject> labelList = null;

				if (TryGetSelectedLabel(out labelList))
				{
					foreach (LabelObject labelObject in labelList)
						labelObject.TextAlign = cboTextAlign.Text;

					if (Globals.Layout != null)
						Globals.Layout.PromptToSave = true;

					UpdateDisplay();
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("cboTextAlign_SelectedIndexChanged", "ObjectForm", ex.Message, ex.StackTrace);
			}
		}

		private void rdoTextStyle_CheckedChanged(object sender, EventArgs e)
		{
			try
			{
				List<LabelObject> labelList = null;

				if (TryGetSelectedLabel(out labelList))
				{
					foreach (LabelObject labelObject in labelList)
						labelObject.TextStyle = (rdoOutlineStyle.Checked ? "Outline" : "Shadow");

					if (Globals.Layout != null)
						Globals.Layout.PromptToSave = true;

					UpdateDisplay();
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("rdoTextStyle_CheckedChanged", "ObjectForm", ex.Message, ex.StackTrace);
			}
		}

		private void chkLabelSizeable_CheckedChanged(object sender, EventArgs e)
		{
			try
			{
				List<LabelObject> labelList = null;

				if (TryGetSelectedLabel(out labelList))
				{
					foreach (LabelObject labelObject in labelList)
					{
						labelObject.Sizeable = chkLabelSizeable.Checked;

						if (!labelObject.Sizeable)
						{
							foreach (LabelCode code in labelObject.Codes)
							{
								if (code.Value == labelObject.Name)
								{
									labelObject.Name = code.Value;
									labelObject.Text = code.Value;
								}
							}
						}
					}

					if (Globals.Layout != null)
						Globals.Layout.PromptToSave = true;

					UpdateDisplay();
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("chkLabelSizeable_CheckedChanged", "ObjectForm", ex.Message, ex.StackTrace);
			}
		}

		private void cboLabelGroup_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				List<LabelObject> labelList = null;

				if (TryGetSelectedLabel(out labelList))
				{
					foreach (LabelObject labelObject in labelList)
						labelObject.Group = cboLabelGroup.SelectedItem.ToString();

					if (Globals.Layout != null)
						Globals.Layout.PromptToSave = true;

					UpdateDisplay();
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("cboLabelGroup_SelectedIndexChanged", "ObjectForm", ex.Message, ex.StackTrace);
			}
		}

		private void chkLabelArrow_CheckedChanged(object sender, EventArgs e)
		{
			try
			{
				List<LabelObject> labelList = null;

				if (TryGetSelectedLabel(out labelList))
				{
					foreach (LabelObject labelObject in labelList)
						labelObject.Arrow = chkLabelArrow.Checked;

					if (Globals.Layout != null)
						Globals.Layout.PromptToSave = true;

					UpdateDisplay();
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("chkLabelArrow_CheckedChanged", "ObjectForm", ex.Message, ex.StackTrace);
			}
		}

		private void chkLabelSpot_CheckedChanged(object sender, EventArgs e)
		{
			try
			{
				List<LabelObject> labelList = null;

				if (TryGetSelectedLabel(out labelList))
				{
					foreach (LabelObject labelObject in labelList)
						labelObject.Spot = chkLabelSpot.Checked;

					if (Globals.Layout != null)
						Globals.Layout.PromptToSave = true;

					UpdateDisplay();
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("chkLabelSpot_CheckedChanged", "ObjectForm", ex.Message, ex.StackTrace);
			}
		}

#endregion

#region Image

		private string[] ImageNames =
        {
            "[IMAGE_LAYOUTSUB]",
            "[IMAGE_CABINET]",
            "[IMAGE_CPANEL]",
            "[IMAGE_FLYER]",
            "[IMAGE_ICON]",
            "[IMAGE_MARQUEE]",
            "[IMAGE_PCB]",
            "[IMAGE_PREVIEW]",
            "[IMAGE_SELECT]",
            "[IMAGE_SNAP]",
            "[IMAGE_TITLE]",
            "[IMAGE_STATUS_BAR]",
            "[IMAGE_COLOR_BAR]",
            "[IMAGE_GRAPHIC_BAR]",
            "[IMAGE_EMULATION_BAR]",
            "[IMAGE_SOUND_BAR]",
            "[IMAGE_SAVE_STATE]",
            "[IMAGE_CPU_CHIPS]",
            "[IMAGE_AUDIO_CHIPS]",
            "[IMAGE_STARS]"
        };

		private bool TryGetSelectedImage(out List<ImageObject> imageList)
		{
			imageList = new List<ImageObject>();

			foreach (LayoutObject layoutObject in Globals.SelectedObjectList)
			{
				if (layoutObject is ImageObject)
				{
					ImageObject imageObject = (ImageObject)layoutObject;
					imageList.Add(imageObject);
				}
			}

			return (imageList.Count > 0);
		}

		private void GetSelectedImageInfo(ImageObject image)
		{
			try
			{
				//RemoveEventHandlers();

				tabControl1.TabPages.Clear();
				tabControl1.TabPages.Add(tabImage);

				tabControl1.SelectedTab = tabImage;

				cboImageName.Items.Clear();
				cboImageName.Items.AddRange(ImageNames);

				cboImageName.Text = image.Name;

				trkHue.Value = (int)image.Hue;
				trkSaturation.Value = (int)Math.Round((image.Saturation / 1f) * (trkSaturation.Maximum / 3f));
				trkBrightness.Value = (int)Math.Round((image.Brightness / 1f) * (trkBrightness.Maximum / 3f));

				chkImageSizeable.Checked = image.Sizeable;
				chkAlphaFade.Checked = image.AlphaFade;
				nudAlphaFadeValue.Value = image.AlphaValue;

				List<string> labelLinkList = new List<string>();
				Dictionary<string, int> labelCountHash = new Dictionary<string, int>();
				labelLinkList.Add("");

				foreach (LayoutObject layoutObject in Globals.Layout.LayoutObjectList)
				{
					if (layoutObject is LabelObject)
					{
						int labelCount = 0;
						LabelObject labelObject = (LabelObject)layoutObject;

						if (labelCountHash.TryGetValue(labelObject.Name, out labelCount))
							labelCountHash[labelObject.Name] = labelCount + 1;
						else
							labelCountHash.Add(labelObject.Name, 1);

						if (labelCount > 0)
							labelLinkList.Add(String.Format("{0} ({1})", labelObject.Name, labelCount));
						else
							labelLinkList.Add(labelObject.Name);
					}
				}

				labelLinkList.Sort();
				cboLabelLink.Items.Clear();
				cboLabelLink.Items.AddRange(labelLinkList.ToArray());
				cboLabelLink.SelectedIndex = cboLabelLink.FindString(image.LabelLink);

				//AddEventHandlers();
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("GetSelectedImageInfo", "ObjectForm", ex.Message, ex.StackTrace);
			}
		}

		private void picImagePreview_Paint(object sender, PaintEventArgs e)
		{
			try
			{
				List<ImageObject> imageArray = null;

				if (!TryGetSelectedImage(out imageArray))
					return;

				Rectangle OldLocation = imageArray[0].Rect;

				imageArray[0].Rect.X = 0;
				imageArray[0].Rect.Y = 0;

				imageArray[0].Render(e.Graphics, true);

				imageArray[0].Rect = OldLocation;
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("picImagePreview_Paint", "ObjectForm", ex.Message, ex.StackTrace);
			}
		}

		private void butImageBrowse_Click(object sender, EventArgs e)
		{
			try
			{
				string imageFileName = null;

				if (!FileIO.TryOpenImage(this, Settings.Folders.Media, out imageFileName))
					return;

				cboImageName.Text = FileIO.GetRelativeFolder(Settings.Folders.Media, imageFileName, false);

				List<ImageObject> imageArray = null;

				if (!TryGetSelectedImage(out imageArray))
					return;

				foreach (ImageObject image in imageArray)
				{
					image.Name = cboImageName.Text;
					image.Bitmap = FileIO.LoadImage(Path.Combine(Settings.Folders.Media, cboImageName.Text));

					if (image.Bitmap != null)
					{
						image.Rect.Width = image.Bitmap.Width;
						image.Rect.Height = image.Bitmap.Height;
					}
				}

				if (Globals.Layout != null)
					Globals.Layout.PromptToSave = true;

				UpdateDisplay();
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("butImageBrowse_Click", "ObjectForm", ex.Message, ex.StackTrace);
			}
		}

		private void cboDynamicImage_SelectedIndexChanged(object sender, EventArgs e)
		{
			List<ImageObject> imageList = null;

			if (!TryGetSelectedImage(out imageList))
				return;

			foreach (ImageObject image in imageList)
			{
				image.Name = cboImageName.Text;

				if (cboImageName.SelectedIndex >= 0)
				{
					chkImageSizeable.Checked = true;

					if (image.Bitmap != null)
					{
						image.Bitmap.Dispose();
						image.Bitmap = null;
					}
				}

				UpdateDisplay();
			}

			if (Globals.Layout != null)
				Globals.Layout.PromptToSave = true;
		}

		private void trkHue_ValueChanged(object sender, EventArgs e)
		{
			try
			{
				List<ImageObject> imageArray = null;

				if (!TryGetSelectedImage(out imageArray))
					return;

				foreach (ImageObject imageObject in imageArray)
					imageObject.Hue = trkHue.Value;

				if (Globals.Layout != null)
					Globals.Layout.PromptToSave = true;

				UpdateDisplay();
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("trkHue_ValueChanged", "ObjectForm", ex.Message, ex.StackTrace);
			}
		}

		private void trkSaturation_ValueChanged(object sender, EventArgs e)
		{
			try
			{
				List<ImageObject> imageList = null;

				if (!TryGetSelectedImage(out imageList))
					return;

				foreach (ImageObject imageObject in imageList)
					imageObject.Saturation = (float)Math.Round(trkSaturation.Value / (trkSaturation.Maximum / 3f));

				if (Globals.Layout != null)
					Globals.Layout.PromptToSave = true;

				UpdateDisplay();
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("trkSaturation_ValueChanged", "ObjectForm", ex.Message, ex.StackTrace);
			}
		}

		private void trkBrightness_ValueChanged(object sender, EventArgs e)
		{
			try
			{
				List<ImageObject> imageList = null;

				if (!TryGetSelectedImage(out imageList))
					return;

				foreach (ImageObject imageObject in imageList)
					imageObject.Brightness = (float)Math.Round(trkBrightness.Value / (trkBrightness.Maximum / 3f));

				if (Globals.Layout != null)
					Globals.Layout.PromptToSave = true;

				UpdateDisplay();
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("trkBrightness_ValueChanged", "ObjectForm", ex.Message, ex.StackTrace);
			}
		}

		private void chkImageSizeable_CheckedChanged(object sender, EventArgs e)
		{
			try
			{
				List<ImageObject> imageList = null;

				if (!TryGetSelectedImage(out imageList))
					return;

				foreach (ImageObject imageObject in imageList)
					imageObject.Sizeable = chkImageSizeable.Checked;

				if (Globals.Layout != null)
					Globals.Layout.PromptToSave = true;

				UpdateDisplay();
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("chkImageSizeable_CheckedChanged", "ObjectForm", ex.Message, ex.StackTrace);
			}
		}

		private void cboLabelLink_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				List<ImageObject> imageList = null;

				if (!TryGetSelectedImage(out imageList))
					return;

				foreach (ImageObject imageObject in imageList)
					imageObject.LabelLink = cboLabelLink.SelectedItem.ToString();

				if (Globals.Layout != null)
					Globals.Layout.PromptToSave = true;

				UpdateDisplay();
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("cboLabelLink_SelectedIndexChanged", "ObjectForm", ex.Message, ex.StackTrace);
			}
		}

		private void chkAlphaFade_CheckedChanged(object sender, EventArgs e)
		{
			try
			{
				List<ImageObject> imageList = null;

				if (!TryGetSelectedImage(out imageList))
					return;

				foreach (ImageObject imageObject in imageList)
					imageObject.AlphaFade = chkAlphaFade.Checked;

				if (Globals.Layout != null)
					Globals.Layout.PromptToSave = true;

				UpdateDisplay();
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("chkAlphaFade_CheckedChanged", "ObjectForm", ex.Message, ex.StackTrace);
			}
		}

		private void nudAlphaFadeValue_ValueChanged(object sender, EventArgs e)
		{
			try
			{
				List<ImageObject> imageList = null;

				if (!TryGetSelectedImage(out imageList))
					return;

				foreach (ImageObject imageObject in imageList)
					imageObject.AlphaValue = (int)nudAlphaFadeValue.Value;

				if (Globals.Layout != null)
					Globals.Layout.PromptToSave = true;

				UpdateDisplay();
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("nudAlphaFadeValue_ValueChanged", "ObjectForm", ex.Message, ex.StackTrace);
			}
		}

#endregion
	}
}