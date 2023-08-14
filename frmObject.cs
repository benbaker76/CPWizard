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
	}
}