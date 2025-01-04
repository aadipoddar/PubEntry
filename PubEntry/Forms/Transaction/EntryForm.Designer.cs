namespace PubEntry.Forms.Transaction;

partial class EntryForm
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
		components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EntryForm));
		nameTextBox = new TextBox();
		nameLabel = new Label();
		numberLabel = new Label();
		cashLabel = new Label();
		saveButton = new Button();
		cardLabel = new Label();
		upiLabel = new Label();
		femaleLabel = new Label();
		maleLabel = new Label();
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
		dateChangeTimer = new System.Windows.Forms.Timer(components);
		loyaltyCheckBox = new CheckBox();
		versionLabel = new Label();
		numberTextBox = new TextBox();
		maleTextBox = new TextBox();
		femaleTextBox = new TextBox();
		cashTextBox = new TextBox();
		cardTextBox = new TextBox();
		amexTextBox = new TextBox();
		upiTextBox = new TextBox();
		SuspendLayout();
		// 
		// nameTextBox
		// 
		nameTextBox.Font = new Font("Segoe UI", 12.75F);
		nameTextBox.Location = new Point(158, 104);
		nameTextBox.MaxLength = 250;
		nameTextBox.Name = "nameTextBox";
		nameTextBox.PlaceholderText = "Name";
		nameTextBox.Size = new Size(234, 30);
		nameTextBox.TabIndex = 2;
		// 
		// nameLabel
		// 
		nameLabel.AutoSize = true;
		nameLabel.Font = new Font("Segoe UI", 12.75F);
		nameLabel.Location = new Point(12, 109);
		nameLabel.Name = "nameLabel";
		nameLabel.Size = new Size(56, 23);
		nameLabel.TabIndex = 1;
		nameLabel.Text = "Name";
		// 
		// numberLabel
		// 
		numberLabel.AutoSize = true;
		numberLabel.Font = new Font("Segoe UI", 12.75F);
		numberLabel.Location = new Point(12, 64);
		numberLabel.Name = "numberLabel";
		numberLabel.Size = new Size(130, 23);
		numberLabel.TabIndex = 3;
		numberLabel.Text = "Mobile Number";
		// 
		// cashLabel
		// 
		cashLabel.AutoSize = true;
		cashLabel.Font = new Font("Segoe UI", 12.75F);
		cashLabel.Location = new Point(414, 144);
		cashLabel.Name = "cashLabel";
		cashLabel.Size = new Size(47, 23);
		cashLabel.TabIndex = 5;
		cashLabel.Text = "Cash";
		// 
		// saveButton
		// 
		saveButton.Font = new Font("Segoe UI", 12.75F);
		saveButton.Location = new Point(142, 388);
		saveButton.Name = "saveButton";
		saveButton.Size = new Size(135, 44);
		saveButton.TabIndex = 12;
		saveButton.Text = "SAVE";
		saveButton.UseVisualStyleBackColor = true;
		saveButton.Click += SaveButton_Click;
		// 
		// cardLabel
		// 
		cardLabel.AutoSize = true;
		cardLabel.Font = new Font("Segoe UI", 12.75F);
		cardLabel.Location = new Point(414, 180);
		cardLabel.Name = "cardLabel";
		cardLabel.Size = new Size(46, 23);
		cardLabel.TabIndex = 15;
		cardLabel.Text = "Card";
		// 
		// upiLabel
		// 
		upiLabel.AutoSize = true;
		upiLabel.Font = new Font("Segoe UI", 12.75F);
		upiLabel.Location = new Point(414, 223);
		upiLabel.Name = "upiLabel";
		upiLabel.Size = new Size(37, 23);
		upiLabel.TabIndex = 17;
		upiLabel.Text = "UPI";
		// 
		// femaleLabel
		// 
		femaleLabel.AutoSize = true;
		femaleLabel.Font = new Font("Segoe UI", 12.75F);
		femaleLabel.Location = new Point(201, 197);
		femaleLabel.Name = "femaleLabel";
		femaleLabel.Size = new Size(64, 23);
		femaleLabel.TabIndex = 21;
		femaleLabel.Text = "Female";
		// 
		// maleLabel
		// 
		maleLabel.AutoSize = true;
		maleLabel.Font = new Font("Segoe UI", 12.75F);
		maleLabel.Location = new Point(72, 197);
		maleLabel.Name = "maleLabel";
		maleLabel.Size = new Size(47, 23);
		maleLabel.TabIndex = 19;
		maleLabel.Text = "Male";
		// 
		// reservationComboBox
		// 
		reservationComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
		reservationComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
		reservationComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
		reservationComboBox.Font = new Font("Segoe UI", 12.75F);
		reservationComboBox.FormattingEnabled = true;
		reservationComboBox.Location = new Point(158, 277);
		reservationComboBox.Name = "reservationComboBox";
		reservationComboBox.Size = new Size(234, 31);
		reservationComboBox.TabIndex = 10;
		// 
		// reservationLabel
		// 
		reservationLabel.AutoSize = true;
		reservationLabel.Font = new Font("Segoe UI", 12.75F);
		reservationLabel.Location = new Point(12, 281);
		reservationLabel.Name = "reservationLabel";
		reservationLabel.Size = new Size(98, 23);
		reservationLabel.TabIndex = 23;
		reservationLabel.Text = "Reservation";
		// 
		// approvedByTextBox
		// 
		approvedByTextBox.Font = new Font("Segoe UI", 12.75F);
		approvedByTextBox.Location = new Point(158, 323);
		approvedByTextBox.MaxLength = 50;
		approvedByTextBox.Name = "approvedByTextBox";
		approvedByTextBox.PlaceholderText = "Approved By";
		approvedByTextBox.Size = new Size(234, 30);
		approvedByTextBox.TabIndex = 11;
		// 
		// approvedByLabel
		// 
		approvedByLabel.AutoSize = true;
		approvedByLabel.Font = new Font("Segoe UI", 12.75F);
		approvedByLabel.Location = new Point(12, 326);
		approvedByLabel.Name = "approvedByLabel";
		approvedByLabel.Size = new Size(107, 23);
		approvedByLabel.TabIndex = 25;
		approvedByLabel.Text = "Approved By";
		// 
		// dateTimeLabel
		// 
		dateTimeLabel.AutoSize = true;
		dateTimeLabel.Font = new Font("Segoe UI", 15F);
		dateTimeLabel.Location = new Point(122, 18);
		dateTimeLabel.Name = "dateTimeLabel";
		dateTimeLabel.Size = new Size(165, 28);
		dateTimeLabel.TabIndex = 26;
		dateTimeLabel.Text = "23-12-24 8:24PM";
		// 
		// printDocumentCustomer
		// 
		printDocumentCustomer.PrintPage += printDocumentCustomer_PrintPage;
		// 
		// richTextBoxFooter
		// 
		richTextBoxFooter.Dock = DockStyle.Bottom;
		richTextBoxFooter.Location = new Point(0, 448);
		richTextBoxFooter.Name = "richTextBoxFooter";
		richTextBoxFooter.Size = new Size(608, 26);
		richTextBoxFooter.TabIndex = 27;
		richTextBoxFooter.Text = "";
		// 
		// brandingLabel
		// 
		brandingLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
		brandingLabel.AutoSize = true;
		brandingLabel.BackColor = Color.White;
		brandingLabel.Location = new Point(528, 526);
		brandingLabel.Name = "brandingLabel";
		brandingLabel.Size = new Size(76, 15);
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
		amexLabel.Font = new Font("Segoe UI", 12.75F);
		amexLabel.Location = new Point(414, 260);
		amexLabel.Name = "amexLabel";
		amexLabel.Size = new Size(53, 23);
		amexLabel.TabIndex = 30;
		amexLabel.Text = "Amex";
		// 
		// dateChangeTimer
		// 
		dateChangeTimer.Enabled = true;
		dateChangeTimer.Tick += dateChangeTimer_Tick;
		// 
		// loyaltyCheckBox
		// 
		loyaltyCheckBox.AutoSize = true;
		loyaltyCheckBox.Font = new Font("Segoe UI", 12.75F);
		loyaltyCheckBox.Location = new Point(142, 155);
		loyaltyCheckBox.Name = "loyaltyCheckBox";
		loyaltyCheckBox.Size = new Size(82, 27);
		loyaltyCheckBox.TabIndex = 3;
		loyaltyCheckBox.Text = "Loyalty";
		loyaltyCheckBox.UseVisualStyleBackColor = true;
		// 
		// versionLabel
		// 
		versionLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
		versionLabel.AutoSize = true;
		versionLabel.BackColor = Color.White;
		versionLabel.Location = new Point(5, 526);
		versionLabel.Name = "versionLabel";
		versionLabel.Size = new Size(84, 15);
		versionLabel.TabIndex = 31;
		versionLabel.Text = "Version: 0.0.0.0";
		// 
		// numberTextBox
		// 
		numberTextBox.Font = new Font("Segoe UI", 12.75F);
		numberTextBox.Location = new Point(158, 61);
		numberTextBox.MaxLength = 15;
		numberTextBox.Name = "numberTextBox";
		numberTextBox.PlaceholderText = "Number";
		numberTextBox.Size = new Size(234, 30);
		numberTextBox.TabIndex = 1;
		numberTextBox.Click += textBox_Click;
		numberTextBox.TextChanged += NumberTextBox_TextChanged;
		numberTextBox.KeyPress += textBox_KeyPress;
		// 
		// maleTextBox
		// 
		maleTextBox.Font = new Font("Segoe UI", 12.75F);
		maleTextBox.Location = new Point(57, 223);
		maleTextBox.MaxLength = 2;
		maleTextBox.Name = "maleTextBox";
		maleTextBox.PlaceholderText = "Male";
		maleTextBox.Size = new Size(84, 30);
		maleTextBox.TabIndex = 4;
		maleTextBox.Text = "0";
		maleTextBox.TextAlign = HorizontalAlignment.Right;
		maleTextBox.Click += textBox_Click;
		maleTextBox.KeyPress += textBox_KeyPress;
		// 
		// femaleTextBox
		// 
		femaleTextBox.Font = new Font("Segoe UI", 12.75F);
		femaleTextBox.Location = new Point(192, 224);
		femaleTextBox.MaxLength = 2;
		femaleTextBox.Name = "femaleTextBox";
		femaleTextBox.PlaceholderText = "Female";
		femaleTextBox.Size = new Size(84, 30);
		femaleTextBox.TabIndex = 5;
		femaleTextBox.Text = "0";
		femaleTextBox.TextAlign = HorizontalAlignment.Right;
		femaleTextBox.Click += textBox_Click;
		femaleTextBox.KeyPress += textBox_KeyPress;
		// 
		// cashTextBox
		// 
		cashTextBox.Font = new Font("Segoe UI", 12.75F);
		cashTextBox.Location = new Point(473, 141);
		cashTextBox.MaxLength = 10;
		cashTextBox.Name = "cashTextBox";
		cashTextBox.PlaceholderText = "Cash";
		cashTextBox.Size = new Size(104, 30);
		cashTextBox.TabIndex = 6;
		cashTextBox.Text = "0";
		cashTextBox.TextAlign = HorizontalAlignment.Right;
		cashTextBox.Click += textBox_Click;
		cashTextBox.KeyPress += textBox_KeyPress;
		// 
		// cardTextBox
		// 
		cardTextBox.Font = new Font("Segoe UI", 12.75F);
		cardTextBox.Location = new Point(473, 177);
		cardTextBox.MaxLength = 10;
		cardTextBox.Name = "cardTextBox";
		cardTextBox.PlaceholderText = "Card";
		cardTextBox.Size = new Size(104, 30);
		cardTextBox.TabIndex = 7;
		cardTextBox.Text = "0";
		cardTextBox.TextAlign = HorizontalAlignment.Right;
		cardTextBox.Click += textBox_Click;
		cardTextBox.KeyPress += textBox_KeyPress;
		// 
		// amexTextBox
		// 
		amexTextBox.Font = new Font("Segoe UI", 12.75F);
		amexTextBox.Location = new Point(473, 257);
		amexTextBox.MaxLength = 10;
		amexTextBox.Name = "amexTextBox";
		amexTextBox.PlaceholderText = "Amex";
		amexTextBox.Size = new Size(104, 30);
		amexTextBox.TabIndex = 9;
		amexTextBox.Text = "0";
		amexTextBox.TextAlign = HorizontalAlignment.Right;
		amexTextBox.Click += textBox_Click;
		amexTextBox.KeyPress += textBox_KeyPress;
		// 
		// upiTextBox
		// 
		upiTextBox.Font = new Font("Segoe UI", 12.75F);
		upiTextBox.Location = new Point(473, 217);
		upiTextBox.MaxLength = 10;
		upiTextBox.Name = "upiTextBox";
		upiTextBox.PlaceholderText = "UPI";
		upiTextBox.Size = new Size(104, 30);
		upiTextBox.TabIndex = 8;
		upiTextBox.Text = "0";
		upiTextBox.TextAlign = HorizontalAlignment.Right;
		upiTextBox.Click += textBox_Click;
		upiTextBox.KeyPress += textBox_KeyPress;
		// 
		// EntryForm
		// 
		AcceptButton = saveButton;
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		ClientSize = new Size(608, 474);
		Controls.Add(amexTextBox);
		Controls.Add(upiTextBox);
		Controls.Add(cardTextBox);
		Controls.Add(cashTextBox);
		Controls.Add(femaleTextBox);
		Controls.Add(maleTextBox);
		Controls.Add(numberTextBox);
		Controls.Add(versionLabel);
		Controls.Add(loyaltyCheckBox);
		Controls.Add(amexLabel);
		Controls.Add(brandingLabel);
		Controls.Add(richTextBoxFooter);
		Controls.Add(dateTimeLabel);
		Controls.Add(approvedByLabel);
		Controls.Add(approvedByTextBox);
		Controls.Add(reservationLabel);
		Controls.Add(reservationComboBox);
		Controls.Add(femaleLabel);
		Controls.Add(maleLabel);
		Controls.Add(upiLabel);
		Controls.Add(cardLabel);
		Controls.Add(saveButton);
		Controls.Add(cashLabel);
		Controls.Add(numberLabel);
		Controls.Add(nameLabel);
		Controls.Add(nameTextBox);
		Font = new Font("Segoe UI", 9F);
		Icon = (Icon)resources.GetObject("$this.Icon");
		Name = "EntryForm";
		StartPosition = FormStartPosition.CenterScreen;
		Text = "Dashboard";
		Load += EntryForm_Load;
		KeyPress += textBox_KeyPress;
		ResumeLayout(false);
		PerformLayout();
	}

	#endregion

	private TextBox nameTextBox;
	private Label nameLabel;
	private Label numberLabel;
	private Label cashLabel;
	private Button saveButton;
	private Label cardLabel;
	private Label upiLabel;
	private Label femaleLabel;
	private Label maleLabel;
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
	private System.Windows.Forms.Timer dateChangeTimer;
	private CheckBox loyaltyCheckBox;
	private Label versionLabel;
	private TextBox numberTextBox;
	private TextBox maleTextBox;
	private TextBox femaleTextBox;
	private TextBox cashTextBox;
	private TextBox cardTextBox;
	private TextBox amexTextBox;
	private TextBox upiTextBox;
}