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
		employeeComboBox = new ComboBox();
		statusLabel = new Label();
		statusComboBox = new ComboBox();
		SuspendLayout();
		// 
		// nameLabel
		// 
		nameLabel.AutoSize = true;
		nameLabel.Font = new Font("Segoe UI", 15F);
		nameLabel.Location = new Point(16, 72);
		nameLabel.Name = "nameLabel";
		nameLabel.Size = new Size(64, 28);
		nameLabel.TabIndex = 5;
		nameLabel.Text = "Name";
		// 
		// employeeNameTextBox
		// 
		employeeNameTextBox.Font = new Font("Segoe UI", 15F);
		employeeNameTextBox.Location = new Point(185, 69);
		employeeNameTextBox.Name = "employeeNameTextBox";
		employeeNameTextBox.PlaceholderText = "Employee Name";
		employeeNameTextBox.Size = new Size(271, 34);
		employeeNameTextBox.TabIndex = 1;
		// 
		// passwordLabel
		// 
		passwordLabel.AutoSize = true;
		passwordLabel.Font = new Font("Segoe UI", 15F);
		passwordLabel.Location = new Point(16, 112);
		passwordLabel.Name = "passwordLabel";
		passwordLabel.Size = new Size(93, 28);
		passwordLabel.TabIndex = 7;
		passwordLabel.Text = "Password";
		// 
		// passwordTextBox
		// 
		passwordTextBox.Font = new Font("Segoe UI", 15F);
		passwordTextBox.Location = new Point(185, 109);
		passwordTextBox.Name = "passwordTextBox";
		passwordTextBox.PasswordChar = '*';
		passwordTextBox.PlaceholderText = "Password";
		passwordTextBox.Size = new Size(271, 34);
		passwordTextBox.TabIndex = 2;
		// 
		// locationComboBox
		// 
		locationComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
		locationComboBox.FlatStyle = FlatStyle.System;
		locationComboBox.Font = new Font("Segoe UI", 15F);
		locationComboBox.FormattingEnabled = true;
		locationComboBox.Location = new Point(185, 149);
		locationComboBox.Name = "locationComboBox";
		locationComboBox.Size = new Size(271, 36);
		locationComboBox.TabIndex = 3;
		// 
		// locationLabel
		// 
		locationLabel.AutoSize = true;
		locationLabel.Font = new Font("Segoe UI", 15F);
		locationLabel.Location = new Point(16, 152);
		locationLabel.Name = "locationLabel";
		locationLabel.Size = new Size(87, 28);
		locationLabel.TabIndex = 12;
		locationLabel.Text = "Location";
		// 
		// saveButton
		// 
		saveButton.Font = new Font("Segoe UI", 15F);
		saveButton.Location = new Point(176, 252);
		saveButton.Name = "saveButton";
		saveButton.Size = new Size(118, 38);
		saveButton.TabIndex = 5;
		saveButton.Text = "SAVE";
		saveButton.UseVisualStyleBackColor = true;
		saveButton.Click += saveButton_Click;
		// 
		// employeeComboBox
		// 
		employeeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
		employeeComboBox.FlatStyle = FlatStyle.System;
		employeeComboBox.Font = new Font("Segoe UI", 15F);
		employeeComboBox.FormattingEnabled = true;
		employeeComboBox.Location = new Point(93, 12);
		employeeComboBox.Name = "employeeComboBox";
		employeeComboBox.Size = new Size(271, 36);
		employeeComboBox.TabIndex = 6;
		employeeComboBox.SelectedIndexChanged += employeeComboBox_SelectedIndexChanged;
		// 
		// statusLabel
		// 
		statusLabel.AutoSize = true;
		statusLabel.Font = new Font("Segoe UI", 15F);
		statusLabel.Location = new Point(16, 194);
		statusLabel.Name = "statusLabel";
		statusLabel.Size = new Size(65, 28);
		statusLabel.TabIndex = 17;
		statusLabel.Text = "Status";
		// 
		// statusComboBox
		// 
		statusComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
		statusComboBox.FlatStyle = FlatStyle.System;
		statusComboBox.Font = new Font("Segoe UI", 15F);
		statusComboBox.FormattingEnabled = true;
		statusComboBox.Items.AddRange(new object[] { "Active", "Inactive" });
		statusComboBox.Location = new Point(185, 191);
		statusComboBox.Name = "statusComboBox";
		statusComboBox.Size = new Size(271, 36);
		statusComboBox.TabIndex = 4;
		// 
		// EmployeeForm
		// 
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		ClientSize = new Size(477, 305);
		Controls.Add(statusLabel);
		Controls.Add(statusComboBox);
		Controls.Add(employeeComboBox);
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
	private ComboBox employeeComboBox;
	private Label statusLabel;
	private ComboBox statusComboBox;
}