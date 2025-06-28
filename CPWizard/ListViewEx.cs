using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace CPWizard
{
	public partial class ListViewEx : ListView
	{
		public enum MoveDirection { Up, Down };
		public enum CellType { Normal, TextBox, ComboBox, ComboBoxBoolean, Button };

		public delegate void ItemChangedHandler(int row, int col, string oldText, string newText);
		public event ItemChangedHandler ItemChanged;

		public bool AddSubItem = false;
		public bool HideComboAfterSelChange = true;

		private int Row = -1;
		private int Col = -1;

		private TextBox TextBox = null;
		private ComboBox ComboBox = null;
		private Button Button = null;

		private bool mouseDown = false;

		private Dictionary<SubItem, CellData> CustomCells = new Dictionary<SubItem, CellData>();

		private bool m_doubleClickDoesCheck = false;
		private bool m_inDoubleClickCheckHack = false;

		public class CellData
		{
			public CellType Type = CellType.Normal;
			public string Text = null;
			public StringCollection Data = null;
			public bool Editable = false;

			public CellData(CellType type)
			{
				Type = type;
			}

			public CellData(CellType type, string text)
			{
				Type = type;
				Text = text;
			}

			public CellData(CellType type, StringCollection data, bool editable)
			{
				Type = type;
				Data = data;
				Editable = editable;
			}
		}

		internal class SubItem
		{
			public readonly int Row;
			public readonly int Col;

			public SubItem(int row, int col)
			{
				Row = row;
				Col = col;
			}
		}

		public ListViewEx()
		{
			InitializeComponent();
		}

		public ListViewEx(IContainer container)
		{
			container.Add(this);

			InitializeComponent();
		}

		private Win32.RECT GetSubItemRect(Point clickPoint)
		{
			Win32.RECT subItemRect = new Win32.RECT();

			Row = Col = -1;

			ListViewItem item = this.GetItemAt(clickPoint.X, clickPoint.Y);

			if (item != null)
			{
				for (int index = 0; index < this.Columns.Count; index++)
				{
					subItemRect.Top = index + 1;
					subItemRect.Left = Win32.LVIR_BOUNDS;

					try
					{
						int result = Win32.SendMessage(this.Handle, Win32.LVM_GETSUBITEMRECT, item.Index, ref subItemRect);
						if (result != 0)
						{
							if (clickPoint.X < subItemRect.Left)
							{
								Row = item.Index;
								Col = 0;
								break;
							}
							if (clickPoint.X >= subItemRect.Left & clickPoint.X <= subItemRect.Right)
							{
								Row = item.Index;
								Col = index + 1;
								break;
							}
						}
						else
						{
							throw new Win32Exception();
						}
					}
					catch (Win32Exception ex)
					{
						Trace.WriteLine(string.Format("Exception while getting subitem rect, {0}", ex.Message));
					}
				}
			}
			return subItemRect;
		}

		public void SetCellText(int row, int col, string newText)
		{
			SetCellText(row, col, newText, false);
		}

		public void SetCellText(int row, int col, string newText, bool raiseEvent)
		{
			string oldText = Items[row].SubItems[col].Text;

			if (oldText != newText)
			{
				Items[row].SubItems[col].Text = newText;

				if (raiseEvent && ItemChanged != null)
					ItemChanged(row, col, oldText, newText);
			}
		}

		public void RemoveCustomCell(int row, int col)
		{
			SubItem cell = GetKey(new SubItem(row, col));

			if (cell != null)
				CustomCells.Remove(cell);
		}

		public void AddTextBoxCell(int row, int col)
		{
			RemoveCustomCell(row, col);

			CustomCells[new SubItem(row, col)] = new CellData(CellType.TextBox);
		}

		public void AddButtonCell(int row, int col)
		{
			RemoveCustomCell(row, col);

			CustomCells[new SubItem(row, col)] = new CellData(CellType.Button);
		}

		public void AddButtonCell(int row, int col, string text)
		{
			RemoveCustomCell(row, col);

			CustomCells[new SubItem(row, col)] = new CellData(CellType.Button, text);
		}

		public void AddComboBoxCell(int row, int col, StringCollection data, bool editable)
		{
			RemoveCustomCell(row, col);

			CustomCells[new SubItem(row, col)] = new CellData(CellType.ComboBox, data, editable);
		}

		public void AddComboBoxBooleanCell(int row, int col)
		{
			AddComboBoxCell(row, col, new string[] { "True", "False" }, false);
		}

		public void AddComboBoxCell(int row, int col, string[] data)
		{
			AddComboBoxCell(row, col, data, false);
		}

		public void AddComboBoxCell(int row, int col, string[] data, bool editable)
		{
			try
			{
				StringCollection param = new StringCollection();
				param.AddRange(data);
				AddComboBoxCell(row, col, param, editable);
			}
			catch (Exception ex)
			{
				Trace.WriteLine(ex.ToString());
			}
		}


		public void MoveListViewItem(MoveDirection direction)
		{
			string cache;
			int index;

			index = SelectedItems[0].Index;

			if (direction == MoveDirection.Up)
			{
				if (index == 0)
					return;

				for (int i = 0; i < this.Items[index].SubItems.Count; i++)
				{
					cache = this.Items[index - 1].SubItems[i].Text;
					this.Items[index - 1].SubItems[i].Text = this.Items[index].SubItems[i].Text;
					this.Items[index].SubItems[i].Text = cache;
				}
				this.Items[index - 1].Selected = true;
				this.Refresh();
				this.Focus();
			}
			else
			{
				if (index == this.Items.Count - 1)
					return;
				for (int i = 0; i < this.Items[index].SubItems.Count; i++)
				{
					cache = this.Items[index + 1].SubItems[i].Text;
					this.Items[index + 1].SubItems[i].Text = this.Items[index].SubItems[i].Text;
					this.Items[index].SubItems[i].Text = cache;
				}
				this.Items[index + 1].Selected = true;
				this.Refresh();
				this.Focus();
			}
		}

		private void ShowComboBox(Point location, Size sz, StringCollection data, bool editable)
		{
			try
			{
				ComboBox.Size = sz;
				ComboBox.Location = location;

				ComboBox.Items.Clear();

				foreach (string text in data)
					ComboBox.Items.Add(text);

				if (editable)
					ComboBox.DropDownStyle = ComboBoxStyle.DropDown;
				else
					ComboBox.DropDownStyle = ComboBoxStyle.DropDownList;

				ComboBox.Text = this.Items[Row].SubItems[Col].Text;
				ComboBox.DropDownWidth = GetDropDownWidth(data);

				ComboBox.Show();
			}
			catch (ArgumentOutOfRangeException)
			{
			}
		}

		private void ShowTextBox(Point location, Size sz)
		{
			try
			{
				TextBox.Size = sz;
				TextBox.Location = location;

				TextBox.Text = this.Items[Row].SubItems[Col].Text;

				TextBox.Show();
				TextBox.Focus();
			}
			catch (ArgumentOutOfRangeException)
			{
			}
		}

		private void ShowButton(Point location, Size sz)
		{
			ShowButton(location, sz, this.Items[Row].SubItems[Col].Text);
		}

		private void ShowButton(Point location, Size sz, string text)
		{
			try
			{
				Button.Size = sz;
				Button.Location = location;

				Button.Text = text;

				Button.Show();
				Button.Focus();
			}
			catch (ArgumentOutOfRangeException)
			{
			}
		}

		protected override void OnMouseUp(System.Windows.Forms.MouseEventArgs e)
		{
			try
			{
				TextBox.Visible = ComboBox.Visible = false;

				if (!mouseDown)
					return;

				if (!this.FullRowSelect || this.View != View.Details)
					return;

				mouseDown = false;

				Win32.RECT rect = this.GetSubItemRect(new Point(e.X, e.Y));

				//System.Windows.Forms.MessageBox.Show(Col.ToString() + ":" + Row.ToString());

				if (Row != -1 && Col != -1)
				{
					SubItem cell = GetKey(new SubItem(Row, Col));

					if (cell != null)
					{
						Size sz = new Size(this.Columns[Col].Width, Items[Row].Bounds.Height);

						Point location = Col == 0 ? new Point(0, rect.Top) : new Point(rect.Left, rect.Top);

						ValidateAndAddSubItems();

						CellData data = CustomCells[cell];

						switch (data.Type)
						{
							case CellType.ComboBox:
								ShowComboBox(location, sz, data.Data, data.Editable);
								break;
							case CellType.TextBox:
								ShowTextBox(location, sz);
								break;
							case CellType.Button:
								if (data.Text != null)
									ShowButton(location, sz, data.Text);
								else
									ShowButton(location, sz);
								break;
						}
					}
				}
			}
			catch (Exception ex)
			{
				Trace.WriteLine(ex.ToString());
			}
		}

		private void ValidateAndAddSubItems()
		{
			try
			{
				while (Items[Row].SubItems.Count < Columns.Count && AddSubItem)
				{
					Items[Row].SubItems.Add("");
				}
			}
			catch (Exception ex)
			{
				Trace.WriteLine(ex.ToString());
			}
		}

		private int GetDropDownWidth(StringCollection data)
		{
			if (data.Count == 0)
				return ComboBox.Width;

			string maximum = data[0];

			foreach (string text in data)
			{
				if (maximum.Length < text.Length)
					maximum = text;
			}
			return (int)(this.CreateGraphics().MeasureString(maximum, this.Font).Width);
		}

		private SubItem GetKey(SubItem cell)
		{
			try
			{
				foreach (SubItem key in CustomCells.Keys)
				{
					if (key.Row == cell.Row && key.Col == cell.Col)
						return key;
					else if (key.Row == -1 && key.Col == cell.Col)
						return key;
					else if (key.Row == cell.Row && key.Col == -1)
						return key;
					else if (key.Row == -1 && key.Col == -1)
						return key;
				}
			}
			catch (Exception ex)
			{
				Trace.WriteLine(ex.ToString());
			}
			return null;
		}

		protected override void OnMouseDoubleClick(System.Windows.Forms.MouseEventArgs e)
		{
			try
			{
				mouseDown = true;
				//this.Capture = true;

				//System.Windows.Forms.MessageBox.Show(e.X + ":" + e.Y);

				HideControls();
			}
			catch (Exception ex)
			{
				Trace.WriteLine(ex.ToString());
			}
		}

		private void TextBox_Leave(object sender, EventArgs e)
		{
			try
			{
				if (Row != -1 && Col != -1)
				{
					TextBox.Hide();

					SetCellText(Row, Col, TextBox.Text, true);
				}
			}
			catch (Exception ex)
			{
				Trace.WriteLine(ex.ToString());
			}
		}

		private void TextBox_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
				TextBox_Leave(null, new EventArgs());
		}

		private void ComboBox_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
				ComboBox_Leave(null, new EventArgs());
		}

		private void ComboBox_Leave(object sender, EventArgs e)
		{
			try
			{
				if (Row != -1 && Col != -1)
				{
					ComboBox.Hide();

					SetCellText(Row, Col, ComboBox.Text, true);
				}
			}
			catch (Exception ex)
			{
				Trace.WriteLine(ex.ToString());
			}
		}

		private void Button_Click(object sender, System.EventArgs e)
		{
			try
			{
				if (Row != -1 && Col != -1)
				{
					Button.Hide();

					if (ItemChanged != null)
						ItemChanged(Row, Col, null, null);
				}
			}
			catch (Exception ex)
			{
				Trace.WriteLine(ex.ToString());
			}
		}

		private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				if (Row != -1 && Col != -1)
				{
					ComboBox.Visible = !HideComboAfterSelChange;

					SetCellText(Row, Col, ComboBox.Text, true);
				}
			}
			catch (Exception ex)
			{
				Trace.WriteLine(ex.ToString());
			}
		}

		private void HideControls()
		{
			TextBox.Hide();
			ComboBox.Hide();
			Button.Hide();
		}

		private unsafe void OnWmReflectNotify(ref Message msg)
		{
			if (!DoubleClickDoesCheck && CheckBoxes)
			{
				Win32.NMHDR* nmhdr = (Win32.NMHDR*)msg.LParam;

				if (nmhdr->code == Win32.NM_DBLCLK)
					m_inDoubleClickCheckHack = true;
			}
		}

		[Browsable(true), Description("When CheckBoxes is true, this controls whether or not double clicking will toggle the check."), Category("My Controls"), DefaultValue(true)]
		public bool DoubleClickDoesCheck
		{
			get { return m_doubleClickDoesCheck; }
			set { m_doubleClickDoesCheck = value; }
		}

		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message msg)
		{
			switch (msg.Msg)
			{
				case Win32.WM_VSCROLL:
				case Win32.WM_HSCROLL:
					HideControls();
					break;
				case Win32.WM_NOTIFY:
					Win32.NMHDR h = (Win32.NMHDR)Marshal.PtrToStructure(msg.LParam, typeof(Win32.NMHDR));
					if (h.code == Win32.HDN_BEGINDRAG ||
						h.code == Win32.HDN_ITEMCHANGINGA ||
						h.code == Win32.HDN_ITEMCHANGINGW)
						HideControls();
					break;
				/* case Win32.WM_LBUTTONDOWN:
					Int16 x = (Int16) msg.LParam;
					Int16 y = (Int16)((int) msg.LParam >> 16);
					Point cursorPosition = new Point(x, y);

					System.Diagnostics.Debug.WriteLine(cursorPosition.X + ":" + cursorPosition.Y);

					if (!this.Bounds.Contains(cursorPosition))
						HideControls();
					break; */
				case Win32.WM_REFLECT + Win32.WM_NOTIFY:
					OnWmReflectNotify(ref msg);
					break;
				case Win32.LVM_HITTEST:
					if (m_inDoubleClickCheckHack)
					{
						m_inDoubleClickCheckHack = false;
						msg.Result = (System.IntPtr)(-1);
						return;
					}
					break;
			}

			base.WndProc(ref msg);
		}
	}

	partial class Win32
	{
		public const int LVM_GETSUBITEMRECT = (0x1000) + 56;
		public const int LVIR_BOUNDS = 0;
		public const uint OBJID_VSCROLL = 0xFFFFFFFB;
		public const int WM_VSCROLL = 0x115;
		public const int WM_HSCROLL = 0x114;
		//public const int WM_LBUTTONDOWN = 0x0201;
		public const int WM_NOTIFY = 0x4E;

		public const int HDN_FIRST = -300;
		public const int HDN_BEGINDRAG = (HDN_FIRST - 10);
		public const int HDN_ITEMCHANGINGA = (HDN_FIRST - 0);
		public const int HDN_ITEMCHANGINGW = (HDN_FIRST - 20);

		public const int WM_USER = 0x0400;
		public const int WM_REFLECT = WM_USER + 0x1C00;
		public const int LVM_HITTEST = (0x1000 + 18);
		public const int NM_DBLCLK = (-3);

		[StructLayout(LayoutKind.Sequential)]
		public struct NMHDR
		{
			public IntPtr hwndFrom;
			public IntPtr idFrom;
			public int code;
		}

		/* [StructLayout(LayoutKind.Sequential)]
		public struct RECT
		{
			public int Left;
			public int Top;
			public int Right;
			public int Bottom;
		} */

		[StructLayout(LayoutKind.Sequential)]
		public struct SCROLLBARINFO
		{
			public int cbSize;
			public Rectangle rcScrollBar;
			public int dxyLineButton;
			public int xyThumbTop;
			public int xyThumbBottom;
			public int reserved;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
			public int[] rgstate;
		}

		[DllImport("user32.dll", SetLastError = true)]
		public static extern int SendMessage(IntPtr hWnd, int messageID, int wParam, ref RECT lParam);
	}
}
