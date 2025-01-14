namespace PubEntry.Forms.Reports;

partial class AdvanceReport
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
		totalDataGridView = new DataGridView();
		advanceDataGridView = new DataGridView();
		versionLabel = new Label();
		brandingLabel = new Label();
		richTextBoxFooter = new RichTextBox();
		((System.ComponentModel.ISupportInitialize)totalDataGridView).BeginInit();
		((System.ComponentModel.ISupportInitialize)advanceDataGridView).BeginInit();
		SuspendLayout();
		// 
		// totalDataGridView
		// 
		totalDataGridView.AllowUserToAddRows = false;
		totalDataGridView.AllowUserToDeleteRows = false;
		totalDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		totalDataGridView.Location = new Point(12, 435);
		totalDataGridView.Name = "totalDataGridView";
		totalDataGridView.ReadOnly = true;
		totalDataGridView.Size = new Size(251, 150);
		totalDataGridView.TabIndex = 0;
		// 
		// advanceDataGridView
		// 
		advanceDataGridView.AllowUserToAddRows = false;
		advanceDataGridView.AllowUserToDeleteRows = false;
		advanceDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		advanceDataGridView.Location = new Point(12, 12);
		advanceDataGridView.Name = "advanceDataGridView";
		advanceDataGridView.ReadOnly = true;
		advanceDataGridView.Size = new Size(1083, 417);
		advanceDataGridView.TabIndex = 1;
		// 
		// versionLabel
		// 
		versionLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
		versionLabel.AutoSize = true;
		versionLabel.BackColor = Color.White;
		versionLabel.Location = new Point(3, 601);
		versionLabel.Name = "versionLabel";
		versionLabel.Size = new Size(84, 15);
		versionLabel.TabIndex = 40;
		versionLabel.Text = "Version: 0.0.0.0";
		// 
		// brandingLabel
		// 
		brandingLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
		brandingLabel.AutoSize = true;
		brandingLabel.BackColor = Color.White;
		brandingLabel.Location = new Point(1026, 601);
		brandingLabel.Name = "brandingLabel";
		brandingLabel.Size = new Size(76, 15);
		brandingLabel.TabIndex = 39;
		brandingLabel.Text = "© AADISOFT";
		// 
		// richTextBoxFooter
		// 
		richTextBoxFooter.Dock = DockStyle.Bottom;
		richTextBoxFooter.Location = new Point(0, 595);
		richTextBoxFooter.Name = "richTextBoxFooter";
		richTextBoxFooter.Size = new Size(1107, 26);
		richTextBoxFooter.TabIndex = 38;
		richTextBoxFooter.Text = "";
		// 
		// AdvanceReport
		// 
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		ClientSize = new Size(1107, 621);
		Controls.Add(versionLabel);
		Controls.Add(brandingLabel);
		Controls.Add(richTextBoxFooter);
		Controls.Add(advanceDataGridView);
		Controls.Add(totalDataGridView);
		Name = "AdvanceReport";
		Text = "AdvanceReport";
		Load += AdvanceReport_Load;
		((System.ComponentModel.ISupportInitialize)totalDataGridView).EndInit();
		((System.ComponentModel.ISupportInitialize)advanceDataGridView).EndInit();
		ResumeLayout(false);
		PerformLayout();
	}

	#endregion

	private DataGridView totalDataGridView;
	private DataGridView advanceDataGridView;
	private Label versionLabel;
	private Label brandingLabel;
	private RichTextBox richTextBoxFooter;
}