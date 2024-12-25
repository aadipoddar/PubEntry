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
		statusLabel = new Label();
		statusComboBox = new ComboBox();
		locationComboBox = new ComboBox();
		saveButton = new Button();
		nameLabel = new Label();
		locationNameTextBox = new TextBox();
		SuspendLayout();
		// 
		// statusLabel
		// 
		statusLabel.AutoSize = true;
		statusLabel.Font = new Font("Segoe UI", 15F);
		statusLabel.Location = new Point(19, 123);
		statusLabel.Name = "statusLabel";
		statusLabel.Size = new Size(65, 28);
		statusLabel.TabIndex = 27;
		statusLabel.Text = "Status";
		// 
		// statusComboBox
		// 
		statusComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
		statusComboBox.FlatStyle = FlatStyle.System;
		statusComboBox.Font = new Font("Segoe UI", 15F);
		statusComboBox.FormattingEnabled = true;
		statusComboBox.Items.AddRange(new object[] { "Active", "Inactive" });
		statusComboBox.Location = new Point(188, 120);
		statusComboBox.Name = "statusComboBox";
		statusComboBox.Size = new Size(271, 36);
		statusComboBox.TabIndex = 21;
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
		locationComboBox.TabIndex = 24;
		locationComboBox.SelectedIndexChanged += locationComboBox_SelectedIndexChanged;
		// 
		// saveButton
		// 
		saveButton.Font = new Font("Segoe UI", 15F);
		saveButton.Location = new Point(179, 181);
		saveButton.Name = "saveButton";
		saveButton.Size = new Size(118, 38);
		saveButton.TabIndex = 22;
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
		locationNameTextBox.TabIndex = 18;
		// 
		// LocationForm
		// 
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		ClientSize = new Size(499, 255);
		Controls.Add(statusLabel);
		Controls.Add(statusComboBox);
		Controls.Add(locationComboBox);
		Controls.Add(saveButton);
		Controls.Add(nameLabel);
		Controls.Add(locationNameTextBox);
		Icon = (Icon)resources.GetObject("$this.Icon");
		Name = "LocationForm";
		StartPosition = FormStartPosition.CenterScreen;
		Text = "LocationForm";
		ResumeLayout(false);
		PerformLayout();
	}

	#endregion

	private Label statusLabel;
	private ComboBox statusComboBox;
	private ComboBox locationComboBox;
	private Button saveButton;
	private Label nameLabel;
	private TextBox locationNameTextBox;
}