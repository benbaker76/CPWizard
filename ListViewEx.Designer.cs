namespace CPWizard
{
	partial class ListViewEx
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.TextBox = new System.Windows.Forms.TextBox();
			this.ComboBox = new System.Windows.Forms.ComboBox();
			this.Button = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// TextBox
			// 
			this.TextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.TextBox.Location = new System.Drawing.Point(0, 0);
			this.TextBox.Name = "TextBox";
			this.TextBox.Size = new System.Drawing.Size(100, 20);
			this.TextBox.TabIndex = 0;
			this.TextBox.Visible = false;
			this.TextBox.Leave += new System.EventHandler(this.TextBox_Leave);
			this.TextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TextBox_KeyUp);
			// 
			// ComboBox
			// 
			this.ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ComboBox.Location = new System.Drawing.Point(0, 0);
			this.ComboBox.Name = "ComboBox";
			this.ComboBox.Size = new System.Drawing.Size(121, 21);
			this.ComboBox.TabIndex = 1;
			this.ComboBox.Visible = false;
			this.ComboBox.SelectedIndexChanged += new System.EventHandler(this.ComboBox_SelectedIndexChanged);
			this.ComboBox.Leave += new System.EventHandler(this.ComboBox_Leave);
			this.ComboBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ComboBox_KeyUp);
			// 
			// Button
			// 
			this.Button.Location = new System.Drawing.Point(0, 0);
			this.Button.Name = "Button";
			this.Button.Size = new System.Drawing.Size(75, 23);
			this.Button.TabIndex = 2;
			this.Button.Visible = false;
			this.Button.Click += new System.EventHandler(this.Button_Click);
			// 
			// ListViewEx
			// 
			this.Controls.Add(this.TextBox);
			this.Controls.Add(this.ComboBox);
			this.Controls.Add(this.Button);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
	}
}
