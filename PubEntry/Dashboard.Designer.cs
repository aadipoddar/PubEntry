﻿namespace PubEntry;

partial class Dashboard
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Dashboard));
		locationComboBox = new ComboBox();
		employeeComboBox = new ComboBox();
		goButton = new Button();
		passwordTextBox = new TextBox();
		brandingLabel = new Label();
		richTextBoxFooter = new RichTextBox();
		manageEmployeeButton = new Button();
		reportsButton = new Button();
		versionLabel = new Label();
		manageLocationButton = new Button();
		SuspendLayout();
		// 
		// locationComboBox
		// 
		locationComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
		locationComboBox.Font = new Font("Segoe UI", 15F);
		locationComboBox.FormattingEnabled = true;
		locationComboBox.Location = new Point(29, 33);
		locationComboBox.Name = "locationComboBox";
		locationComboBox.Size = new Size(271, 36);
		locationComboBox.TabIndex = 10;
		locationComboBox.SelectedIndexChanged += locationComboBox_SelectedIndexChanged;
		// 
		// employeeComboBox
		// 
		employeeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
		employeeComboBox.Font = new Font("Segoe UI", 15F);
		employeeComboBox.FormattingEnabled = true;
		employeeComboBox.Location = new Point(29, 99);
		employeeComboBox.Name = "employeeComboBox";
		employeeComboBox.Size = new Size(271, 36);
		employeeComboBox.TabIndex = 11;
		employeeComboBox.SelectedIndexChanged += employeeComboBox_SelectedIndexChanged;
		// 
		// goButton
		// 
		goButton.Font = new Font("Segoe UI", 15F);
		goButton.Location = new Point(99, 225);
		goButton.Name = "goButton";
		goButton.Size = new Size(118, 38);
		goButton.TabIndex = 13;
		goButton.Text = "GO";
		goButton.UseVisualStyleBackColor = true;
		goButton.Click += goButton_Click;
		// 
		// passwordTextBox
		// 
		passwordTextBox.Font = new Font("Segoe UI", 18F);
		passwordTextBox.Location = new Point(29, 165);
		passwordTextBox.Name = "passwordTextBox";
		passwordTextBox.PasswordChar = '*';
		passwordTextBox.PlaceholderText = "Password";
		passwordTextBox.Size = new Size(271, 39);
		passwordTextBox.TabIndex = 12;
		// 
		// brandingLabel
		// 
		brandingLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
		brandingLabel.AutoSize = true;
		brandingLabel.BackColor = Color.White;
		brandingLabel.Location = new Point(257, 492);
		brandingLabel.Name = "brandingLabel";
		brandingLabel.Size = new Size(76, 15);
		brandingLabel.TabIndex = 30;
		brandingLabel.Text = "© AADISOFT";
		// 
		// richTextBoxFooter
		// 
		richTextBoxFooter.Dock = DockStyle.Bottom;
		richTextBoxFooter.Location = new Point(0, 486);
		richTextBoxFooter.Name = "richTextBoxFooter";
		richTextBoxFooter.Size = new Size(336, 26);
		richTextBoxFooter.TabIndex = 29;
		richTextBoxFooter.Text = "";
		// 
		// manageEmployeeButton
		// 
		manageEmployeeButton.Font = new Font("Segoe UI", 15F);
		manageEmployeeButton.Location = new Point(56, 389);
		manageEmployeeButton.Name = "manageEmployeeButton";
		manageEmployeeButton.Size = new Size(213, 38);
		manageEmployeeButton.TabIndex = 15;
		manageEmployeeButton.Text = "Manage Employees";
		manageEmployeeButton.UseVisualStyleBackColor = true;
		manageEmployeeButton.Click += manageEmployeeButton_Click;
		// 
		// reportsButton
		// 
		reportsButton.Font = new Font("Segoe UI", 15F);
		reportsButton.Location = new Point(70, 290);
		reportsButton.Name = "reportsButton";
		reportsButton.Size = new Size(188, 38);
		reportsButton.TabIndex = 14;
		reportsButton.Text = "Reports";
		reportsButton.UseVisualStyleBackColor = true;
		reportsButton.Click += reportsButton_Click;
		// 
		// versionLabel
		// 
		versionLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
		versionLabel.AutoSize = true;
		versionLabel.BackColor = Color.White;
		versionLabel.Location = new Point(2, 492);
		versionLabel.Name = "versionLabel";
		versionLabel.Size = new Size(84, 15);
		versionLabel.TabIndex = 32;
		versionLabel.Text = "Version: 1.9.3.0";
		// 
		// manageLocationButton
		// 
		manageLocationButton.Font = new Font("Segoe UI", 15F);
		manageLocationButton.Location = new Point(56, 433);
		manageLocationButton.Name = "manageLocationButton";
		manageLocationButton.Size = new Size(213, 38);
		manageLocationButton.TabIndex = 16;
		manageLocationButton.Text = "Manage Locations";
		manageLocationButton.UseVisualStyleBackColor = true;
		manageLocationButton.Click += manageLocationButton_Click;
		// 
		// Dashboard
		// 
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		ClientSize = new Size(336, 512);
		Controls.Add(manageLocationButton);
		Controls.Add(versionLabel);
		Controls.Add(reportsButton);
		Controls.Add(manageEmployeeButton);
		Controls.Add(brandingLabel);
		Controls.Add(richTextBoxFooter);
		Controls.Add(passwordTextBox);
		Controls.Add(goButton);
		Controls.Add(employeeComboBox);
		Controls.Add(locationComboBox);
		Icon = (Icon)resources.GetObject("$this.Icon");
		Name = "Dashboard";
		StartPosition = FormStartPosition.CenterScreen;
		Text = "Dashboard";
		Load += SelectLocation_Load;
		ResumeLayout(false);
		PerformLayout();
	}

	#endregion

	private ComboBox locationComboBox;
	private ComboBox employeeComboBox;
	private Button goButton;
	private TextBox passwordTextBox;
	private Label brandingLabel;
	private RichTextBox richTextBoxFooter;
	private Button manageEmployeeButton;
	private Button reportsButton;
	private Label versionLabel;
	private Button manageLocationButton;
}