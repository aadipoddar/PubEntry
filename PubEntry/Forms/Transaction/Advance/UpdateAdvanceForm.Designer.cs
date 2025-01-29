namespace PubEntry.Forms.Transaction.Advance
{
	partial class UpdateAdvanceForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateAdvanceForm));
			locationComboBox = new System.Windows.Forms.ComboBox();
			label3 = new System.Windows.Forms.Label();
			advanceDateTimePicker = new System.Windows.Forms.DateTimePicker();
			label1 = new System.Windows.Forms.Label();
			saveButton = new System.Windows.Forms.Button();
			approvedByLabel = new System.Windows.Forms.Label();
			approvedByTextBox = new System.Windows.Forms.TextBox();
			numberTextBox = new System.Windows.Forms.TextBox();
			loyaltyCheckBox = new System.Windows.Forms.CheckBox();
			numberLabel = new System.Windows.Forms.Label();
			nameLabel = new System.Windows.Forms.Label();
			nameTextBox = new System.Windows.Forms.TextBox();
			brandingLabel = new System.Windows.Forms.Label();
			richTextBoxFooter = new System.Windows.Forms.RichTextBox();
			SuspendLayout();
			// 
			// locationComboBox
			// 
			locationComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			locationComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			locationComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			locationComboBox.Font = new System.Drawing.Font("Segoe UI", 15F);
			locationComboBox.FormattingEnabled = true;
			locationComboBox.Location = new System.Drawing.Point(51, 12);
			locationComboBox.Name = "locationComboBox";
			locationComboBox.Size = new System.Drawing.Size(271, 36);
			locationComboBox.TabIndex = 39;
			locationComboBox.SelectedIndexChanged += locationComboBox_SelectedIndexChanged;
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Font = new System.Drawing.Font("Segoe UI", 12.75F);
			label3.Location = new System.Drawing.Point(39, 201);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(113, 23);
			label3.TabIndex = 50;
			label3.Text = "Booking Date";
			// 
			// advanceDateTimePicker
			// 
			advanceDateTimePicker.CalendarFont = new System.Drawing.Font("Segoe UI", 13F);
			advanceDateTimePicker.Font = new System.Drawing.Font("Segoe UI", 13F);
			advanceDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			advanceDateTimePicker.Location = new System.Drawing.Point(167, 195);
			advanceDateTimePicker.Name = "advanceDateTimePicker";
			advanceDateTimePicker.Size = new System.Drawing.Size(127, 31);
			advanceDateTimePicker.TabIndex = 43;
			advanceDateTimePicker.ValueChanged += advanceDateTimePicker_ValueChanged;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("Segoe UI", 10F);
			label1.Location = new System.Drawing.Point(51, 258);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(61, 19);
			label1.TabIndex = 49;
			label1.Text = "Remarks";
			// 
			// saveButton
			// 
			saveButton.Font = new System.Drawing.Font("Segoe UI", 12.75F);
			saveButton.Location = new System.Drawing.Point(107, 297);
			saveButton.Name = "saveButton";
			saveButton.Size = new System.Drawing.Size(135, 44);
			saveButton.TabIndex = 47;
			saveButton.Text = "SAVE";
			saveButton.UseVisualStyleBackColor = true;
			saveButton.Click += saveButton_Click;
			// 
			// approvedByLabel
			// 
			approvedByLabel.AutoSize = true;
			approvedByLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
			approvedByLabel.Location = new System.Drawing.Point(39, 243);
			approvedByLabel.Name = "approvedByLabel";
			approvedByLabel.Size = new System.Drawing.Size(88, 19);
			approvedByLabel.TabIndex = 48;
			approvedByLabel.Text = "Approved By";
			// 
			// approvedByTextBox
			// 
			approvedByTextBox.Font = new System.Drawing.Font("Segoe UI", 12.75F);
			approvedByTextBox.Location = new System.Drawing.Point(133, 247);
			approvedByTextBox.MaxLength = 50;
			approvedByTextBox.Name = "approvedByTextBox";
			approvedByTextBox.PlaceholderText = "Approved By / Remakrs";
			approvedByTextBox.Size = new System.Drawing.Size(208, 30);
			approvedByTextBox.TabIndex = 45;
			// 
			// numberTextBox
			// 
			numberTextBox.Font = new System.Drawing.Font("Segoe UI", 12.75F);
			numberTextBox.Location = new System.Drawing.Point(107, 63);
			numberTextBox.MaxLength = 15;
			numberTextBox.Name = "numberTextBox";
			numberTextBox.PlaceholderText = "Number";
			numberTextBox.Size = new System.Drawing.Size(234, 30);
			numberTextBox.TabIndex = 40;
			numberTextBox.TextChanged += numberTextBox_TextChanged;
			numberTextBox.KeyPress += textBox_KeyPress;
			// 
			// loyaltyCheckBox
			// 
			loyaltyCheckBox.AutoSize = true;
			loyaltyCheckBox.Font = new System.Drawing.Font("Segoe UI", 12.75F);
			loyaltyCheckBox.Location = new System.Drawing.Point(97, 149);
			loyaltyCheckBox.Name = "loyaltyCheckBox";
			loyaltyCheckBox.Size = new System.Drawing.Size(82, 27);
			loyaltyCheckBox.TabIndex = 42;
			loyaltyCheckBox.Text = "Loyalty";
			loyaltyCheckBox.UseVisualStyleBackColor = true;
			// 
			// numberLabel
			// 
			numberLabel.AutoSize = true;
			numberLabel.Font = new System.Drawing.Font("Segoe UI", 12.75F);
			numberLabel.Location = new System.Drawing.Point(39, 69);
			numberLabel.Name = "numberLabel";
			numberLabel.Size = new System.Drawing.Size(62, 23);
			numberLabel.TabIndex = 46;
			numberLabel.Text = "Mobile";
			// 
			// nameLabel
			// 
			nameLabel.AutoSize = true;
			nameLabel.Font = new System.Drawing.Font("Segoe UI", 12.75F);
			nameLabel.Location = new System.Drawing.Point(39, 112);
			nameLabel.Name = "nameLabel";
			nameLabel.Size = new System.Drawing.Size(56, 23);
			nameLabel.TabIndex = 44;
			nameLabel.Text = "Name";
			// 
			// nameTextBox
			// 
			nameTextBox.Font = new System.Drawing.Font("Segoe UI", 12.75F);
			nameTextBox.Location = new System.Drawing.Point(107, 106);
			nameTextBox.MaxLength = 250;
			nameTextBox.Name = "nameTextBox";
			nameTextBox.PlaceholderText = "Name";
			nameTextBox.Size = new System.Drawing.Size(234, 30);
			nameTextBox.TabIndex = 41;
			// 
			// brandingLabel
			// 
			brandingLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
			brandingLabel.AutoSize = true;
			brandingLabel.BackColor = System.Drawing.Color.White;
			brandingLabel.Location = new System.Drawing.Point(310, 367);
			brandingLabel.Name = "brandingLabel";
			brandingLabel.Size = new System.Drawing.Size(76, 15);
			brandingLabel.TabIndex = 52;
			brandingLabel.Text = "© AADISOFT";
			// 
			// richTextBoxFooter
			// 
			richTextBoxFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
			richTextBoxFooter.Location = new System.Drawing.Point(0, 361);
			richTextBoxFooter.Name = "richTextBoxFooter";
			richTextBoxFooter.Size = new System.Drawing.Size(391, 26);
			richTextBoxFooter.TabIndex = 51;
			richTextBoxFooter.Text = "";
			// 
			// UpdateAdvanceForm
			// 
			AcceptButton = saveButton;
			AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			ClientSize = new System.Drawing.Size(391, 387);
			Controls.Add(brandingLabel);
			Controls.Add(richTextBoxFooter);
			Controls.Add(locationComboBox);
			Controls.Add(label3);
			Controls.Add(advanceDateTimePicker);
			Controls.Add(label1);
			Controls.Add(saveButton);
			Controls.Add(approvedByLabel);
			Controls.Add(approvedByTextBox);
			Controls.Add(numberTextBox);
			Controls.Add(loyaltyCheckBox);
			Controls.Add(numberLabel);
			Controls.Add(nameLabel);
			Controls.Add(nameTextBox);
			Icon = ((System.Drawing.Icon)resources.GetObject("$this.Icon"));
			StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			Text = "UpdateAdvance";
			Load += UpdateAdvance_Load;
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private System.Windows.Forms.ComboBox locationComboBox;
		private Label label3;
		private System.Windows.Forms.DateTimePicker advanceDateTimePicker;
		private Label label1;
		private System.Windows.Forms.Button saveButton;
		private Label approvedByLabel;
		private TextBox approvedByTextBox;
		private System.Windows.Forms.TextBox numberTextBox;
		private CheckBox loyaltyCheckBox;
		private Label numberLabel;
		private Label nameLabel;
		private TextBox nameTextBox;
		private Label brandingLabel;
		private RichTextBox richTextBoxFooter;
	}
}