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
		manageLocationButton = new System.Windows.Forms.Button();
		manageUsersButton = new System.Windows.Forms.Button();
		manageTransactionsButton = new System.Windows.Forms.Button();
		managePersonsButton = new System.Windows.Forms.Button();
		settingsButton = new System.Windows.Forms.Button();
		paymentModeButton = new System.Windows.Forms.Button();
		advanceButton = new System.Windows.Forms.Button();
		versionLabel = new System.Windows.Forms.Label();
		brandingLabel = new System.Windows.Forms.Label();
		richTextBoxFooter = new System.Windows.Forms.RichTextBox();
		reservationButton = new System.Windows.Forms.Button();
		sqlQueryButton = new System.Windows.Forms.Button();
		button1 = new System.Windows.Forms.Button();
		SuspendLayout();
		// 
		// manageLocationButton
		// 
		manageLocationButton.Font = new System.Drawing.Font("Segoe UI", 15F);
		manageLocationButton.Location = new System.Drawing.Point(38, 249);
		manageLocationButton.Name = "manageLocationButton";
		manageLocationButton.Size = new System.Drawing.Size(213, 52);
		manageLocationButton.TabIndex = 5;
		manageLocationButton.Text = "Manage Locations";
		manageLocationButton.UseVisualStyleBackColor = true;
		manageLocationButton.Click += manageLocationButton_Click;
		// 
		// manageUsersButton
		// 
		manageUsersButton.Font = new System.Drawing.Font("Segoe UI", 15F);
		manageUsersButton.Location = new System.Drawing.Point(38, 191);
		manageUsersButton.Name = "manageUsersButton";
		manageUsersButton.Size = new System.Drawing.Size(213, 52);
		manageUsersButton.TabIndex = 4;
		manageUsersButton.Text = "Manage Users";
		manageUsersButton.UseVisualStyleBackColor = true;
		manageUsersButton.Click += manageUsersButton_Click;
		// 
		// manageTransactionsButton
		// 
		manageTransactionsButton.Font = new System.Drawing.Font("Segoe UI", 15F);
		manageTransactionsButton.Location = new System.Drawing.Point(38, 75);
		manageTransactionsButton.Name = "manageTransactionsButton";
		manageTransactionsButton.Size = new System.Drawing.Size(213, 52);
		manageTransactionsButton.TabIndex = 2;
		manageTransactionsButton.Text = "Manage Transactions";
		manageTransactionsButton.UseVisualStyleBackColor = true;
		manageTransactionsButton.Click += manageTransactionsButton_Click;
		// 
		// managePersonsButton
		// 
		managePersonsButton.Font = new System.Drawing.Font("Segoe UI", 15F);
		managePersonsButton.Location = new System.Drawing.Point(38, 307);
		managePersonsButton.Name = "managePersonsButton";
		managePersonsButton.Size = new System.Drawing.Size(213, 52);
		managePersonsButton.TabIndex = 6;
		managePersonsButton.Text = "Manage Persons";
		managePersonsButton.UseVisualStyleBackColor = true;
		managePersonsButton.Click += managePersonsButton_Click;
		// 
		// settingsButton
		// 
		settingsButton.Font = new System.Drawing.Font("Segoe UI", 15F);
		settingsButton.Location = new System.Drawing.Point(38, 481);
		settingsButton.Name = "settingsButton";
		settingsButton.Size = new System.Drawing.Size(213, 52);
		settingsButton.TabIndex = 9;
		settingsButton.Text = "Settings";
		settingsButton.UseVisualStyleBackColor = true;
		settingsButton.Click += settingsButton_Click;
		// 
		// paymentModeButton
		// 
		paymentModeButton.Font = new System.Drawing.Font("Segoe UI", 15F);
		paymentModeButton.Location = new System.Drawing.Point(38, 365);
		paymentModeButton.Name = "paymentModeButton";
		paymentModeButton.Size = new System.Drawing.Size(213, 52);
		paymentModeButton.TabIndex = 7;
		paymentModeButton.Text = "Payment Modes";
		paymentModeButton.UseVisualStyleBackColor = true;
		paymentModeButton.Click += paymentModeButton_Click;
		// 
		// advanceButton
		// 
		advanceButton.Font = new System.Drawing.Font("Segoe UI", 15F);
		advanceButton.Location = new System.Drawing.Point(38, 17);
		advanceButton.Name = "advanceButton";
		advanceButton.Size = new System.Drawing.Size(213, 52);
		advanceButton.TabIndex = 1;
		advanceButton.Text = "Advance Entry";
		advanceButton.UseVisualStyleBackColor = true;
		advanceButton.Click += advanceButton_Click;
		// 
		// versionLabel
		// 
		versionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
		versionLabel.AutoSize = true;
		versionLabel.BackColor = System.Drawing.Color.White;
		versionLabel.Location = new System.Drawing.Point(4, 619);
		versionLabel.Name = "versionLabel";
		versionLabel.Size = new System.Drawing.Size(84, 15);
		versionLabel.TabIndex = 40;
		versionLabel.Text = "Version: 0.0.0.0";
		// 
		// brandingLabel
		// 
		brandingLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
		brandingLabel.AutoSize = true;
		brandingLabel.BackColor = System.Drawing.Color.White;
		brandingLabel.Location = new System.Drawing.Point(212, 619);
		brandingLabel.Name = "brandingLabel";
		brandingLabel.Size = new System.Drawing.Size(76, 15);
		brandingLabel.TabIndex = 39;
		brandingLabel.Text = "© AADISOFT";
		// 
		// richTextBoxFooter
		// 
		richTextBoxFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
		richTextBoxFooter.Location = new System.Drawing.Point(0, 613);
		richTextBoxFooter.Name = "richTextBoxFooter";
		richTextBoxFooter.Size = new System.Drawing.Size(292, 26);
		richTextBoxFooter.TabIndex = 38;
		richTextBoxFooter.Text = "";
		// 
		// reservationButton
		// 
		reservationButton.Font = new System.Drawing.Font("Segoe UI", 15F);
		reservationButton.Location = new System.Drawing.Point(38, 423);
		reservationButton.Name = "reservationButton";
		reservationButton.Size = new System.Drawing.Size(213, 52);
		reservationButton.TabIndex = 8;
		reservationButton.Text = "Reservation Type";
		reservationButton.UseVisualStyleBackColor = true;
		reservationButton.Click += reservationButton_Click;
		// 
		// sqlQueryButton
		// 
		sqlQueryButton.Font = new System.Drawing.Font("Segoe UI", 15F);
		sqlQueryButton.Location = new System.Drawing.Point(38, 539);
		sqlQueryButton.Name = "sqlQueryButton";
		sqlQueryButton.Size = new System.Drawing.Size(213, 52);
		sqlQueryButton.TabIndex = 10;
		sqlQueryButton.Text = "SQL Editor";
		sqlQueryButton.UseVisualStyleBackColor = true;
		sqlQueryButton.Click += sqlQueryButton_Click;
		// 
		// button1
		// 
		button1.Font = new System.Drawing.Font("Segoe UI", 15F);
		button1.Location = new System.Drawing.Point(38, 133);
		button1.Name = "button1";
		button1.Size = new System.Drawing.Size(213, 52);
		button1.TabIndex = 3;
		button1.Text = "Update Advance";
		button1.UseVisualStyleBackColor = true;
		button1.Click += button1_Click;
		// 
		// AdminPanel
		// 
		AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
		AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		ClientSize = new System.Drawing.Size(292, 639);
		Controls.Add(button1);
		Controls.Add(sqlQueryButton);
		Controls.Add(reservationButton);
		Controls.Add(versionLabel);
		Controls.Add(brandingLabel);
		Controls.Add(richTextBoxFooter);
		Controls.Add(advanceButton);
		Controls.Add(paymentModeButton);
		Controls.Add(settingsButton);
		Controls.Add(manageTransactionsButton);
		Controls.Add(managePersonsButton);
		Controls.Add(manageLocationButton);
		Controls.Add(manageUsersButton);
		Icon = ((System.Drawing.Icon)resources.GetObject("$this.Icon"));
		StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
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
	private System.Windows.Forms.Label versionLabel;
	private System.Windows.Forms.Label brandingLabel;
	private System.Windows.Forms.RichTextBox richTextBoxFooter;
	private System.Windows.Forms.Button reservationButton;
	private System.Windows.Forms.Button sqlQueryButton;
}