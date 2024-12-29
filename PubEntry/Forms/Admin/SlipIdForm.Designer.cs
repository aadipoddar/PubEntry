namespace PubEntry.Admin;

partial class SlipIdForm
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
		slipIdLabel = new Label();
		slipIdTextBox = new TextBox();
		goButton = new Button();
		SuspendLayout();
		// 
		// slipIdLabel
		// 
		slipIdLabel.AutoSize = true;
		slipIdLabel.Font = new Font("Segoe UI", 15F);
		slipIdLabel.Location = new Point(21, 33);
		slipIdLabel.Name = "slipIdLabel";
		slipIdLabel.Size = new Size(67, 28);
		slipIdLabel.TabIndex = 37;
		slipIdLabel.Text = "Slip Id";
		// 
		// slipIdTextBox
		// 
		slipIdTextBox.Font = new Font("Segoe UI", 15F);
		slipIdTextBox.Location = new Point(126, 30);
		slipIdTextBox.Name = "slipIdTextBox";
		slipIdTextBox.PlaceholderText = "Slip ID";
		slipIdTextBox.Size = new Size(134, 34);
		slipIdTextBox.TabIndex = 1;
		slipIdTextBox.KeyPress += slipIdTextBox_KeyPress;
		// 
		// goButton
		// 
		goButton.Font = new Font("Segoe UI", 15F);
		goButton.Location = new Point(65, 88);
		goButton.Name = "goButton";
		goButton.Size = new Size(118, 38);
		goButton.TabIndex = 2;
		goButton.Text = "GO";
		goButton.UseVisualStyleBackColor = true;
		goButton.Click += goButton_Click;
		// 
		// SlipIdForm
		// 
		AcceptButton = goButton;
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		ClientSize = new Size(291, 158);
		Controls.Add(slipIdLabel);
		Controls.Add(slipIdTextBox);
		Controls.Add(goButton);
		Name = "SlipIdForm";
		StartPosition = FormStartPosition.CenterScreen;
		Text = "TransactionForm";
		ResumeLayout(false);
		PerformLayout();
	}

	#endregion

	private Label slipIdLabel;
	private TextBox slipIdTextBox;
	private Label loyaltyLabel;
	private ComboBox loyaltyComboBox;
	private Button goButton;
	private Label nameLabel;
	private TextBox employeeNameTextBox;
	private Label label1;
	private TextBox textBox1;
}