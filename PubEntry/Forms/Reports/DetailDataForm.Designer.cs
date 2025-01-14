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
		versionLabel = new Label();
		brandingLabel = new Label();
		richTextBoxFooter = new RichTextBox();
		((System.ComponentModel.ISupportInitialize)transactionDataGridView).BeginInit();
		((System.ComponentModel.ISupportInitialize)advanceDataGridView).BeginInit();
		SuspendLayout();
		// 
		// transactionDataGridView
		// 
		transactionDataGridView.AllowUserToAddRows = false;
		transactionDataGridView.AllowUserToDeleteRows = false;
		transactionDataGridView.AllowUserToOrderColumns = true;
		transactionDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
		transactionDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		transactionDataGridView.Location = new Point(12, 40);
		transactionDataGridView.Name = "transactionDataGridView";
		transactionDataGridView.ReadOnly = true;
		transactionDataGridView.Size = new Size(1185, 291);
		transactionDataGridView.TabIndex = 0;
		// 
		// locationNameLabel
		// 
		locationNameLabel.AutoSize = true;
		locationNameLabel.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
		locationNameLabel.Location = new Point(530, 9);
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
		dateLabel.Size = new Size(342, 28);
		dateLabel.TabIndex = 2;
		dateLabel.Text = "01/12/24 01:00 to 01/12/24 01:00";
		dateLabel.TextAlign = ContentAlignment.MiddleCenter;
		// 
		// label1
		// 
		label1.AutoSize = true;
		label1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
		label1.Location = new Point(12, 340);
		label1.Name = "label1";
		label1.Size = new Size(97, 19);
		label1.TabIndex = 3;
		label1.Text = "Total People:";
		// 
		// label2
		// 
		label2.AutoSize = true;
		label2.Font = new Font("Segoe UI", 10F);
		label2.Location = new Point(67, 374);
		label2.Name = "label2";
		label2.Size = new Size(42, 19);
		label2.TabIndex = 4;
		label2.Text = "Male:";
		// 
		// label3
		// 
		label3.AutoSize = true;
		label3.Font = new Font("Segoe UI", 10F);
		label3.Location = new Point(54, 408);
		label3.Name = "label3";
		label3.Size = new Size(55, 19);
		label3.TabIndex = 5;
		label3.Text = "Female:";
		// 
		// label10
		// 
		label10.AutoSize = true;
		label10.Font = new Font("Segoe UI", 10F);
		label10.Location = new Point(1012, 400);
		label10.Name = "label10";
		label10.Size = new Size(41, 19);
		label10.TabIndex = 11;
		label10.Text = "Card:";
		// 
		// label11
		// 
		label11.AutoSize = true;
		label11.Font = new Font("Segoe UI", 10F);
		label11.Location = new Point(1011, 370);
		label11.Name = "label11";
		label11.Size = new Size(42, 19);
		label11.TabIndex = 10;
		label11.Text = "Cash:";
		// 
		// label12
		// 
		label12.AutoSize = true;
		label12.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
		label12.Location = new Point(950, 340);
		label12.Name = "label12";
		label12.Size = new Size(103, 19);
		label12.TabIndex = 9;
		label12.Text = "Total Amount:";
		// 
		// label15
		// 
		label15.AutoSize = true;
		label15.Font = new Font("Segoe UI", 10F);
		label15.Location = new Point(1007, 460);
		label15.Name = "label15";
		label15.Size = new Size(46, 19);
		label15.TabIndex = 16;
		label15.Text = "Amex:";
		// 
		// label16
		// 
		label16.AutoSize = true;
		label16.Font = new Font("Segoe UI", 10F);
		label16.Location = new Point(1019, 430);
		label16.Name = "label16";
		label16.Size = new Size(34, 19);
		label16.TabIndex = 15;
		label16.Text = "UPI:";
		// 
		// printButton
		// 
		printButton.Font = new Font("Segoe UI", 15F);
		printButton.Location = new Point(550, 442);
		printButton.Name = "printButton";
		printButton.Size = new Size(118, 35);
		printButton.TabIndex = 2;
		printButton.Text = "PRINT";
		printButton.UseVisualStyleBackColor = true;
		printButton.Click += printButton_Click;
		// 
		// excelButton
		// 
		excelButton.Font = new Font("Segoe UI", 15F);
		excelButton.Location = new Point(333, 442);
		excelButton.Name = "excelButton";
		excelButton.Size = new Size(118, 35);
		excelButton.TabIndex = 1;
		excelButton.Text = "EXCEL";
		excelButton.UseVisualStyleBackColor = true;
		excelButton.Click += excelButton_Click;
		// 
		// refreshButton
		// 
		refreshButton.Font = new Font("Segoe UI", 15F);
		refreshButton.Location = new Point(767, 442);
		refreshButton.Name = "refreshButton";
		refreshButton.Size = new Size(124, 35);
		refreshButton.TabIndex = 3;
		refreshButton.Text = "REFRESH";
		refreshButton.UseVisualStyleBackColor = true;
		refreshButton.Click += refreshButton_Click;
		// 
		// label5
		// 
		label5.AutoSize = true;
		label5.Font = new Font("Segoe UI", 10F);
		label5.Location = new Point(20, 442);
		label5.Name = "label5";
		label5.Size = new Size(89, 19);
		label5.TabIndex = 22;
		label5.Text = "Total Loyalty:";
		// 
		// peopleTextBox
		// 
		peopleTextBox.Font = new Font("Segoe UI", 10F);
		peopleTextBox.Location = new Point(115, 337);
		peopleTextBox.Name = "peopleTextBox";
		peopleTextBox.ReadOnly = true;
		peopleTextBox.Size = new Size(120, 25);
		peopleTextBox.TabIndex = 34;
		peopleTextBox.Text = "0";
		peopleTextBox.TextAlign = HorizontalAlignment.Right;
		// 
		// amexTextBox
		// 
		amexTextBox.Font = new Font("Segoe UI", 10F);
		amexTextBox.Location = new Point(1077, 457);
		amexTextBox.Name = "amexTextBox";
		amexTextBox.ReadOnly = true;
		amexTextBox.Size = new Size(120, 25);
		amexTextBox.TabIndex = 33;
		amexTextBox.Text = "0";
		amexTextBox.TextAlign = HorizontalAlignment.Right;
		// 
		// upiTextBox
		// 
		upiTextBox.Font = new Font("Segoe UI", 10F);
		upiTextBox.Location = new Point(1077, 427);
		upiTextBox.Name = "upiTextBox";
		upiTextBox.ReadOnly = true;
		upiTextBox.Size = new Size(120, 25);
		upiTextBox.TabIndex = 30;
		upiTextBox.Text = "0";
		upiTextBox.TextAlign = HorizontalAlignment.Right;
		// 
		// loyaltyTextBox
		// 
		loyaltyTextBox.Font = new Font("Segoe UI", 10F);
		loyaltyTextBox.Location = new Point(115, 436);
		loyaltyTextBox.Name = "loyaltyTextBox";
		loyaltyTextBox.ReadOnly = true;
		loyaltyTextBox.Size = new Size(120, 25);
		loyaltyTextBox.TabIndex = 29;
		loyaltyTextBox.Text = "0";
		loyaltyTextBox.TextAlign = HorizontalAlignment.Right;
		// 
		// cardTextBox
		// 
		cardTextBox.Font = new Font("Segoe UI", 10F);
		cardTextBox.Location = new Point(1077, 397);
		cardTextBox.Name = "cardTextBox";
		cardTextBox.ReadOnly = true;
		cardTextBox.Size = new Size(120, 25);
		cardTextBox.TabIndex = 28;
		cardTextBox.Text = "0";
		cardTextBox.TextAlign = HorizontalAlignment.Right;
		// 
		// femaleTextBox
		// 
		femaleTextBox.Font = new Font("Segoe UI", 10F);
		femaleTextBox.Location = new Point(115, 403);
		femaleTextBox.Name = "femaleTextBox";
		femaleTextBox.ReadOnly = true;
		femaleTextBox.Size = new Size(120, 25);
		femaleTextBox.TabIndex = 27;
		femaleTextBox.Text = "0";
		femaleTextBox.TextAlign = HorizontalAlignment.Right;
		// 
		// cashTextBox
		// 
		cashTextBox.Font = new Font("Segoe UI", 10F);
		cashTextBox.Location = new Point(1077, 367);
		cashTextBox.Name = "cashTextBox";
		cashTextBox.ReadOnly = true;
		cashTextBox.Size = new Size(120, 25);
		cashTextBox.TabIndex = 26;
		cashTextBox.Text = "0";
		cashTextBox.TextAlign = HorizontalAlignment.Right;
		// 
		// maleTextBox
		// 
		maleTextBox.Font = new Font("Segoe UI", 10F);
		maleTextBox.Location = new Point(115, 370);
		maleTextBox.Name = "maleTextBox";
		maleTextBox.ReadOnly = true;
		maleTextBox.Size = new Size(120, 25);
		maleTextBox.TabIndex = 25;
		maleTextBox.Text = "0";
		maleTextBox.TextAlign = HorizontalAlignment.Right;
		// 
		// amountTextBox
		// 
		amountTextBox.Font = new Font("Segoe UI", 10F);
		amountTextBox.Location = new Point(1077, 337);
		amountTextBox.Name = "amountTextBox";
		amountTextBox.ReadOnly = true;
		amountTextBox.Size = new Size(120, 25);
		amountTextBox.TabIndex = 24;
		amountTextBox.Text = "0";
		amountTextBox.TextAlign = HorizontalAlignment.Right;
		// 
		// label4
		// 
		label4.AutoSize = true;
		label4.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
		label4.Location = new Point(356, 337);
		label4.Name = "label4";
		label4.Size = new Size(71, 19);
		label4.TabIndex = 35;
		label4.Text = "Advance:";
		// 
		// label40
		// 
		label40.AutoSize = true;
		label40.Font = new Font("Segoe UI", 10F);
		label40.Location = new Point(351, 366);
		label40.Name = "label40";
		label40.Size = new Size(76, 19);
		label40.TabIndex = 36;
		label40.Text = "Redeemed:";
		// 
		// advanceTextBox
		// 
		advanceTextBox.Font = new Font("Segoe UI", 10F);
		advanceTextBox.Location = new Point(433, 334);
		advanceTextBox.Name = "advanceTextBox";
		advanceTextBox.ReadOnly = true;
		advanceTextBox.Size = new Size(120, 25);
		advanceTextBox.TabIndex = 37;
		advanceTextBox.Text = "0";
		advanceTextBox.TextAlign = HorizontalAlignment.Right;
		// 
		// label7
		// 
		label7.AutoSize = true;
		label7.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
		label7.Location = new Point(682, 337);
		label7.Name = "label7";
		label7.Size = new Size(69, 19);
		label7.TabIndex = 39;
		label7.Text = "Booking:";
		// 
		// bookingTextBox
		// 
		bookingTextBox.Font = new Font("Segoe UI", 10F);
		bookingTextBox.Location = new Point(757, 337);
		bookingTextBox.Name = "bookingTextBox";
		bookingTextBox.ReadOnly = true;
		bookingTextBox.Size = new Size(120, 25);
		bookingTextBox.TabIndex = 38;
		bookingTextBox.Text = "0";
		bookingTextBox.TextAlign = HorizontalAlignment.Right;
		// 
		// label8
		// 
		label8.AutoSize = true;
		label8.Font = new Font("Segoe UI", 10F);
		label8.Location = new Point(322, 395);
		label8.Name = "label8";
		label8.Size = new Size(105, 19);
		label8.TabIndex = 40;
		label8.Text = "Not-Redeemed:";
		// 
		// redeemedAdvanceTextBox
		// 
		redeemedAdvanceTextBox.Font = new Font("Segoe UI", 10F);
		redeemedAdvanceTextBox.Location = new Point(433, 363);
		redeemedAdvanceTextBox.Name = "redeemedAdvanceTextBox";
		redeemedAdvanceTextBox.ReadOnly = true;
		redeemedAdvanceTextBox.Size = new Size(120, 25);
		redeemedAdvanceTextBox.TabIndex = 41;
		redeemedAdvanceTextBox.Text = "0";
		redeemedAdvanceTextBox.TextAlign = HorizontalAlignment.Right;
		// 
		// notRedeemedAdvanceTextBox
		// 
		notRedeemedAdvanceTextBox.Font = new Font("Segoe UI", 10F);
		notRedeemedAdvanceTextBox.Location = new Point(433, 392);
		notRedeemedAdvanceTextBox.Name = "notRedeemedAdvanceTextBox";
		notRedeemedAdvanceTextBox.ReadOnly = true;
		notRedeemedAdvanceTextBox.Size = new Size(120, 25);
		notRedeemedAdvanceTextBox.TabIndex = 42;
		notRedeemedAdvanceTextBox.Text = "0";
		notRedeemedAdvanceTextBox.TextAlign = HorizontalAlignment.Right;
		// 
		// label50
		// 
		label50.AutoSize = true;
		label50.Font = new Font("Segoe UI", 10F);
		label50.Location = new Point(675, 366);
		label50.Name = "label50";
		label50.Size = new Size(76, 19);
		label50.TabIndex = 43;
		label50.Text = "Redeemed:";
		// 
		// label53
		// 
		label53.AutoSize = true;
		label53.Font = new Font("Segoe UI", 10F);
		label53.Location = new Point(646, 395);
		label53.Name = "label53";
		label53.Size = new Size(105, 19);
		label53.TabIndex = 44;
		label53.Text = "Not-Redeemed:";
		// 
		// redeemedBookingTextBox
		// 
		redeemedBookingTextBox.Font = new Font("Segoe UI", 10F);
		redeemedBookingTextBox.Location = new Point(757, 364);
		redeemedBookingTextBox.Name = "redeemedBookingTextBox";
		redeemedBookingTextBox.ReadOnly = true;
		redeemedBookingTextBox.Size = new Size(120, 25);
		redeemedBookingTextBox.TabIndex = 45;
		redeemedBookingTextBox.Text = "0";
		redeemedBookingTextBox.TextAlign = HorizontalAlignment.Right;
		// 
		// notRedeemedBookingTextBox
		// 
		notRedeemedBookingTextBox.Font = new Font("Segoe UI", 10F);
		notRedeemedBookingTextBox.Location = new Point(757, 391);
		notRedeemedBookingTextBox.Name = "notRedeemedBookingTextBox";
		notRedeemedBookingTextBox.ReadOnly = true;
		notRedeemedBookingTextBox.Size = new Size(120, 25);
		notRedeemedBookingTextBox.TabIndex = 46;
		notRedeemedBookingTextBox.Text = "0";
		notRedeemedBookingTextBox.TextAlign = HorizontalAlignment.Right;
		// 
		// advanceDataGridView
		// 
		advanceDataGridView.AllowUserToAddRows = false;
		advanceDataGridView.AllowUserToDeleteRows = false;
		advanceDataGridView.AllowUserToOrderColumns = true;
		advanceDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
		advanceDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		advanceDataGridView.Location = new Point(12, 488);
		advanceDataGridView.Name = "advanceDataGridView";
		advanceDataGridView.ReadOnly = true;
		advanceDataGridView.Size = new Size(1185, 201);
		advanceDataGridView.TabIndex = 24;
		advanceDataGridView.CellClick += advanceDataGridView_CellClick;
		// 
		// versionLabel
		// 
		versionLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
		versionLabel.AutoSize = true;
		versionLabel.BackColor = Color.White;
		versionLabel.Location = new Point(3, 703);
		versionLabel.Name = "versionLabel";
		versionLabel.Size = new Size(84, 15);
		versionLabel.TabIndex = 49;
		versionLabel.Text = "Version: 0.0.0.0";
		// 
		// brandingLabel
		// 
		brandingLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
		brandingLabel.AutoSize = true;
		brandingLabel.BackColor = Color.White;
		brandingLabel.Location = new Point(1130, 705);
		brandingLabel.Name = "brandingLabel";
		brandingLabel.Size = new Size(76, 15);
		brandingLabel.TabIndex = 48;
		brandingLabel.Text = "© AADISOFT";
		// 
		// richTextBoxFooter
		// 
		richTextBoxFooter.Dock = DockStyle.Bottom;
		richTextBoxFooter.Location = new Point(0, 697);
		richTextBoxFooter.Name = "richTextBoxFooter";
		richTextBoxFooter.Size = new Size(1211, 26);
		richTextBoxFooter.TabIndex = 47;
		richTextBoxFooter.Text = "";
		// 
		// DetailDataForm
		// 
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		AutoScroll = true;
		ClientSize = new Size(1211, 723);
		Controls.Add(versionLabel);
		Controls.Add(brandingLabel);
		Controls.Add(richTextBoxFooter);
		Controls.Add(amexTextBox);
		Controls.Add(peopleTextBox);
		Controls.Add(upiTextBox);
		Controls.Add(advanceDataGridView);
		Controls.Add(cardTextBox);
		Controls.Add(cashTextBox);
		Controls.Add(loyaltyTextBox);
		Controls.Add(amountTextBox);
		Controls.Add(dateLabel);
		Controls.Add(label12);
		Controls.Add(label15);
		Controls.Add(locationNameLabel);
		Controls.Add(label16);
		Controls.Add(femaleTextBox);
		Controls.Add(label11);
		Controls.Add(transactionDataGridView);
		Controls.Add(label10);
		Controls.Add(label3);
		Controls.Add(maleTextBox);
		Controls.Add(excelButton);
		Controls.Add(printButton);
		Controls.Add(label7);
		Controls.Add(refreshButton);
		Controls.Add(bookingTextBox);
		Controls.Add(label4);
		Controls.Add(label50);
		Controls.Add(label40);
		Controls.Add(label53);
		Controls.Add(label2);
		Controls.Add(redeemedBookingTextBox);
		Controls.Add(advanceTextBox);
		Controls.Add(notRedeemedBookingTextBox);
		Controls.Add(label5);
		Controls.Add(label1);
		Controls.Add(notRedeemedAdvanceTextBox);
		Controls.Add(label8);
		Controls.Add(redeemedAdvanceTextBox);
		Icon = (Icon)resources.GetObject("$this.Icon");
		Name = "DetailDataForm";
		StartPosition = FormStartPosition.CenterScreen;
		Text = "Detail Data";
		WindowState = FormWindowState.Maximized;
		Load += DetailDataForm_Load;
		((System.ComponentModel.ISupportInitialize)transactionDataGridView).EndInit();
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
	private Label versionLabel;
	private Label brandingLabel;
	private RichTextBox richTextBoxFooter;
}