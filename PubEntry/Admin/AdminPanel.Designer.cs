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
		manageEmployeeButton = new Button();
		manageTransactionsButton = new Button();
		managePersonsButton = new Button();
		settingsButton = new Button();
		SuspendLayout();
		// 
		// manageLocationButton
		// 
		manageLocationButton.Font = new Font("Segoe UI", 15F);
		manageLocationButton.Location = new Point(38, 89);
		manageLocationButton.Name = "manageLocationButton";
		manageLocationButton.Size = new Size(213, 52);
		manageLocationButton.TabIndex = 18;
		manageLocationButton.Text = "Manage Locations";
		manageLocationButton.UseVisualStyleBackColor = true;
		manageLocationButton.Click += manageLocationButton_Click;
		// 
		// manageEmployeeButton
		// 
		manageEmployeeButton.Font = new Font("Segoe UI", 15F);
		manageEmployeeButton.Location = new Point(38, 31);
		manageEmployeeButton.Name = "manageEmployeeButton";
		manageEmployeeButton.Size = new Size(213, 52);
		manageEmployeeButton.TabIndex = 17;
		manageEmployeeButton.Text = "Manage Employees";
		manageEmployeeButton.UseVisualStyleBackColor = true;
		manageEmployeeButton.Click += manageEmployeeButton_Click;
		// 
		// manageTransactionsButton
		// 
		manageTransactionsButton.Font = new Font("Segoe UI", 15F);
		manageTransactionsButton.Location = new Point(38, 205);
		manageTransactionsButton.Name = "manageTransactionsButton";
		manageTransactionsButton.Size = new Size(213, 52);
		manageTransactionsButton.TabIndex = 20;
		manageTransactionsButton.Text = "Manage Transactions";
		manageTransactionsButton.UseVisualStyleBackColor = true;
		// 
		// managePersonsButton
		// 
		managePersonsButton.Font = new Font("Segoe UI", 15F);
		managePersonsButton.Location = new Point(38, 147);
		managePersonsButton.Name = "managePersonsButton";
		managePersonsButton.Size = new Size(213, 52);
		managePersonsButton.TabIndex = 19;
		managePersonsButton.Text = "Manage Persons";
		managePersonsButton.UseVisualStyleBackColor = true;
		// 
		// settingsButton
		// 
		settingsButton.Font = new Font("Segoe UI", 15F);
		settingsButton.Location = new Point(38, 263);
		settingsButton.Name = "settingsButton";
		settingsButton.Size = new Size(213, 52);
		settingsButton.TabIndex = 21;
		settingsButton.Text = "Settings";
		settingsButton.UseVisualStyleBackColor = true;
		// 
		// AdminPanel
		// 
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		ClientSize = new Size(292, 366);
		Controls.Add(settingsButton);
		Controls.Add(manageTransactionsButton);
		Controls.Add(managePersonsButton);
		Controls.Add(manageLocationButton);
		Controls.Add(manageEmployeeButton);
		Name = "AdminPanel";
		StartPosition = FormStartPosition.CenterScreen;
		Text = "AdminPanel";
		ResumeLayout(false);
	}

	#endregion

	private Button manageLocationButton;
	private Button manageEmployeeButton;
	private Button manageTransactionsButton;
	private Button managePersonsButton;
	private Button settingsButton;
}