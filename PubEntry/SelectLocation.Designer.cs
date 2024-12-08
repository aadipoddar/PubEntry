namespace PubEntry;

partial class SelectLocation
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
		locationComboBox = new ComboBox();
		employeeComboBox = new ComboBox();
		goButton = new Button();
		finalPrintButton = new Button();
		passwordTextBox = new TextBox();
		SuspendLayout();
		// 
		// locationComboBox
		// 
		locationComboBox.Font = new Font("Segoe UI", 15F);
		locationComboBox.FormattingEnabled = true;
		locationComboBox.Location = new Point(90, 52);
		locationComboBox.Name = "locationComboBox";
		locationComboBox.Size = new Size(271, 36);
		locationComboBox.TabIndex = 10;
		// 
		// employeeComboBox
		// 
		employeeComboBox.Font = new Font("Segoe UI", 15F);
		employeeComboBox.FormattingEnabled = true;
		employeeComboBox.Location = new Point(90, 118);
		employeeComboBox.Name = "employeeComboBox";
		employeeComboBox.Size = new Size(271, 36);
		employeeComboBox.TabIndex = 11;
		// 
		// goButton
		// 
		goButton.Font = new Font("Segoe UI", 15F);
		goButton.Location = new Point(90, 253);
		goButton.Name = "goButton";
		goButton.Size = new Size(118, 38);
		goButton.TabIndex = 13;
		goButton.Text = "GO";
		goButton.UseVisualStyleBackColor = true;
		goButton.Click += goButton_Click;
		// 
		// finalPrintButton
		// 
		finalPrintButton.Font = new Font("Segoe UI", 15F);
		finalPrintButton.Location = new Point(243, 253);
		finalPrintButton.Name = "finalPrintButton";
		finalPrintButton.Size = new Size(118, 38);
		finalPrintButton.TabIndex = 14;
		finalPrintButton.Text = "Final Print";
		finalPrintButton.UseVisualStyleBackColor = true;
		finalPrintButton.Click += finalPrintButton_Click;
		// 
		// passwordTextBox
		// 
		passwordTextBox.Font = new Font("Segoe UI", 18F);
		passwordTextBox.Location = new Point(90, 184);
		passwordTextBox.Name = "passwordTextBox";
		passwordTextBox.PasswordChar = '*';
		passwordTextBox.PlaceholderText = "Password";
		passwordTextBox.Size = new Size(271, 39);
		passwordTextBox.TabIndex = 12;
		// 
		// SelectLocation
		// 
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		ClientSize = new Size(487, 325);
		Controls.Add(passwordTextBox);
		Controls.Add(finalPrintButton);
		Controls.Add(goButton);
		Controls.Add(employeeComboBox);
		Controls.Add(locationComboBox);
		Name = "SelectLocation";
		StartPosition = FormStartPosition.CenterScreen;
		Text = "Select Location";
		ResumeLayout(false);
		PerformLayout();
	}

	#endregion

	private ComboBox locationComboBox;
	private ComboBox employeeComboBox;
	private Button goButton;
	private Button finalPrintButton;
	private TextBox passwordTextBox;
}