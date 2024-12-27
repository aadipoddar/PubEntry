namespace PubEntry.Admin;

partial class PersonForm
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
		saveButton = new Button();
		nameLabel = new Label();
		employeeNameTextBox = new TextBox();
		loyaltyComboBox = new ComboBox();
		loyaltyLabel = new Label();
		numberLabel = new Label();
		numberTextBox = new TextBox();
		SuspendLayout();
		// 
		// saveButton
		// 
		saveButton.Font = new Font("Segoe UI", 15F);
		saveButton.Location = new Point(192, 170);
		saveButton.Name = "saveButton";
		saveButton.Size = new Size(118, 38);
		saveButton.TabIndex = 22;
		saveButton.Text = "SAVE";
		saveButton.UseVisualStyleBackColor = true;
		// 
		// nameLabel
		// 
		nameLabel.AutoSize = true;
		nameLabel.Font = new Font("Segoe UI", 15F);
		nameLabel.Location = new Point(32, 72);
		nameLabel.Name = "nameLabel";
		nameLabel.Size = new Size(64, 28);
		nameLabel.TabIndex = 23;
		nameLabel.Text = "Name";
		// 
		// employeeNameTextBox
		// 
		employeeNameTextBox.Font = new Font("Segoe UI", 15F);
		employeeNameTextBox.Location = new Point(201, 69);
		employeeNameTextBox.Name = "employeeNameTextBox";
		employeeNameTextBox.PlaceholderText = "Name";
		employeeNameTextBox.Size = new Size(271, 34);
		employeeNameTextBox.TabIndex = 18;
		// 
		// loyaltyComboBox
		// 
		loyaltyComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
		loyaltyComboBox.FlatStyle = FlatStyle.System;
		loyaltyComboBox.Font = new Font("Segoe UI", 15F);
		loyaltyComboBox.FormattingEnabled = true;
		loyaltyComboBox.Items.AddRange(new object[] { "Active", "Inactive" });
		loyaltyComboBox.Location = new Point(201, 109);
		loyaltyComboBox.Name = "loyaltyComboBox";
		loyaltyComboBox.Size = new Size(271, 36);
		loyaltyComboBox.TabIndex = 21;
		// 
		// loyaltyLabel
		// 
		loyaltyLabel.AutoSize = true;
		loyaltyLabel.Font = new Font("Segoe UI", 15F);
		loyaltyLabel.Location = new Point(32, 112);
		loyaltyLabel.Name = "loyaltyLabel";
		loyaltyLabel.Size = new Size(75, 28);
		loyaltyLabel.TabIndex = 27;
		loyaltyLabel.Text = "Loyalty";
		// 
		// numberLabel
		// 
		numberLabel.AutoSize = true;
		numberLabel.Font = new Font("Segoe UI", 15F);
		numberLabel.Location = new Point(32, 32);
		numberLabel.Name = "numberLabel";
		numberLabel.Size = new Size(84, 28);
		numberLabel.TabIndex = 29;
		numberLabel.Text = "Number";
		// 
		// numberTextBox
		// 
		numberTextBox.Font = new Font("Segoe UI", 15F);
		numberTextBox.Location = new Point(201, 29);
		numberTextBox.Name = "numberTextBox";
		numberTextBox.PlaceholderText = "Number";
		numberTextBox.Size = new Size(271, 34);
		numberTextBox.TabIndex = 28;
		// 
		// PersonForm
		// 
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		ClientSize = new Size(524, 232);
		Controls.Add(numberLabel);
		Controls.Add(numberTextBox);
		Controls.Add(loyaltyLabel);
		Controls.Add(loyaltyComboBox);
		Controls.Add(saveButton);
		Controls.Add(nameLabel);
		Controls.Add(employeeNameTextBox);
		Name = "PersonForm";
		Text = "PersonForm";
		ResumeLayout(false);
		PerformLayout();
	}

	#endregion
	private Button saveButton;
	private Label nameLabel;
	private TextBox employeeNameTextBox;
	private ComboBox loyaltyComboBox;
	private Label loyaltyLabel;
	private Label numberLabel;
	private TextBox numberTextBox;
}