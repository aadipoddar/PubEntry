namespace PubEntry;

partial class EmployeeForm
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
		nameLabel = new Label();
		employeeNameTextBox = new TextBox();
		passwordLabel = new Label();
		passwordTextBox = new TextBox();
		locationComboBox = new ComboBox();
		locationLabel = new Label();
		saveButton = new Button();
		SuspendLayout();
		// 
		// nameLabel
		// 
		nameLabel.AutoSize = true;
		nameLabel.Font = new Font("Segoe UI", 15F);
		nameLabel.Location = new Point(7, 21);
		nameLabel.Name = "nameLabel";
		nameLabel.Size = new Size(64, 28);
		nameLabel.TabIndex = 5;
		nameLabel.Text = "Name";
		// 
		// employeeNameTextBox
		// 
		employeeNameTextBox.Font = new Font("Segoe UI", 15F);
		employeeNameTextBox.Location = new Point(176, 18);
		employeeNameTextBox.Name = "employeeNameTextBox";
		employeeNameTextBox.PlaceholderText = "Employee Name";
		employeeNameTextBox.Size = new Size(271, 34);
		employeeNameTextBox.TabIndex = 4;
		// 
		// passwordLabel
		// 
		passwordLabel.AutoSize = true;
		passwordLabel.Font = new Font("Segoe UI", 15F);
		passwordLabel.Location = new Point(7, 61);
		passwordLabel.Name = "passwordLabel";
		passwordLabel.Size = new Size(93, 28);
		passwordLabel.TabIndex = 7;
		passwordLabel.Text = "Password";
		// 
		// passwordTextBox
		// 
		passwordTextBox.Font = new Font("Segoe UI", 15F);
		passwordTextBox.Location = new Point(176, 58);
		passwordTextBox.Name = "passwordTextBox";
		passwordTextBox.PasswordChar = '*';
		passwordTextBox.PlaceholderText = "Password";
		passwordTextBox.Size = new Size(271, 34);
		passwordTextBox.TabIndex = 6;
		// 
		// locationComboBox
		// 
		locationComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
		locationComboBox.FlatStyle = FlatStyle.System;
		locationComboBox.Font = new Font("Segoe UI", 15F);
		locationComboBox.FormattingEnabled = true;
		locationComboBox.Location = new Point(176, 98);
		locationComboBox.Name = "locationComboBox";
		locationComboBox.Size = new Size(271, 36);
		locationComboBox.TabIndex = 11;
		// 
		// locationLabel
		// 
		locationLabel.AutoSize = true;
		locationLabel.Font = new Font("Segoe UI", 15F);
		locationLabel.Location = new Point(7, 101);
		locationLabel.Name = "locationLabel";
		locationLabel.Size = new Size(87, 28);
		locationLabel.TabIndex = 12;
		locationLabel.Text = "Location";
		// 
		// saveButton
		// 
		saveButton.Font = new Font("Segoe UI", 15F);
		saveButton.Location = new Point(167, 186);
		saveButton.Name = "saveButton";
		saveButton.Size = new Size(118, 38);
		saveButton.TabIndex = 14;
		saveButton.Text = "SAVE";
		saveButton.UseVisualStyleBackColor = true;
		saveButton.Click += saveButton_Click;
		// 
		// EmployeeForm
		// 
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		ClientSize = new Size(477, 280);
		Controls.Add(saveButton);
		Controls.Add(locationLabel);
		Controls.Add(locationComboBox);
		Controls.Add(passwordLabel);
		Controls.Add(passwordTextBox);
		Controls.Add(nameLabel);
		Controls.Add(employeeNameTextBox);
		Name = "EmployeeForm";
		StartPosition = FormStartPosition.CenterScreen;
		Text = "EmployeeForm";
		ResumeLayout(false);
		PerformLayout();
	}

	#endregion

	private Label nameLabel;
	private TextBox employeeNameTextBox;
	private Label passwordLabel;
	private TextBox passwordTextBox;
	private ComboBox locationComboBox;
	private Label locationLabel;
	private Button saveButton;
}