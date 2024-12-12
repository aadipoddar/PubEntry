namespace PubEntry
{
	partial class SelectDataForm
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
			toDateLabel = new Label();
			toTimeLabel = new Label();
			fromTimeLabel = new Label();
			toTimeTextBox = new TextBox();
			fromTimeTextBox = new TextBox();
			formDateLabel = new Label();
			toDateTimePicker = new DateTimePicker();
			fromDateTimePicker = new DateTimePicker();
			summaryReportButton = new Button();
			detailReportButton = new Button();
			brandingLabel = new Label();
			richTextBoxFooter = new RichTextBox();
			SuspendLayout();
			// 
			// toDateLabel
			// 
			toDateLabel.AutoSize = true;
			toDateLabel.Font = new Font("Segoe UI", 15F);
			toDateLabel.Location = new Point(237, 32);
			toDateLabel.Name = "toDateLabel";
			toDateLabel.Size = new Size(78, 28);
			toDateLabel.TabIndex = 33;
			toDateLabel.Text = "To Date";
			// 
			// toTimeLabel
			// 
			toTimeLabel.AutoSize = true;
			toTimeLabel.Font = new Font("Segoe UI", 15F);
			toTimeLabel.Location = new Point(237, 107);
			toTimeLabel.Name = "toTimeLabel";
			toTimeLabel.Size = new Size(79, 28);
			toTimeLabel.TabIndex = 32;
			toTimeLabel.Text = "To Time";
			// 
			// fromTimeLabel
			// 
			fromTimeLabel.AutoSize = true;
			fromTimeLabel.Font = new Font("Segoe UI", 15F);
			fromTimeLabel.Location = new Point(30, 107);
			fromTimeLabel.Name = "fromTimeLabel";
			fromTimeLabel.Size = new Size(105, 28);
			fromTimeLabel.TabIndex = 31;
			fromTimeLabel.Text = "From Time";
			// 
			// toTimeTextBox
			// 
			toTimeTextBox.Font = new Font("Segoe UI", 18F);
			toTimeTextBox.Location = new Point(250, 142);
			toTimeTextBox.MaxLength = 2;
			toTimeTextBox.Name = "toTimeTextBox";
			toTimeTextBox.PlaceholderText = "24hr Time";
			toTimeTextBox.RightToLeft = RightToLeft.Yes;
			toTimeTextBox.Size = new Size(53, 39);
			toTimeTextBox.TabIndex = 28;
			toTimeTextBox.TextChanged += timeTextBox_TextChanged;
			toTimeTextBox.KeyPress += timeTextBox_KeyPress;
			// 
			// fromTimeTextBox
			// 
			fromTimeTextBox.Font = new Font("Segoe UI", 18F);
			fromTimeTextBox.Location = new Point(48, 142);
			fromTimeTextBox.MaxLength = 2;
			fromTimeTextBox.Name = "fromTimeTextBox";
			fromTimeTextBox.PlaceholderText = "24hr Time";
			fromTimeTextBox.RightToLeft = RightToLeft.Yes;
			fromTimeTextBox.Size = new Size(53, 39);
			fromTimeTextBox.TabIndex = 26;
			fromTimeTextBox.Text = "5";
			fromTimeTextBox.TextChanged += timeTextBox_TextChanged;
			fromTimeTextBox.KeyPress += timeTextBox_KeyPress;
			// 
			// formDateLabel
			// 
			formDateLabel.AutoSize = true;
			formDateLabel.Font = new Font("Segoe UI", 15F);
			formDateLabel.Location = new Point(30, 32);
			formDateLabel.Name = "formDateLabel";
			formDateLabel.Size = new Size(104, 28);
			formDateLabel.TabIndex = 30;
			formDateLabel.Text = "Form Date";
			// 
			// toDateTimePicker
			// 
			toDateTimePicker.Font = new Font("Segoe UI", 15F);
			toDateTimePicker.Format = DateTimePickerFormat.Short;
			toDateTimePicker.Location = new Point(215, 63);
			toDateTimePicker.Name = "toDateTimePicker";
			toDateTimePicker.Size = new Size(135, 34);
			toDateTimePicker.TabIndex = 27;
			// 
			// fromDateTimePicker
			// 
			fromDateTimePicker.Font = new Font("Segoe UI", 15F);
			fromDateTimePicker.Format = DateTimePickerFormat.Short;
			fromDateTimePicker.Location = new Point(20, 63);
			fromDateTimePicker.Name = "fromDateTimePicker";
			fromDateTimePicker.Size = new Size(131, 34);
			fromDateTimePicker.TabIndex = 25;
			// 
			// summaryReportButton
			// 
			summaryReportButton.Font = new Font("Segoe UI", 15F);
			summaryReportButton.Location = new Point(90, 212);
			summaryReportButton.Name = "summaryReportButton";
			summaryReportButton.Size = new Size(175, 38);
			summaryReportButton.TabIndex = 29;
			summaryReportButton.Text = "Summary Report";
			summaryReportButton.UseVisualStyleBackColor = true;
			summaryReportButton.Click += summaryReportButton_Click;
			// 
			// detailReportButton
			// 
			detailReportButton.Font = new Font("Segoe UI", 15F);
			detailReportButton.Location = new Point(90, 269);
			detailReportButton.Name = "detailReportButton";
			detailReportButton.Size = new Size(175, 38);
			detailReportButton.TabIndex = 34;
			detailReportButton.Text = "Detail Report";
			detailReportButton.UseVisualStyleBackColor = true;
			detailReportButton.Click += detailReportButton_Click;
			// 
			// brandingLabel
			// 
			brandingLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			brandingLabel.AutoSize = true;
			brandingLabel.BackColor = Color.White;
			brandingLabel.Location = new Point(313, 334);
			brandingLabel.Name = "brandingLabel";
			brandingLabel.Size = new Size(75, 15);
			brandingLabel.TabIndex = 36;
			brandingLabel.Text = "© AADISOFT";
			// 
			// richTextBoxFooter
			// 
			richTextBoxFooter.Dock = DockStyle.Bottom;
			richTextBoxFooter.Location = new Point(0, 327);
			richTextBoxFooter.Name = "richTextBoxFooter";
			richTextBoxFooter.Size = new Size(392, 26);
			richTextBoxFooter.TabIndex = 35;
			richTextBoxFooter.Text = "";
			// 
			// SelectDataForm
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(392, 353);
			Controls.Add(brandingLabel);
			Controls.Add(richTextBoxFooter);
			Controls.Add(detailReportButton);
			Controls.Add(toDateLabel);
			Controls.Add(toTimeLabel);
			Controls.Add(fromTimeLabel);
			Controls.Add(toTimeTextBox);
			Controls.Add(fromTimeTextBox);
			Controls.Add(formDateLabel);
			Controls.Add(toDateTimePicker);
			Controls.Add(fromDateTimePicker);
			Controls.Add(summaryReportButton);
			Name = "SelectDataForm";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "SelectDataForm";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Label toDateLabel;
		private Label toTimeLabel;
		private Label fromTimeLabel;
		private TextBox toTimeTextBox;
		private TextBox fromTimeTextBox;
		private Label formDateLabel;
		private DateTimePicker toDateTimePicker;
		private DateTimePicker fromDateTimePicker;
		private Button summaryReportButton;
		private Button detailReportButton;
		private Label brandingLabel;
		private RichTextBox richTextBoxFooter;
	}
}