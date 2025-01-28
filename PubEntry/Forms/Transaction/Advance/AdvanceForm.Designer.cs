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
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdvanceForm));
		numberTextBox = new System.Windows.Forms.TextBox();
		loyaltyCheckBox = new System.Windows.Forms.CheckBox();
		numberLabel = new System.Windows.Forms.Label();
		nameLabel = new System.Windows.Forms.Label();
		nameTextBox = new System.Windows.Forms.TextBox();
		approvedByLabel = new System.Windows.Forms.Label();
		approvedByTextBox = new System.Windows.Forms.TextBox();
		saveButton = new System.Windows.Forms.Button();
		paymentComboBox = new System.Windows.Forms.ComboBox();
		amountTextBox = new System.Windows.Forms.TextBox();
		amountDataGridView = new System.Windows.Forms.DataGridView();
		Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
		PaymentMode = new System.Windows.Forms.DataGridViewTextBoxColumn();
		Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
		bookingLabel = new System.Windows.Forms.Label();
		bookingTextBox = new System.Windows.Forms.TextBox();
		label1 = new System.Windows.Forms.Label();
		totalTextBox = new System.Windows.Forms.TextBox();
		label2 = new System.Windows.Forms.Label();
		advanceDateTimePicker = new System.Windows.Forms.DateTimePicker();
		label3 = new System.Windows.Forms.Label();
		locationComboBox = new System.Windows.Forms.ComboBox();
		addButton = new System.Windows.Forms.Button();
		brandingLabel = new System.Windows.Forms.Label();
		richTextBoxFooter = new System.Windows.Forms.RichTextBox();
		((System.ComponentModel.ISupportInitialize)amountDataGridView).BeginInit();
		SuspendLayout();
		// 
		// numberTextBox
		// 
		numberTextBox.Font = new System.Drawing.Font("Segoe UI", 12.75F);
		numberTextBox.Location = new System.Drawing.Point(80, 63);
		numberTextBox.MaxLength = 15;
		numberTextBox.Name = "numberTextBox";
		numberTextBox.PlaceholderText = "Number";
		numberTextBox.Size = new System.Drawing.Size(234, 30);
		numberTextBox.TabIndex = 2;
		numberTextBox.Click += textBox_Click;
		numberTextBox.TextChanged += numberTextBox_TextChanged;
		numberTextBox.KeyPress += textBox_KeyPress;
		// 
		// loyaltyCheckBox
		// 
		loyaltyCheckBox.AutoSize = true;
		loyaltyCheckBox.Font = new System.Drawing.Font("Segoe UI", 12.75F);
		loyaltyCheckBox.Location = new System.Drawing.Point(70, 149);
		loyaltyCheckBox.Name = "loyaltyCheckBox";
		loyaltyCheckBox.Size = new System.Drawing.Size(82, 27);
		loyaltyCheckBox.TabIndex = 4;
		loyaltyCheckBox.Text = "Loyalty";
		loyaltyCheckBox.UseVisualStyleBackColor = true;
		// 
		// numberLabel
		// 
		numberLabel.AutoSize = true;
		numberLabel.Font = new System.Drawing.Font("Segoe UI", 12.75F);
		numberLabel.Location = new System.Drawing.Point(12, 69);
		numberLabel.Name = "numberLabel";
		numberLabel.Size = new System.Drawing.Size(62, 23);
		numberLabel.TabIndex = 8;
		numberLabel.Text = "Mobile";
		// 
		// nameLabel
		// 
		nameLabel.AutoSize = true;
		nameLabel.Font = new System.Drawing.Font("Segoe UI", 12.75F);
		nameLabel.Location = new System.Drawing.Point(12, 112);
		nameLabel.Name = "nameLabel";
		nameLabel.Size = new System.Drawing.Size(56, 23);
		nameLabel.TabIndex = 5;
		nameLabel.Text = "Name";
		// 
		// nameTextBox
		// 
		nameTextBox.Font = new System.Drawing.Font("Segoe UI", 12.75F);
		nameTextBox.Location = new System.Drawing.Point(80, 106);
		nameTextBox.MaxLength = 250;
		nameTextBox.Name = "nameTextBox";
		nameTextBox.PlaceholderText = "Name";
		nameTextBox.Size = new System.Drawing.Size(234, 30);
		nameTextBox.TabIndex = 3;
		// 
		// approvedByLabel
		// 
		approvedByLabel.AutoSize = true;
		approvedByLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
		approvedByLabel.Location = new System.Drawing.Point(12, 243);
		approvedByLabel.Name = "approvedByLabel";
		approvedByLabel.Size = new System.Drawing.Size(88, 19);
		approvedByLabel.TabIndex = 27;
		approvedByLabel.Text = "Approved By";
		// 
		// approvedByTextBox
		// 
		approvedByTextBox.Font = new System.Drawing.Font("Segoe UI", 12.75F);
		approvedByTextBox.Location = new System.Drawing.Point(106, 247);
		approvedByTextBox.MaxLength = 50;
		approvedByTextBox.Name = "approvedByTextBox";
		approvedByTextBox.PlaceholderText = "Approved By / Remakrs";
		approvedByTextBox.Size = new System.Drawing.Size(208, 30);
		approvedByTextBox.TabIndex = 6;
		// 
		// saveButton
		// 
		saveButton.Font = new System.Drawing.Font("Segoe UI", 12.75F);
		saveButton.Location = new System.Drawing.Point(106, 294);
		saveButton.Name = "saveButton";
		saveButton.Size = new System.Drawing.Size(135, 44);
		saveButton.TabIndex = 11;
		saveButton.Text = "SAVE";
		saveButton.UseVisualStyleBackColor = true;
		saveButton.Click += saveButton_Click;
		// 
		// paymentComboBox
		// 
		paymentComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		paymentComboBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
		paymentComboBox.Font = new System.Drawing.Font("Segoe UI", 13F);
		paymentComboBox.FormattingEnabled = true;
		paymentComboBox.Location = new System.Drawing.Point(376, 100);
		paymentComboBox.Name = "paymentComboBox";
		paymentComboBox.Size = new System.Drawing.Size(103, 31);
		paymentComboBox.TabIndex = 8;
		// 
		// amountTextBox
		// 
		amountTextBox.Font = new System.Drawing.Font("Segoe UI", 12.75F);
		amountTextBox.Location = new System.Drawing.Point(485, 101);
		amountTextBox.MaxLength = 10;
		amountTextBox.Name = "amountTextBox";
		amountTextBox.PlaceholderText = "Amount";
		amountTextBox.Size = new System.Drawing.Size(151, 30);
		amountTextBox.TabIndex = 9;
		amountTextBox.Text = "0";
		amountTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		amountTextBox.Click += textBox_Click;
		amountTextBox.KeyPress += textBox_KeyPress;
		// 
		// amountDataGridView
		// 
		amountDataGridView.AllowUserToAddRows = false;
		amountDataGridView.AllowUserToOrderColumns = true;
		amountDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		amountDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { Id, PaymentMode, Amount });
		amountDataGridView.Location = new System.Drawing.Point(376, 178);
		amountDataGridView.Name = "amountDataGridView";
		amountDataGridView.ReadOnly = true;
		amountDataGridView.Size = new System.Drawing.Size(260, 171);
		amountDataGridView.TabIndex = 31;
		amountDataGridView.CellClick += amountDataGridView_CellClick;
		// 
		// Id
		// 
		Id.HeaderText = "Id";
		Id.Name = "Id";
		Id.ReadOnly = true;
		Id.Visible = false;
		Id.Width = 100;
		// 
		// PaymentMode
		// 
		PaymentMode.HeaderText = "Mode";
		PaymentMode.Name = "PaymentMode";
		PaymentMode.ReadOnly = true;
		PaymentMode.Width = 100;
		// 
		// Amount
		// 
		Amount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
		dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle1.NullValue = null;
		Amount.DefaultCellStyle = dataGridViewCellStyle1;
		Amount.HeaderText = "Amount";
		Amount.Name = "Amount";
		Amount.ReadOnly = true;
		Amount.Width = 117;
		// 
		// bookingLabel
		// 
		bookingLabel.AutoSize = true;
		bookingLabel.Font = new System.Drawing.Font("Segoe UI", 12.75F);
		bookingLabel.Location = new System.Drawing.Point(340, 15);
		bookingLabel.Name = "bookingLabel";
		bookingLabel.Size = new System.Drawing.Size(139, 23);
		bookingLabel.TabIndex = 32;
		bookingLabel.Text = "Booking Amount";
		// 
		// bookingTextBox
		// 
		bookingTextBox.Font = new System.Drawing.Font("Segoe UI", 12.75F);
		bookingTextBox.Location = new System.Drawing.Point(485, 12);
		bookingTextBox.MaxLength = 10;
		bookingTextBox.Name = "bookingTextBox";
		bookingTextBox.PlaceholderText = "Amount";
		bookingTextBox.Size = new System.Drawing.Size(151, 30);
		bookingTextBox.TabIndex = 7;
		bookingTextBox.Text = "0";
		bookingTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		bookingTextBox.Click += textBox_Click;
		bookingTextBox.KeyPress += textBox_KeyPress;
		// 
		// label1
		// 
		label1.AutoSize = true;
		label1.Font = new System.Drawing.Font("Segoe UI", 10F);
		label1.Location = new System.Drawing.Point(24, 258);
		label1.Name = "label1";
		label1.Size = new System.Drawing.Size(61, 19);
		label1.TabIndex = 34;
		label1.Text = "Remarks";
		// 
		// totalTextBox
		// 
		totalTextBox.Font = new System.Drawing.Font("Segoe UI", 12.75F);
		totalTextBox.Location = new System.Drawing.Point(485, 48);
		totalTextBox.MaxLength = 10;
		totalTextBox.Name = "totalTextBox";
		totalTextBox.PlaceholderText = "Amount";
		totalTextBox.ReadOnly = true;
		totalTextBox.Size = new System.Drawing.Size(151, 30);
		totalTextBox.TabIndex = 36;
		totalTextBox.Text = "0";
		totalTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		// 
		// label2
		// 
		label2.AutoSize = true;
		label2.Font = new System.Drawing.Font("Segoe UI", 12.75F);
		label2.Location = new System.Drawing.Point(360, 54);
		label2.Name = "label2";
		label2.Size = new System.Drawing.Size(113, 23);
		label2.TabIndex = 35;
		label2.Text = "Total Amount";
		// 
		// advanceDateTimePicker
		// 
		advanceDateTimePicker.CalendarFont = new System.Drawing.Font("Segoe UI", 13F);
		advanceDateTimePicker.Font = new System.Drawing.Font("Segoe UI", 13F);
		advanceDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		advanceDateTimePicker.Location = new System.Drawing.Point(140, 195);
		advanceDateTimePicker.Name = "advanceDateTimePicker";
		advanceDateTimePicker.Size = new System.Drawing.Size(127, 31);
		advanceDateTimePicker.TabIndex = 5;
		advanceDateTimePicker.ValueChanged += advanceDateTimePicker_ValueChanged;
		// 
		// label3
		// 
		label3.AutoSize = true;
		label3.Font = new System.Drawing.Font("Segoe UI", 12.75F);
		label3.Location = new System.Drawing.Point(12, 201);
		label3.Name = "label3";
		label3.Size = new System.Drawing.Size(113, 23);
		label3.TabIndex = 38;
		label3.Text = "Booking Date";
		// 
		// locationComboBox
		// 
		locationComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		locationComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		locationComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		locationComboBox.Font = new System.Drawing.Font("Segoe UI", 15F);
		locationComboBox.FormattingEnabled = true;
		locationComboBox.Location = new System.Drawing.Point(24, 12);
		locationComboBox.Name = "locationComboBox";
		locationComboBox.Size = new System.Drawing.Size(271, 36);
		locationComboBox.TabIndex = 1;
		locationComboBox.SelectedIndexChanged += locationComboBox_SelectedIndexChanged;
		// 
		// addButton
		// 
		addButton.Font = new System.Drawing.Font("Segoe UI", 12.75F);
		addButton.Location = new System.Drawing.Point(460, 137);
		addButton.Name = "addButton";
		addButton.Size = new System.Drawing.Size(106, 35);
		addButton.TabIndex = 10;
		addButton.Text = "ADD";
		addButton.UseVisualStyleBackColor = true;
		addButton.Click += addButton_Click;
		// 
		// brandingLabel
		// 
		brandingLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
		brandingLabel.AutoSize = true;
		brandingLabel.BackColor = System.Drawing.Color.White;
		brandingLabel.Location = new System.Drawing.Point(574, 364);
		brandingLabel.Name = "brandingLabel";
		brandingLabel.Size = new System.Drawing.Size(76, 15);
		brandingLabel.TabIndex = 45;
		brandingLabel.Text = "© AADISOFT";
		// 
		// richTextBoxFooter
		// 
		richTextBoxFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
		richTextBoxFooter.Location = new System.Drawing.Point(0, 358);
		richTextBoxFooter.Name = "richTextBoxFooter";
		richTextBoxFooter.Size = new System.Drawing.Size(652, 26);
		richTextBoxFooter.TabIndex = 44;
		richTextBoxFooter.Text = "";
		// 
		// AdvanceForm
		// 
		AcceptButton = saveButton;
		AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
		AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		ClientSize = new System.Drawing.Size(652, 384);
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
		Icon = ((System.Drawing.Icon)resources.GetObject("$this.Icon"));
		StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		Text = "Advance Entry";
		Load += AdvanceForm_Load;
		((System.ComponentModel.ISupportInitialize)amountDataGridView).EndInit();
		ResumeLayout(false);
		PerformLayout();
	}

	#endregion

	private System.Windows.Forms.TextBox numberTextBox;
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
	private Label brandingLabel;
	private RichTextBox richTextBoxFooter;
}