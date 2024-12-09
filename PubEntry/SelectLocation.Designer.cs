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
		showDataButton = new Button();
		passwordTextBox = new TextBox();
		fromDateTimePicker = new DateTimePicker();
		toDateTimePicker = new DateTimePicker();
		fromLabel = new Label();
		toLabel = new Label();
		fromTimeTextBox = new TextBox();
		toTimeTextBox = new TextBox();
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
		// showDataButton
		// 
		showDataButton.Font = new Font("Segoe UI", 15F);
		showDataButton.Location = new Point(161, 450);
		showDataButton.Name = "showDataButton";
		showDataButton.Size = new Size(118, 38);
		showDataButton.TabIndex = 18;
		showDataButton.Text = "Show Data";
		showDataButton.UseVisualStyleBackColor = true;
		showDataButton.Click += showDataButton_Click;
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
		fromDateTimePicker.Location = new Point(90, 340);
		fromDateTimePicker.Name = "fromDateTimePicker";
		fromDateTimePicker.Size = new Size(131, 34);
		fromDateTimePicker.TabIndex = 14;
		// 
		// toDateTimePicker
		// 
		toDateTimePicker.Font = new Font("Segoe UI", 15F);
		toDateTimePicker.Format = DateTimePickerFormat.Short;
		toDateTimePicker.Location = new Point(259, 340);
		toDateTimePicker.Name = "toDateTimePicker";
		toDateTimePicker.Size = new Size(135, 34);
		toDateTimePicker.TabIndex = 16;
		// 
		// fromLabel
		// 
		fromLabel.AutoSize = true;
		fromLabel.Font = new Font("Segoe UI", 15F);
		fromLabel.Location = new Point(112, 309);
		fromLabel.Name = "fromLabel";
		fromLabel.Size = new Size(58, 28);
		fromLabel.TabIndex = 19;
		fromLabel.Text = "From";
		// 
		// toLabel
		// 
		toLabel.AutoSize = true;
		toLabel.Font = new Font("Segoe UI", 15F);
		toLabel.Location = new Point(279, 309);
		toLabel.Name = "toLabel";
		toLabel.Size = new Size(32, 28);
		toLabel.TabIndex = 20;
		toLabel.Text = "To";
		// 
		// fromTimeTextBox
		// 
		fromTimeTextBox.Font = new Font("Segoe UI", 18F);
		fromTimeTextBox.Location = new Point(90, 380);
		fromTimeTextBox.Name = "fromTimeTextBox";
		fromTimeTextBox.PlaceholderText = "24hr Time";
		fromTimeTextBox.Size = new Size(131, 39);
		fromTimeTextBox.TabIndex = 15;
		fromTimeTextBox.Text = "12";
		// 
		// toTimeTextBox
		// 
		toTimeTextBox.Font = new Font("Segoe UI", 18F);
		toTimeTextBox.Location = new Point(259, 380);
		toTimeTextBox.Name = "toTimeTextBox";
		toTimeTextBox.PlaceholderText = "24hr Time";
		toTimeTextBox.Size = new Size(135, 39);
		toTimeTextBox.TabIndex = 17;
		toTimeTextBox.Text = "12";
		// 
		// SelectLocation
		// 
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		ClientSize = new Size(487, 524);
		Controls.Add(toTimeTextBox);
		Controls.Add(fromTimeTextBox);
		Controls.Add(toLabel);
		Controls.Add(fromLabel);
		Controls.Add(toDateTimePicker);
		Controls.Add(fromDateTimePicker);
		Controls.Add(passwordTextBox);
		Controls.Add(showDataButton);
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
	private Button showDataButton;
	private TextBox passwordTextBox;
	private DateTimePicker fromDateTimePicker;
	private DateTimePicker toDateTimePicker;
	private Label fromLabel;
	private Label toLabel;
	private TextBox fromTimeTextBox;
	private TextBox toTimeTextBox;
}