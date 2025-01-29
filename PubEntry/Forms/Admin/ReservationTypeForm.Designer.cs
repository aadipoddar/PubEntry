namespace PubEntry.Forms.Admin;

partial class ReservationTypeForm
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReservationTypeForm));
		statusCheckBox = new CheckBox();
		reservationComboBox = new ComboBox();
		saveButton = new Button();
		nameLabel = new Label();
		nameTextBox = new TextBox();
		brandingLabel = new Label();
		richTextBoxFooter = new RichTextBox();
		SuspendLayout();
		// 
		// statusCheckBox
		// 
		statusCheckBox.AutoSize = true;
		statusCheckBox.Font = new Font("Segoe UI", 15F);
		statusCheckBox.Location = new Point(30, 122);
		statusCheckBox.Name = "statusCheckBox";
		statusCheckBox.Size = new Size(84, 32);
		statusCheckBox.TabIndex = 30;
		statusCheckBox.Text = "Status";
		statusCheckBox.UseVisualStyleBackColor = true;
		// 
		// reservationComboBox
		// 
		reservationComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
		reservationComboBox.FlatStyle = FlatStyle.System;
		reservationComboBox.Font = new Font("Segoe UI", 15F);
		reservationComboBox.FormattingEnabled = true;
		reservationComboBox.Location = new Point(67, 12);
		reservationComboBox.Name = "reservationComboBox";
		reservationComboBox.Size = new Size(271, 36);
		reservationComboBox.TabIndex = 32;
		reservationComboBox.SelectedIndexChanged += reservationComboBox_SelectedIndexChanged;
		// 
		// saveButton
		// 
		saveButton.Font = new Font("Segoe UI", 15F);
		saveButton.Location = new Point(167, 122);
		saveButton.Name = "saveButton";
		saveButton.Size = new Size(118, 38);
		saveButton.TabIndex = 31;
		saveButton.Text = "SAVE";
		saveButton.UseVisualStyleBackColor = true;
		saveButton.Click += saveButton_Click;
		// 
		// nameLabel
		// 
		nameLabel.AutoSize = true;
		nameLabel.Font = new Font("Segoe UI", 15F);
		nameLabel.Location = new Point(10, 72);
		nameLabel.Name = "nameLabel";
		nameLabel.Size = new Size(64, 28);
		nameLabel.TabIndex = 33;
		nameLabel.Text = "Name";
		// 
		// nameTextBox
		// 
		nameTextBox.Font = new Font("Segoe UI", 15F);
		nameTextBox.Location = new Point(92, 69);
		nameTextBox.MaxLength = 20;
		nameTextBox.Name = "nameTextBox";
		nameTextBox.PlaceholderText = "Name";
		nameTextBox.Size = new Size(271, 34);
		nameTextBox.TabIndex = 29;
		// 
		// brandingLabel
		// 
		brandingLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
		brandingLabel.AutoSize = true;
		brandingLabel.BackColor = Color.White;
		brandingLabel.Location = new Point(311, 175);
		brandingLabel.Name = "brandingLabel";
		brandingLabel.Size = new Size(76, 15);
		brandingLabel.TabIndex = 42;
		brandingLabel.Text = "© AADISOFT";
		// 
		// richTextBoxFooter
		// 
		richTextBoxFooter.Dock = DockStyle.Bottom;
		richTextBoxFooter.Location = new Point(0, 169);
		richTextBoxFooter.Name = "richTextBoxFooter";
		richTextBoxFooter.Size = new Size(392, 26);
		richTextBoxFooter.TabIndex = 41;
		richTextBoxFooter.Text = "";
		// 
		// ReservationTypeForm
		// 
		AcceptButton = saveButton;
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		ClientSize = new Size(392, 195);
		Controls.Add(brandingLabel);
		Controls.Add(richTextBoxFooter);
		Controls.Add(statusCheckBox);
		Controls.Add(reservationComboBox);
		Controls.Add(saveButton);
		Controls.Add(nameLabel);
		Controls.Add(nameTextBox);
		Icon = (Icon)resources.GetObject("$this.Icon");
		Name = "ReservationTypeForm";
		StartPosition = FormStartPosition.CenterScreen;
		Text = "Reservation Type";
		Load += ReservationTypeForm_Load;
		ResumeLayout(false);
		PerformLayout();
	}

	#endregion

	private CheckBox statusCheckBox;
	private ComboBox reservationComboBox;
	private Button saveButton;
	private Label nameLabel;
	private TextBox nameTextBox;
	private Label brandingLabel;
	private RichTextBox richTextBoxFooter;
}