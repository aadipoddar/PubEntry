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
		slipIdLabel = new System.Windows.Forms.Label();
		slipIdTextBox = new System.Windows.Forms.TextBox();
		goButton = new System.Windows.Forms.Button();
		brandingLabel = new System.Windows.Forms.Label();
		richTextBoxFooter = new System.Windows.Forms.RichTextBox();
		SuspendLayout();
		// 
		// slipIdLabel
		// 
		slipIdLabel.AutoSize = true;
		slipIdLabel.Font = new System.Drawing.Font("Segoe UI", 15F);
		slipIdLabel.Location = new System.Drawing.Point(21, 33);
		slipIdLabel.Name = "slipIdLabel";
		slipIdLabel.Size = new System.Drawing.Size(67, 28);
		slipIdLabel.TabIndex = 37;
		slipIdLabel.Text = "Slip Id";
		// 
		// slipIdTextBox
		// 
		slipIdTextBox.Font = new System.Drawing.Font("Segoe UI", 15F);
		slipIdTextBox.Location = new System.Drawing.Point(126, 30);
		slipIdTextBox.Name = "slipIdTextBox";
		slipIdTextBox.PlaceholderText = "Slip ID";
		slipIdTextBox.Size = new System.Drawing.Size(134, 34);
		slipIdTextBox.TabIndex = 1;
		slipIdTextBox.KeyPress += slipIdTextBox_KeyPress;
		// 
		// goButton
		// 
		goButton.Font = new System.Drawing.Font("Segoe UI", 15F);
		goButton.Location = new System.Drawing.Point(65, 88);
		goButton.Name = "goButton";
		goButton.Size = new System.Drawing.Size(118, 38);
		goButton.TabIndex = 2;
		goButton.Text = "GO";
		goButton.UseVisualStyleBackColor = true;
		goButton.Click += goButton_Click;
		// 
		// brandingLabel
		// 
		brandingLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
		brandingLabel.AutoSize = true;
		brandingLabel.BackColor = System.Drawing.Color.White;
		brandingLabel.Location = new System.Drawing.Point(215, 138);
		brandingLabel.Name = "brandingLabel";
		brandingLabel.Size = new System.Drawing.Size(76, 15);
		brandingLabel.TabIndex = 45;
		brandingLabel.Text = "© AADISOFT";
		// 
		// richTextBoxFooter
		// 
		richTextBoxFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
		richTextBoxFooter.Location = new System.Drawing.Point(0, 132);
		richTextBoxFooter.Name = "richTextBoxFooter";
		richTextBoxFooter.Size = new System.Drawing.Size(291, 24);
		richTextBoxFooter.TabIndex = 44;
		richTextBoxFooter.Text = "";
		// 
		// SlipIdForm
		// 
		AcceptButton = goButton;
		AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
		AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		ClientSize = new System.Drawing.Size(291, 156);
		Controls.Add(brandingLabel);
		Controls.Add(richTextBoxFooter);
		Controls.Add(slipIdLabel);
		Controls.Add(slipIdTextBox);
		Controls.Add(goButton);
		Icon = ((System.Drawing.Icon)resources.GetObject("$this.Icon"));
		MaximizeBox = false;
		MinimizeBox = false;
		StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		Text = "Slip ID";
		Load += SlipIdForm_Load;
		ResumeLayout(false);
		PerformLayout();
	}

	#endregion

	private Label slipIdLabel;
	private TextBox slipIdTextBox;
	private Button goButton;
	private Label brandingLabel;
	private RichTextBox richTextBoxFooter;
}