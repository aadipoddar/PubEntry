namespace PubEntry.Forms.Reports;

partial class DetailDataForm
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DetailDataForm));
		transactionDataGridView = new DataGridView();
		locationNameLabel = new Label();
		dateLabel = new Label();
		label1 = new Label();
		label2 = new Label();
		label3 = new Label();
		label10 = new Label();
		label11 = new Label();
		label12 = new Label();
		label15 = new Label();
		label16 = new Label();
		printButton = new Button();
		excelButton = new Button();
		refreshButton = new Button();
		label5 = new Label();
		tableLayoutPanel1 = new TableLayoutPanel();
		peopleTextBox = new TextBox();
		amexTextBox = new TextBox();
		upiTextBox = new TextBox();
		loyaltyTextBox = new TextBox();
		cardTextBox = new TextBox();
		femaleTextBox = new TextBox();
		cashTextBox = new TextBox();
		maleTextBox = new TextBox();
		amountTextBox = new TextBox();
		label4 = new Label();
		label40 = new Label();
		advanceTextBox = new TextBox();
		label7 = new Label();
		bookingTextBox = new TextBox();
		label8 = new Label();
		redeemedAdvanceTextBox = new TextBox();
		notRedeemedAdvanceTextBox = new TextBox();
		label50 = new Label();
		label53 = new Label();
		redeemedBookingTextBox = new TextBox();
		notRedeemedBookingTextBox = new TextBox();
		advanceDataGridView = new DataGridView();
		((System.ComponentModel.ISupportInitialize)transactionDataGridView).BeginInit();
		tableLayoutPanel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)advanceDataGridView).BeginInit();
		SuspendLayout();
		// 
		// transactionDataGridView
		// 
		transactionDataGridView.AllowUserToAddRows = false;
		transactionDataGridView.AllowUserToDeleteRows = false;
		transactionDataGridView.AllowUserToOrderColumns = true;
		transactionDataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
		transactionDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
		transactionDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		transactionDataGridView.Location = new Point(12, 40);
		transactionDataGridView.Name = "transactionDataGridView";
		transactionDataGridView.ReadOnly = true;
		transactionDataGridView.Size = new Size(1365, 288);
		transactionDataGridView.TabIndex = 0;
		// 
		// locationNameLabel
		// 
		locationNameLabel.Anchor = AnchorStyles.Top;
		locationNameLabel.AutoSize = true;
		locationNameLabel.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
		locationNameLabel.Location = new Point(623, 9);
		locationNameLabel.Name = "locationNameLabel";
		locationNameLabel.Size = new Size(93, 28);
		locationNameLabel.TabIndex = 1;
		locationNameLabel.Text = "Location";
		locationNameLabel.TextAlign = ContentAlignment.MiddleCenter;
		// 
		// dateLabel
		// 
		dateLabel.AutoSize = true;
		dateLabel.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
		dateLabel.Location = new Point(16, 9);
		dateLabel.Name = "dateLabel";
		dateLabel.Size = new Size(330, 28);
		dateLabel.TabIndex = 2;
		dateLabel.Text = "01/12/24 01:00 - 01/12/24 01:00";
		dateLabel.TextAlign = ContentAlignment.MiddleCenter;
		// 
		// label1
		// 
		label1.AutoSize = true;
		label1.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
		label1.Location = new Point(3, 0);
		label1.Name = "label1";
		label1.Size = new Size(133, 28);
		label1.TabIndex = 3;
		label1.Text = "Total People:";
		// 
		// label2
		// 
		label2.AutoSize = true;
		label2.Font = new Font("Segoe UI", 15F);
		label2.Location = new Point(3, 36);
		label2.Name = "label2";
		label2.Size = new Size(59, 28);
		label2.TabIndex = 4;
		label2.Text = "Male:";
		// 
		// label3
		// 
		label3.AutoSize = true;
		label3.Font = new Font("Segoe UI", 15F);
		label3.Location = new Point(3, 72);
		label3.Name = "label3";
		label3.Size = new Size(78, 28);
		label3.TabIndex = 5;
		label3.Text = "Female:";
		// 
		// label10
		// 
		label10.AutoSize = true;
		label10.Font = new Font("Segoe UI", 15F);
		label10.Location = new Point(1051, 72);
		label10.Name = "label10";
		label10.Size = new Size(57, 28);
		label10.TabIndex = 11;
		label10.Text = "Card:";
		// 
		// label11
		// 
		label11.AutoSize = true;
		label11.Font = new Font("Segoe UI", 15F);
		label11.Location = new Point(1051, 36);
		label11.Name = "label11";
		label11.Size = new Size(57, 28);
		label11.TabIndex = 10;
		label11.Text = "Cash:";
		// 
		// label12
		// 
		label12.AutoSize = true;
		label12.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
		label12.Location = new Point(1051, 0);
		label12.Name = "label12";
		label12.Size = new Size(146, 28);
		label12.TabIndex = 9;
		label12.Text = "Total Amount:";
		// 
		// label15
		// 
		label15.AutoSize = true;
		label15.Font = new Font("Segoe UI", 15F);
		label15.Location = new Point(1051, 149);
		label15.Name = "label15";
		label15.Size = new Size(65, 28);
		label15.TabIndex = 16;
		label15.Text = "Amex:";
		// 
		// label16
		// 
		label16.AutoSize = true;
		label16.Font = new Font("Segoe UI", 15F);
		label16.Location = new Point(1051, 112);
		label16.Name = "label16";
		label16.Size = new Size(46, 28);
		label16.TabIndex = 15;
		label16.Text = "UPI:";
		// 
		// printButton
		// 
		printButton.Anchor = AnchorStyles.Bottom;
		printButton.Font = new Font("Segoe UI", 15F);
		printButton.Location = new Point(509, 153);
		printButton.Name = "printButton";
		printButton.Size = new Size(118, 35);
		printButton.TabIndex = 19;
		printButton.Text = "PRINT";
		printButton.UseVisualStyleBackColor = true;
		printButton.Click += printButton_Click;
		// 
		// excelButton
		// 
		excelButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
		excelButton.Font = new Font("Segoe UI", 15F);
		excelButton.Location = new Point(341, 153);
		excelButton.Name = "excelButton";
		excelButton.Size = new Size(118, 35);
		excelButton.TabIndex = 20;
		excelButton.Text = "EXCEL";
		excelButton.UseVisualStyleBackColor = true;
		excelButton.Click += excelButton_Click;
		// 
		// refreshButton
		// 
		refreshButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
		refreshButton.Font = new Font("Segoe UI", 15F);
		refreshButton.Location = new Point(722, 153);
		refreshButton.Name = "refreshButton";
		refreshButton.Size = new Size(124, 35);
		refreshButton.TabIndex = 21;
		refreshButton.Text = "REFRESH";
		refreshButton.UseVisualStyleBackColor = true;
		refreshButton.Click += refreshButton_Click;
		// 
		// label5
		// 
		label5.AutoSize = true;
		label5.Font = new Font("Segoe UI", 15F);
		label5.Location = new Point(3, 112);
		label5.Name = "label5";
		label5.Size = new Size(126, 28);
		label5.TabIndex = 22;
		label5.Text = "Total Loyalty:";
		// 
		// tableLayoutPanel1
		// 
		tableLayoutPanel1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		tableLayoutPanel1.ColumnCount = 11;
		tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
		tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
		tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
		tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
		tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
		tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
		tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
		tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
		tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
		tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
		tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
		tableLayoutPanel1.Controls.Add(peopleTextBox, 1, 0);
		tableLayoutPanel1.Controls.Add(amexTextBox, 10, 4);
		tableLayoutPanel1.Controls.Add(upiTextBox, 10, 3);
		tableLayoutPanel1.Controls.Add(loyaltyTextBox, 1, 3);
		tableLayoutPanel1.Controls.Add(cardTextBox, 10, 2);
		tableLayoutPanel1.Controls.Add(femaleTextBox, 1, 2);
		tableLayoutPanel1.Controls.Add(cashTextBox, 10, 1);
		tableLayoutPanel1.Controls.Add(maleTextBox, 1, 1);
		tableLayoutPanel1.Controls.Add(amountTextBox, 10, 0);
		tableLayoutPanel1.Controls.Add(label1, 0, 0);
		tableLayoutPanel1.Controls.Add(label5, 0, 3);
		tableLayoutPanel1.Controls.Add(label2, 0, 1);
		tableLayoutPanel1.Controls.Add(label12, 9, 0);
		tableLayoutPanel1.Controls.Add(label15, 9, 4);
		tableLayoutPanel1.Controls.Add(label3, 0, 2);
		tableLayoutPanel1.Controls.Add(label16, 9, 3);
		tableLayoutPanel1.Controls.Add(label11, 9, 1);
		tableLayoutPanel1.Controls.Add(label10, 9, 2);
		tableLayoutPanel1.Controls.Add(label4, 3, 0);
		tableLayoutPanel1.Controls.Add(label40, 3, 1);
		tableLayoutPanel1.Controls.Add(advanceTextBox, 4, 0);
		tableLayoutPanel1.Controls.Add(label7, 6, 0);
		tableLayoutPanel1.Controls.Add(bookingTextBox, 7, 0);
		tableLayoutPanel1.Controls.Add(label8, 3, 2);
		tableLayoutPanel1.Controls.Add(redeemedAdvanceTextBox, 4, 1);
		tableLayoutPanel1.Controls.Add(notRedeemedAdvanceTextBox, 4, 2);
		tableLayoutPanel1.Controls.Add(label50, 6, 1);
		tableLayoutPanel1.Controls.Add(label53, 6, 2);
		tableLayoutPanel1.Controls.Add(redeemedBookingTextBox, 7, 1);
		tableLayoutPanel1.Controls.Add(notRedeemedBookingTextBox, 7, 2);
		tableLayoutPanel1.Controls.Add(excelButton, 3, 4);
		tableLayoutPanel1.Controls.Add(printButton, 4, 4);
		tableLayoutPanel1.Controls.Add(refreshButton, 6, 4);
		tableLayoutPanel1.Font = new Font("Segoe UI", 9F);
		tableLayoutPanel1.Location = new Point(16, 337);
		tableLayoutPanel1.Name = "tableLayoutPanel1";
		tableLayoutPanel1.RowCount = 5;
		tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
		tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
		tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
		tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 37F));
		tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
		tableLayoutPanel1.Size = new Size(1365, 191);
		tableLayoutPanel1.TabIndex = 23;
		// 
		// peopleTextBox
		// 
		peopleTextBox.Font = new Font("Segoe UI", 15F);
		peopleTextBox.Location = new Point(142, 3);
		peopleTextBox.Name = "peopleTextBox";
		peopleTextBox.ReadOnly = true;
		peopleTextBox.Size = new Size(143, 34);
		peopleTextBox.TabIndex = 34;
		peopleTextBox.Text = "0";
		peopleTextBox.TextAlign = HorizontalAlignment.Right;
		// 
		// amexTextBox
		// 
		amexTextBox.Font = new Font("Segoe UI", 15F);
		amexTextBox.Location = new Point(1203, 152);
		amexTextBox.Name = "amexTextBox";
		amexTextBox.ReadOnly = true;
		amexTextBox.Size = new Size(143, 34);
		amexTextBox.TabIndex = 33;
		amexTextBox.Text = "0";
		amexTextBox.TextAlign = HorizontalAlignment.Right;
		// 
		// upiTextBox
		// 
		upiTextBox.Font = new Font("Segoe UI", 15F);
		upiTextBox.Location = new Point(1203, 115);
		upiTextBox.Name = "upiTextBox";
		upiTextBox.ReadOnly = true;
		upiTextBox.Size = new Size(143, 34);
		upiTextBox.TabIndex = 30;
		upiTextBox.Text = "0";
		upiTextBox.TextAlign = HorizontalAlignment.Right;
		// 
		// loyaltyTextBox
		// 
		loyaltyTextBox.Font = new Font("Segoe UI", 15F);
		loyaltyTextBox.Location = new Point(142, 115);
		loyaltyTextBox.Name = "loyaltyTextBox";
		loyaltyTextBox.ReadOnly = true;
		loyaltyTextBox.Size = new Size(143, 34);
		loyaltyTextBox.TabIndex = 29;
		loyaltyTextBox.Text = "0";
		loyaltyTextBox.TextAlign = HorizontalAlignment.Right;
		// 
		// cardTextBox
		// 
		cardTextBox.Font = new Font("Segoe UI", 15F);
		cardTextBox.Location = new Point(1203, 75);
		cardTextBox.Name = "cardTextBox";
		cardTextBox.ReadOnly = true;
		cardTextBox.Size = new Size(143, 34);
		cardTextBox.TabIndex = 28;
		cardTextBox.Text = "0";
		cardTextBox.TextAlign = HorizontalAlignment.Right;
		// 
		// femaleTextBox
		// 
		femaleTextBox.Font = new Font("Segoe UI", 15F);
		femaleTextBox.Location = new Point(142, 75);
		femaleTextBox.Name = "femaleTextBox";
		femaleTextBox.ReadOnly = true;
		femaleTextBox.Size = new Size(143, 34);
		femaleTextBox.TabIndex = 27;
		femaleTextBox.Text = "0";
		femaleTextBox.TextAlign = HorizontalAlignment.Right;
		// 
		// cashTextBox
		// 
		cashTextBox.Font = new Font("Segoe UI", 15F);
		cashTextBox.Location = new Point(1203, 39);
		cashTextBox.Name = "cashTextBox";
		cashTextBox.ReadOnly = true;
		cashTextBox.Size = new Size(143, 34);
		cashTextBox.TabIndex = 26;
		cashTextBox.Text = "0";
		cashTextBox.TextAlign = HorizontalAlignment.Right;
		// 
		// maleTextBox
		// 
		maleTextBox.Font = new Font("Segoe UI", 15F);
		maleTextBox.Location = new Point(142, 39);
		maleTextBox.Name = "maleTextBox";
		maleTextBox.ReadOnly = true;
		maleTextBox.Size = new Size(143, 34);
		maleTextBox.TabIndex = 25;
		maleTextBox.Text = "0";
		maleTextBox.TextAlign = HorizontalAlignment.Right;
		// 
		// amountTextBox
		// 
		amountTextBox.Font = new Font("Segoe UI", 15F);
		amountTextBox.Location = new Point(1203, 3);
		amountTextBox.Name = "amountTextBox";
		amountTextBox.ReadOnly = true;
		amountTextBox.Size = new Size(143, 34);
		amountTextBox.TabIndex = 24;
		amountTextBox.Text = "0";
		amountTextBox.TextAlign = HorizontalAlignment.Right;
		// 
		// label4
		// 
		label4.AutoSize = true;
		label4.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
		label4.Location = new Point(341, 0);
		label4.Name = "label4";
		label4.Size = new Size(98, 28);
		label4.TabIndex = 35;
		label4.Text = "Advance:";
		// 
		// label40
		// 
		label40.AutoSize = true;
		label40.Font = new Font("Segoe UI", 15F);
		label40.Location = new Point(341, 36);
		label40.Name = "label40";
		label40.Size = new Size(108, 28);
		label40.TabIndex = 36;
		label40.Text = "Redeemed:";
		// 
		// advanceTextBox
		// 
		advanceTextBox.Font = new Font("Segoe UI", 15F);
		advanceTextBox.Location = new Point(497, 3);
		advanceTextBox.Name = "advanceTextBox";
		advanceTextBox.ReadOnly = true;
		advanceTextBox.Size = new Size(143, 34);
		advanceTextBox.TabIndex = 37;
		advanceTextBox.Text = "0";
		advanceTextBox.TextAlign = HorizontalAlignment.Right;
		// 
		// label7
		// 
		label7.AutoSize = true;
		label7.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
		label7.Location = new Point(696, 0);
		label7.Name = "label7";
		label7.Size = new Size(95, 28);
		label7.TabIndex = 39;
		label7.Text = "Booking:";
		// 
		// bookingTextBox
		// 
		bookingTextBox.Font = new Font("Segoe UI", 15F);
		bookingTextBox.Location = new Point(852, 3);
		bookingTextBox.Name = "bookingTextBox";
		bookingTextBox.ReadOnly = true;
		bookingTextBox.Size = new Size(143, 34);
		bookingTextBox.TabIndex = 38;
		bookingTextBox.Text = "0";
		bookingTextBox.TextAlign = HorizontalAlignment.Right;
		// 
		// label8
		// 
		label8.AutoSize = true;
		label8.Font = new Font("Segoe UI", 15F);
		label8.Location = new Point(341, 72);
		label8.Name = "label8";
		label8.Size = new Size(150, 28);
		label8.TabIndex = 40;
		label8.Text = "Not-Redeemed:";
		// 
		// redeemedAdvanceTextBox
		// 
		redeemedAdvanceTextBox.Font = new Font("Segoe UI", 15F);
		redeemedAdvanceTextBox.Location = new Point(497, 39);
		redeemedAdvanceTextBox.Name = "redeemedAdvanceTextBox";
		redeemedAdvanceTextBox.ReadOnly = true;
		redeemedAdvanceTextBox.Size = new Size(143, 34);
		redeemedAdvanceTextBox.TabIndex = 41;
		redeemedAdvanceTextBox.Text = "0";
		redeemedAdvanceTextBox.TextAlign = HorizontalAlignment.Right;
		// 
		// notRedeemedAdvanceTextBox
		// 
		notRedeemedAdvanceTextBox.Font = new Font("Segoe UI", 15F);
		notRedeemedAdvanceTextBox.Location = new Point(497, 75);
		notRedeemedAdvanceTextBox.Name = "notRedeemedAdvanceTextBox";
		notRedeemedAdvanceTextBox.ReadOnly = true;
		notRedeemedAdvanceTextBox.Size = new Size(143, 34);
		notRedeemedAdvanceTextBox.TabIndex = 42;
		notRedeemedAdvanceTextBox.Text = "0";
		notRedeemedAdvanceTextBox.TextAlign = HorizontalAlignment.Right;
		// 
		// label50
		// 
		label50.AutoSize = true;
		label50.Font = new Font("Segoe UI", 15F);
		label50.Location = new Point(696, 36);
		label50.Name = "label50";
		label50.Size = new Size(108, 28);
		label50.TabIndex = 43;
		label50.Text = "Redeemed:";
		// 
		// label53
		// 
		label53.AutoSize = true;
		label53.Font = new Font("Segoe UI", 15F);
		label53.Location = new Point(696, 72);
		label53.Name = "label53";
		label53.Size = new Size(150, 28);
		label53.TabIndex = 44;
		label53.Text = "Not-Redeemed:";
		// 
		// redeemedBookingTextBox
		// 
		redeemedBookingTextBox.Font = new Font("Segoe UI", 15F);
		redeemedBookingTextBox.Location = new Point(852, 39);
		redeemedBookingTextBox.Name = "redeemedBookingTextBox";
		redeemedBookingTextBox.ReadOnly = true;
		redeemedBookingTextBox.Size = new Size(143, 34);
		redeemedBookingTextBox.TabIndex = 45;
		redeemedBookingTextBox.Text = "0";
		redeemedBookingTextBox.TextAlign = HorizontalAlignment.Right;
		// 
		// notRedeemedBookingTextBox
		// 
		notRedeemedBookingTextBox.Font = new Font("Segoe UI", 15F);
		notRedeemedBookingTextBox.Location = new Point(852, 75);
		notRedeemedBookingTextBox.Name = "notRedeemedBookingTextBox";
		notRedeemedBookingTextBox.ReadOnly = true;
		notRedeemedBookingTextBox.Size = new Size(143, 34);
		notRedeemedBookingTextBox.TabIndex = 46;
		notRedeemedBookingTextBox.Text = "0";
		notRedeemedBookingTextBox.TextAlign = HorizontalAlignment.Right;
		// 
		// advanceDataGridView
		// 
		advanceDataGridView.AllowUserToAddRows = false;
		advanceDataGridView.AllowUserToDeleteRows = false;
		advanceDataGridView.AllowUserToOrderColumns = true;
		advanceDataGridView.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
		advanceDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
		advanceDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		advanceDataGridView.Location = new Point(12, 534);
		advanceDataGridView.Name = "advanceDataGridView";
		advanceDataGridView.ReadOnly = true;
		advanceDataGridView.Size = new Size(1369, 316);
		advanceDataGridView.TabIndex = 24;
		advanceDataGridView.CellClick += advanceDataGridView_CellClick;
		// 
		// DetailDataForm
		// 
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		AutoScroll = true;
		ClientSize = new Size(1393, 850);
		Controls.Add(advanceDataGridView);
		Controls.Add(tableLayoutPanel1);
		Controls.Add(dateLabel);
		Controls.Add(locationNameLabel);
		Controls.Add(transactionDataGridView);
		Icon = (Icon)resources.GetObject("$this.Icon");
		Name = "DetailDataForm";
		StartPosition = FormStartPosition.CenterScreen;
		Text = "Detail Data";
		WindowState = FormWindowState.Maximized;
		Load += DetailDataForm_Load;
		((System.ComponentModel.ISupportInitialize)transactionDataGridView).EndInit();
		tableLayoutPanel1.ResumeLayout(false);
		tableLayoutPanel1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)advanceDataGridView).EndInit();
		ResumeLayout(false);
		PerformLayout();
	}

	#endregion

	private DataGridView transactionDataGridView;
	private Label locationNameLabel;
	private Label dateLabel;
	private Label label1;
	private Label label2;
	private Label label3;
	private Label label10;
	private Label label11;
	private Label label12;
	private Label label15;
	private Label label16;
	private Button printButton;
	private Button excelButton;
	private Button refreshButton;
	private Label label5;
	private TableLayoutPanel tableLayoutPanel1;
	private TextBox amexTextBox;
	private TextBox upiTextBox;
	private TextBox loyaltyTextBox;
	private TextBox cardTextBox;
	private TextBox femaleTextBox;
	private TextBox cashTextBox;
	private TextBox maleTextBox;
	private TextBox amountTextBox;
	private TextBox peopleTextBox;
	private Label label4;
	private Label label40;
	private TextBox advanceTextBox;
	private TextBox bookingTextBox;
	private Label label7;
	private Label label8;
	private TextBox redeemedAdvanceTextBox;
	private TextBox notRedeemedAdvanceTextBox;
	private Label label50;
	private Label label53;
	private TextBox redeemedBookingTextBox;
	private TextBox notRedeemedBookingTextBox;
	private DataGridView advanceDataGridView;
}