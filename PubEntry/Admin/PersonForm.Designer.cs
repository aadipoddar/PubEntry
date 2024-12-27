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
		nameTextBox = new TextBox();
		numberLabel = new Label();
		numberTextBox = new TextBox();
		loyaltyCheckBox = new CheckBox();
		SuspendLayout();
		// 
		// saveButton
		// 
		saveButton.Font = new Font("Segoe UI", 15F);
		saveButton.Location = new Point(192, 170);
		saveButton.Name = "saveButton";
		saveButton.Size = new Size(118, 38);
		saveButton.TabIndex = 4;
		saveButton.Text = "SAVE";
		saveButton.UseVisualStyleBackColor = true;
		saveButton.Click += saveButton_Click;
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
		// nameTextBox
		// 
		nameTextBox.Font = new Font("Segoe UI", 15F);
		nameTextBox.Location = new Point(201, 69);
		nameTextBox.Name = "nameTextBox";
		nameTextBox.PlaceholderText = "Name";
		nameTextBox.Size = new Size(271, 34);
		nameTextBox.TabIndex = 2;
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
		numberTextBox.TabIndex = 1;
		numberTextBox.TextChanged += numberTextBox_TextChanged;
		numberTextBox.KeyPress += textBox_KeyPress;
		// 
		// loyaltyCheckBox
		// 
		loyaltyCheckBox.AutoSize = true;
		loyaltyCheckBox.Font = new Font("Segoe UI", 15F);
		loyaltyCheckBox.Location = new Point(201, 123);
		loyaltyCheckBox.Name = "loyaltyCheckBox";
		loyaltyCheckBox.Size = new Size(94, 32);
		loyaltyCheckBox.TabIndex = 3;
		loyaltyCheckBox.Text = "Loyalty";
		loyaltyCheckBox.UseVisualStyleBackColor = true;
		// 
		// PersonForm
		// 
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		ClientSize = new Size(524, 232);
		Controls.Add(loyaltyCheckBox);
		Controls.Add(numberLabel);
		Controls.Add(numberTextBox);
		Controls.Add(saveButton);
		Controls.Add(nameLabel);
		Controls.Add(nameTextBox);
		Name = "PersonForm";
		StartPosition = FormStartPosition.CenterScreen;
		Text = "PersonForm";
		ResumeLayout(false);
		PerformLayout();
	}

	#endregion
	private Button saveButton;
	private Label nameLabel;
	private TextBox nameTextBox;
	private Label numberLabel;
	private TextBox numberTextBox;
	private CheckBox loyaltyCheckBox;
}