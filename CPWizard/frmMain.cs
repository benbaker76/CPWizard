using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace CPWizard
{
	public partial class frmMain : Form
	{
		public enum CloseMethodType
		{
			Unknown,
			OSShutdown,
			XButton,
			Menu,
			Auto
		}

		public bool ShiftDown = false;
		public CloseMethodType CloseType = CloseMethodType.Unknown;

		public frmMain()
		{
			InitializeComponent();
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			this.SetStyle(ControlStyles.Opaque, true);
			this.SetStyle(ControlStyles.DoubleBuffer, true);
			this.SetStyle(ControlStyles.ResizeRedraw, true);
			this.SetStyle(ControlStyles.UserPaint, true);

			SetTitleBar();

			if (Settings.General.FirstRun)
			{
				Globals.OptionsForm = new frmOptions();
				Globals.OptionsForm.GoToMAMETab();
				Globals.OptionsForm.ShowDialog(this);
			}
		}

		private void MainForm_Shown(object sender, EventArgs e)
		{
			WindowTools.ReadFormLocationIni(Settings.Files.Ini, this, !Settings.General.Minimized);

			if (Settings.General.Minimized)
				HideForm();
		}

		private void pictureBox1_Paint(object sender, PaintEventArgs e)
		{
			if (Globals.Layout != null)
			{
				pictureBox1.Width = Globals.Layout.Width;
				pictureBox1.Height = Globals.Layout.Height;
			}

			Rectangle Rect = Rectangle.Empty;
			int ObjCount = 0;

			if (Globals.SelectedObjectList != null)
			{
				ObjCount = Globals.SelectedObjectList.Count;

				if (Globals.SelectedObjectList.Count > 0)
				{
					Rect = Globals.SelectedObjectList[0].Rect;

					foreach (LayoutObject layoutObject in Globals.SelectedObjectList)
						Rect = Rectangle.Union(Rect, layoutObject.Rect);

					tsslInfo4.Text = Globals.SelectedObjectList[0].Name;
				}
				else
					tsslInfo4.Text = "[None]";
			}

			tsslInfo1.Text = String.Format("X: {0}, Y: {1}", Rect.X, Rect.Y);
			tsslInfo2.Text = String.Format("W: {0}, H: {1}", Rect.Width, Rect.Height);
			tsslInfo3.Text = String.Format("{0} Object(s) Selected", ObjCount);

			Globals.VisibleRect = new Rectangle(Math.Abs(panel1.AutoScrollPosition.X), Math.Abs(panel1.AutoScrollPosition.Y), panel1.Width, panel1.Height);

			EventManager.Paint(sender, e);
		}

		private void pictureBox1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			EventManager.MouseDown(sender, e);

			//if (Global.Layout == null)
			//    return;

			//Global.Layout.ObjectArray.Sort();

			//if (Global.ObjectForm != null)
			//    Global.ObjectForm.GetSelectedObjectInfo();

			pictureBox1.Invalidate();
		}

		private void pictureBox1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			EventManager.MouseUp(sender, e);

			if (Globals.Layout == null)
				return;

			//Global.Layout.ObjectArray.Sort();
			//Global.Layout.SortObjectArray();

			if (Globals.ObjectForm != null)
				Globals.ObjectForm.GetSelectedObjectInfo();

			pictureBox1.Invalidate();
		}

		private void pictureBox1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			EventManager.MouseMove(sender, e);

			pictureBox1.Invalidate();
		}

		private void pictureBox1_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			ShowObjectProperties();
		}

		private void notifyIcon1_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			ShowForm();
		}

		private void MainForm_Resize(object sender, EventArgs e)
		{
			if (this.WindowState == FormWindowState.Minimized)
				HideForm();
		}

		private void ShowForm()
		{
			if (Settings.Display.SubScreenEnable)
			{
				if (Globals.LayoutSubForm != null)
					Globals.LayoutSubForm.Close();
			}

			if (Globals.ProgramManager != null)
				Globals.ProgramManager.CloseLoadingScreens();

			if (Globals.LayoutForm != null)
				Globals.LayoutForm.Close();

			this.Show();
			this.SetTopLevel(true);
			this.BringToFront();
			this.Focus();
			this.Activate();
			this.WindowState = FormWindowState.Normal;
			pictureBox1.Invalidate();
			Settings.General.Minimized = false;
		}

		private void HideForm()
		{
			PromptToSave();

			this.Hide();
			Settings.General.Minimized = true;
		}

		protected override bool IsInputKey(Keys keyData)
		{
			switch (keyData)
			{
				case Keys.Up:
				case Keys.Down:
				case Keys.Left:
				case Keys.Right:
					return true;
				default:
					return base.IsInputKey(keyData);
			}
		}

		private void MainForm_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			ShiftDown = e.Shift;

			if (e.KeyCode == Keys.Up)
			{
				foreach (LayoutObject layoutObject in Globals.SelectedObjectList)
					layoutObject.Rect.Y--;

				pictureBox1.Invalidate();
			}
			if (e.KeyCode == Keys.Down)
			{
				foreach (LayoutObject layoutObject in Globals.SelectedObjectList)
					layoutObject.Rect.Y++;

				pictureBox1.Invalidate();
			}
			if (e.KeyCode == Keys.Left)
			{
				foreach (LayoutObject layoutObject in Globals.SelectedObjectList)
					layoutObject.Rect.X--;

				pictureBox1.Invalidate();
			}
			if (e.KeyCode == Keys.Right)
			{
				foreach (LayoutObject layoutObject in Globals.SelectedObjectList)
					layoutObject.Rect.X++;

				pictureBox1.Invalidate();
			}
		}

		private void MainForm_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			ShiftDown = e.Shift;

			if (e.Control && e.KeyCode == Keys.C)
				CopyObject();

			if (e.Control && e.KeyCode == Keys.X)
				CutObject();

			if (e.Control && e.KeyCode == Keys.V)
				PasteObject();

			if (e.KeyCode == Keys.Delete)
				DeleteObject();
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (CloseType == CloseMethodType.XButton ||
				CloseType == CloseMethodType.Menu)
			{
				WindowTools.WriteFormLocationIni(Settings.Files.Ini, this);
				
				PromptToSave();

				if (MessageBox.Show("Really Quit?", "Quitting", MessageBoxButtons.YesNo) == DialogResult.No)
				{
					e.Cancel = true;
					CloseType = CloseMethodType.Unknown;
					return;
				}
			}
		}

		private void tsNew_Click(object sender, EventArgs e)
		{
			NewLayout();
		}

		private void tsMenuOpen_Click(object sender, EventArgs e)
		{
			OpenLayout();
		}

		private void tsMenuSave_Click(object sender, EventArgs e)
		{
			SaveLayout();
		}

		private void tsMenuSaveAs_Click(object sender, EventArgs e)
		{
			SaveAsLayout();
		}

		private void tsImportLayout_Click(object sender, EventArgs e)
		{
			ImportLayout();
		}

		private void tsExportLayout_Click(object sender, EventArgs e)
		{
			ExportLayout();
		}

		private void tsExportBatch_Click(object sender, EventArgs e)
		{
			ExportBatch();
		}

		private void tsMenuPrint_Click(object sender, EventArgs e)
		{
			PrintLayout();
		}

		private void tsMenuExit_Click(object sender, EventArgs e)
		{
			Globals.MainForm.CloseType = frmMain.CloseMethodType.Menu;
			this.Close();
		}

		private void tsMenuCut_Click(object sender, EventArgs e)
		{
			CutObject();
		}

		private void tsMenuCopy_Click(object sender, EventArgs e)
		{
			CopyObject();
		}

		private void tsMenuPaste_Click(object sender, EventArgs e)
		{
			PasteObject();
		}

		private void tsMenuDelete_Click(object sender, EventArgs e)
		{
			DeleteObject();
		}

		private void tsMenuOptions_Click(object sender, EventArgs e)
		{
			ShowOptions();
		}

		private void tsMenuRotate90CW_Click(object sender, EventArgs e)
		{

		}

		private void tsMenuRotate90CCW_Click(object sender, EventArgs e)
		{

		}

		private void tsMenuFlipVertical_Click(object sender, EventArgs e)
		{

		}

		private void tsMenuFlipHorizontal_Click(object sender, EventArgs e)
		{

		}

		private void tsMenuLeft_Click(object sender, EventArgs e)
		{
			if (Globals.SelectedObjectList.Count == 1)
				Globals.SelectedObjectList[0].AlignLeft();
		}

		private void tsMenuHorizontalCenter_Click(object sender, EventArgs e)
		{
			if (Globals.SelectedObjectList.Count == 1)
				if (Globals.Layout != null)
					Globals.SelectedObjectList[0].HorizontalCenter(Globals.Layout.LayoutBitmap.Width);
		}

		private void tsMenuRight_Click(object sender, EventArgs e)
		{
			if (Globals.SelectedObjectList.Count == 1)
				if (Globals.Layout != null)
					Globals.SelectedObjectList[0].AlignRight(Globals.Layout.LayoutBitmap.Width);
		}

		private void tsMenuTop_Click(object sender, EventArgs e)
		{
			if (Globals.SelectedObjectList.Count == 1)
				if (Globals.Layout != null)
					Globals.SelectedObjectList[0].AlignTop();
		}

		private void tsMenuVerticalCenter_Click(object sender, EventArgs e)
		{
			if (Globals.SelectedObjectList.Count == 1)
				if (Globals.Layout != null)
					Globals.SelectedObjectList[0].VerticalCenter(Globals.Layout.LayoutBitmap.Height);
		}

		private void tsMenuBottom_Click(object sender, EventArgs e)
		{
			if (Globals.SelectedObjectList.Count == 1)
				if (Globals.Layout != null)
					Globals.SelectedObjectList[0].AlignBotton(Globals.Layout.LayoutBitmap.Height);

		}

		private void tsMenuAdjustColor_Click(object sender, EventArgs e)
		{
		}

		private void tsMenuAddImage_Click(object sender, EventArgs e)
		{
			AddImage();
		}

		private void tsMenuAddLabel_Click(object sender, EventArgs e)
		{
			AddLabel();
		}

		private void tsMenuProperties_Click(object sender, EventArgs e)
		{
			ShowObjectProperties();
		}

		private void tsMenuAbout_Click(object sender, EventArgs e)
		{
			using (frmAbout AboutForm = new frmAbout())
			{
				AboutForm.ShowDialog(this);
			}
		}

		private void tsHelpFileMenu_Click(object sender, EventArgs e)
		{
			try
			{
				System.Diagnostics.Process.Start(Settings.Files.HelpFile);
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("tsHelpFileMenu_Click", "MainForm", ex.Message, ex.StackTrace);
			}
		}

		private void tsButNew_Click(object sender, EventArgs e)
		{
			NewLayout();
		}

		private void tsButOpen_Click(object sender, EventArgs e)
		{
			OpenLayout();
		}

		private void tsButSave_Click(object sender, EventArgs e)
		{
			SaveLayout();
		}

		private void tsButPrint_Click(object sender, EventArgs e)
		{
			PrintLayout();
		}

		private void tsButAddImage_Click(object sender, EventArgs e)
		{
			AddImage();
		}

		private void tsButAddLabel_Click(object sender, EventArgs e)
		{
			AddLabel();
		}

		private void tsButDelete_Click(object sender, EventArgs e)
		{
			DeleteObject();
		}

		private void tsButProperties_Click(object sender, EventArgs e)
		{
			ShowObjectProperties();
		}

		private void tsButCut_Click(object sender, EventArgs e)
		{
			CutObject();
		}

		private void tsButCopy_Click(object sender, EventArgs e)
		{
			CopyObject();
		}

		private void tsButPaste_Click(object sender, EventArgs e)
		{
			PasteObject();
		}

		private void cmMenuAddImage_Click(object sender, EventArgs e)
		{
			AddImage();
		}

		private void cmMenuAddLabel_Click(object sender, EventArgs e)
		{
			AddLabel();
		}

		private void cmMenuCut_Click(object sender, EventArgs e)
		{
			CutObject();
		}

		private void cmMenuCopy_Click(object sender, EventArgs e)
		{
			CopyObject();
		}

		private void cmMenuPaste_Click(object sender, EventArgs e)
		{
			PasteObject();
		}

		private void cmMenuDelete_Click(object sender, EventArgs e)
		{
			DeleteObject();
		}

		private void cmMenuProperties_Click(object sender, EventArgs e)
		{
			ShowObjectProperties();
		}

		private void tsPreview_Click(object sender, EventArgs e)
		{
			ShowPreview();
		}

		private void tsButToggleGrid_Click(object sender, EventArgs e)
		{
			Settings.Display.ShowGrid = tsButToggleGrid.Checked;
			Settings.Display.SnapToGrid = Settings.Display.ShowGrid;

			pictureBox1.Invalidate();
		}

		private void ShowObjectProperties()
		{
			try
			{
				if (Globals.SelectedObjectList.Count == 0)
					return;

				if (Globals.ObjectForm != null)
					return;

				Globals.ObjectForm = new frmObject();
				Globals.ObjectForm.StartPosition = FormStartPosition.Manual;

				int x = this.Location.X + (this.Width / 2) - (Globals.ObjectForm.Width / 2);
				int y = this.Location.Y + (this.Height / 2) - (Globals.ObjectForm.Height / 2);

				Globals.ObjectForm.Location = new Point(x, y);
				Globals.ObjectForm.Show(this);
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("ShowObjectProperties", "MainForm", ex.Message, ex.StackTrace);
			}
		}

		private void ShowPreview()
		{
			try
			{
				using (frmPreview previewForm = new frmPreview())
					previewForm.ShowDialog(this);
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("ShowPreview", "MainForm", ex.Message, ex.StackTrace);
			}
		}

		private void ShowOptions()
		{
			if (Globals.OptionsForm == null)
			{
				Globals.OptionsForm = new frmOptions();
				Globals.OptionsForm.ShowDialog(this);
			}
		}

		private void NewLayout()
		{
			try
			{
				if (Globals.Layout != null)
				{
					Globals.Layout.Dispose();
					Globals.Layout = null;
				}

				if (Globals.LayoutList != null)
				{
					for (int i = 0; i < Globals.LayoutList.Count; i++)
					{
						if (Globals.LayoutList[i] != null)
						{
							Globals.LayoutList[i].Dispose();
							Globals.LayoutList[i] = null;
						}
					}
				}

				Globals.Layout = new Layout();
				//Global.LayoutRestore = Global.Layout;
				Globals.LayoutList = null;
				Globals.Layout.BackgroundFileName = "BlankBak.png";
				Globals.Layout.LoadBackground();
				Settings.Layout.Name = null;
				Globals.SelectedObjectList.Clear();
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("NewLayout", "MainForm", ex.Message, ex.StackTrace);
			}

			pictureBox1.Invalidate();
		}

		private void OpenLayout()
		{
			try
			{
				OpenFileDialog fd = new OpenFileDialog();

				fd.Title = "Open Layout";
				fd.InitialDirectory = Settings.Folders.Layout;
				fd.FileName = Settings.Layout.Name + ".xml";
				fd.Filter = "Layout Files (*.xml)|*.xml|All Files (*.*)|*.*";
				fd.RestoreDirectory = true;
				fd.CheckFileExists = true;

				if (fd.ShowDialog(this) == DialogResult.OK)
				{
					if (Globals.ProgramManager.LoadLayout(fd.FileName, ref Globals.Layout, ref Globals.LayoutList, false))
					{
						Settings.Layout.Name = Globals.Layout.Name;

						SetTitleBar();
					}
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("OpenLayout", "MainForm", ex.Message, ex.StackTrace);
			}

			pictureBox1.Invalidate();
		}

		public void SaveLayout()
		{
			try
			{
				if (Settings.Layout.Name == null)
				{
					SaveAsLayout();
					return;
				}

				if (Globals.Layout != null)
				{
					Globals.Layout.WriteLayout(Path.Combine(Settings.Folders.Layout, Settings.Layout.Name + ".xml"));
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("SaveLayout", "MainForm", ex.Message, ex.StackTrace);
			}
		}

		private void SaveAsLayout()
		{
			try
			{
				if (Globals.Layout != null)
				{
					string fileName = null;

					if (FileIO.TrySaveLayout(this, out fileName))
					{
						Settings.Layout.Name = FileIO.GetRelativeFolder(Settings.Folders.Layout, fileName, true);
						Globals.Layout.WriteLayout(Path.Combine(Settings.Folders.Layout, Settings.Layout.Name + ".xml"));
						Globals.Layout.Name = Settings.Layout.Name;

						SetTitleBar();
					}
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("SaveAsLayout", "MainForm", ex.Message, ex.StackTrace);
			}
		}

		private void ImportLayout()
		{
			try
			{
				OpenFileDialog fd = new OpenFileDialog();

				fd.Title = "Import Layout";
				fd.InitialDirectory = Settings.Folders.Layout;
				fd.Filter = "Layout Files (*.xml)|*.xml|All Files (*.*)|*.*";
				fd.RestoreDirectory = true;
				fd.CheckFileExists = true;

				if (fd.ShowDialog(this) == DialogResult.OK)
				{
					Globals.ProgramManager.ImportLayout(fd.FileName);
					Globals.SelectedObjectList.Clear();
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("ImportLayout", "MainForm", ex.Message, ex.StackTrace);
			}

			pictureBox1.Invalidate();
		}

		private void ExportLayout()
		{
			string fileName = null;

			try
			{
				if (FileIO.TrySaveImage(this, out fileName))
				{
					using (Bitmap bitmap = new Bitmap(Globals.Layout.Width, Globals.Layout.Height))
					{
						using (Graphics g = Graphics.FromImage(bitmap))
						{
							Globals.DisplayManager.DrawBackground(g, BakType.Default, bitmap.Width, bitmap.Height, true, false, 0, false, false, 1.0f);

							Globals.LayoutManager.Paint(g, 0, 0, bitmap.Width, bitmap.Height, false);

							Globals.DisplayManager.DrawWatermark(bitmap);

							bitmap.Save(fileName, System.Drawing.Imaging.ImageFormat.Png);
						}
					}
				}
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("ExportLayout", "frmExportBatch", ex.Message, ex.StackTrace);
			}
		}

		private void ExportBatch()
		{
			try
			{
				using (frmExport exportForm = new frmExport())
					exportForm.ShowDialog(this);
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("ExportBatch", "MainForm", ex.Message, ex.StackTrace);
			}
		}

		private void PrintLayout()
		{
			try
			{
				PrintDialog pd = new PrintDialog();

				pd.Document = printDocument1;

				if (pd.ShowDialog(this) == DialogResult.OK)
					printDocument1.Print();
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("PrintLayout", "MainForm", ex.Message, ex.StackTrace);
			}
		}

		private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
		{
			Globals.LayoutManager.DrawLayout(e.Graphics, ref Globals.Layout, false);
		}

		private void CutObject()
		{
			try
			{
				if (Globals.SelectedObjectList.Count == 0)
					return;

				Globals.DisplayManager.MultiSelectRect = Rectangle.Empty;
				Globals.Layout.PromptToSave = true;
				Globals.ClipboardObjectList.Clear();

				foreach (LayoutObject layoutObject in Globals.SelectedObjectList)
				{
					Globals.ClipboardObjectList.Add(layoutObject);
					Globals.Layout.LayoutObjectList.Remove(layoutObject);
				}

				Globals.SelectedObjectList.Clear();

				pictureBox1.Invalidate();
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("CutObject", "MainForm", ex.Message, ex.StackTrace);
			}
		}

		private void CopyObject()
		{
			if (Globals.SelectedObjectList.Count == 0)
				return;

			Globals.ClipboardObjectList.Clear();

			foreach (LayoutObject layoutObject in Globals.SelectedObjectList)
				Globals.ClipboardObjectList.Add(layoutObject);
		}

		private void PasteObject()
		{
			try
			{
				if (Globals.ClipboardObjectList.Count == 0)
					return;

				Globals.Layout.PromptToSave = true;
				Globals.SelectedObjectList.Clear();

				foreach (LayoutObject layoutObject in Globals.ClipboardObjectList)
				{
					LayoutObject layoutObjectCopy = (LayoutObject)layoutObject.Clone();

					layoutObjectCopy.Rect.X += 16;
					layoutObjectCopy.Rect.Y += 16;

					Globals.Layout.LayoutObjectList.Add(layoutObjectCopy);
					Globals.SelectedObjectList.Add(layoutObjectCopy);

					Globals.Layout.SortObjectList();
				}

				Globals.ClipboardObjectList.Clear();

				foreach (LayoutObject layoutObject in Globals.SelectedObjectList)
					Globals.ClipboardObjectList.Add(layoutObject);

				if (Globals.ObjectForm != null)
					Globals.ObjectForm.GetSelectedObjectInfo();

				pictureBox1.Invalidate();
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("PasteObject", "MainForm", ex.Message, ex.StackTrace);
			}
		}

		private void AddImage()
		{
			try
			{
				Globals.Layout.PromptToSave = true;
				Point centerDisplay = Globals.DisplayManager.GetCenterDisplay();

				ImageObject imageObject = new ImageObject("New Image", new Rectangle(centerDisplay.X, centerDisplay.Y, 32, 32), false, "", Settings.Display.AlphaFade, Settings.Display.AlphaFadeValue);

				Globals.Layout.LayoutObjectList.Add(imageObject);
				Globals.Layout.SortObjectList();

				Globals.SelectedObjectList.Clear();
				Globals.SelectedObjectList.Add(imageObject);

				pictureBox1.Invalidate();
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("AddImage", "MainForm", ex.Message, ex.StackTrace);
			}
		}

		private void AddLabel()
		{
			try
			{
				Globals.Layout.PromptToSave = true;
				Point centerDisplay = Globals.DisplayManager.GetCenterDisplay();

				LabelObject labelObject = new LabelObject("New Label", null, new Rectangle(centerDisplay.X, centerDisplay.Y, 32, 32), false, Settings.Display.LabelArrowShow, Settings.Display.LabelSpotShow);

				labelObject.Codes.Add(new LabelCode("Custom Text", "New Label"));

				Globals.Layout.LayoutObjectList.Add(labelObject);
				Globals.Layout.SortObjectList();

				Globals.SelectedObjectList.Clear();
				Globals.SelectedObjectList.Add(labelObject);

				pictureBox1.Invalidate();
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("AddLabel", "MainForm", ex.Message, ex.StackTrace);
			}
		}

		private void DeleteObject()
		{
			try
			{
				if (Globals.SelectedObjectList.Count == 0)
					return;

				Globals.DisplayManager.MultiSelectRect = Rectangle.Empty;

				foreach (LayoutObject layoutObject in Globals.SelectedObjectList)
				{
					Globals.Layout.LayoutObjectList.Remove(layoutObject);
					layoutObject.Dispose();
				}

				Globals.SelectedObjectList.Clear();

				pictureBox1.Invalidate();
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("DeleteObject", "MainForm", ex.Message, ex.StackTrace);
			}
		}

		private void PromptToSave()
		{
			if (Globals.Layout == null)
				return;

			if (!Globals.Layout.PromptToSave)
				return;

			if (MessageBox.Show("Save Layout?", "Save Layout", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				SaveLayout();
			else
				Globals.Layout.PromptToSave = false;
		}

		public void UpdateDisplay()
		{
			pictureBox1.Invalidate();
		}

		public void SetTitleBar()
		{
            var version = Assembly.GetExecutingAssembly().GetName().Version;

            this.Text = String.Format("CPWizard {0} [{1}]", version.ToString(3), (Globals.Layout == null ? "None" : Settings.Layout.Name));
		}

		public void SetMenuEnabled(bool enabled)
		{
			mnuMain.Enabled = enabled;
		}

		public void SetToolbarEnabled(bool enabled)
		{
			tbMain.Enabled = enabled;
		}

		private const int WM_SYSCOMMAND = 0x0112;
		private const int WM_SETTINGCHANGE = 0x001A;
		private const int WM_DISPLAYCHANGE = 0x007E;

		private const int SC_CLOSE = 0xF060;
		private const int SPI_SETWORKAREA = 0x002F;

		protected override void WndProc(ref Message msg)
		{
			switch (msg.Msg)
			{
				case WM_SYSCOMMAND:
					if (msg.WParam.ToInt32() == SC_CLOSE)
						CloseType = CloseMethodType.XButton;
					break;
				case WM_SETTINGCHANGE:
					//if (msg.WParam == IntPtr.Zero)
					//    LogFile.WriteLine(String.Format("WM_SETTINGCHANGE WParam={0}, LParam={1}", msg.WParam, msg.LParam));
					break;
				case WM_DISPLAYCHANGE:
					//LogFile.WriteLine(String.Format("WM_DISPLAYCHANGE WParam={0}, LParam={1}", msg.WParam, msg.LParam));
					break;
			}

			EventManager.WndProc(ref msg);

			base.WndProc(ref msg);
		}

		protected override bool ShowWithoutActivation
		{
			get { return true; }
		}

		private void tsslHeadsoftLogo_Click(object sender, EventArgs e)
		{
            try
            {
                var processStartInfo = new ProcessStartInfo
                {
                    FileName = "https://baker76.com",
                    UseShellExecute = true
                };
                Process.Start(processStartInfo);
            }
            catch
            {
            }
        }
	}
}