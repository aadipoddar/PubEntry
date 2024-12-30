namespace PubEntry;

partial class AdvanceEntryForm
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
		advanceDateTimePicker = new DateTimePicker();
		saveButton = new Button();
		loyaltyCheckBox = new CheckBox();
		numberLabel = new Label();
		numberTextBox = new TextBox();
		nameLabel = new Label();
		nameTextBox = new TextBox();
		cashLabel = new Label();
		amountTextBox = new TextBox();
		locationComboBox = new ComboBox();
		SuspendLayout();
		// 
		// advanceDateTimePicker
		// 
		advanceDateTimePicker.CalendarFont = new Font("Segoe UI", 15F);
		advanceDateTimePicker.Font = new Font("Segoe UI", 15F);
		advanceDateTimePicker.Format = DateTimePickerFormat.Short;
		advanceDateTimePicker.Location = new Point(154, 296);
		advanceDateTimePicker.Name = "advanceDateTimePicker";
		advanceDateTimePicker.Size = new Size(136, 34);
		advanceDateTimePicker.TabIndex = 6;
		// 
		// saveButton
		// 
		saveButton.Font = new Font("Segoe UI", 15F);
		saveButton.Location = new Point(155, 354);
		saveButton.Name = "saveButton";
		saveButton.Size = new Size(135, 44);
		saveButton.TabIndex = 7;
		saveButton.Text = "SAVE";
		saveButton.UseVisualStyleBackColor = true;
		saveButton.Click += saveButton_Click;
		// 
		// loyaltyCheckBox
		// 
		loyaltyCheckBox.AutoSize = true;
		loyaltyCheckBox.Font = new Font("Segoe UI", 15F);
		loyaltyCheckBox.Location = new Point(196, 184);
		loyaltyCheckBox.Name = "loyaltyCheckBox";
		loyaltyCheckBox.Size = new Size(94, 32);
		loyaltyCheckBox.TabIndex = 4;
		loyaltyCheckBox.Text = "Loyalty";
		loyaltyCheckBox.UseVisualStyleBackColor = true;
		// 
		// numberLabel
		// 
		numberLabel.AutoSize = true;
		numberLabel.Font = new Font("Segoe UI", 15F);
		numberLabel.Location = new Point(19, 79);
		numberLabel.Name = "numberLabel";
		numberLabel.Size = new Size(151, 28);
		numberLabel.TabIndex = 17;
		numberLabel.Text = "Mobile Number";
		// 
		// numberTextBox
		// 
		numberTextBox.Font = new Font("Segoe UI", 15F);
		numberTextBox.Location = new Point(192, 79);
		numberTextBox.Name = "numberTextBox";
		numberTextBox.PlaceholderText = "Mobile Number";
		numberTextBox.Size = new Size(271, 34);
		numberTextBox.TabIndex = 2;
		numberTextBox.TextChanged += numberTextBox_TextChanged;
		numberTextBox.KeyPress += textBox_KeyPress;
		// 
		// nameLabel
		// 
		nameLabel.AutoSize = true;
		nameLabel.Font = new Font("Segoe UI", 15F);
		nameLabel.Location = new Point(20, 120);
		nameLabel.Name = "nameLabel";
		nameLabel.Size = new Size(64, 28);
		nameLabel.TabIndex = 14;
		nameLabel.Text = "Name";
		// 
		// nameTextBox
		// 
		nameTextBox.Font = new Font("Segoe UI", 15F);
		nameTextBox.Location = new Point(192, 119);
		nameTextBox.Name = "nameTextBox";
		nameTextBox.PlaceholderText = "Name";
		nameTextBox.Size = new Size(271, 34);
		nameTextBox.TabIndex = 3;
		// 
		// cashLabel
		// 
		cashLabel.AutoSize = true;
		cashLabel.Font = new Font("Segoe UI", 15F);
		cashLabel.Location = new Point(116, 240);
		cashLabel.Name = "cashLabel";
		cashLabel.Size = new Size(83, 28);
		cashLabel.TabIndex = 18;
		cashLabel.Text = "Amount";
		// 
		// amountTextBox
		// 
		amountTextBox.Font = new Font("Segoe UI", 15F);
		amountTextBox.Location = new Point(205, 237);
		amountTextBox.Name = "amountTextBox";
		amountTextBox.PlaceholderText = "Cash Amount";
		amountTextBox.RightToLeft = RightToLeft.Yes;
		amountTextBox.Size = new Size(109, 34);
		amountTextBox.TabIndex = 5;
		amountTextBox.Text = "0";
		amountTextBox.KeyPress += textBox_KeyPress;
		// 
		// locationComboBox
		// 
		locationComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
		locationComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
		locationComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
		locationComboBox.Font = new Font("Segoe UI", 15F);
		locationComboBox.FormattingEnabled = true;
		locationComboBox.Location = new Point(115, 12);
		locationComboBox.Name = "locationComboBox";
		locationComboBox.Size = new Size(271, 36);
		locationComboBox.TabIndex = 1;
		// 
		// AdvanceEntryForm
		// 
		AcceptButton = saveButton;
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		ClientSize = new Size(501, 427);
		Controls.Add(locationComboBox);
		Controls.Add(cashLabel);
		Controls.Add(amountTextBox);
		Controls.Add(loyaltyCheckBox);
		Controls.Add(numberLabel);
		Controls.Add(numberTextBox);
		Controls.Add(nameLabel);
		Controls.Add(nameTextBox);
		Controls.Add(saveButton);
		Controls.Add(advanceDateTimePicker);
		Name = "AdvanceEntryForm";
		StartPosition = FormStartPosition.CenterScreen;
		Text = "Advance Form";
		Load += AdvanceEntryForm_Load;
		ResumeLayout(false);
		PerformLayout();
	}

	#endregion

	private DateTimePicker advanceDateTimePicker;
	private Button saveButton;
	private CheckBox loyaltyCheckBox;
	private Label numberLabel;
	private TextBox numberTextBox;
	private Label nameLabel;
	private TextBox nameTextBox;
	private Label cashLabel;
	private TextBox amountTextBox;
	private ComboBox locationComboBox;
}