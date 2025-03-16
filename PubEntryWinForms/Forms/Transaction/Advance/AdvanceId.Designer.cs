using System.ComponentModel;

namespace PubEntry.Forms.Transaction.Advance;

partial class AdvanceId
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private IContainer components = null;

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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdvanceId));
		brandingLabel = new System.Windows.Forms.Label();
		richTextBoxFooter = new System.Windows.Forms.RichTextBox();
		advanceIdLabel = new System.Windows.Forms.Label();
		advanceIdTextBox = new System.Windows.Forms.TextBox();
		goButton = new System.Windows.Forms.Button();
		SuspendLayout();
		// 
		// brandingLabel
		// 
		brandingLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
		brandingLabel.AutoSize = true;
		brandingLabel.BackColor = System.Drawing.Color.White;
		brandingLabel.Location = new System.Drawing.Point(194, 135);
		brandingLabel.Name = "brandingLabel";
		brandingLabel.Size = new System.Drawing.Size(76, 15);
		brandingLabel.TabIndex = 51;
		brandingLabel.Text = "© AADISOFT";
		// 
		// richTextBoxFooter
		// 
		richTextBoxFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
		richTextBoxFooter.Location = new System.Drawing.Point(0, 128);
		richTextBoxFooter.Name = "richTextBoxFooter";
		richTextBoxFooter.Size = new System.Drawing.Size(273, 24);
		richTextBoxFooter.TabIndex = 50;
		richTextBoxFooter.Text = "";
		// 
		// advanceIdLabel
		// 
		advanceIdLabel.AutoSize = true;
		advanceIdLabel.Font = new System.Drawing.Font("Segoe UI", 15F);
		advanceIdLabel.Location = new System.Drawing.Point(11, 18);
		advanceIdLabel.Name = "advanceIdLabel";
		advanceIdLabel.Size = new System.Drawing.Size(109, 28);
		advanceIdLabel.TabIndex = 49;
		advanceIdLabel.Text = "Advance Id";
		// 
		// advanceIdTextBox
		// 
		advanceIdTextBox.Font = new System.Drawing.Font("Segoe UI", 15F);
		advanceIdTextBox.Location = new System.Drawing.Point(126, 15);
		advanceIdTextBox.Name = "advanceIdTextBox";
		advanceIdTextBox.PlaceholderText = "Advance Id";
		advanceIdTextBox.Size = new System.Drawing.Size(134, 34);
		advanceIdTextBox.TabIndex = 47;
		advanceIdTextBox.KeyPress += advanceIdTextBox_KeyPress;
		// 
		// goButton
		// 
		goButton.Font = new System.Drawing.Font("Segoe UI", 15F);
		goButton.Location = new System.Drawing.Point(65, 73);
		goButton.Name = "goButton";
		goButton.Size = new System.Drawing.Size(118, 38);
		goButton.TabIndex = 48;
		goButton.Text = "GO";
		goButton.UseVisualStyleBackColor = true;
		goButton.Click += goButton_Click;
		// 
		// AdvanceId
		// 
		AcceptButton = goButton;
		AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
		AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		ClientSize = new System.Drawing.Size(273, 152);
		Controls.Add(brandingLabel);
		Controls.Add(richTextBoxFooter);
		Controls.Add(advanceIdLabel);
		Controls.Add(advanceIdTextBox);
		Controls.Add(goButton);
		Icon = ((System.Drawing.Icon)resources.GetObject("$this.Icon"));
		StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		Text = "AdvanceId";
		Load += AdvanceId_Load;
		ResumeLayout(false);
		PerformLayout();
	}

	#endregion

	private Label brandingLabel;
	private RichTextBox richTextBoxFooter;
	private Label advanceIdLabel;
	private System.Windows.Forms.TextBox advanceIdTextBox;
	private System.Windows.Forms.Button goButton;
}