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
		passwordTextBox = new TextBox();
		brandingLabel = new Label();
		richTextBoxFooter = new RichTextBox();
		newEmployeeButton = new Button();
		reportsButton = new Button();
		SuspendLayout();
		// 
		// locationComboBox
		// 
		locationComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
		locationComboBox.Font = new Font("Segoe UI", 15F);
		locationComboBox.FormattingEnabled = true;
		locationComboBox.Location = new Point(29, 33);
		locationComboBox.Name = "locationComboBox";
		locationComboBox.Size = new Size(271, 36);
		locationComboBox.TabIndex = 10;
		locationComboBox.SelectedIndexChanged += locationComboBox_SelectedIndexChanged;
		// 
		// employeeComboBox
		// 
		employeeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
		employeeComboBox.Font = new Font("Segoe UI", 15F);
		employeeComboBox.FormattingEnabled = true;
		employeeComboBox.Location = new Point(29, 99);
		employeeComboBox.Name = "employeeComboBox";
		employeeComboBox.Size = new Size(271, 36);
		employeeComboBox.TabIndex = 11;
		employeeComboBox.SelectedIndexChanged += employeeComboBox_SelectedIndexChanged;
		// 
		// goButton
		// 
		goButton.Font = new Font("Segoe UI", 15F);
		goButton.Location = new Point(99, 225);
		goButton.Name = "goButton";
		goButton.Size = new Size(118, 38);
		goButton.TabIndex = 13;
		goButton.Text = "GO";
		goButton.UseVisualStyleBackColor = true;
		goButton.Click += goButton_Click;
		// 
		// passwordTextBox
		// 
		passwordTextBox.Font = new Font("Segoe UI", 18F);
		passwordTextBox.Location = new Point(29, 165);
		passwordTextBox.Name = "passwordTextBox";
		passwordTextBox.PasswordChar = '*';
		passwordTextBox.PlaceholderText = "Password";
		passwordTextBox.Size = new Size(271, 39);
		passwordTextBox.TabIndex = 12;
		// 
		// brandingLabel
		// 
		brandingLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
		brandingLabel.AutoSize = true;
		brandingLabel.BackColor = Color.White;
		brandingLabel.Location = new Point(257, 428);
		brandingLabel.Name = "brandingLabel";
		brandingLabel.Size = new Size(76, 15);
		brandingLabel.TabIndex = 30;
		brandingLabel.Text = "© AADISOFT";
		// 
		// richTextBoxFooter
		// 
		richTextBoxFooter.Dock = DockStyle.Bottom;
		richTextBoxFooter.Location = new Point(0, 422);
		richTextBoxFooter.Name = "richTextBoxFooter";
		richTextBoxFooter.Size = new Size(336, 26);
		richTextBoxFooter.TabIndex = 29;
		richTextBoxFooter.Text = "";
		// 
		// newEmployeeButton
		// 
		newEmployeeButton.Font = new Font("Segoe UI", 15F);
		newEmployeeButton.Location = new Point(70, 355);
		newEmployeeButton.Name = "newEmployeeButton";
		newEmployeeButton.Size = new Size(188, 38);
		newEmployeeButton.TabIndex = 15;
		newEmployeeButton.Text = "New Employee";
		newEmployeeButton.UseVisualStyleBackColor = true;
		newEmployeeButton.Click += newEmployeeButton_Click;
		// 
		// reportsButton
		// 
		reportsButton.Font = new Font("Segoe UI", 15F);
		reportsButton.Location = new Point(70, 290);
		reportsButton.Name = "reportsButton";
		reportsButton.Size = new Size(188, 38);
		reportsButton.TabIndex = 14;
		reportsButton.Text = "Reports";
		reportsButton.UseVisualStyleBackColor = true;
		reportsButton.Click += reportsButton_Click;
		// 
		// SelectLocation
		// 
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		ClientSize = new Size(336, 448);
		Controls.Add(reportsButton);
		Controls.Add(newEmployeeButton);
		Controls.Add(brandingLabel);
		Controls.Add(richTextBoxFooter);
		Controls.Add(passwordTextBox);
		Controls.Add(goButton);
		Controls.Add(employeeComboBox);
		Controls.Add(locationComboBox);
		Name = "SelectLocation";
		StartPosition = FormStartPosition.CenterScreen;
		Text = "Select Location";
		Load += SelectLocation_Load;
		ResumeLayout(false);
		PerformLayout();
	}

	#endregion

	private ComboBox locationComboBox;
	private ComboBox employeeComboBox;
	private Button goButton;
	private TextBox passwordTextBox;
	private Label brandingLabel;
	private RichTextBox richTextBoxFooter;
	private Button newEmployeeButton;
	private Button reportsButton;
}