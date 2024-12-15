﻿namespace PubEntry.Reports;

partial class LoadingScreen
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
		loadingLabel = new Label();
		SuspendLayout();
		// 
		// loadingLabel
		// 
		loadingLabel.AutoSize = true;
		loadingLabel.Font = new Font("Segoe UI", 20F);
		loadingLabel.Location = new Point(50, 62);
		loadingLabel.Name = "loadingLabel";
		loadingLabel.Size = new Size(429, 37);
		loadingLabel.TabIndex = 0;
		loadingLabel.Text = "Please Wait Data is Being Loaded...";
		// 
		// LoadingScreen
		// 
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		ClientSize = new Size(577, 180);
		Controls.Add(loadingLabel);
		Name = "LoadingScreen";
		StartPosition = FormStartPosition.CenterScreen;
		Text = "LoadingScreen";
		ResumeLayout(false);
		PerformLayout();
	}

	#endregion

	private Label loadingLabel;
}