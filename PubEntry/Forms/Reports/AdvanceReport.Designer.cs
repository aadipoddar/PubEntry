namespace PubEntry.Forms.Reports;

partial class AdvanceReport
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
		totalDataGridView = new DataGridView();
		advanceDataGridView = new DataGridView();
		((System.ComponentModel.ISupportInitialize)totalDataGridView).BeginInit();
		((System.ComponentModel.ISupportInitialize)advanceDataGridView).BeginInit();
		SuspendLayout();
		// 
		// totalDataGridView
		// 
		totalDataGridView.AllowUserToAddRows = false;
		totalDataGridView.AllowUserToDeleteRows = false;
		totalDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		totalDataGridView.Location = new Point(12, 435);
		totalDataGridView.Name = "totalDataGridView";
		totalDataGridView.ReadOnly = true;
		totalDataGridView.Size = new Size(251, 150);
		totalDataGridView.TabIndex = 0;
		// 
		// advanceDataGridView
		// 
		advanceDataGridView.AllowUserToAddRows = false;
		advanceDataGridView.AllowUserToDeleteRows = false;
		advanceDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		advanceDataGridView.Location = new Point(12, 12);
		advanceDataGridView.Name = "advanceDataGridView";
		advanceDataGridView.ReadOnly = true;
		advanceDataGridView.Size = new Size(1083, 417);
		advanceDataGridView.TabIndex = 1;
		// 
		// AdvanceReport
		// 
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		ClientSize = new Size(1107, 597);
		Controls.Add(advanceDataGridView);
		Controls.Add(totalDataGridView);
		Name = "AdvanceReport";
		Text = "AdvanceReport";
		Load += AdvanceReport_Load;
		((System.ComponentModel.ISupportInitialize)totalDataGridView).EndInit();
		((System.ComponentModel.ISupportInitialize)advanceDataGridView).EndInit();
		ResumeLayout(false);
	}

	#endregion

	private DataGridView totalDataGridView;
	private DataGridView advanceDataGridView;
}