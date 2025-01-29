namespace PubEntry.Forms.Admin;

partial class AdminPanel
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminPanel));
		manageLocationButton = new Button();
		manageUsersButton = new Button();
		manageTransactionsButton = new Button();
		managePersonsButton = new Button();
		settingsButton = new Button();
		paymentModeButton = new Button();
		advanceButton = new Button();
		brandingLabel = new Label();
		richTextBoxFooter = new RichTextBox();
		reservationButton = new Button();
		sqlQueryButton = new Button();
		button1 = new Button();
		SuspendLayout();
		// 
		// manageLocationButton
		// 
		manageLocationButton.Font = new Font("Segoe UI", 15F);
		manageLocationButton.Location = new Point(38, 249);
		manageLocationButton.Name = "manageLocationButton";
		manageLocationButton.Size = new Size(213, 52);
		manageLocationButton.TabIndex = 5;
		manageLocationButton.Text = "Manage Locations";
		manageLocationButton.UseVisualStyleBackColor = true;
		manageLocationButton.Click += manageLocationButton_Click;
		// 
		// manageUsersButton
		// 
		manageUsersButton.Font = new Font("Segoe UI", 15F);
		manageUsersButton.Location = new Point(38, 191);
		manageUsersButton.Name = "manageUsersButton";
		manageUsersButton.Size = new Size(213, 52);
		manageUsersButton.TabIndex = 4;
		manageUsersButton.Text = "Manage Users";
		manageUsersButton.UseVisualStyleBackColor = true;
		manageUsersButton.Click += manageUsersButton_Click;
		// 
		// manageTransactionsButton
		// 
		manageTransactionsButton.Font = new Font("Segoe UI", 15F);
		manageTransactionsButton.Location = new Point(38, 75);
		manageTransactionsButton.Name = "manageTransactionsButton";
		manageTransactionsButton.Size = new Size(213, 52);
		manageTransactionsButton.TabIndex = 2;
		manageTransactionsButton.Text = "Manage Transactions";
		manageTransactionsButton.UseVisualStyleBackColor = true;
		manageTransactionsButton.Click += manageTransactionsButton_Click;
		// 
		// managePersonsButton
		// 
		managePersonsButton.Font = new Font("Segoe UI", 15F);
		managePersonsButton.Location = new Point(38, 307);
		managePersonsButton.Name = "managePersonsButton";
		managePersonsButton.Size = new Size(213, 52);
		managePersonsButton.TabIndex = 6;
		managePersonsButton.Text = "Manage Persons";
		managePersonsButton.UseVisualStyleBackColor = true;
		managePersonsButton.Click += managePersonsButton_Click;
		// 
		// settingsButton
		// 
		settingsButton.Font = new Font("Segoe UI", 15F);
		settingsButton.Location = new Point(38, 481);
		settingsButton.Name = "settingsButton";
		settingsButton.Size = new Size(213, 52);
		settingsButton.TabIndex = 9;
		settingsButton.Text = "Settings";
		settingsButton.UseVisualStyleBackColor = true;
		settingsButton.Click += settingsButton_Click;
		// 
		// paymentModeButton
		// 
		paymentModeButton.Font = new Font("Segoe UI", 15F);
		paymentModeButton.Location = new Point(38, 365);
		paymentModeButton.Name = "paymentModeButton";
		paymentModeButton.Size = new Size(213, 52);
		paymentModeButton.TabIndex = 7;
		paymentModeButton.Text = "Payment Modes";
		paymentModeButton.UseVisualStyleBackColor = true;
		paymentModeButton.Click += paymentModeButton_Click;
		// 
		// advanceButton
		// 
		advanceButton.Font = new Font("Segoe UI", 15F);
		advanceButton.Location = new Point(38, 17);
		advanceButton.Name = "advanceButton";
		advanceButton.Size = new Size(213, 52);
		advanceButton.TabIndex = 1;
		advanceButton.Text = "Advance Entry";
		advanceButton.UseVisualStyleBackColor = true;
		advanceButton.Click += advanceButton_Click;
		// 
		// brandingLabel
		// 
		brandingLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
		brandingLabel.AutoSize = true;
		brandingLabel.BackColor = Color.White;
		brandingLabel.Location = new Point(212, 619);
		brandingLabel.Name = "brandingLabel";
		brandingLabel.Size = new Size(76, 15);
		brandingLabel.TabIndex = 39;
		brandingLabel.Text = "© AADISOFT";
		// 
		// richTextBoxFooter
		// 
		richTextBoxFooter.Dock = DockStyle.Bottom;
		richTextBoxFooter.Location = new Point(0, 613);
		richTextBoxFooter.Name = "richTextBoxFooter";
		richTextBoxFooter.Size = new Size(292, 26);
		richTextBoxFooter.TabIndex = 38;
		richTextBoxFooter.Text = "";
		// 
		// reservationButton
		// 
		reservationButton.Font = new Font("Segoe UI", 15F);
		reservationButton.Location = new Point(38, 423);
		reservationButton.Name = "reservationButton";
		reservationButton.Size = new Size(213, 52);
		reservationButton.TabIndex = 8;
		reservationButton.Text = "Reservation Type";
		reservationButton.UseVisualStyleBackColor = true;
		reservationButton.Click += reservationButton_Click;
		// 
		// sqlQueryButton
		// 
		sqlQueryButton.Font = new Font("Segoe UI", 15F);
		sqlQueryButton.Location = new Point(38, 539);
		sqlQueryButton.Name = "sqlQueryButton";
		sqlQueryButton.Size = new Size(213, 52);
		sqlQueryButton.TabIndex = 10;
		sqlQueryButton.Text = "SQL Editor";
		sqlQueryButton.UseVisualStyleBackColor = true;
		sqlQueryButton.Click += sqlQueryButton_Click;
		// 
		// button1
		// 
		button1.Font = new Font("Segoe UI", 15F);
		button1.Location = new Point(38, 133);
		button1.Name = "button1";
		button1.Size = new Size(213, 52);
		button1.TabIndex = 3;
		button1.Text = "Update Advance";
		button1.UseVisualStyleBackColor = true;
		button1.Click += button1_Click;
		// 
		// AdminPanel
		// 
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		ClientSize = new Size(292, 639);
		Controls.Add(button1);
		Controls.Add(sqlQueryButton);
		Controls.Add(reservationButton);
		Controls.Add(brandingLabel);
		Controls.Add(richTextBoxFooter);
		Controls.Add(advanceButton);
		Controls.Add(paymentModeButton);
		Controls.Add(settingsButton);
		Controls.Add(manageTransactionsButton);
		Controls.Add(managePersonsButton);
		Controls.Add(manageLocationButton);
		Controls.Add(manageUsersButton);
		Icon = (Icon)resources.GetObject("$this.Icon");
		Name = "AdminPanel";
		StartPosition = FormStartPosition.CenterScreen;
		Text = "Admin Panel";
		Load += AdminPanel_Load;
		ResumeLayout(false);
		PerformLayout();
	}

	private System.Windows.Forms.Button button1;

	#endregion

	private System.Windows.Forms.Button manageLocationButton;
	private System.Windows.Forms.Button manageUsersButton;
	private Button manageTransactionsButton;
	private System.Windows.Forms.Button managePersonsButton;
	private System.Windows.Forms.Button settingsButton;
	private System.Windows.Forms.Button paymentModeButton;
	private Button advanceButton;
	private System.Windows.Forms.Label brandingLabel;
	private System.Windows.Forms.RichTextBox richTextBoxFooter;
	private System.Windows.Forms.Button reservationButton;
	private System.Windows.Forms.Button sqlQueryButton;
}