namespace PubEntry;

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
		numberTextBox = new TextBox();
		saveButton = new Button();
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
		dateChangeTimer = new System.Windows.Forms.Timer(components);
		loyaltyCheckBox = new CheckBox();
		versionLabel = new Label();
		advanceLabel = new Label();
		advanceAmountTextBox = new TextBox();
		label1 = new Label();
		bookingAmountTextBox = new TextBox();
		addButton = new Button();
		amountTextBox = new TextBox();
		paymentModeComboBox = new ComboBox();
		amountDataGridView = new DataGridView();
		PaymentId = new DataGridViewTextBoxColumn();
		PaymentMode = new DataGridViewTextBoxColumn();
		Amount = new DataGridViewTextBoxColumn();
		advancePanel = new Panel();
		label2 = new Label();
		((System.ComponentModel.ISupportInitialize)amountDataGridView).BeginInit();
		advancePanel.SuspendLayout();
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
		numberTextBox.TextChanged += numberTextBox_TextChanged;
		numberTextBox.KeyPress += textBox_KeyPress;
		// 
		// saveButton
		// 
		saveButton.Font = new Font("Segoe UI", 15F);
		saveButton.Location = new Point(122, 408);
		saveButton.Name = "saveButton";
		saveButton.Size = new Size(253, 70);
		saveButton.TabIndex = 11;
		saveButton.Text = "SAVE";
		saveButton.UseVisualStyleBackColor = true;
		saveButton.Click += insertButton_Click;
		// 
		// femaleLabel
		// 
		femaleLabel.AutoSize = true;
		femaleLabel.Font = new Font("Segoe UI", 15F);
		femaleLabel.Location = new Point(194, 199);
		femaleLabel.Name = "femaleLabel";
		femaleLabel.Size = new Size(74, 28);
		femaleLabel.TabIndex = 21;
		femaleLabel.Text = "Female";
		// 
		// femaleTextBox
		// 
		femaleTextBox.Font = new Font("Segoe UI", 15F);
		femaleTextBox.Location = new Point(185, 230);
		femaleTextBox.Name = "femaleTextBox";
		femaleTextBox.PlaceholderText = "Female";
		femaleTextBox.RightToLeft = RightToLeft.Yes;
		femaleTextBox.Size = new Size(92, 34);
		femaleTextBox.TabIndex = 4;
		femaleTextBox.Text = "0";
		femaleTextBox.KeyPress += textBox_KeyPress;
		// 
		// maleLabel
		// 
		maleLabel.AutoSize = true;
		maleLabel.Font = new Font("Segoe UI", 15F);
		maleLabel.Location = new Point(71, 199);
		maleLabel.Name = "maleLabel";
		maleLabel.Size = new Size(55, 28);
		maleLabel.TabIndex = 19;
		maleLabel.Text = "Male";
		// 
		// maleTextBox
		// 
		maleTextBox.Font = new Font("Segoe UI", 15F);
		maleTextBox.Location = new Point(50, 230);
		maleTextBox.Name = "maleTextBox";
		maleTextBox.PlaceholderText = "Male";
		maleTextBox.RightToLeft = RightToLeft.Yes;
		maleTextBox.Size = new Size(92, 34);
		maleTextBox.TabIndex = 3;
		maleTextBox.Text = "0";
		maleTextBox.KeyPress += textBox_KeyPress;
		// 
		// reservationComboBox
		// 
		reservationComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
		reservationComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
		reservationComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
		reservationComboBox.Font = new Font("Segoe UI", 15F);
		reservationComboBox.FormattingEnabled = true;
		reservationComboBox.Location = new Point(185, 285);
		reservationComboBox.Name = "reservationComboBox";
		reservationComboBox.Size = new Size(271, 36);
		reservationComboBox.TabIndex = 8;
		// 
		// reservationLabel
		// 
		reservationLabel.AutoSize = true;
		reservationLabel.Font = new Font("Segoe UI", 15F);
		reservationLabel.Location = new Point(22, 288);
		reservationLabel.Name = "reservationLabel";
		reservationLabel.Size = new Size(113, 28);
		reservationLabel.TabIndex = 23;
		reservationLabel.Text = "Reservation";
		// 
		// approvedByTextBox
		// 
		approvedByTextBox.Font = new Font("Segoe UI", 15F);
		approvedByTextBox.Location = new Point(185, 327);
		approvedByTextBox.Name = "approvedByTextBox";
		approvedByTextBox.PlaceholderText = "Approved By";
		approvedByTextBox.Size = new Size(271, 34);
		approvedByTextBox.TabIndex = 9;
		// 
		// approvedByLabel
		// 
		approvedByLabel.AutoSize = true;
		approvedByLabel.Font = new Font("Segoe UI", 15F);
		approvedByLabel.Location = new Point(22, 330);
		approvedByLabel.Name = "approvedByLabel";
		approvedByLabel.Size = new Size(126, 28);
		approvedByLabel.TabIndex = 25;
		approvedByLabel.Text = "Approved By";
		// 
		// dateTimeLabel
		// 
		dateTimeLabel.AutoSize = true;
		dateTimeLabel.Font = new Font("Segoe UI", 15F);
		dateTimeLabel.Location = new Point(137, 22);
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
		richTextBoxFooter.Location = new Point(0, 484);
		richTextBoxFooter.Name = "richTextBoxFooter";
		richTextBoxFooter.Size = new Size(855, 26);
		richTextBoxFooter.TabIndex = 27;
		richTextBoxFooter.Text = "";
		// 
		// brandingLabel
		// 
		brandingLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
		brandingLabel.AutoSize = true;
		brandingLabel.BackColor = Color.White;
		brandingLabel.Location = new Point(775, 490);
		brandingLabel.Name = "brandingLabel";
		brandingLabel.Size = new Size(76, 15);
		brandingLabel.TabIndex = 28;
		brandingLabel.Text = "© AADISOFT";
		// 
		// printDocumentMerchant
		// 
		printDocumentMerchant.PrintPage += printDocumentMerchant_PrintPage;
		// 
		// dateChangeTimer
		// 
		dateChangeTimer.Enabled = true;
		dateChangeTimer.Tick += dateChangeTimer_Tick;
		// 
		// loyaltyCheckBox
		// 
		loyaltyCheckBox.AutoSize = true;
		loyaltyCheckBox.Font = new Font("Segoe UI", 15F);
		loyaltyCheckBox.Location = new Point(137, 158);
		loyaltyCheckBox.Name = "loyaltyCheckBox";
		loyaltyCheckBox.Size = new Size(94, 32);
		loyaltyCheckBox.TabIndex = 2;
		loyaltyCheckBox.Text = "Loyalty";
		loyaltyCheckBox.UseVisualStyleBackColor = true;
		// 
		// versionLabel
		// 
		versionLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
		versionLabel.AutoSize = true;
		versionLabel.BackColor = Color.White;
		versionLabel.Location = new Point(5, 490);
		versionLabel.Name = "versionLabel";
		versionLabel.Size = new Size(84, 15);
		versionLabel.TabIndex = 31;
		versionLabel.Text = "Version: 0.0.0.0";
		// 
		// advanceLabel
		// 
		advanceLabel.AutoSize = true;
		advanceLabel.Font = new Font("Segoe UI", 15F);
		advanceLabel.Location = new Point(239, 12);
		advanceLabel.Name = "advanceLabel";
		advanceLabel.Size = new Size(87, 28);
		advanceLabel.TabIndex = 32;
		advanceLabel.Text = "Advance";
		// 
		// advanceAmountTextBox
		// 
		advanceAmountTextBox.Font = new Font("Segoe UI", 15F);
		advanceAmountTextBox.Location = new Point(228, 43);
		advanceAmountTextBox.Name = "advanceAmountTextBox";
		advanceAmountTextBox.PlaceholderText = "Cash Amount";
		advanceAmountTextBox.ReadOnly = true;
		advanceAmountTextBox.RightToLeft = RightToLeft.Yes;
		advanceAmountTextBox.Size = new Size(109, 34);
		advanceAmountTextBox.TabIndex = 33;
		advanceAmountTextBox.Text = "0";
		// 
		// label1
		// 
		label1.AutoSize = true;
		label1.Font = new Font("Segoe UI", 15F);
		label1.Location = new Point(35, 12);
		label1.Name = "label1";
		label1.Size = new Size(85, 28);
		label1.TabIndex = 34;
		label1.Text = "Booking";
		// 
		// bookingAmountTextBox
		// 
		bookingAmountTextBox.Font = new Font("Segoe UI", 15F);
		bookingAmountTextBox.Location = new Point(21, 43);
		bookingAmountTextBox.Name = "bookingAmountTextBox";
		bookingAmountTextBox.PlaceholderText = "Cash Amount";
		bookingAmountTextBox.ReadOnly = true;
		bookingAmountTextBox.RightToLeft = RightToLeft.Yes;
		bookingAmountTextBox.Size = new Size(109, 34);
		bookingAmountTextBox.TabIndex = 35;
		bookingAmountTextBox.Text = "0";
		// 
		// addButton
		// 
		addButton.Font = new Font("Segoe UI", 15F);
		addButton.Location = new Point(572, 216);
		addButton.Name = "addButton";
		addButton.Size = new Size(176, 50);
		addButton.TabIndex = 7;
		addButton.Text = "ADD";
		addButton.UseVisualStyleBackColor = true;
		addButton.Click += addButton_Click;
		// 
		// amountTextBox
		// 
		amountTextBox.Font = new Font("Segoe UI", 15F);
		amountTextBox.Location = new Point(642, 169);
		amountTextBox.Name = "amountTextBox";
		amountTextBox.PlaceholderText = "Amount";
		amountTextBox.RightToLeft = RightToLeft.Yes;
		amountTextBox.Size = new Size(195, 34);
		amountTextBox.TabIndex = 6;
		amountTextBox.Text = "0";
		// 
		// paymentModeComboBox
		// 
		paymentModeComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
		paymentModeComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
		paymentModeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
		paymentModeComboBox.Font = new Font("Segoe UI", 15F);
		paymentModeComboBox.FormattingEnabled = true;
		paymentModeComboBox.Location = new Point(475, 169);
		paymentModeComboBox.Name = "paymentModeComboBox";
		paymentModeComboBox.Size = new Size(161, 36);
		paymentModeComboBox.TabIndex = 5;
		// 
		// amountDataGridView
		// 
		amountDataGridView.AllowUserToAddRows = false;
		amountDataGridView.AllowUserToOrderColumns = true;
		amountDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		amountDataGridView.Columns.AddRange(new DataGridViewColumn[] { PaymentId, PaymentMode, Amount });
		amountDataGridView.Location = new Point(475, 276);
		amountDataGridView.Name = "amountDataGridView";
		amountDataGridView.ReadOnly = true;
		amountDataGridView.RowTemplate.Height = 40;
		amountDataGridView.Size = new Size(362, 202);
		amountDataGridView.TabIndex = 36;
		// 
		// PaymentId
		// 
		PaymentId.HeaderText = "Id";
		PaymentId.Name = "PaymentId";
		PaymentId.ReadOnly = true;
		PaymentId.Visible = false;
		// 
		// PaymentMode
		// 
		PaymentMode.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
		PaymentMode.HeaderText = "Payment Mode";
		PaymentMode.Name = "PaymentMode";
		PaymentMode.ReadOnly = true;
		PaymentMode.Width = 113;
		// 
		// Amount
		// 
		Amount.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
		Amount.HeaderText = "Amount";
		Amount.Name = "Amount";
		Amount.ReadOnly = true;
		// 
		// advancePanel
		// 
		advancePanel.Controls.Add(advanceLabel);
		advancePanel.Controls.Add(advanceAmountTextBox);
		advancePanel.Controls.Add(label1);
		advancePanel.Controls.Add(bookingAmountTextBox);
		advancePanel.Location = new Point(475, 22);
		advancePanel.Name = "advancePanel";
		advancePanel.Size = new Size(362, 89);
		advancePanel.TabIndex = 40;
		advancePanel.Visible = false;
		// 
		// label2
		// 
		label2.AutoSize = true;
		label2.Font = new Font("Segoe UI", 15F);
		label2.Location = new Point(572, 127);
		label2.Name = "label2";
		label2.Size = new Size(165, 28);
		label2.TabIndex = 36;
		label2.Text = "Amount Received";
		// 
		// EntryForm
		// 
		AcceptButton = saveButton;
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		ClientSize = new Size(855, 510);
		Controls.Add(label2);
		Controls.Add(advancePanel);
		Controls.Add(addButton);
		Controls.Add(amountTextBox);
		Controls.Add(paymentModeComboBox);
		Controls.Add(amountDataGridView);
		Controls.Add(versionLabel);
		Controls.Add(loyaltyCheckBox);
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
		Controls.Add(saveButton);
		Controls.Add(numberLabel);
		Controls.Add(numberTextBox);
		Controls.Add(nameLabel);
		Controls.Add(nameTextBox);
		Icon = (Icon)resources.GetObject("$this.Icon");
		Name = "EntryForm";
		StartPosition = FormStartPosition.CenterScreen;
		Text = "Dashboard";
		Load += EntryForm_Load;
		((System.ComponentModel.ISupportInitialize)amountDataGridView).EndInit();
		advancePanel.ResumeLayout(false);
		advancePanel.PerformLayout();
		ResumeLayout(false);
		PerformLayout();
	}

	#endregion

	private TextBox nameTextBox;
	private Label nameLabel;
	private Label numberLabel;
	private TextBox numberTextBox;
	private Button saveButton;
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
	private System.Windows.Forms.Timer dateChangeTimer;
	private CheckBox loyaltyCheckBox;
	private Label versionLabel;
	private Label advanceLabel;
	private TextBox advanceAmountTextBox;
	private Label label1;
	private TextBox bookingAmountTextBox;
	private Button addButton;
	private TextBox amountTextBox;
	private ComboBox paymentModeComboBox;
	private DataGridView amountDataGridView;
	private DataGridViewTextBoxColumn PaymentId;
	private DataGridViewTextBoxColumn PaymentMode;
	private DataGridViewTextBoxColumn Amount;
	private Panel advancePanel;
	private Label label2;
}