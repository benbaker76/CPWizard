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
	partial class frmObject
	{
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
	}
}
