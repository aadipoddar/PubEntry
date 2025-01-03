﻿namespace PubEntry.Admin;

partial class UserForm
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserForm));
		nameLabel = new Label();
		nameTextBox = new TextBox();
		passwordLabel = new Label();
		passwordTextBox = new TextBox();
		locationComboBox = new ComboBox();
		locationLabel = new Label();
		saveButton = new Button();
		userComboBox = new ComboBox();
		statusCheckBox = new CheckBox();
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
		// nameTextBox
		// 
		nameTextBox.Font = new Font("Segoe UI", 15F);
		nameTextBox.Location = new Point(185, 69);
		nameTextBox.Name = "nameTextBox";
		nameTextBox.PlaceholderText = "Name";
		nameTextBox.Size = new Size(271, 34);
		nameTextBox.TabIndex = 1;
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
		// userComboBox
		// 
		userComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
		userComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
		userComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
		userComboBox.FlatStyle = FlatStyle.System;
		userComboBox.Font = new Font("Segoe UI", 15F);
		userComboBox.FormattingEnabled = true;
		userComboBox.Location = new Point(93, 12);
		userComboBox.Name = "userComboBox";
		userComboBox.Size = new Size(271, 36);
		userComboBox.TabIndex = 6;
		userComboBox.SelectedIndexChanged += userComboBox_SelectedIndexChanged;
		// 
		// statusCheckBox
		// 
		statusCheckBox.AutoSize = true;
		statusCheckBox.Font = new Font("Segoe UI", 15F);
		statusCheckBox.Location = new Point(194, 205);
		statusCheckBox.Name = "statusCheckBox";
		statusCheckBox.Size = new Size(84, 32);
		statusCheckBox.TabIndex = 4;
		statusCheckBox.Text = "Status";
		statusCheckBox.UseVisualStyleBackColor = true;
		// 
		// UserForm
		// 
		AcceptButton = saveButton;
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		ClientSize = new Size(477, 305);
		Controls.Add(statusCheckBox);
		Controls.Add(userComboBox);
		Controls.Add(saveButton);
		Controls.Add(locationLabel);
		Controls.Add(locationComboBox);
		Controls.Add(passwordLabel);
		Controls.Add(passwordTextBox);
		Controls.Add(nameLabel);
		Controls.Add(nameTextBox);
		Icon = (Icon)resources.GetObject("$this.Icon");
		Name = "UserForm";
		StartPosition = FormStartPosition.CenterScreen;
		Text = "User Form";
		Load += UserForm_Load;
		ResumeLayout(false);
		PerformLayout();
	}

	#endregion

	private Label nameLabel;
	private TextBox nameTextBox;
	private Label passwordLabel;
	private TextBox passwordTextBox;
	private ComboBox locationComboBox;
	private Label locationLabel;
	private Button saveButton;
	private ComboBox userComboBox;
	private CheckBox statusCheckBox;
}