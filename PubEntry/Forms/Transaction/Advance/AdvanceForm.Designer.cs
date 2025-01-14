namespace PubEntry.Forms.Transaction.Advance;

partial class AdvanceForm
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
		DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
		numberTextBox = new TextBox();
		loyaltyCheckBox = new CheckBox();
		numberLabel = new Label();
		nameLabel = new Label();
		nameTextBox = new TextBox();
		approvedByLabel = new Label();
		approvedByTextBox = new TextBox();
		saveButton = new Button();
		paymentComboBox = new ComboBox();
		amountTextBox = new TextBox();
		amountDataGridView = new DataGridView();
		Id = new DataGridViewTextBoxColumn();
		PaymentMode = new DataGridViewTextBoxColumn();
		Amount = new DataGridViewTextBoxColumn();
		bookingLabel = new Label();
		bookingTextBox = new TextBox();
		label1 = new Label();
		totalTextBox = new TextBox();
		label2 = new Label();
		advanceDateTimePicker = new DateTimePicker();
		label3 = new Label();
		locationComboBox = new ComboBox();
		addButton = new Button();
		versionLabel = new Label();
		brandingLabel = new Label();
		richTextBoxFooter = new RichTextBox();
		((System.ComponentModel.ISupportInitialize)amountDataGridView).BeginInit();
		SuspendLayout();
		// 
		// numberTextBox
		// 
		numberTextBox.Font = new Font("Segoe UI", 12.75F);
		numberTextBox.Location = new Point(80, 63);
		numberTextBox.MaxLength = 15;
		numberTextBox.Name = "numberTextBox";
		numberTextBox.PlaceholderText = "Number";
		numberTextBox.Size = new Size(234, 30);
		numberTextBox.TabIndex = 2;
		numberTextBox.Click += textBox_Click;
		numberTextBox.TextChanged += numberTextBox_TextChanged;
		numberTextBox.KeyPress += textBox_KeyPress;
		// 
		// loyaltyCheckBox
		// 
		loyaltyCheckBox.AutoSize = true;
		loyaltyCheckBox.Font = new Font("Segoe UI", 12.75F);
		loyaltyCheckBox.Location = new Point(70, 149);
		loyaltyCheckBox.Name = "loyaltyCheckBox";
		loyaltyCheckBox.Size = new Size(82, 27);
		loyaltyCheckBox.TabIndex = 4;
		loyaltyCheckBox.Text = "Loyalty";
		loyaltyCheckBox.UseVisualStyleBackColor = true;
		// 
		// numberLabel
		// 
		numberLabel.AutoSize = true;
		numberLabel.Font = new Font("Segoe UI", 12.75F);
		numberLabel.Location = new Point(12, 69);
		numberLabel.Name = "numberLabel";
		numberLabel.Size = new Size(62, 23);
		numberLabel.TabIndex = 8;
		numberLabel.Text = "Mobile";
		// 
		// nameLabel
		// 
		nameLabel.AutoSize = true;
		nameLabel.Font = new Font("Segoe UI", 12.75F);
		nameLabel.Location = new Point(12, 112);
		nameLabel.Name = "nameLabel";
		nameLabel.Size = new Size(56, 23);
		nameLabel.TabIndex = 5;
		nameLabel.Text = "Name";
		// 
		// nameTextBox
		// 
		nameTextBox.Font = new Font("Segoe UI", 12.75F);
		nameTextBox.Location = new Point(80, 106);
		nameTextBox.MaxLength = 250;
		nameTextBox.Name = "nameTextBox";
		nameTextBox.PlaceholderText = "Name";
		nameTextBox.Size = new Size(234, 30);
		nameTextBox.TabIndex = 3;
		// 
		// approvedByLabel
		// 
		approvedByLabel.AutoSize = true;
		approvedByLabel.Font = new Font("Segoe UI", 10F);
		approvedByLabel.Location = new Point(12, 243);
		approvedByLabel.Name = "approvedByLabel";
		approvedByLabel.Size = new Size(88, 19);
		approvedByLabel.TabIndex = 27;
		approvedByLabel.Text = "Approved By";
		// 
		// approvedByTextBox
		// 
		approvedByTextBox.Font = new Font("Segoe UI", 12.75F);
		approvedByTextBox.Location = new Point(106, 247);
		approvedByTextBox.MaxLength = 50;
		approvedByTextBox.Name = "approvedByTextBox";
		approvedByTextBox.PlaceholderText = "Approved By / Remakrs";
		approvedByTextBox.Size = new Size(208, 30);
		approvedByTextBox.TabIndex = 6;
		// 
		// saveButton
		// 
		saveButton.Font = new Font("Segoe UI", 12.75F);
		saveButton.Location = new Point(106, 294);
		saveButton.Name = "saveButton";
		saveButton.Size = new Size(135, 44);
		saveButton.TabIndex = 11;
		saveButton.Text = "SAVE";
		saveButton.UseVisualStyleBackColor = true;
		saveButton.Click += saveButton_Click;
		// 
		// paymentComboBox
		// 
		paymentComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
		paymentComboBox.FlatStyle = FlatStyle.System;
		paymentComboBox.Font = new Font("Segoe UI", 13F);
		paymentComboBox.FormattingEnabled = true;
		paymentComboBox.Location = new Point(376, 100);
		paymentComboBox.Name = "paymentComboBox";
		paymentComboBox.Size = new Size(103, 31);
		paymentComboBox.TabIndex = 8;
		// 
		// amountTextBox
		// 
		amountTextBox.Font = new Font("Segoe UI", 12.75F);
		amountTextBox.Location = new Point(485, 101);
		amountTextBox.MaxLength = 10;
		amountTextBox.Name = "amountTextBox";
		amountTextBox.PlaceholderText = "Amount";
		amountTextBox.Size = new Size(151, 30);
		amountTextBox.TabIndex = 9;
		amountTextBox.Text = "0";
		amountTextBox.TextAlign = HorizontalAlignment.Right;
		amountTextBox.Click += textBox_Click;
		amountTextBox.KeyPress += textBox_KeyPress;
		// 
		// amountDataGridView
		// 
		amountDataGridView.AllowUserToAddRows = false;
		amountDataGridView.AllowUserToOrderColumns = true;
		amountDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		amountDataGridView.Columns.AddRange(new DataGridViewColumn[] { Id, PaymentMode, Amount });
		amountDataGridView.Location = new Point(376, 178);
		amountDataGridView.Name = "amountDataGridView";
		amountDataGridView.ReadOnly = true;
		amountDataGridView.Size = new Size(260, 171);
		amountDataGridView.TabIndex = 31;
		amountDataGridView.CellClick += amountDataGridView_CellClick;
		// 
		// Id
		// 
		Id.HeaderText = "Id";
		Id.Name = "Id";
		Id.ReadOnly = true;
		Id.Visible = false;
		// 
		// PaymentMode
		// 
		PaymentMode.HeaderText = "Mode";
		PaymentMode.Name = "PaymentMode";
		PaymentMode.ReadOnly = true;
		// 
		// Amount
		// 
		Amount.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
		dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle1.NullValue = null;
		Amount.DefaultCellStyle = dataGridViewCellStyle1;
		Amount.HeaderText = "Amount";
		Amount.Name = "Amount";
		Amount.ReadOnly = true;
		// 
		// bookingLabel
		// 
		bookingLabel.AutoSize = true;
		bookingLabel.Font = new Font("Segoe UI", 12.75F);
		bookingLabel.Location = new Point(340, 15);
		bookingLabel.Name = "bookingLabel";
		bookingLabel.Size = new Size(139, 23);
		bookingLabel.TabIndex = 32;
		bookingLabel.Text = "Booking Amount";
		// 
		// bookingTextBox
		// 
		bookingTextBox.Font = new Font("Segoe UI", 12.75F);
		bookingTextBox.Location = new Point(485, 12);
		bookingTextBox.MaxLength = 10;
		bookingTextBox.Name = "bookingTextBox";
		bookingTextBox.PlaceholderText = "Amount";
		bookingTextBox.Size = new Size(151, 30);
		bookingTextBox.TabIndex = 7;
		bookingTextBox.Text = "0";
		bookingTextBox.TextAlign = HorizontalAlignment.Right;
		bookingTextBox.Click += textBox_Click;
		bookingTextBox.KeyPress += textBox_KeyPress;
		// 
		// label1
		// 
		label1.AutoSize = true;
		label1.Font = new Font("Segoe UI", 10F);
		label1.Location = new Point(24, 258);
		label1.Name = "label1";
		label1.Size = new Size(61, 19);
		label1.TabIndex = 34;
		label1.Text = "Remarks";
		// 
		// totalTextBox
		// 
		totalTextBox.Font = new Font("Segoe UI", 12.75F);
		totalTextBox.Location = new Point(485, 48);
		totalTextBox.MaxLength = 10;
		totalTextBox.Name = "totalTextBox";
		totalTextBox.PlaceholderText = "Amount";
		totalTextBox.ReadOnly = true;
		totalTextBox.Size = new Size(151, 30);
		totalTextBox.TabIndex = 36;
		totalTextBox.Text = "0";
		totalTextBox.TextAlign = HorizontalAlignment.Right;
		// 
		// label2
		// 
		label2.AutoSize = true;
		label2.Font = new Font("Segoe UI", 12.75F);
		label2.Location = new Point(360, 54);
		label2.Name = "label2";
		label2.Size = new Size(113, 23);
		label2.TabIndex = 35;
		label2.Text = "Total Amount";
		// 
		// advanceDateTimePicker
		// 
		advanceDateTimePicker.CalendarFont = new Font("Segoe UI", 13F);
		advanceDateTimePicker.Font = new Font("Segoe UI", 13F);
		advanceDateTimePicker.Format = DateTimePickerFormat.Short;
		advanceDateTimePicker.Location = new Point(140, 195);
		advanceDateTimePicker.Name = "advanceDateTimePicker";
		advanceDateTimePicker.Size = new Size(127, 31);
		advanceDateTimePicker.TabIndex = 5;
		advanceDateTimePicker.ValueChanged += advanceDateTimePicker_ValueChanged;
		// 
		// label3
		// 
		label3.AutoSize = true;
		label3.Font = new Font("Segoe UI", 12.75F);
		label3.Location = new Point(12, 201);
		label3.Name = "label3";
		label3.Size = new Size(113, 23);
		label3.TabIndex = 38;
		label3.Text = "Booking Date";
		// 
		// locationComboBox
		// 
		locationComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
		locationComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
		locationComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
		locationComboBox.Font = new Font("Segoe UI", 15F);
		locationComboBox.FormattingEnabled = true;
		locationComboBox.Location = new Point(24, 12);
		locationComboBox.Name = "locationComboBox";
		locationComboBox.Size = new Size(271, 36);
		locationComboBox.TabIndex = 1;
		locationComboBox.SelectedIndexChanged += locationComboBox_SelectedIndexChanged;
		// 
		// addButton
		// 
		addButton.Font = new Font("Segoe UI", 12.75F);
		addButton.Location = new Point(460, 137);
		addButton.Name = "addButton";
		addButton.Size = new Size(106, 35);
		addButton.TabIndex = 10;
		addButton.Text = "ADD";
		addButton.UseVisualStyleBackColor = true;
		addButton.Click += addButton_Click;
		// 
		// versionLabel
		// 
		versionLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
		versionLabel.AutoSize = true;
		versionLabel.BackColor = Color.White;
		versionLabel.Location = new Point(3, 364);
		versionLabel.Name = "versionLabel";
		versionLabel.Size = new Size(84, 15);
		versionLabel.TabIndex = 46;
		versionLabel.Text = "Version: 0.0.0.0";
		// 
		// brandingLabel
		// 
		brandingLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
		brandingLabel.AutoSize = true;
		brandingLabel.BackColor = Color.White;
		brandingLabel.Location = new Point(574, 364);
		brandingLabel.Name = "brandingLabel";
		brandingLabel.Size = new Size(76, 15);
		brandingLabel.TabIndex = 45;
		brandingLabel.Text = "© AADISOFT";
		// 
		// richTextBoxFooter
		// 
		richTextBoxFooter.Dock = DockStyle.Bottom;
		richTextBoxFooter.Location = new Point(0, 358);
		richTextBoxFooter.Name = "richTextBoxFooter";
		richTextBoxFooter.Size = new Size(652, 26);
		richTextBoxFooter.TabIndex = 44;
		richTextBoxFooter.Text = "";
		// 
		// AdvanceForm
		// 
		AcceptButton = saveButton;
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		ClientSize = new Size(652, 384);
		Controls.Add(versionLabel);
		Controls.Add(brandingLabel);
		Controls.Add(richTextBoxFooter);
		Controls.Add(addButton);
		Controls.Add(locationComboBox);
		Controls.Add(label3);
		Controls.Add(advanceDateTimePicker);
		Controls.Add(totalTextBox);
		Controls.Add(label2);
		Controls.Add(label1);
		Controls.Add(bookingTextBox);
		Controls.Add(bookingLabel);
		Controls.Add(amountDataGridView);
		Controls.Add(amountTextBox);
		Controls.Add(paymentComboBox);
		Controls.Add(saveButton);
		Controls.Add(approvedByLabel);
		Controls.Add(approvedByTextBox);
		Controls.Add(numberTextBox);
		Controls.Add(loyaltyCheckBox);
		Controls.Add(numberLabel);
		Controls.Add(nameLabel);
		Controls.Add(nameTextBox);
		Name = "AdvanceForm";
		StartPosition = FormStartPosition.CenterScreen;
		Text = "Advance Entry";
		Load += AdvanceForm_Load;
		((System.ComponentModel.ISupportInitialize)amountDataGridView).EndInit();
		ResumeLayout(false);
		PerformLayout();
	}

	#endregion

	private TextBox numberTextBox;
	private CheckBox loyaltyCheckBox;
	private Label numberLabel;
	private Label nameLabel;
	private TextBox nameTextBox;
	private Label approvedByLabel;
	private TextBox approvedByTextBox;
	private Button saveButton;
	private ComboBox paymentComboBox;
	private TextBox amountTextBox;
	private DataGridView amountDataGridView;
	private Label bookingLabel;
	private TextBox bookingTextBox;
	private Label label1;
	private TextBox totalTextBox;
	private Label label2;
	private DateTimePicker advanceDateTimePicker;
	private Label label3;
	private ComboBox locationComboBox;
	private Button addButton;
	private DataGridViewTextBoxColumn Id;
	private DataGridViewTextBoxColumn PaymentMode;
	private DataGridViewTextBoxColumn Amount;
	private Label versionLabel;
	private Label brandingLabel;
	private RichTextBox richTextBoxFooter;
}