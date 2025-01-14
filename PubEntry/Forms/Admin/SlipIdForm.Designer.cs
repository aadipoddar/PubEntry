namespace PubEntry.Forms.Admin;

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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SlipIdForm));
		slipIdLabel = new Label();
		slipIdTextBox = new TextBox();
		goButton = new Button();
		versionLabel = new Label();
		brandingLabel = new Label();
		richTextBoxFooter = new RichTextBox();
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
		// versionLabel
		// 
		versionLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
		versionLabel.AutoSize = true;
		versionLabel.BackColor = Color.White;
		versionLabel.Location = new Point(4, 137);
		versionLabel.Name = "versionLabel";
		versionLabel.Size = new Size(84, 15);
		versionLabel.TabIndex = 46;
		versionLabel.Text = "Version: 0.0.0.0";
		// 
		// brandingLabel
		// 
		brandingLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
		brandingLabel.AutoSize = true;
		brandingLabel.BackColor = Color.White;
		brandingLabel.Location = new Point(215, 138);
		brandingLabel.Name = "brandingLabel";
		brandingLabel.Size = new Size(76, 15);
		brandingLabel.TabIndex = 45;
		brandingLabel.Text = "© AADISOFT";
		// 
		// richTextBoxFooter
		// 
		richTextBoxFooter.Dock = DockStyle.Bottom;
		richTextBoxFooter.Location = new Point(0, 132);
		richTextBoxFooter.Name = "richTextBoxFooter";
		richTextBoxFooter.Size = new Size(291, 24);
		richTextBoxFooter.TabIndex = 44;
		richTextBoxFooter.Text = "";
		// 
		// SlipIdForm
		// 
		AcceptButton = goButton;
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		ClientSize = new Size(291, 156);
		Controls.Add(versionLabel);
		Controls.Add(brandingLabel);
		Controls.Add(richTextBoxFooter);
		Controls.Add(slipIdLabel);
		Controls.Add(slipIdTextBox);
		Controls.Add(goButton);
		Icon = (Icon)resources.GetObject("$this.Icon");
		MaximizeBox = false;
		MinimizeBox = false;
		Name = "SlipIdForm";
		StartPosition = FormStartPosition.CenterScreen;
		Text = "Slip ID";
		Load += SlipIdForm_Load;
		ResumeLayout(false);
		PerformLayout();
	}

	#endregion

	private Label slipIdLabel;
	private TextBox slipIdTextBox;
	private Button goButton;
	private Label versionLabel;
	private Label brandingLabel;
	private RichTextBox richTextBoxFooter;
}