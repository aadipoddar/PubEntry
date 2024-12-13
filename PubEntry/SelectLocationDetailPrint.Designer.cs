namespace PubEntry;

partial class SelectLocationDetailPrint
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
		goButton = new Button();
		SuspendLayout();
		// 
		// locationComboBox
		// 
		locationComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
		locationComboBox.Font = new Font("Segoe UI", 15F);
		locationComboBox.FormattingEnabled = true;
		locationComboBox.Location = new Point(31, 29);
		locationComboBox.Name = "locationComboBox";
		locationComboBox.Size = new Size(271, 36);
		locationComboBox.TabIndex = 11;
		// 
		// goButton
		// 
		goButton.Font = new Font("Segoe UI", 15F);
		goButton.Location = new Point(111, 94);
		goButton.Name = "goButton";
		goButton.Size = new Size(118, 38);
		goButton.TabIndex = 14;
		goButton.Text = "GO";
		goButton.UseVisualStyleBackColor = true;
		goButton.Click += goButton_Click;
		// 
		// SelectLocationDetailPrint
		// 
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		ClientSize = new Size(346, 167);
		Controls.Add(goButton);
		Controls.Add(locationComboBox);
		Name = "SelectLocationDetailPrint";
		Text = "SelectLocationDetailPrint";
		ResumeLayout(false);
	}

	#endregion

	private ComboBox locationComboBox;
	private Button goButton;
}