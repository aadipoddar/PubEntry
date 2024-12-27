namespace PubEntry.Admin;

partial class TransactionForm
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
		numberLabel = new Label();
		numberTextBox = new TextBox();
		goButton = new Button();
		SuspendLayout();
		// 
		// numberLabel
		// 
		numberLabel.AutoSize = true;
		numberLabel.Font = new Font("Segoe UI", 15F);
		numberLabel.Location = new Point(21, 33);
		numberLabel.Name = "numberLabel";
		numberLabel.Size = new Size(67, 28);
		numberLabel.TabIndex = 37;
		numberLabel.Text = "Slip Id";
		// 
		// numberTextBox
		// 
		numberTextBox.Font = new Font("Segoe UI", 15F);
		numberTextBox.Location = new Point(126, 30);
		numberTextBox.Name = "numberTextBox";
		numberTextBox.PlaceholderText = "Slip ID";
		numberTextBox.Size = new Size(134, 34);
		numberTextBox.TabIndex = 36;
		// 
		// goButton
		// 
		goButton.Font = new Font("Segoe UI", 15F);
		goButton.Location = new Point(65, 88);
		goButton.Name = "goButton";
		goButton.Size = new Size(118, 38);
		goButton.TabIndex = 32;
		goButton.Text = "GO";
		goButton.UseVisualStyleBackColor = true;
		goButton.Click += goButton_Click;
		// 
		// TransactionForm
		// 
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		ClientSize = new Size(291, 158);
		Controls.Add(numberLabel);
		Controls.Add(numberTextBox);
		Controls.Add(goButton);
		Name = "TransactionForm";
		Text = "TransactionForm";
		ResumeLayout(false);
		PerformLayout();
	}

	#endregion

	private Label numberLabel;
	private TextBox numberTextBox;
	private Label loyaltyLabel;
	private ComboBox loyaltyComboBox;
	private Button goButton;
	private Label nameLabel;
	private TextBox employeeNameTextBox;
	private Label label1;
	private TextBox textBox1;
}