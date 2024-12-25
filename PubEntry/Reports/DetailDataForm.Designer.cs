namespace PubEntry.Reports;

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
		dataGridView = new DataGridView();
		locationNameLabel = new Label();
		dateLabel = new Label();
		label1 = new Label();
		label2 = new Label();
		label3 = new Label();
		totalPeopleLabel = new Label();
		maleLabel = new Label();
		femaleLabel = new Label();
		cardLabel = new Label();
		cashLabel = new Label();
		totalAmountLabel = new Label();
		label10 = new Label();
		label11 = new Label();
		label12 = new Label();
		amexLabel = new Label();
		upiLabel = new Label();
		label15 = new Label();
		label16 = new Label();
		printButton = new Button();
		excelButton = new Button();
		refreshButton = new Button();
		totalLoyaltyLabel = new Label();
		label5 = new Label();
		((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
		SuspendLayout();
		// 
		// dataGridView
		// 
		dataGridView.AllowUserToAddRows = false;
		dataGridView.AllowUserToDeleteRows = false;
		dataGridView.AllowUserToOrderColumns = true;
		dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		dataGridView.Location = new Point(12, 86);
		dataGridView.Name = "dataGridView";
		dataGridView.ReadOnly = true;
		dataGridView.Size = new Size(1068, 500);
		dataGridView.TabIndex = 0;
		// 
		// locationNameLabel
		// 
		locationNameLabel.AutoSize = true;
		locationNameLabel.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold);
		locationNameLabel.Location = new Point(473, 46);
		locationNameLabel.Name = "locationNameLabel";
		locationNameLabel.Size = new Size(128, 37);
		locationNameLabel.TabIndex = 1;
		locationNameLabel.Text = "Location";
		// 
		// dateLabel
		// 
		dateLabel.AutoSize = true;
		dateLabel.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold);
		dateLabel.Location = new Point(320, 9);
		dateLabel.Name = "dateLabel";
		dateLabel.Size = new Size(438, 37);
		dateLabel.TabIndex = 2;
		dateLabel.Text = "01/12/24 01:00 - 01/12/24 01:00";
		// 
		// label1
		// 
		label1.AutoSize = true;
		label1.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold);
		label1.Location = new Point(12, 600);
		label1.Name = "label1";
		label1.Size = new Size(184, 37);
		label1.TabIndex = 3;
		label1.Text = "Total People:";
		// 
		// label2
		// 
		label2.AutoSize = true;
		label2.Font = new Font("Segoe UI", 20F);
		label2.Location = new Point(100, 637);
		label2.Name = "label2";
		label2.Size = new Size(82, 37);
		label2.TabIndex = 4;
		label2.Text = "Male:";
		// 
		// label3
		// 
		label3.AutoSize = true;
		label3.Font = new Font("Segoe UI", 20F);
		label3.Location = new Point(74, 674);
		label3.Name = "label3";
		label3.Size = new Size(108, 37);
		label3.TabIndex = 5;
		label3.Text = "Female:";
		// 
		// totalPeopleLabel
		// 
		totalPeopleLabel.AutoSize = true;
		totalPeopleLabel.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold);
		totalPeopleLabel.Location = new Point(202, 600);
		totalPeopleLabel.Name = "totalPeopleLabel";
		totalPeopleLabel.RightToLeft = RightToLeft.Yes;
		totalPeopleLabel.Size = new Size(65, 37);
		totalPeopleLabel.TabIndex = 6;
		totalPeopleLabel.Text = "000";
		// 
		// maleLabel
		// 
		maleLabel.AutoSize = true;
		maleLabel.Font = new Font("Segoe UI", 20F);
		maleLabel.Location = new Point(188, 637);
		maleLabel.Name = "maleLabel";
		maleLabel.RightToLeft = RightToLeft.Yes;
		maleLabel.Size = new Size(62, 37);
		maleLabel.TabIndex = 7;
		maleLabel.Text = "000";
		// 
		// femaleLabel
		// 
		femaleLabel.AutoSize = true;
		femaleLabel.Font = new Font("Segoe UI", 20F);
		femaleLabel.Location = new Point(188, 674);
		femaleLabel.Name = "femaleLabel";
		femaleLabel.RightToLeft = RightToLeft.Yes;
		femaleLabel.Size = new Size(62, 37);
		femaleLabel.TabIndex = 8;
		femaleLabel.Text = "000";
		// 
		// cardLabel
		// 
		cardLabel.AutoSize = true;
		cardLabel.Font = new Font("Segoe UI", 20F);
		cardLabel.Location = new Point(939, 674);
		cardLabel.Name = "cardLabel";
		cardLabel.RightToLeft = RightToLeft.Yes;
		cardLabel.Size = new Size(122, 37);
		cardLabel.TabIndex = 14;
		cardLabel.Text = "0000000";
		// 
		// cashLabel
		// 
		cashLabel.AutoSize = true;
		cashLabel.Font = new Font("Segoe UI", 20F);
		cashLabel.Location = new Point(939, 637);
		cashLabel.Name = "cashLabel";
		cashLabel.RightToLeft = RightToLeft.Yes;
		cashLabel.Size = new Size(122, 37);
		cashLabel.TabIndex = 13;
		cashLabel.Text = "0000000";
		// 
		// totalAmountLabel
		// 
		totalAmountLabel.AutoSize = true;
		totalAmountLabel.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold);
		totalAmountLabel.Location = new Point(939, 600);
		totalAmountLabel.Name = "totalAmountLabel";
		totalAmountLabel.RightToLeft = RightToLeft.Yes;
		totalAmountLabel.Size = new Size(129, 37);
		totalAmountLabel.TabIndex = 12;
		totalAmountLabel.Text = "0000000";
		// 
		// label10
		// 
		label10.AutoSize = true;
		label10.Font = new Font("Segoe UI", 20F);
		label10.Location = new Point(854, 674);
		label10.Name = "label10";
		label10.Size = new Size(79, 37);
		label10.TabIndex = 11;
		label10.Text = "Card:";
		// 
		// label11
		// 
		label11.AutoSize = true;
		label11.Font = new Font("Segoe UI", 20F);
		label11.Location = new Point(853, 637);
		label11.Name = "label11";
		label11.Size = new Size(80, 37);
		label11.TabIndex = 10;
		label11.Text = "Cash:";
		// 
		// label12
		// 
		label12.AutoSize = true;
		label12.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold);
		label12.Location = new Point(733, 600);
		label12.Name = "label12";
		label12.Size = new Size(200, 37);
		label12.TabIndex = 9;
		label12.Text = "Total Amount:";
		// 
		// amexLabel
		// 
		amexLabel.AutoSize = true;
		amexLabel.Font = new Font("Segoe UI", 20F);
		amexLabel.Location = new Point(939, 748);
		amexLabel.Name = "amexLabel";
		amexLabel.RightToLeft = RightToLeft.Yes;
		amexLabel.Size = new Size(122, 37);
		amexLabel.TabIndex = 18;
		amexLabel.Text = "0000000";
		// 
		// upiLabel
		// 
		upiLabel.AutoSize = true;
		upiLabel.Font = new Font("Segoe UI", 20F);
		upiLabel.Location = new Point(939, 711);
		upiLabel.Name = "upiLabel";
		upiLabel.RightToLeft = RightToLeft.Yes;
		upiLabel.Size = new Size(122, 37);
		upiLabel.TabIndex = 17;
		upiLabel.Text = "0000000";
		// 
		// label15
		// 
		label15.AutoSize = true;
		label15.Font = new Font("Segoe UI", 20F);
		label15.Location = new Point(844, 748);
		label15.Name = "label15";
		label15.Size = new Size(89, 37);
		label15.TabIndex = 16;
		label15.Text = "Amex:";
		// 
		// label16
		// 
		label16.AutoSize = true;
		label16.Font = new Font("Segoe UI", 20F);
		label16.Location = new Point(869, 711);
		label16.Name = "label16";
		label16.Size = new Size(64, 37);
		label16.TabIndex = 15;
		label16.Text = "UPI:";
		// 
		// printButton
		// 
		printButton.Font = new Font("Segoe UI", 15F);
		printButton.Location = new Point(504, 853);
		printButton.Name = "printButton";
		printButton.Size = new Size(118, 38);
		printButton.TabIndex = 19;
		printButton.Text = "PRINT";
		printButton.UseVisualStyleBackColor = true;
		printButton.Click += printButton_Click;
		// 
		// excelButton
		// 
		excelButton.Font = new Font("Segoe UI", 15F);
		excelButton.Location = new Point(132, 853);
		excelButton.Name = "excelButton";
		excelButton.Size = new Size(118, 38);
		excelButton.TabIndex = 20;
		excelButton.Text = "EXCEL";
		excelButton.UseVisualStyleBackColor = true;
		excelButton.Click += excelButton_Click;
		// 
		// refreshButton
		// 
		refreshButton.Font = new Font("Segoe UI", 15F);
		refreshButton.Location = new Point(939, 853);
		refreshButton.Name = "refreshButton";
		refreshButton.Size = new Size(118, 38);
		refreshButton.TabIndex = 21;
		refreshButton.Text = "REFRESH";
		refreshButton.UseVisualStyleBackColor = true;
		refreshButton.Click += refreshButton_Click;
		// 
		// totalLoyaltyLabel
		// 
		totalLoyaltyLabel.AutoSize = true;
		totalLoyaltyLabel.Font = new Font("Segoe UI", 20F);
		totalLoyaltyLabel.Location = new Point(188, 711);
		totalLoyaltyLabel.Name = "totalLoyaltyLabel";
		totalLoyaltyLabel.RightToLeft = RightToLeft.Yes;
		totalLoyaltyLabel.Size = new Size(62, 37);
		totalLoyaltyLabel.TabIndex = 23;
		totalLoyaltyLabel.Text = "000";
		// 
		// label5
		// 
		label5.AutoSize = true;
		label5.Font = new Font("Segoe UI", 20F);
		label5.Location = new Point(12, 711);
		label5.Name = "label5";
		label5.Size = new Size(172, 37);
		label5.TabIndex = 22;
		label5.Text = "Total Loyalty:";
		// 
		// DetailDataForm
		// 
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		ClientSize = new Size(1092, 928);
		Controls.Add(totalLoyaltyLabel);
		Controls.Add(label5);
		Controls.Add(refreshButton);
		Controls.Add(excelButton);
		Controls.Add(printButton);
		Controls.Add(amexLabel);
		Controls.Add(upiLabel);
		Controls.Add(label15);
		Controls.Add(label16);
		Controls.Add(cardLabel);
		Controls.Add(cashLabel);
		Controls.Add(totalAmountLabel);
		Controls.Add(label10);
		Controls.Add(label11);
		Controls.Add(label12);
		Controls.Add(femaleLabel);
		Controls.Add(maleLabel);
		Controls.Add(totalPeopleLabel);
		Controls.Add(label3);
		Controls.Add(label2);
		Controls.Add(label1);
		Controls.Add(dateLabel);
		Controls.Add(locationNameLabel);
		Controls.Add(dataGridView);
		Icon = (Icon)resources.GetObject("$this.Icon");
		Name = "DetailDataForm";
		StartPosition = FormStartPosition.CenterScreen;
		Text = "DetailDataForm";
		((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
		ResumeLayout(false);
		PerformLayout();
	}

	#endregion

	private DataGridView dataGridView;
	private Label locationNameLabel;
	private Label dateLabel;
	private Label label1;
	private Label label2;
	private Label label3;
	private Label totalPeopleLabel;
	private Label maleLabel;
	private Label femaleLabel;
	private Label cardLabel;
	private Label cashLabel;
	private Label totalAmountLabel;
	private Label label10;
	private Label label11;
	private Label label12;
	private Label amexLabel;
	private Label upiLabel;
	private Label label15;
	private Label label16;
	private Button printButton;
	private Button excelButton;
	private Button refreshButton;
	private Label totalLoyaltyLabel;
	private Label label5;
}