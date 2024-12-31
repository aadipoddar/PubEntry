namespace PubEntry.Admin;

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
		advanceButton = new Button();
		paymentModesButton = new Button();
		SuspendLayout();
		// 
		// manageLocationButton
		// 
		manageLocationButton.Font = new Font("Segoe UI", 15F);
		manageLocationButton.Location = new Point(39, 136);
		manageLocationButton.Name = "manageLocationButton";
		manageLocationButton.Size = new Size(213, 52);
		manageLocationButton.TabIndex = 3;
		manageLocationButton.Text = "Manage Locations";
		manageLocationButton.UseVisualStyleBackColor = true;
		manageLocationButton.Click += manageLocationButton_Click;
		// 
		// manageUsersButton
		// 
		manageUsersButton.Font = new Font("Segoe UI", 15F);
		manageUsersButton.Location = new Point(39, 78);
		manageUsersButton.Name = "manageUsersButton";
		manageUsersButton.Size = new Size(213, 52);
		manageUsersButton.TabIndex = 2;
		manageUsersButton.Text = "Manage Users";
		manageUsersButton.UseVisualStyleBackColor = true;
		manageUsersButton.Click += manageUsersButton_Click;
		// 
		// manageTransactionsButton
		// 
		manageTransactionsButton.Font = new Font("Segoe UI", 15F);
		manageTransactionsButton.Location = new Point(39, 252);
		manageTransactionsButton.Name = "manageTransactionsButton";
		manageTransactionsButton.Size = new Size(213, 52);
		manageTransactionsButton.TabIndex = 5;
		manageTransactionsButton.Text = "Manage Transactions";
		manageTransactionsButton.UseVisualStyleBackColor = true;
		manageTransactionsButton.Click += manageTransactionsButton_Click;
		// 
		// managePersonsButton
		// 
		managePersonsButton.Font = new Font("Segoe UI", 15F);
		managePersonsButton.Location = new Point(39, 194);
		managePersonsButton.Name = "managePersonsButton";
		managePersonsButton.Size = new Size(213, 52);
		managePersonsButton.TabIndex = 4;
		managePersonsButton.Text = "Manage Persons";
		managePersonsButton.UseVisualStyleBackColor = true;
		managePersonsButton.Click += managePersonsButton_Click;
		// 
		// settingsButton
		// 
		settingsButton.Font = new Font("Segoe UI", 15F);
		settingsButton.Location = new Point(39, 368);
		settingsButton.Name = "settingsButton";
		settingsButton.Size = new Size(213, 52);
		settingsButton.TabIndex = 6;
		settingsButton.Text = "Settings";
		settingsButton.UseVisualStyleBackColor = true;
		// 
		// advanceButton
		// 
		advanceButton.Font = new Font("Segoe UI", 15F);
		advanceButton.Location = new Point(39, 20);
		advanceButton.Name = "advanceButton";
		advanceButton.Size = new Size(213, 52);
		advanceButton.TabIndex = 1;
		advanceButton.Text = "Advance Entry";
		advanceButton.UseVisualStyleBackColor = true;
		advanceButton.Click += advanceButton_Click;
		// 
		// paymentModesButton
		// 
		paymentModesButton.Font = new Font("Segoe UI", 15F);
		paymentModesButton.Location = new Point(39, 310);
		paymentModesButton.Name = "paymentModesButton";
		paymentModesButton.Size = new Size(213, 52);
		paymentModesButton.TabIndex = 7;
		paymentModesButton.Text = "Payment Modes";
		paymentModesButton.UseVisualStyleBackColor = true;
		paymentModesButton.Click += paymentModesButton_Click;
		// 
		// AdminPanel
		// 
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		ClientSize = new Size(292, 461);
		Controls.Add(paymentModesButton);
		Controls.Add(advanceButton);
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
	private Button advanceButton;
	private Button paymentModesButton;
}