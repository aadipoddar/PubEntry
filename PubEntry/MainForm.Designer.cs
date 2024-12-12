namespace PubEntry;

partial class MainForm
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
		nameTextBox = new TextBox();
		nameLabel = new Label();
		numberLabel = new Label();
		numberTextBox = new TextBox();
		cashLabel = new Label();
		cashAmountTextBox = new TextBox();
		saveButton = new Button();
		cardLabel = new Label();
		cardAmountTextBox = new TextBox();
		upiLabel = new Label();
		upiAmountTextBox = new TextBox();
		femaleLabel = new Label();
		femaleTextBox = new TextBox();
		maleLabel = new Label();
		maleTextBox = new TextBox();
		reservationComboBox = new ComboBox();
		reservationLabel = new Label();
		approvedByTextBox = new TextBox();
		approvedByLabel = new Label();
		dateTimeLabel = new Label();
		printDocumentCustomer = new System.Drawing.Printing.PrintDocument();
		richTextBoxFooter = new RichTextBox();
		brandingLabel = new Label();
		printDocumentMerchant = new System.Drawing.Printing.PrintDocument();
		amexLabel = new Label();
		amexAmountTextBox = new TextBox();
		SuspendLayout();
		// 
		// nameTextBox
		// 
		nameTextBox.Font = new Font("Segoe UI", 15F);
		nameTextBox.Location = new Point(185, 108);
		nameTextBox.Name = "nameTextBox";
		nameTextBox.PlaceholderText = "Name";
		nameTextBox.Size = new Size(271, 34);
		nameTextBox.TabIndex = 1;
		nameTextBox.KeyDown += nameTextBox_KeyDown;
		// 
		// nameLabel
		// 
		nameLabel.AutoSize = true;
		nameLabel.Font = new Font("Segoe UI", 15F);
		nameLabel.Location = new Point(13, 109);
		nameLabel.Name = "nameLabel";
		nameLabel.Size = new Size(64, 28);
		nameLabel.TabIndex = 1;
		nameLabel.Text = "Name";
		// 
		// numberLabel
		// 
		numberLabel.AutoSize = true;
		numberLabel.Font = new Font("Segoe UI", 15F);
		numberLabel.Location = new Point(12, 68);
		numberLabel.Name = "numberLabel";
		numberLabel.Size = new Size(151, 28);
		numberLabel.TabIndex = 3;
		numberLabel.Text = "Mobile Number";
		// 
		// numberTextBox
		// 
		numberTextBox.Font = new Font("Segoe UI", 15F);
		numberTextBox.Location = new Point(185, 68);
		numberTextBox.Name = "numberTextBox";
		numberTextBox.PlaceholderText = "Mobile Number";
		numberTextBox.Size = new Size(271, 34);
		numberTextBox.TabIndex = 0;
		numberTextBox.KeyDown += numberTextBox_KeyDown;
		numberTextBox.KeyPress += textBox_KeyPress;
		// 
		// cashLabel
		// 
		cashLabel.AutoSize = true;
		cashLabel.Font = new Font("Segoe UI", 15F);
		cashLabel.Location = new Point(281, 176);
		cashLabel.Name = "cashLabel";
		cashLabel.Size = new Size(53, 28);
		cashLabel.TabIndex = 5;
		cashLabel.Text = "Cash";
		// 
		// cashAmountTextBox
		// 
		cashAmountTextBox.Font = new Font("Segoe UI", 15F);
		cashAmountTextBox.Location = new Point(343, 170);
		cashAmountTextBox.Name = "cashAmountTextBox";
		cashAmountTextBox.PlaceholderText = "Cash Amount";
		cashAmountTextBox.RightToLeft = RightToLeft.Yes;
		cashAmountTextBox.Size = new Size(109, 34);
		cashAmountTextBox.TabIndex = 4;
		cashAmountTextBox.Text = "0";
		cashAmountTextBox.KeyPress += textBox_KeyPress;
		// 
		// saveButton
		// 
		saveButton.Font = new Font("Segoe UI", 15F);
		saveButton.Location = new Point(167, 463);
		saveButton.Name = "saveButton";
		saveButton.Size = new Size(135, 44);
		saveButton.TabIndex = 10;
		saveButton.Text = "SAVE";
		saveButton.UseVisualStyleBackColor = true;
		saveButton.Click += insertButton_Click;
		// 
		// cardLabel
		// 
		cardLabel.AutoSize = true;
		cardLabel.Font = new Font("Segoe UI", 15F);
		cardLabel.Location = new Point(281, 216);
		cardLabel.Name = "cardLabel";
		cardLabel.Size = new Size(53, 28);
		cardLabel.TabIndex = 15;
		cardLabel.Text = "Card";
		// 
		// cardAmountTextBox
		// 
		cardAmountTextBox.Font = new Font("Segoe UI", 15F);
		cardAmountTextBox.Location = new Point(343, 213);
		cardAmountTextBox.Name = "cardAmountTextBox";
		cardAmountTextBox.PlaceholderText = "Card Amount";
		cardAmountTextBox.RightToLeft = RightToLeft.Yes;
		cardAmountTextBox.Size = new Size(109, 34);
		cardAmountTextBox.TabIndex = 5;
		cardAmountTextBox.Text = "0";
		cardAmountTextBox.KeyPress += textBox_KeyPress;
		// 
		// upiLabel
		// 
		upiLabel.AutoSize = true;
		upiLabel.Font = new Font("Segoe UI", 15F);
		upiLabel.Location = new Point(281, 259);
		upiLabel.Name = "upiLabel";
		upiLabel.Size = new Size(42, 28);
		upiLabel.TabIndex = 17;
		upiLabel.Text = "UPI";
		// 
		// upiAmountTextBox
		// 
		upiAmountTextBox.Font = new Font("Segoe UI", 15F);
		upiAmountTextBox.Location = new Point(343, 253);
		upiAmountTextBox.Name = "upiAmountTextBox";
		upiAmountTextBox.PlaceholderText = "UPI Amount";
		upiAmountTextBox.RightToLeft = RightToLeft.Yes;
		upiAmountTextBox.Size = new Size(109, 34);
		upiAmountTextBox.TabIndex = 6;
		upiAmountTextBox.Text = "0";
		upiAmountTextBox.KeyPress += textBox_KeyPress;
		// 
		// femaleLabel
		// 
		femaleLabel.AutoSize = true;
		femaleLabel.Font = new Font("Segoe UI", 15F);
		femaleLabel.Location = new Point(22, 213);
		femaleLabel.Name = "femaleLabel";
		femaleLabel.Size = new Size(74, 28);
		femaleLabel.TabIndex = 21;
		femaleLabel.Text = "Female";
		// 
		// femaleTextBox
		// 
		femaleTextBox.Font = new Font("Segoe UI", 15F);
		femaleTextBox.Location = new Point(105, 210);
		femaleTextBox.Name = "femaleTextBox";
		femaleTextBox.PlaceholderText = "Card Amount";
		femaleTextBox.RightToLeft = RightToLeft.Yes;
		femaleTextBox.Size = new Size(126, 34);
		femaleTextBox.TabIndex = 3;
		femaleTextBox.Text = "0";
		femaleTextBox.KeyPress += textBox_KeyPress;
		// 
		// maleLabel
		// 
		maleLabel.AutoSize = true;
		maleLabel.Font = new Font("Segoe UI", 15F);
		maleLabel.Location = new Point(22, 173);
		maleLabel.Name = "maleLabel";
		maleLabel.Size = new Size(55, 28);
		maleLabel.TabIndex = 19;
		maleLabel.Text = "Male";
		// 
		// maleTextBox
		// 
		maleTextBox.Font = new Font("Segoe UI", 15F);
		maleTextBox.Location = new Point(105, 170);
		maleTextBox.Name = "maleTextBox";
		maleTextBox.PlaceholderText = "Cash Amount";
		maleTextBox.RightToLeft = RightToLeft.Yes;
		maleTextBox.Size = new Size(126, 34);
		maleTextBox.TabIndex = 2;
		maleTextBox.Text = "0";
		maleTextBox.KeyPress += textBox_KeyPress;
		// 
		// reservationComboBox
		// 
		reservationComboBox.Font = new Font("Segoe UI", 15F);
		reservationComboBox.FormattingEnabled = true;
		reservationComboBox.Location = new Point(185, 365);
		reservationComboBox.Name = "reservationComboBox";
		reservationComboBox.Size = new Size(271, 36);
		reservationComboBox.TabIndex = 8;
		// 
		// reservationLabel
		// 
		reservationLabel.AutoSize = true;
		reservationLabel.Font = new Font("Segoe UI", 15F);
		reservationLabel.Location = new Point(22, 368);
		reservationLabel.Name = "reservationLabel";
		reservationLabel.Size = new Size(113, 28);
		reservationLabel.TabIndex = 23;
		reservationLabel.Text = "Reservation";
		// 
		// approvedByTextBox
		// 
		approvedByTextBox.Font = new Font("Segoe UI", 15F);
		approvedByTextBox.Location = new Point(185, 407);
		approvedByTextBox.Name = "approvedByTextBox";
		approvedByTextBox.PlaceholderText = "Approved By";
		approvedByTextBox.Size = new Size(271, 34);
		approvedByTextBox.TabIndex = 9;
		// 
		// approvedByLabel
		// 
		approvedByLabel.AutoSize = true;
		approvedByLabel.Font = new Font("Segoe UI", 15F);
		approvedByLabel.Location = new Point(22, 410);
		approvedByLabel.Name = "approvedByLabel";
		approvedByLabel.Size = new Size(126, 28);
		approvedByLabel.TabIndex = 25;
		approvedByLabel.Text = "Approved By";
		// 
		// dateTimeLabel
		// 
		dateTimeLabel.AutoSize = true;
		dateTimeLabel.Font = new Font("Segoe UI", 15F);
		dateTimeLabel.Location = new Point(94, 22);
		dateTimeLabel.Name = "dateTimeLabel";
		dateTimeLabel.Size = new Size(267, 28);
		dateTimeLabel.TabIndex = 26;
		dateTimeLabel.Text = "23rd December 2024 8:24PM";
		// 
		// printDocumentCustomer
		// 
		printDocumentCustomer.PrintPage += printDocumentCustomer_PrintPage;
		// 
		// richTextBoxFooter
		// 
		richTextBoxFooter.Dock = DockStyle.Bottom;
		richTextBoxFooter.Location = new Point(0, 526);
		richTextBoxFooter.Name = "richTextBoxFooter";
		richTextBoxFooter.Size = new Size(486, 26);
		richTextBoxFooter.TabIndex = 27;
		richTextBoxFooter.Text = "";
		// 
		// brandingLabel
		// 
		brandingLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
		brandingLabel.AutoSize = true;
		brandingLabel.BackColor = Color.White;
		brandingLabel.Location = new Point(406, 532);
		brandingLabel.Name = "brandingLabel";
		brandingLabel.Size = new Size(75, 15);
		brandingLabel.TabIndex = 28;
		brandingLabel.Text = "© AADISOFT";
		// 
		// printDocumentMerchant
		// 
		printDocumentMerchant.PrintPage += printDocumentMerchant_PrintPage;
		// 
		// amexLabel
		// 
		amexLabel.AutoSize = true;
		amexLabel.Font = new Font("Segoe UI", 15F);
		amexLabel.Location = new Point(281, 296);
		amexLabel.Name = "amexLabel";
		amexLabel.Size = new Size(61, 28);
		amexLabel.TabIndex = 30;
		amexLabel.Text = "Amex";
		// 
		// amexAmountTextBox
		// 
		amexAmountTextBox.Font = new Font("Segoe UI", 15F);
		amexAmountTextBox.Location = new Point(343, 293);
		amexAmountTextBox.Name = "amexAmountTextBox";
		amexAmountTextBox.PlaceholderText = "UPI Amount";
		amexAmountTextBox.RightToLeft = RightToLeft.Yes;
		amexAmountTextBox.Size = new Size(109, 34);
		amexAmountTextBox.TabIndex = 7;
		amexAmountTextBox.Text = "0";
		amexAmountTextBox.KeyPress += textBox_KeyPress;
		// 
		// MainForm
		// 
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		ClientSize = new Size(486, 552);
		Controls.Add(amexLabel);
		Controls.Add(amexAmountTextBox);
		Controls.Add(brandingLabel);
		Controls.Add(richTextBoxFooter);
		Controls.Add(dateTimeLabel);
		Controls.Add(approvedByLabel);
		Controls.Add(approvedByTextBox);
		Controls.Add(reservationLabel);
		Controls.Add(reservationComboBox);
		Controls.Add(femaleLabel);
		Controls.Add(femaleTextBox);
		Controls.Add(maleLabel);
		Controls.Add(maleTextBox);
		Controls.Add(upiLabel);
		Controls.Add(upiAmountTextBox);
		Controls.Add(cardLabel);
		Controls.Add(cardAmountTextBox);
		Controls.Add(saveButton);
		Controls.Add(cashLabel);
		Controls.Add(cashAmountTextBox);
		Controls.Add(numberLabel);
		Controls.Add(numberTextBox);
		Controls.Add(nameLabel);
		Controls.Add(nameTextBox);
		Name = "MainForm";
		StartPosition = FormStartPosition.CenterScreen;
		Text = "Dashboard";
		FormClosed += MainForm_FormClosed;
		ResumeLayout(false);
		PerformLayout();
	}

	#endregion

	private TextBox nameTextBox;
	private Label nameLabel;
	private Label numberLabel;
	private TextBox numberTextBox;
	private Label cashLabel;
	private TextBox cashAmountTextBox;
	private Button saveButton;
	private Label cardLabel;
	private TextBox cardAmountTextBox;
	private Label upiLabel;
	private TextBox upiAmountTextBox;
	private Label femaleLabel;
	private TextBox femaleTextBox;
	private Label maleLabel;
	private TextBox maleTextBox;
	private ComboBox reservationComboBox;
	private Label reservationLabel;
	private TextBox approvedByTextBox;
	private Label approvedByLabel;
	private Label dateTimeLabel;
	private System.Drawing.Printing.PrintDocument printDocumentCustomer;
	private RichTextBox richTextBoxFooter;
	private Label brandingLabel;
	private System.Drawing.Printing.PrintDocument printDocumentMerchant;
	private Label amexLabel;
	private TextBox amexAmountTextBox;
}