namespace PubEntry;

partial class AdvanceEntryForm
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
		advanceDateTimePicker = new DateTimePicker();
		saveButton = new Button();
		loyaltyCheckBox = new CheckBox();
		numberLabel = new Label();
		numberTextBox = new TextBox();
		nameLabel = new Label();
		nameTextBox = new TextBox();
		bookingAmountLabel = new Label();
		bookingAmountTextBox = new TextBox();
		locationComboBox = new ComboBox();
		amountDataGridView = new DataGridView();
		PaymentId = new DataGridViewTextBoxColumn();
		PaymentMode = new DataGridViewTextBoxColumn();
		Amount = new DataGridViewTextBoxColumn();
		paymentModeComboBox = new ComboBox();
		amountTextBox = new TextBox();
		addButton = new Button();
		label2 = new Label();
		((System.ComponentModel.ISupportInitialize)amountDataGridView).BeginInit();
		SuspendLayout();
		// 
		// advanceDateTimePicker
		// 
		advanceDateTimePicker.CalendarFont = new Font("Segoe UI", 15F);
		advanceDateTimePicker.Font = new Font("Segoe UI", 15F);
		advanceDateTimePicker.Format = DateTimePickerFormat.Short;
		advanceDateTimePicker.Location = new Point(132, 265);
		advanceDateTimePicker.Name = "advanceDateTimePicker";
		advanceDateTimePicker.Size = new Size(136, 34);
		advanceDateTimePicker.TabIndex = 6;
		// 
		// saveButton
		// 
		saveButton.Font = new Font("Segoe UI", 15F);
		saveButton.Location = new Point(69, 342);
		saveButton.Name = "saveButton";
		saveButton.Size = new Size(287, 61);
		saveButton.TabIndex = 7;
		saveButton.Text = "SAVE";
		saveButton.UseVisualStyleBackColor = true;
		saveButton.Click += saveButton_Click;
		// 
		// loyaltyCheckBox
		// 
		loyaltyCheckBox.AutoSize = true;
		loyaltyCheckBox.Font = new Font("Segoe UI", 15F);
		loyaltyCheckBox.Location = new Point(152, 174);
		loyaltyCheckBox.Name = "loyaltyCheckBox";
		loyaltyCheckBox.Size = new Size(94, 32);
		loyaltyCheckBox.TabIndex = 4;
		loyaltyCheckBox.Text = "Loyalty";
		loyaltyCheckBox.UseVisualStyleBackColor = true;
		// 
		// numberLabel
		// 
		numberLabel.AutoSize = true;
		numberLabel.Font = new Font("Segoe UI", 15F);
		numberLabel.Location = new Point(19, 79);
		numberLabel.Name = "numberLabel";
		numberLabel.Size = new Size(151, 28);
		numberLabel.TabIndex = 17;
		numberLabel.Text = "Mobile Number";
		// 
		// numberTextBox
		// 
		numberTextBox.Font = new Font("Segoe UI", 15F);
		numberTextBox.Location = new Point(176, 79);
		numberTextBox.Name = "numberTextBox";
		numberTextBox.PlaceholderText = "Mobile Number";
		numberTextBox.Size = new Size(236, 34);
		numberTextBox.TabIndex = 2;
		numberTextBox.TextChanged += numberTextBox_TextChanged;
		numberTextBox.KeyPress += textBox_KeyPress;
		// 
		// nameLabel
		// 
		nameLabel.AutoSize = true;
		nameLabel.Font = new Font("Segoe UI", 15F);
		nameLabel.Location = new Point(20, 120);
		nameLabel.Name = "nameLabel";
		nameLabel.Size = new Size(64, 28);
		nameLabel.TabIndex = 14;
		nameLabel.Text = "Name";
		// 
		// nameTextBox
		// 
		nameTextBox.Font = new Font("Segoe UI", 15F);
		nameTextBox.Location = new Point(176, 119);
		nameTextBox.Name = "nameTextBox";
		nameTextBox.PlaceholderText = "Name";
		nameTextBox.Size = new Size(236, 34);
		nameTextBox.TabIndex = 3;
		// 
		// bookingAmountLabel
		// 
		bookingAmountLabel.AutoSize = true;
		bookingAmountLabel.Font = new Font("Segoe UI", 15F);
		bookingAmountLabel.Location = new Point(438, 29);
		bookingAmountLabel.Name = "bookingAmountLabel";
		bookingAmountLabel.Size = new Size(161, 28);
		bookingAmountLabel.TabIndex = 18;
		bookingAmountLabel.Text = "Booking Amount";
		// 
		// bookingAmountTextBox
		// 
		bookingAmountTextBox.Font = new Font("Segoe UI", 15F);
		bookingAmountTextBox.Location = new Point(605, 26);
		bookingAmountTextBox.Name = "bookingAmountTextBox";
		bookingAmountTextBox.PlaceholderText = "Amount";
		bookingAmountTextBox.RightToLeft = RightToLeft.Yes;
		bookingAmountTextBox.Size = new Size(195, 34);
		bookingAmountTextBox.TabIndex = 5;
		bookingAmountTextBox.Text = "0";
		bookingAmountTextBox.KeyPress += textBox_KeyPress;
		// 
		// locationComboBox
		// 
		locationComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
		locationComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
		locationComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
		locationComboBox.Font = new Font("Segoe UI", 15F);
		locationComboBox.FormattingEnabled = true;
		locationComboBox.Location = new Point(115, 12);
		locationComboBox.Name = "locationComboBox";
		locationComboBox.Size = new Size(271, 36);
		locationComboBox.TabIndex = 1;
		// 
		// amountDataGridView
		// 
		amountDataGridView.AllowUserToAddRows = false;
		amountDataGridView.AllowUserToOrderColumns = true;
		amountDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		amountDataGridView.Columns.AddRange(new DataGridViewColumn[] { PaymentId, PaymentMode, Amount });
		amountDataGridView.Location = new Point(438, 239);
		amountDataGridView.Name = "amountDataGridView";
		amountDataGridView.ReadOnly = true;
		amountDataGridView.RowTemplate.Height = 40;
		amountDataGridView.Size = new Size(362, 164);
		amountDataGridView.TabIndex = 19;
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
		// paymentModeComboBox
		// 
		paymentModeComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
		paymentModeComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
		paymentModeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
		paymentModeComboBox.Font = new Font("Segoe UI", 15F);
		paymentModeComboBox.FormattingEnabled = true;
		paymentModeComboBox.Location = new Point(438, 127);
		paymentModeComboBox.Name = "paymentModeComboBox";
		paymentModeComboBox.Size = new Size(161, 36);
		paymentModeComboBox.TabIndex = 20;
		// 
		// amountTextBox
		// 
		amountTextBox.Font = new Font("Segoe UI", 15F);
		amountTextBox.Location = new Point(605, 127);
		amountTextBox.Name = "amountTextBox";
		amountTextBox.PlaceholderText = "Amount";
		amountTextBox.RightToLeft = RightToLeft.Yes;
		amountTextBox.Size = new Size(195, 34);
		amountTextBox.TabIndex = 21;
		amountTextBox.Text = "0";
		// 
		// addButton
		// 
		addButton.Font = new Font("Segoe UI", 15F);
		addButton.Location = new Point(535, 174);
		addButton.Name = "addButton";
		addButton.Size = new Size(176, 50);
		addButton.TabIndex = 22;
		addButton.Text = "ADD";
		addButton.UseVisualStyleBackColor = true;
		addButton.Click += addButton_Click;
		// 
		// label2
		// 
		label2.AutoSize = true;
		label2.Font = new Font("Segoe UI", 15F);
		label2.Location = new Point(535, 96);
		label2.Name = "label2";
		label2.Size = new Size(165, 28);
		label2.TabIndex = 37;
		label2.Text = "Amount Received";
		// 
		// AdvanceEntryForm
		// 
		AcceptButton = saveButton;
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		ClientSize = new Size(826, 417);
		Controls.Add(label2);
		Controls.Add(addButton);
		Controls.Add(amountTextBox);
		Controls.Add(paymentModeComboBox);
		Controls.Add(amountDataGridView);
		Controls.Add(locationComboBox);
		Controls.Add(bookingAmountLabel);
		Controls.Add(bookingAmountTextBox);
		Controls.Add(loyaltyCheckBox);
		Controls.Add(numberLabel);
		Controls.Add(numberTextBox);
		Controls.Add(nameLabel);
		Controls.Add(nameTextBox);
		Controls.Add(saveButton);
		Controls.Add(advanceDateTimePicker);
		Name = "AdvanceEntryForm";
		StartPosition = FormStartPosition.CenterScreen;
		Text = "Advance Form";
		Load += AdvanceEntryForm_Load;
		((System.ComponentModel.ISupportInitialize)amountDataGridView).EndInit();
		ResumeLayout(false);
		PerformLayout();
	}

	#endregion

	private DateTimePicker advanceDateTimePicker;
	private Button saveButton;
	private CheckBox loyaltyCheckBox;
	private Label numberLabel;
	private TextBox numberTextBox;
	private Label nameLabel;
	private TextBox nameTextBox;
	private Label bookingAmountLabel;
	private TextBox bookingAmountTextBox;
	private ComboBox locationComboBox;
	private DataGridView amountDataGridView;
	private ComboBox paymentModeComboBox;
	private TextBox amountTextBox;
	private Button addButton;
	private DataGridViewTextBoxColumn PaymentId;
	private DataGridViewTextBoxColumn PaymentMode;
	private DataGridViewTextBoxColumn Amount;
	private Label label2;
}