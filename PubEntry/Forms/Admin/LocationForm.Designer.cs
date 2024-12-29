namespace PubEntry.Admin;

partial class LocationForm
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LocationForm));
		locationComboBox = new ComboBox();
		saveButton = new Button();
		nameLabel = new Label();
		locationNameTextBox = new TextBox();
		statusCheckBox = new CheckBox();
		SuspendLayout();
		// 
		// locationComboBox
		// 
		locationComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
		locationComboBox.FlatStyle = FlatStyle.System;
		locationComboBox.Font = new Font("Segoe UI", 15F);
		locationComboBox.FormattingEnabled = true;
		locationComboBox.Location = new Point(96, 23);
		locationComboBox.Name = "locationComboBox";
		locationComboBox.Size = new Size(271, 36);
		locationComboBox.TabIndex = 4;
		locationComboBox.SelectedIndexChanged += locationComboBox_SelectedIndexChanged;
		// 
		// saveButton
		// 
		saveButton.Font = new Font("Segoe UI", 15F);
		saveButton.Location = new Point(179, 188);
		saveButton.Name = "saveButton";
		saveButton.Size = new Size(118, 38);
		saveButton.TabIndex = 3;
		saveButton.Text = "SAVE";
		saveButton.UseVisualStyleBackColor = true;
		saveButton.Click += saveButton_Click;
		// 
		// nameLabel
		// 
		nameLabel.AutoSize = true;
		nameLabel.Font = new Font("Segoe UI", 15F);
		nameLabel.Location = new Point(19, 83);
		nameLabel.Name = "nameLabel";
		nameLabel.Size = new Size(64, 28);
		nameLabel.TabIndex = 23;
		nameLabel.Text = "Name";
		// 
		// locationNameTextBox
		// 
		locationNameTextBox.Font = new Font("Segoe UI", 15F);
		locationNameTextBox.Location = new Point(188, 80);
		locationNameTextBox.Name = "locationNameTextBox";
		locationNameTextBox.PlaceholderText = "Location Name";
		locationNameTextBox.Size = new Size(271, 34);
		locationNameTextBox.TabIndex = 1;
		// 
		// statusCheckBox
		// 
		statusCheckBox.AutoSize = true;
		statusCheckBox.Font = new Font("Segoe UI", 15F);
		statusCheckBox.Location = new Point(197, 133);
		statusCheckBox.Name = "statusCheckBox";
		statusCheckBox.Size = new Size(84, 32);
		statusCheckBox.TabIndex = 2;
		statusCheckBox.Text = "Status";
		statusCheckBox.UseVisualStyleBackColor = true;
		// 
		// LocationForm
		// 
		AcceptButton = saveButton;
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		ClientSize = new Size(499, 255);
		Controls.Add(statusCheckBox);
		Controls.Add(locationComboBox);
		Controls.Add(saveButton);
		Controls.Add(nameLabel);
		Controls.Add(locationNameTextBox);
		Icon = (Icon)resources.GetObject("$this.Icon");
		Name = "LocationForm";
		StartPosition = FormStartPosition.CenterScreen;
		Text = "LocationForm";
		Load += LocationForm_Load;
		ResumeLayout(false);
		PerformLayout();
	}

	#endregion
	private ComboBox locationComboBox;
	private Button saveButton;
	private Label nameLabel;
	private TextBox locationNameTextBox;
	private CheckBox statusCheckBox;
}