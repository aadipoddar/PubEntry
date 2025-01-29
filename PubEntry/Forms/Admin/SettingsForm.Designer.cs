namespace PubEntry.Forms.Admin;

partial class SettingsForm
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
		saveButton = new Button();
		inactivityTimeTextBox = new TextBox();
		inactivityTimeLabel = new Label();
		thermalGroupBox = new GroupBox();
		label13 = new Label();
		footerFontStyleComboBox = new ComboBox();
		label14 = new Label();
		footerFontFamilyComboBox = new ComboBox();
		label15 = new Label();
		footerFontSizeTextBox = new TextBox();
		label16 = new Label();
		label8 = new Label();
		regularFontStyleComboBox = new ComboBox();
		label9 = new Label();
		regularFontFamilyComboBox = new ComboBox();
		label11 = new Label();
		regularFontSizeTextBox = new TextBox();
		label12 = new Label();
		label4 = new Label();
		subHeaderFontStyleComboBox = new ComboBox();
		label5 = new Label();
		subHeaderFontFamilyComboBox = new ComboBox();
		label6 = new Label();
		subHeaderFontSizeTextBox = new TextBox();
		label7 = new Label();
		label10 = new Label();
		headerFontStyleComboBox = new ComboBox();
		label3 = new Label();
		headerFontFamilyComboBox = new ComboBox();
		label2 = new Label();
		headerFontSizeTextBox = new TextBox();
		label1 = new Label();
		label17 = new Label();
		label18 = new Label();
		pubOpenTimePicker = new DateTimePicker();
		pubCloseTimePicker = new DateTimePicker();
		brandingLabel = new Label();
		richTextBoxFooter = new RichTextBox();
		thermalGroupBox.SuspendLayout();
		SuspendLayout();
		// 
		// saveButton
		// 
		saveButton.Font = new Font("Segoe UI", 12.75F);
		saveButton.Location = new Point(214, 594);
		saveButton.Name = "saveButton";
		saveButton.Size = new Size(135, 44);
		saveButton.TabIndex = 50;
		saveButton.Text = "SAVE";
		saveButton.UseVisualStyleBackColor = true;
		saveButton.Click += saveButton_Click;
		// 
		// inactivityTimeTextBox
		// 
		inactivityTimeTextBox.Font = new Font("Segoe UI", 12.75F);
		inactivityTimeTextBox.Location = new Point(245, 108);
		inactivityTimeTextBox.MaxLength = 5;
		inactivityTimeTextBox.Name = "inactivityTimeTextBox";
		inactivityTimeTextBox.PlaceholderText = "Inactivity Time";
		inactivityTimeTextBox.Size = new Size(77, 30);
		inactivityTimeTextBox.TabIndex = 3;
		inactivityTimeTextBox.Text = "5";
		inactivityTimeTextBox.Click += textBox_Click;
		inactivityTimeTextBox.KeyPress += textBox_KeyPress;
		// 
		// inactivityTimeLabel
		// 
		inactivityTimeLabel.AutoSize = true;
		inactivityTimeLabel.Font = new Font("Segoe UI", 12.75F);
		inactivityTimeLabel.Location = new Point(12, 111);
		inactivityTimeLabel.Name = "inactivityTimeLabel";
		inactivityTimeLabel.Size = new Size(215, 23);
		inactivityTimeLabel.TabIndex = 15;
		inactivityTimeLabel.Text = "Inactivity Time (in minutes)";
		// 
		// thermalGroupBox
		// 
		thermalGroupBox.Controls.Add(label13);
		thermalGroupBox.Controls.Add(footerFontStyleComboBox);
		thermalGroupBox.Controls.Add(label14);
		thermalGroupBox.Controls.Add(footerFontFamilyComboBox);
		thermalGroupBox.Controls.Add(label15);
		thermalGroupBox.Controls.Add(footerFontSizeTextBox);
		thermalGroupBox.Controls.Add(label16);
		thermalGroupBox.Controls.Add(label8);
		thermalGroupBox.Controls.Add(regularFontStyleComboBox);
		thermalGroupBox.Controls.Add(label9);
		thermalGroupBox.Controls.Add(regularFontFamilyComboBox);
		thermalGroupBox.Controls.Add(label11);
		thermalGroupBox.Controls.Add(regularFontSizeTextBox);
		thermalGroupBox.Controls.Add(label12);
		thermalGroupBox.Controls.Add(label4);
		thermalGroupBox.Controls.Add(subHeaderFontStyleComboBox);
		thermalGroupBox.Controls.Add(label5);
		thermalGroupBox.Controls.Add(subHeaderFontFamilyComboBox);
		thermalGroupBox.Controls.Add(label6);
		thermalGroupBox.Controls.Add(subHeaderFontSizeTextBox);
		thermalGroupBox.Controls.Add(label7);
		thermalGroupBox.Controls.Add(label10);
		thermalGroupBox.Controls.Add(headerFontStyleComboBox);
		thermalGroupBox.Controls.Add(label3);
		thermalGroupBox.Controls.Add(headerFontFamilyComboBox);
		thermalGroupBox.Controls.Add(label2);
		thermalGroupBox.Controls.Add(headerFontSizeTextBox);
		thermalGroupBox.Controls.Add(label1);
		thermalGroupBox.Location = new Point(13, 166);
		thermalGroupBox.Name = "thermalGroupBox";
		thermalGroupBox.Size = new Size(549, 422);
		thermalGroupBox.TabIndex = 16;
		thermalGroupBox.TabStop = false;
		thermalGroupBox.Text = "Thermal Printing";
		// 
		// label13
		// 
		label13.AutoSize = true;
		label13.Font = new Font("Segoe UI", 12.75F);
		label13.Location = new Point(10, 312);
		label13.Name = "label13";
		label13.Size = new Size(98, 23);
		label13.TabIndex = 56;
		label13.Text = "Footer Font";
		// 
		// footerFontStyleComboBox
		// 
		footerFontStyleComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
		footerFontStyleComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
		footerFontStyleComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
		footerFontStyleComboBox.Font = new Font("Segoe UI", 13F);
		footerFontStyleComboBox.FormattingEnabled = true;
		footerFontStyleComboBox.Location = new Point(384, 361);
		footerFontStyleComboBox.Name = "footerFontStyleComboBox";
		footerFontStyleComboBox.Size = new Size(151, 31);
		footerFontStyleComboBox.TabIndex = 31;
		// 
		// label14
		// 
		label14.AutoSize = true;
		label14.Font = new Font("Segoe UI", 12.75F);
		label14.Location = new Point(384, 335);
		label14.Name = "label14";
		label14.Size = new Size(45, 23);
		label14.TabIndex = 54;
		label14.Text = "Style";
		// 
		// footerFontFamilyComboBox
		// 
		footerFontFamilyComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
		footerFontFamilyComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
		footerFontFamilyComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
		footerFontFamilyComboBox.Font = new Font("Segoe UI", 13F);
		footerFontFamilyComboBox.FormattingEnabled = true;
		footerFontFamilyComboBox.Location = new Point(11, 361);
		footerFontFamilyComboBox.Name = "footerFontFamilyComboBox";
		footerFontFamilyComboBox.Size = new Size(258, 31);
		footerFontFamilyComboBox.TabIndex = 29;
		// 
		// label15
		// 
		label15.AutoSize = true;
		label15.Font = new Font("Segoe UI", 12.75F);
		label15.Location = new Point(10, 335);
		label15.Name = "label15";
		label15.Size = new Size(57, 23);
		label15.TabIndex = 52;
		label15.Text = "Family";
		// 
		// footerFontSizeTextBox
		// 
		footerFontSizeTextBox.Font = new Font("Segoe UI", 12.75F);
		footerFontSizeTextBox.Location = new Point(296, 362);
		footerFontSizeTextBox.MaxLength = 5;
		footerFontSizeTextBox.Name = "footerFontSizeTextBox";
		footerFontSizeTextBox.PlaceholderText = "Inactivity Time";
		footerFontSizeTextBox.Size = new Size(63, 30);
		footerFontSizeTextBox.TabIndex = 30;
		footerFontSizeTextBox.Text = "8";
		footerFontSizeTextBox.Click += textBox_Click;
		footerFontSizeTextBox.KeyPress += textBox_KeyPress;
		// 
		// label16
		// 
		label16.AutoSize = true;
		label16.Font = new Font("Segoe UI", 12.75F);
		label16.Location = new Point(296, 335);
		label16.Name = "label16";
		label16.Size = new Size(40, 23);
		label16.TabIndex = 51;
		label16.Text = "Size";
		// 
		// label8
		// 
		label8.AutoSize = true;
		label8.Font = new Font("Segoe UI", 12.75F);
		label8.Location = new Point(11, 211);
		label8.Name = "label8";
		label8.Size = new Size(107, 23);
		label8.TabIndex = 49;
		label8.Text = "Regular Font";
		// 
		// regularFontStyleComboBox
		// 
		regularFontStyleComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
		regularFontStyleComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
		regularFontStyleComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
		regularFontStyleComboBox.Font = new Font("Segoe UI", 13F);
		regularFontStyleComboBox.FormattingEnabled = true;
		regularFontStyleComboBox.Location = new Point(385, 260);
		regularFontStyleComboBox.Name = "regularFontStyleComboBox";
		regularFontStyleComboBox.Size = new Size(151, 31);
		regularFontStyleComboBox.TabIndex = 28;
		// 
		// label9
		// 
		label9.AutoSize = true;
		label9.Font = new Font("Segoe UI", 12.75F);
		label9.Location = new Point(385, 234);
		label9.Name = "label9";
		label9.Size = new Size(45, 23);
		label9.TabIndex = 47;
		label9.Text = "Style";
		// 
		// regularFontFamilyComboBox
		// 
		regularFontFamilyComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
		regularFontFamilyComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
		regularFontFamilyComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
		regularFontFamilyComboBox.Font = new Font("Segoe UI", 13F);
		regularFontFamilyComboBox.FormattingEnabled = true;
		regularFontFamilyComboBox.Location = new Point(12, 260);
		regularFontFamilyComboBox.Name = "regularFontFamilyComboBox";
		regularFontFamilyComboBox.Size = new Size(258, 31);
		regularFontFamilyComboBox.TabIndex = 26;
		// 
		// label11
		// 
		label11.AutoSize = true;
		label11.Font = new Font("Segoe UI", 12.75F);
		label11.Location = new Point(11, 234);
		label11.Name = "label11";
		label11.Size = new Size(57, 23);
		label11.TabIndex = 45;
		label11.Text = "Family";
		// 
		// regularFontSizeTextBox
		// 
		regularFontSizeTextBox.Font = new Font("Segoe UI", 12.75F);
		regularFontSizeTextBox.Location = new Point(297, 261);
		regularFontSizeTextBox.MaxLength = 5;
		regularFontSizeTextBox.Name = "regularFontSizeTextBox";
		regularFontSizeTextBox.PlaceholderText = "Inactivity Time";
		regularFontSizeTextBox.Size = new Size(63, 30);
		regularFontSizeTextBox.TabIndex = 27;
		regularFontSizeTextBox.Text = "12";
		regularFontSizeTextBox.Click += textBox_Click;
		regularFontSizeTextBox.KeyPress += textBox_KeyPress;
		// 
		// label12
		// 
		label12.AutoSize = true;
		label12.Font = new Font("Segoe UI", 12.75F);
		label12.Location = new Point(297, 234);
		label12.Name = "label12";
		label12.Size = new Size(40, 23);
		label12.TabIndex = 44;
		label12.Text = "Size";
		// 
		// label4
		// 
		label4.AutoSize = true;
		label4.Font = new Font("Segoe UI", 12.75F);
		label4.Location = new Point(10, 114);
		label4.Name = "label4";
		label4.Size = new Size(138, 23);
		label4.TabIndex = 42;
		label4.Text = "Sub Header Font";
		// 
		// subHeaderFontStyleComboBox
		// 
		subHeaderFontStyleComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
		subHeaderFontStyleComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
		subHeaderFontStyleComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
		subHeaderFontStyleComboBox.Font = new Font("Segoe UI", 13F);
		subHeaderFontStyleComboBox.FormattingEnabled = true;
		subHeaderFontStyleComboBox.Location = new Point(384, 163);
		subHeaderFontStyleComboBox.Name = "subHeaderFontStyleComboBox";
		subHeaderFontStyleComboBox.Size = new Size(151, 31);
		subHeaderFontStyleComboBox.TabIndex = 25;
		// 
		// label5
		// 
		label5.AutoSize = true;
		label5.Font = new Font("Segoe UI", 12.75F);
		label5.Location = new Point(384, 137);
		label5.Name = "label5";
		label5.Size = new Size(45, 23);
		label5.TabIndex = 40;
		label5.Text = "Style";
		// 
		// subHeaderFontFamilyComboBox
		// 
		subHeaderFontFamilyComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
		subHeaderFontFamilyComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
		subHeaderFontFamilyComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
		subHeaderFontFamilyComboBox.Font = new Font("Segoe UI", 13F);
		subHeaderFontFamilyComboBox.FormattingEnabled = true;
		subHeaderFontFamilyComboBox.Location = new Point(11, 163);
		subHeaderFontFamilyComboBox.Name = "subHeaderFontFamilyComboBox";
		subHeaderFontFamilyComboBox.Size = new Size(258, 31);
		subHeaderFontFamilyComboBox.TabIndex = 23;
		// 
		// label6
		// 
		label6.AutoSize = true;
		label6.Font = new Font("Segoe UI", 12.75F);
		label6.Location = new Point(10, 137);
		label6.Name = "label6";
		label6.Size = new Size(57, 23);
		label6.TabIndex = 38;
		label6.Text = "Family";
		// 
		// subHeaderFontSizeTextBox
		// 
		subHeaderFontSizeTextBox.Font = new Font("Segoe UI", 12.75F);
		subHeaderFontSizeTextBox.Location = new Point(296, 164);
		subHeaderFontSizeTextBox.MaxLength = 5;
		subHeaderFontSizeTextBox.Name = "subHeaderFontSizeTextBox";
		subHeaderFontSizeTextBox.PlaceholderText = "Inactivity Time";
		subHeaderFontSizeTextBox.Size = new Size(63, 30);
		subHeaderFontSizeTextBox.TabIndex = 24;
		subHeaderFontSizeTextBox.Text = "15";
		subHeaderFontSizeTextBox.Click += textBox_Click;
		subHeaderFontSizeTextBox.KeyPress += textBox_KeyPress;
		// 
		// label7
		// 
		label7.AutoSize = true;
		label7.Font = new Font("Segoe UI", 12.75F);
		label7.Location = new Point(296, 137);
		label7.Name = "label7";
		label7.Size = new Size(40, 23);
		label7.TabIndex = 37;
		label7.Text = "Size";
		// 
		// label10
		// 
		label10.AutoSize = true;
		label10.Font = new Font("Segoe UI", 12.75F);
		label10.Location = new Point(6, 19);
		label10.Name = "label10";
		label10.Size = new Size(104, 23);
		label10.TabIndex = 35;
		label10.Text = "Header Font";
		// 
		// headerFontStyleComboBox
		// 
		headerFontStyleComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
		headerFontStyleComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
		headerFontStyleComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
		headerFontStyleComboBox.Font = new Font("Segoe UI", 13F);
		headerFontStyleComboBox.FormattingEnabled = true;
		headerFontStyleComboBox.Location = new Point(380, 68);
		headerFontStyleComboBox.Name = "headerFontStyleComboBox";
		headerFontStyleComboBox.Size = new Size(151, 31);
		headerFontStyleComboBox.TabIndex = 22;
		// 
		// label3
		// 
		label3.AutoSize = true;
		label3.Font = new Font("Segoe UI", 12.75F);
		label3.Location = new Point(380, 42);
		label3.Name = "label3";
		label3.Size = new Size(45, 23);
		label3.TabIndex = 21;
		label3.Text = "Style";
		// 
		// headerFontFamilyComboBox
		// 
		headerFontFamilyComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
		headerFontFamilyComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
		headerFontFamilyComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
		headerFontFamilyComboBox.Font = new Font("Segoe UI", 13F);
		headerFontFamilyComboBox.FormattingEnabled = true;
		headerFontFamilyComboBox.Location = new Point(7, 68);
		headerFontFamilyComboBox.Name = "headerFontFamilyComboBox";
		headerFontFamilyComboBox.Size = new Size(258, 31);
		headerFontFamilyComboBox.TabIndex = 20;
		// 
		// label2
		// 
		label2.AutoSize = true;
		label2.Font = new Font("Segoe UI", 12.75F);
		label2.Location = new Point(6, 42);
		label2.Name = "label2";
		label2.Size = new Size(57, 23);
		label2.TabIndex = 19;
		label2.Text = "Family";
		// 
		// headerFontSizeTextBox
		// 
		headerFontSizeTextBox.Font = new Font("Segoe UI", 12.75F);
		headerFontSizeTextBox.Location = new Point(292, 69);
		headerFontSizeTextBox.MaxLength = 5;
		headerFontSizeTextBox.Name = "headerFontSizeTextBox";
		headerFontSizeTextBox.PlaceholderText = "Inactivity Time";
		headerFontSizeTextBox.Size = new Size(63, 30);
		headerFontSizeTextBox.TabIndex = 21;
		headerFontSizeTextBox.Text = "25";
		headerFontSizeTextBox.Click += textBox_Click;
		headerFontSizeTextBox.KeyPress += textBox_KeyPress;
		// 
		// label1
		// 
		label1.AutoSize = true;
		label1.Font = new Font("Segoe UI", 12.75F);
		label1.Location = new Point(292, 42);
		label1.Name = "label1";
		label1.Size = new Size(40, 23);
		label1.TabIndex = 18;
		label1.Text = "Size";
		// 
		// label17
		// 
		label17.AutoSize = true;
		label17.Font = new Font("Segoe UI", 12.75F);
		label17.Location = new Point(13, 20);
		label17.Name = "label17";
		label17.Size = new Size(129, 23);
		label17.TabIndex = 52;
		label17.Text = "Pub Open Time";
		// 
		// label18
		// 
		label18.AutoSize = true;
		label18.Font = new Font("Segoe UI", 12.75F);
		label18.Location = new Point(12, 56);
		label18.Name = "label18";
		label18.Size = new Size(128, 23);
		label18.TabIndex = 54;
		label18.Text = "Pub Close Time";
		// 
		// pubOpenTimePicker
		// 
		pubOpenTimePicker.CustomFormat = "hh tt";
		pubOpenTimePicker.Font = new Font("Segoe UI", 13F);
		pubOpenTimePicker.Format = DateTimePickerFormat.Custom;
		pubOpenTimePicker.Location = new Point(245, 14);
		pubOpenTimePicker.Name = "pubOpenTimePicker";
		pubOpenTimePicker.ShowUpDown = true;
		pubOpenTimePicker.Size = new Size(77, 31);
		pubOpenTimePicker.TabIndex = 1;
		pubOpenTimePicker.Value = new DateTime(2025, 1, 16, 17, 0, 0, 0);
		// 
		// pubCloseTimePicker
		// 
		pubCloseTimePicker.CustomFormat = "hh tt";
		pubCloseTimePicker.Font = new Font("Segoe UI", 13F);
		pubCloseTimePicker.Format = DateTimePickerFormat.Custom;
		pubCloseTimePicker.Location = new Point(245, 51);
		pubCloseTimePicker.Name = "pubCloseTimePicker";
		pubCloseTimePicker.ShowUpDown = true;
		pubCloseTimePicker.Size = new Size(77, 31);
		pubCloseTimePicker.TabIndex = 2;
		pubCloseTimePicker.Value = new DateTime(2025, 1, 16, 5, 0, 0, 0);
		// 
		// brandingLabel
		// 
		brandingLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
		brandingLabel.AutoSize = true;
		brandingLabel.BackColor = Color.White;
		brandingLabel.Location = new Point(492, 649);
		brandingLabel.Name = "brandingLabel";
		brandingLabel.Size = new Size(76, 15);
		brandingLabel.TabIndex = 56;
		brandingLabel.Text = "© AADISOFT";
		// 
		// richTextBoxFooter
		// 
		richTextBoxFooter.Dock = DockStyle.Bottom;
		richTextBoxFooter.Location = new Point(0, 643);
		richTextBoxFooter.Name = "richTextBoxFooter";
		richTextBoxFooter.Size = new Size(574, 24);
		richTextBoxFooter.TabIndex = 55;
		richTextBoxFooter.Text = "";
		// 
		// SettingsForm
		// 
		AcceptButton = saveButton;
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		ClientSize = new Size(574, 667);
		Controls.Add(brandingLabel);
		Controls.Add(richTextBoxFooter);
		Controls.Add(pubCloseTimePicker);
		Controls.Add(pubOpenTimePicker);
		Controls.Add(label18);
		Controls.Add(label17);
		Controls.Add(thermalGroupBox);
		Controls.Add(inactivityTimeTextBox);
		Controls.Add(inactivityTimeLabel);
		Controls.Add(saveButton);
		Icon = (Icon)resources.GetObject("$this.Icon");
		Name = "SettingsForm";
		StartPosition = FormStartPosition.CenterScreen;
		Text = "Settings";
		Load += SettingForm_Load;
		thermalGroupBox.ResumeLayout(false);
		thermalGroupBox.PerformLayout();
		ResumeLayout(false);
		PerformLayout();
	}

	#endregion

	private Button saveButton;
	private TextBox inactivityTimeTextBox;
	private Label inactivityTimeLabel;
	private GroupBox thermalGroupBox;
	private Label label2;
	private TextBox headerFontSizeTextBox;
	private Label label1;
	private ComboBox headerFontFamilyComboBox;
	private ComboBox headerFontStyleComboBox;
	private Label label3;
	private Label label10;
	private Label label4;
	private ComboBox subHeaderFontStyleComboBox;
	private Label label5;
	private ComboBox subHeaderFontFamilyComboBox;
	private Label label6;
	private TextBox subHeaderFontSizeTextBox;
	private Label label7;
	private Label label8;
	private ComboBox regularFontStyleComboBox;
	private Label label9;
	private ComboBox regularFontFamilyComboBox;
	private Label label11;
	private TextBox regularFontSizeTextBox;
	private Label label12;
	private Label label13;
	private ComboBox footerFontStyleComboBox;
	private Label label14;
	private ComboBox footerFontFamilyComboBox;
	private Label label15;
	private TextBox footerFontSizeTextBox;
	private Label label16;
	private Label label17;
	private Label label18;
	private DateTimePicker pubOpenTimePicker;
	private DateTimePicker pubCloseTimePicker;
	private Label brandingLabel;
	private RichTextBox richTextBoxFooter;
}