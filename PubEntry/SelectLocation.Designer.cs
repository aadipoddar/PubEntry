namespace PubEntry;

partial class SelectLocation
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
		locationComboBox = new ComboBox();
		employeeComboBox = new ComboBox();
		goButton = new Button();
		summaryReportButton = new Button();
		passwordTextBox = new TextBox();
		fromDateTimePicker = new DateTimePicker();
		toDateTimePicker = new DateTimePicker();
		formDateLabel = new Label();
		fromTimeTextBox = new TextBox();
		toTimeTextBox = new TextBox();
		fromTimeLabel = new Label();
		toTimeLabel = new Label();
		toDateLabel = new Label();
		brandingLabel = new Label();
		richTextBoxFooter = new RichTextBox();
		SuspendLayout();
		// 
		// locationComboBox
		// 
		locationComboBox.Font = new Font("Segoe UI", 15F);
		locationComboBox.FormattingEnabled = true;
		locationComboBox.Location = new Point(90, 52);
		locationComboBox.Name = "locationComboBox";
		locationComboBox.Size = new Size(271, 36);
		locationComboBox.TabIndex = 10;
		// 
		// employeeComboBox
		// 
		employeeComboBox.Font = new Font("Segoe UI", 15F);
		employeeComboBox.FormattingEnabled = true;
		employeeComboBox.Location = new Point(90, 118);
		employeeComboBox.Name = "employeeComboBox";
		employeeComboBox.Size = new Size(271, 36);
		employeeComboBox.TabIndex = 11;
		// 
		// goButton
		// 
		goButton.Font = new Font("Segoe UI", 15F);
		goButton.Location = new Point(161, 248);
		goButton.Name = "goButton";
		goButton.Size = new Size(118, 38);
		goButton.TabIndex = 13;
		goButton.Text = "GO";
		goButton.UseVisualStyleBackColor = true;
		goButton.Click += goButton_Click;
		// 
		// summaryReportButton
		// 
		summaryReportButton.Font = new Font("Segoe UI", 15F);
		summaryReportButton.Location = new Point(152, 474);
		summaryReportButton.Name = "summaryReportButton";
		summaryReportButton.Size = new Size(175, 38);
		summaryReportButton.TabIndex = 18;
		summaryReportButton.Text = "Summary Report";
		summaryReportButton.UseVisualStyleBackColor = true;
		summaryReportButton.Click += summaryReportButton_Click;
		// 
		// passwordTextBox
		// 
		passwordTextBox.Font = new Font("Segoe UI", 18F);
		passwordTextBox.Location = new Point(90, 184);
		passwordTextBox.Name = "passwordTextBox";
		passwordTextBox.PasswordChar = '*';
		passwordTextBox.PlaceholderText = "Password";
		passwordTextBox.Size = new Size(271, 39);
		passwordTextBox.TabIndex = 12;
		// 
		// fromDateTimePicker
		// 
		fromDateTimePicker.Font = new Font("Segoe UI", 15F);
		fromDateTimePicker.Format = DateTimePickerFormat.Short;
		fromDateTimePicker.Location = new Point(27, 341);
		fromDateTimePicker.Name = "fromDateTimePicker";
		fromDateTimePicker.Size = new Size(131, 34);
		fromDateTimePicker.TabIndex = 14;
		// 
		// toDateTimePicker
		// 
		toDateTimePicker.Font = new Font("Segoe UI", 15F);
		toDateTimePicker.Format = DateTimePickerFormat.Short;
		toDateTimePicker.Location = new Point(331, 341);
		toDateTimePicker.Name = "toDateTimePicker";
		toDateTimePicker.Size = new Size(135, 34);
		toDateTimePicker.TabIndex = 16;
		// 
		// formDateLabel
		// 
		formDateLabel.AutoSize = true;
		formDateLabel.Font = new Font("Segoe UI", 15F);
		formDateLabel.Location = new Point(37, 310);
		formDateLabel.Name = "formDateLabel";
		formDateLabel.Size = new Size(104, 28);
		formDateLabel.TabIndex = 19;
		formDateLabel.Text = "Form Date";
		// 
		// fromTimeTextBox
		// 
		fromTimeTextBox.Font = new Font("Segoe UI", 18F);
		fromTimeTextBox.Location = new Point(55, 420);
		fromTimeTextBox.Name = "fromTimeTextBox";
		fromTimeTextBox.PlaceholderText = "24hr Time";
		fromTimeTextBox.RightToLeft = RightToLeft.Yes;
		fromTimeTextBox.Size = new Size(53, 39);
		fromTimeTextBox.TabIndex = 15;
		fromTimeTextBox.Text = "5";
		fromTimeTextBox.KeyPress += fromTimeTextBox_KeyPress;
		// 
		// toTimeTextBox
		// 
		toTimeTextBox.Font = new Font("Segoe UI", 18F);
		toTimeTextBox.Location = new Point(366, 420);
		toTimeTextBox.Name = "toTimeTextBox";
		toTimeTextBox.PlaceholderText = "24hr Time";
		toTimeTextBox.RightToLeft = RightToLeft.Yes;
		toTimeTextBox.Size = new Size(53, 39);
		toTimeTextBox.TabIndex = 17;
		toTimeTextBox.KeyPress += fromTimeTextBox_KeyPress;
		// 
		// fromTimeLabel
		// 
		fromTimeLabel.AutoSize = true;
		fromTimeLabel.Font = new Font("Segoe UI", 15F);
		fromTimeLabel.Location = new Point(37, 385);
		fromTimeLabel.Name = "fromTimeLabel";
		fromTimeLabel.Size = new Size(105, 28);
		fromTimeLabel.TabIndex = 21;
		fromTimeLabel.Text = "From Time";
		// 
		// toTimeLabel
		// 
		toTimeLabel.AutoSize = true;
		toTimeLabel.Font = new Font("Segoe UI", 15F);
		toTimeLabel.Location = new Point(353, 385);
		toTimeLabel.Name = "toTimeLabel";
		toTimeLabel.Size = new Size(79, 28);
		toTimeLabel.TabIndex = 23;
		toTimeLabel.Text = "To Time";
		// 
		// toDateLabel
		// 
		toDateLabel.AutoSize = true;
		toDateLabel.Font = new Font("Segoe UI", 15F);
		toDateLabel.Location = new Point(353, 310);
		toDateLabel.Name = "toDateLabel";
		toDateLabel.Size = new Size(78, 28);
		toDateLabel.TabIndex = 24;
		toDateLabel.Text = "To Date";
		// 
		// brandingLabel
		// 
		brandingLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
		brandingLabel.AutoSize = true;
		brandingLabel.BackColor = Color.White;
		brandingLabel.Location = new Point(408, 552);
		brandingLabel.Name = "brandingLabel";
		brandingLabel.Size = new Size(75, 15);
		brandingLabel.TabIndex = 30;
		brandingLabel.Text = "© AADISOFT";
		// 
		// richTextBoxFooter
		// 
		richTextBoxFooter.Dock = DockStyle.Bottom;
		richTextBoxFooter.Location = new Point(0, 546);
		richTextBoxFooter.Name = "richTextBoxFooter";
		richTextBoxFooter.Size = new Size(487, 26);
		richTextBoxFooter.TabIndex = 29;
		richTextBoxFooter.Text = "";
		// 
		// SelectLocation
		// 
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		ClientSize = new Size(487, 572);
		Controls.Add(brandingLabel);
		Controls.Add(richTextBoxFooter);
		Controls.Add(toDateLabel);
		Controls.Add(toTimeLabel);
		Controls.Add(fromTimeLabel);
		Controls.Add(toTimeTextBox);
		Controls.Add(fromTimeTextBox);
		Controls.Add(formDateLabel);
		Controls.Add(toDateTimePicker);
		Controls.Add(fromDateTimePicker);
		Controls.Add(passwordTextBox);
		Controls.Add(summaryReportButton);
		Controls.Add(goButton);
		Controls.Add(employeeComboBox);
		Controls.Add(locationComboBox);
		Name = "SelectLocation";
		StartPosition = FormStartPosition.CenterScreen;
		Text = "Select Location";
		ResumeLayout(false);
		PerformLayout();
	}

	#endregion

	private ComboBox locationComboBox;
	private ComboBox employeeComboBox;
	private Button goButton;
	private Button summaryReportButton;
	private TextBox passwordTextBox;
	private DateTimePicker fromDateTimePicker;
	private DateTimePicker toDateTimePicker;
	private Label formDateLabel;
	private TextBox fromTimeTextBox;
	private TextBox toTimeTextBox;
	private Label fromTimeLabel;
	private Label toTimeLabel;
	private Label toDateLabel;
	private Label brandingLabel;
	private RichTextBox richTextBoxFooter;
}