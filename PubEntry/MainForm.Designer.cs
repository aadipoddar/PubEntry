namespace PubEntry;

partial class MainForm
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
		nameTextBox = new TextBox();
		nameLabel = new Label();
		numberLabel = new Label();
		numberTextBox = new TextBox();
		amountLabel = new Label();
		amountTextBox = new TextBox();
		paymentMethodLabel = new Label();
		paymentMethodComboBox = new ComboBox();
		insertButton = new Button();
		SuspendLayout();
		// 
		// nameTextBox
		// 
		nameTextBox.Font = new Font("Segoe UI", 15F);
		nameTextBox.Location = new Point(266, 42);
		nameTextBox.Name = "nameTextBox";
		nameTextBox.PlaceholderText = "Name";
		nameTextBox.Size = new Size(271, 34);
		nameTextBox.TabIndex = 0;
		// 
		// nameLabel
		// 
		nameLabel.AutoSize = true;
		nameLabel.Font = new Font("Segoe UI", 15F);
		nameLabel.Location = new Point(94, 45);
		nameLabel.Name = "nameLabel";
		nameLabel.Size = new Size(64, 28);
		nameLabel.TabIndex = 1;
		nameLabel.Text = "Name";
		// 
		// numberLabel
		// 
		numberLabel.AutoSize = true;
		numberLabel.Font = new Font("Segoe UI", 15F);
		numberLabel.Location = new Point(94, 103);
		numberLabel.Name = "numberLabel";
		numberLabel.Size = new Size(84, 28);
		numberLabel.TabIndex = 3;
		numberLabel.Text = "Number";
		// 
		// numberTextBox
		// 
		numberTextBox.Font = new Font("Segoe UI", 15F);
		numberTextBox.Location = new Point(266, 100);
		numberTextBox.Name = "numberTextBox";
		numberTextBox.PlaceholderText = "Number";
		numberTextBox.Size = new Size(271, 34);
		numberTextBox.TabIndex = 2;
		numberTextBox.TextChanged += numberTextBox_TextChanged;
		// 
		// amountLabel
		// 
		amountLabel.AutoSize = true;
		amountLabel.Font = new Font("Segoe UI", 15F);
		amountLabel.Location = new Point(94, 161);
		amountLabel.Name = "amountLabel";
		amountLabel.Size = new Size(83, 28);
		amountLabel.TabIndex = 5;
		amountLabel.Text = "Amount";
		// 
		// amountTextBox
		// 
		amountTextBox.Font = new Font("Segoe UI", 15F);
		amountTextBox.Location = new Point(266, 158);
		amountTextBox.Name = "amountTextBox";
		amountTextBox.PlaceholderText = "Amount";
		amountTextBox.Size = new Size(271, 34);
		amountTextBox.TabIndex = 4;
		// 
		// paymentMethodLabel
		// 
		paymentMethodLabel.AutoSize = true;
		paymentMethodLabel.Font = new Font("Segoe UI", 15F);
		paymentMethodLabel.Location = new Point(94, 219);
		paymentMethodLabel.Name = "paymentMethodLabel";
		paymentMethodLabel.Size = new Size(162, 28);
		paymentMethodLabel.TabIndex = 7;
		paymentMethodLabel.Text = "Payment Method";
		// 
		// paymentMethodComboBox
		// 
		paymentMethodComboBox.Font = new Font("Segoe UI", 15F);
		paymentMethodComboBox.FormattingEnabled = true;
		paymentMethodComboBox.Location = new Point(266, 216);
		paymentMethodComboBox.Name = "paymentMethodComboBox";
		paymentMethodComboBox.Size = new Size(271, 36);
		paymentMethodComboBox.TabIndex = 8;
		// 
		// insertButton
		// 
		insertButton.Font = new Font("Segoe UI", 15F);
		insertButton.Location = new Point(256, 284);
		insertButton.Name = "insertButton";
		insertButton.Size = new Size(135, 44);
		insertButton.TabIndex = 13;
		insertButton.Text = "INSERT";
		insertButton.UseVisualStyleBackColor = true;
		insertButton.Click += insertButton_Click;
		// 
		// MainForm
		// 
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		ClientSize = new Size(711, 340);
		Controls.Add(insertButton);
		Controls.Add(paymentMethodComboBox);
		Controls.Add(paymentMethodLabel);
		Controls.Add(amountLabel);
		Controls.Add(amountTextBox);
		Controls.Add(numberLabel);
		Controls.Add(numberTextBox);
		Controls.Add(nameLabel);
		Controls.Add(nameTextBox);
		Name = "MainForm";
		Text = "MainForm";
		FormClosed += MainForm_FormClosed;
		ResumeLayout(false);
		PerformLayout();
	}

	#endregion

	private TextBox nameTextBox;
	private Label nameLabel;
	private Label numberLabel;
	private TextBox numberTextBox;
	private Label amountLabel;
	private TextBox amountTextBox;
	private Label paymentMethodLabel;
	private ComboBox paymentMethodComboBox;
	private Button insertButton;
}