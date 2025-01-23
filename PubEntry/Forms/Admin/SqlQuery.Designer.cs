namespace PubEntry.Forms.Admin;

partial class SqlQuery
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

	#region Windows Form Designer generated code

	/// <summary>
	/// Required method for Designer support - do not modify
	/// the contents of this method with the code editor.
	/// </summary>
	private void InitializeComponent()
	{
		Syncfusion.Windows.Forms.Edit.Implementation.Config.Config config1 = new Syncfusion.Windows.Forms.Edit.Implementation.Config.Config();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SqlQuery));
		runButton = new Button();
		tabControl1 = new TabControl();
		queryEditControl = new Syncfusion.Windows.Forms.Edit.EditControl();
		((System.ComponentModel.ISupportInitialize)queryEditControl).BeginInit();
		SuspendLayout();
		// 
		// runButton
		// 
		runButton.Location = new Point(12, 12);
		runButton.Name = "runButton";
		runButton.Size = new Size(75, 23);
		runButton.TabIndex = 2;
		runButton.Text = "Run";
		runButton.UseVisualStyleBackColor = true;
		runButton.Click += runButton_Click;
		// 
		// tabControl1
		// 
		tabControl1.Location = new Point(12, 230);
		tabControl1.Name = "tabControl1";
		tabControl1.SelectedIndex = 0;
		tabControl1.Size = new Size(912, 447);
		tabControl1.TabIndex = 3;
		// 
		// queryEditControl
		// 
		queryEditControl.AllowZoom = false;
		queryEditControl.ChangedLinesMarkingLineColor = Color.FromArgb(255, 238, 98);
		queryEditControl.CodeSnipptSize = new Size(100, 100);
		queryEditControl.Configurator = config1;
		queryEditControl.ContextChoiceBackColor = Color.FromArgb(255, 255, 255);
		queryEditControl.ContextChoiceBorderColor = Color.FromArgb(233, 166, 50);
		queryEditControl.ContextChoiceForeColor = SystemColors.InfoText;
		queryEditControl.IndicatorMarginBackColor = Color.Empty;
		queryEditControl.LineNumbersColor = Color.FromArgb(0, 128, 128);
		queryEditControl.Location = new Point(12, 41);
		queryEditControl.Name = "queryEditControl";
		queryEditControl.RenderRightToLeft = false;
		queryEditControl.ScrollPosition = new Point(0, 0);
		queryEditControl.SelectionTextColor = Color.FromArgb(173, 214, 255);
		queryEditControl.ShowEndOfLine = false;
		queryEditControl.Size = new Size(912, 183);
		queryEditControl.StatusBarSettings.CoordsPanel.Width = 150;
		queryEditControl.StatusBarSettings.EncodingPanel.Width = 100;
		queryEditControl.StatusBarSettings.FileNamePanel.Width = 100;
		queryEditControl.StatusBarSettings.InsertPanel.Width = 33;
		queryEditControl.StatusBarSettings.Offcie2007ColorScheme = Syncfusion.Windows.Forms.Office2007Theme.Blue;
		queryEditControl.StatusBarSettings.Offcie2010ColorScheme = Syncfusion.Windows.Forms.Office2010Theme.Blue;
		queryEditControl.StatusBarSettings.StatusPanel.Width = 70;
		queryEditControl.StatusBarSettings.TextPanel.Width = 214;
		queryEditControl.StatusBarSettings.VisualStyle = Syncfusion.Windows.Forms.Tools.Controls.StatusBar.VisualStyle.Default;
		queryEditControl.TabIndex = 2;
		queryEditControl.Text = "";
		queryEditControl.UseXPStyleBorder = true;
		queryEditControl.VisualColumn = 1;
		queryEditControl.VScrollMode = Syncfusion.Windows.Forms.Edit.ScrollMode.Immediate;
		queryEditControl.ZoomFactor = 1F;
		queryEditControl.KeyDown += queryEditControl_KeyDown;
		// 
		// SqlQuery
		// 
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		ClientSize = new Size(936, 689);
		Controls.Add(queryEditControl);
		Controls.Add(tabControl1);
		Controls.Add(runButton);
		Icon = (Icon)resources.GetObject("$this.Icon");
		Name = "SqlQuery";
		StartPosition = FormStartPosition.CenterScreen;
		Text = "SqlQuery";
		((System.ComponentModel.ISupportInitialize)queryEditControl).EndInit();
		ResumeLayout(false);
	}

	#endregion

	private Button runButton;
	private TabControl tabControl1;
	private Syncfusion.Windows.Forms.Edit.EditControl queryEditControl;
}