namespace PubEntry.Forms.Admin
{
	partial class PaymentModeForm
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
			paymentModeComboBox = new ComboBox();
			saveButton = new Button();
			paymentModeLabel = new Label();
			nameTextBox = new TextBox();
			SuspendLayout();
			// 
			// statusCheckBox
			// 
			statusCheckBox.AutoSize = true;
			statusCheckBox.Font = new Font("Segoe UI", 15F);
			statusCheckBox.Location = new Point(179, 144);
			statusCheckBox.Name = "statusCheckBox";
			statusCheckBox.Size = new Size(84, 32);
			statusCheckBox.TabIndex = 25;
			statusCheckBox.Text = "Status";
			statusCheckBox.UseVisualStyleBackColor = true;
			// 
			// paymentModeComboBox
			// 
			paymentModeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
			paymentModeComboBox.FlatStyle = FlatStyle.System;
			paymentModeComboBox.Font = new Font("Segoe UI", 15F);
			paymentModeComboBox.FormattingEnabled = true;
			paymentModeComboBox.Location = new Point(98, 31);
			paymentModeComboBox.Name = "paymentModeComboBox";
			paymentModeComboBox.Size = new Size(271, 36);
			paymentModeComboBox.TabIndex = 27;
			paymentModeComboBox.SelectedIndexChanged += paymentModeComboBox_SelectedIndexChanged;
			// 
			// saveButton
			// 
			saveButton.Font = new Font("Segoe UI", 15F);
			saveButton.Location = new Point(161, 194);
			saveButton.Name = "saveButton";
			saveButton.Size = new Size(118, 43);
			saveButton.TabIndex = 26;
			saveButton.Text = "SAVE";
			saveButton.UseVisualStyleBackColor = true;
			saveButton.Click += saveButton_Click;
			// 
			// paymentModeLabel
			// 
			paymentModeLabel.AutoSize = true;
			paymentModeLabel.Font = new Font("Segoe UI", 15F);
			paymentModeLabel.Location = new Point(21, 91);
			paymentModeLabel.Name = "paymentModeLabel";
			paymentModeLabel.Size = new Size(144, 28);
			paymentModeLabel.TabIndex = 28;
			paymentModeLabel.Text = "Payment Mode";
			// 
			// nameTextBox
			// 
			nameTextBox.Font = new Font("Segoe UI", 15F);
			nameTextBox.Location = new Point(190, 88);
			nameTextBox.Name = "nameTextBox";
			nameTextBox.PlaceholderText = "Name";
			nameTextBox.Size = new Size(271, 34);
			nameTextBox.TabIndex = 24;
			// 
			// PaymentModeForm
			// 
			AcceptButton = saveButton;
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(490, 256);
			Controls.Add(statusCheckBox);
			Controls.Add(paymentModeComboBox);
			Controls.Add(saveButton);
			Controls.Add(paymentModeLabel);
			Controls.Add(nameTextBox);
			Name = "PaymentModeForm";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "Payment Mode";
			Load += PaymentModeForm_Load;
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private CheckBox statusCheckBox;
		private ComboBox paymentModeComboBox;
		private Button saveButton;
		private Label paymentModeLabel;
		private TextBox nameTextBox;
	}
}