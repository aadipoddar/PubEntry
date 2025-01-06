namespace PubEntry.Forms.Reports;

partial class SelectDataForm
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectDataForm));
		toDateLabel = new Label();
		formDateLabel = new Label();
		fromDateTimePicker = new DateTimePicker();
		summaryReportButton = new Button();
		detailReportButton = new Button();
		brandingLabel = new Label();
		richTextBoxFooter = new RichTextBox();
		versionLabel = new Label();
		toDateTimePicker = new DateTimePicker();
		locationComboBox = new ComboBox();
		SuspendLayout();
		// 
		// toDateLabel
		// 
		toDateLabel.AutoSize = true;
		toDateLabel.Font = new Font("Segoe UI", 15F);
		toDateLabel.Location = new Point(48, 137);
		toDateLabel.Name = "toDateLabel";
		toDateLabel.Size = new Size(36, 28);
		toDateLabel.TabIndex = 33;
		toDateLabel.Text = "To:";
		// 
		// formDateLabel
		// 
		formDateLabel.AutoSize = true;
		formDateLabel.Font = new Font("Segoe UI", 15F);
		formDateLabel.Location = new Point(22, 97);
		formDateLabel.Name = "formDateLabel";
		formDateLabel.Size = new Size(62, 28);
		formDateLabel.TabIndex = 30;
		formDateLabel.Text = "From:";
		// 
		// fromDateTimePicker
		// 
		fromDateTimePicker.CustomFormat = "dd-MMM-yy   hh tt";
		fromDateTimePicker.Font = new Font("Segoe UI", 15F);
		fromDateTimePicker.Format = DateTimePickerFormat.Custom;
		fromDateTimePicker.Location = new Point(90, 92);
		fromDateTimePicker.Name = "fromDateTimePicker";
		fromDateTimePicker.Size = new Size(216, 34);
		fromDateTimePicker.TabIndex = 2;
		fromDateTimePicker.Value = new DateTime(2025, 1, 6, 17, 0, 0, 0);
		// 
		// summaryReportButton
		// 
		summaryReportButton.Font = new Font("Segoe UI", 15F);
		summaryReportButton.Location = new Point(90, 198);
		summaryReportButton.Name = "summaryReportButton";
		summaryReportButton.Size = new Size(175, 38);
		summaryReportButton.TabIndex = 4;
		summaryReportButton.Text = "Summary Report";
		summaryReportButton.UseVisualStyleBackColor = true;
		summaryReportButton.Click += summaryReportButton_Click;
		// 
		// detailReportButton
		// 
		detailReportButton.Font = new Font("Segoe UI", 15F);
		detailReportButton.Location = new Point(90, 255);
		detailReportButton.Name = "detailReportButton";
		detailReportButton.Size = new Size(175, 38);
		detailReportButton.TabIndex = 5;
		detailReportButton.Text = "Detail Report";
		detailReportButton.UseVisualStyleBackColor = true;
		detailReportButton.Click += detailReportButton_Click;
		// 
		// brandingLabel
		// 
		brandingLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
		brandingLabel.AutoSize = true;
		brandingLabel.BackColor = Color.White;
		brandingLabel.Location = new Point(281, 335);
		brandingLabel.Name = "brandingLabel";
		brandingLabel.Size = new Size(76, 15);
		brandingLabel.TabIndex = 36;
		brandingLabel.Text = "© AADISOFT";
		// 
		// richTextBoxFooter
		// 
		richTextBoxFooter.Dock = DockStyle.Bottom;
		richTextBoxFooter.Location = new Point(0, 328);
		richTextBoxFooter.Name = "richTextBoxFooter";
		richTextBoxFooter.Size = new Size(360, 26);
		richTextBoxFooter.TabIndex = 35;
		richTextBoxFooter.Text = "";
		// 
		// versionLabel
		// 
		versionLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
		versionLabel.AutoSize = true;
		versionLabel.BackColor = Color.White;
		versionLabel.Location = new Point(5, 335);
		versionLabel.Name = "versionLabel";
		versionLabel.Size = new Size(84, 15);
		versionLabel.TabIndex = 37;
		versionLabel.Text = "Version: 0.0.0.0";
		// 
		// toDateTimePicker
		// 
		toDateTimePicker.CustomFormat = "dd-MMM-yy   hh tt";
		toDateTimePicker.Font = new Font("Segoe UI", 15F);
		toDateTimePicker.Format = DateTimePickerFormat.Custom;
		toDateTimePicker.Location = new Point(90, 132);
		toDateTimePicker.Name = "toDateTimePicker";
		toDateTimePicker.Size = new Size(216, 34);
		toDateTimePicker.TabIndex = 3;
		toDateTimePicker.Value = new DateTime(2025, 1, 6, 5, 0, 0, 0);
		// 
		// locationComboBox
		// 
		locationComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
		locationComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
		locationComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
		locationComboBox.Font = new Font("Segoe UI", 15F);
		locationComboBox.FormattingEnabled = true;
		locationComboBox.Location = new Point(35, 27);
		locationComboBox.Name = "locationComboBox";
		locationComboBox.Size = new Size(271, 36);
		locationComboBox.TabIndex = 1;
		// 
		// SelectDataForm
		// 
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		ClientSize = new Size(360, 354);
		Controls.Add(locationComboBox);
		Controls.Add(toDateTimePicker);
		Controls.Add(versionLabel);
		Controls.Add(brandingLabel);
		Controls.Add(richTextBoxFooter);
		Controls.Add(detailReportButton);
		Controls.Add(toDateLabel);
		Controls.Add(formDateLabel);
		Controls.Add(fromDateTimePicker);
		Controls.Add(summaryReportButton);
		Icon = (Icon)resources.GetObject("$this.Icon");
		Name = "SelectDataForm";
		StartPosition = FormStartPosition.CenterScreen;
		Text = "Select Data";
		Load += SelectDataForm_Load;
		ResumeLayout(false);
		PerformLayout();
	}

	#endregion

	private Label toDateLabel;
	private Label formDateLabel;
	private DateTimePicker fromDateTimePicker;
	private Button summaryReportButton;
	private Button detailReportButton;
	private Label brandingLabel;
	private RichTextBox richTextBoxFooter;
	private Label versionLabel;
	private DateTimePicker toDateTimePicker;
	private ComboBox locationComboBox;
}