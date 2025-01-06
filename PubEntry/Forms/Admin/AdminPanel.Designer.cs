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
		manageLocationButton = new Button();
		manageUsersButton = new Button();
		manageTransactionsButton = new Button();
		managePersonsButton = new Button();
		settingsButton = new Button();
		paymentModeButton = new Button();
		advanceButton = new Button();
		SuspendLayout();
		// 
		// manageLocationButton
		// 
		manageLocationButton.Font = new Font("Segoe UI", 15F);
		manageLocationButton.Location = new Point(38, 191);
		manageLocationButton.Name = "manageLocationButton";
		manageLocationButton.Size = new Size(213, 52);
		manageLocationButton.TabIndex = 4;
		manageLocationButton.Text = "Manage Locations";
		manageLocationButton.UseVisualStyleBackColor = true;
		manageLocationButton.Click += manageLocationButton_Click;
		// 
		// manageUsersButton
		// 
		manageUsersButton.Font = new Font("Segoe UI", 15F);
		manageUsersButton.Location = new Point(38, 133);
		manageUsersButton.Name = "manageUsersButton";
		manageUsersButton.Size = new Size(213, 52);
		manageUsersButton.TabIndex = 3;
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
		managePersonsButton.Location = new Point(38, 249);
		managePersonsButton.Name = "managePersonsButton";
		managePersonsButton.Size = new Size(213, 52);
		managePersonsButton.TabIndex = 5;
		managePersonsButton.Text = "Manage Persons";
		managePersonsButton.UseVisualStyleBackColor = true;
		managePersonsButton.Click += managePersonsButton_Click;
		// 
		// settingsButton
		// 
		settingsButton.Font = new Font("Segoe UI", 15F);
		settingsButton.Location = new Point(38, 365);
		settingsButton.Name = "settingsButton";
		settingsButton.Size = new Size(213, 52);
		settingsButton.TabIndex = 7;
		settingsButton.Text = "Settings";
		settingsButton.UseVisualStyleBackColor = true;
		// 
		// paymentModeButton
		// 
		paymentModeButton.Font = new Font("Segoe UI", 15F);
		paymentModeButton.Location = new Point(38, 307);
		paymentModeButton.Name = "paymentModeButton";
		paymentModeButton.Size = new Size(213, 52);
		paymentModeButton.TabIndex = 6;
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
		// AdminPanel
		// 
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		ClientSize = new Size(292, 439);
		Controls.Add(advanceButton);
		Controls.Add(paymentModeButton);
		Controls.Add(settingsButton);
		Controls.Add(manageTransactionsButton);
		Controls.Add(managePersonsButton);
		Controls.Add(manageLocationButton);
		Controls.Add(manageUsersButton);
		Name = "AdminPanel";
		StartPosition = FormStartPosition.CenterScreen;
		Text = "AdminPanel";
		ResumeLayout(false);
	}

	#endregion

	private Button manageLocationButton;
	private Button manageUsersButton;
	private Button manageTransactionsButton;
	private Button managePersonsButton;
	private Button settingsButton;
	private Button paymentModeButton;
	private Button advanceButton;
}