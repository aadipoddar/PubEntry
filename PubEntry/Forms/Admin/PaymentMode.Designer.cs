namespace PubEntry.Forms.Admin;

partial class PaymentMode
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
		statusCheckBox = new CheckBox();
		paymentComboBox = new ComboBox();
		saveButton = new Button();
		nameLabel = new Label();
		nameTextBox = new TextBox();
		SuspendLayout();
		// 
		// statusCheckBox
		// 
		statusCheckBox.AutoSize = true;
		statusCheckBox.Font = new Font("Segoe UI", 15F);
		statusCheckBox.Location = new Point(33, 134);
		statusCheckBox.Name = "statusCheckBox";
		statusCheckBox.Size = new Size(84, 32);
		statusCheckBox.TabIndex = 25;
		statusCheckBox.Text = "Status";
		statusCheckBox.UseVisualStyleBackColor = true;
		// 
		// paymentComboBox
		// 
		paymentComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
		paymentComboBox.FlatStyle = FlatStyle.System;
		paymentComboBox.Font = new Font("Segoe UI", 15F);
		paymentComboBox.FormattingEnabled = true;
		paymentComboBox.Location = new Point(70, 24);
		paymentComboBox.Name = "paymentComboBox";
		paymentComboBox.Size = new Size(271, 36);
		paymentComboBox.TabIndex = 27;
		paymentComboBox.SelectedIndexChanged += paymentComboBox_SelectedIndexChanged;
		// 
		// saveButton
		// 
		saveButton.Font = new Font("Segoe UI", 15F);
		saveButton.Location = new Point(170, 134);
		saveButton.Name = "saveButton";
		saveButton.Size = new Size(118, 38);
		saveButton.TabIndex = 26;
		saveButton.Text = "SAVE";
		saveButton.UseVisualStyleBackColor = true;
		saveButton.Click += saveButton_Click;
		// 
		// nameLabel
		// 
		nameLabel.AutoSize = true;
		nameLabel.Font = new Font("Segoe UI", 15F);
		nameLabel.Location = new Point(13, 84);
		nameLabel.Name = "nameLabel";
		nameLabel.Size = new Size(64, 28);
		nameLabel.TabIndex = 28;
		nameLabel.Text = "Name";
		// 
		// nameTextBox
		// 
		nameTextBox.Font = new Font("Segoe UI", 15F);
		nameTextBox.Location = new Point(95, 81);
		nameTextBox.MaxLength = 20;
		nameTextBox.Name = "nameTextBox";
		nameTextBox.PlaceholderText = "Name";
		nameTextBox.Size = new Size(271, 34);
		nameTextBox.TabIndex = 24;
		// 
		// PaymentMode
		// 
		AcceptButton = saveButton;
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		ClientSize = new Size(404, 190);
		Controls.Add(statusCheckBox);
		Controls.Add(paymentComboBox);
		Controls.Add(saveButton);
		Controls.Add(nameLabel);
		Controls.Add(nameTextBox);
		Name = "PaymentMode";
		StartPosition = FormStartPosition.CenterScreen;
		Text = "PaymentMode";
		Load += PaymentMode_Load;
		ResumeLayout(false);
		PerformLayout();
	}

	#endregion

	private CheckBox statusCheckBox;
	private ComboBox paymentComboBox;
	private Button saveButton;
	private Label nameLabel;
	private TextBox nameTextBox;
}