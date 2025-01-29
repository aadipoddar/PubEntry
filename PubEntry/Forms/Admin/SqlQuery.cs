using System.Data;
using System.Reflection;

using Microsoft.Data.SqlClient;

using Syncfusion.Windows.Forms.Edit.Enums;

namespace PubEntry.Forms.Admin;

public partial class SqlQuery : Form
{
	public SqlQuery()
	{
		InitializeComponent();
		queryEditControl.ApplyConfiguration(KnownLanguages.SQL);
		richTextBoxFooter.Text = $"Version: {Assembly.GetExecutingAssembly().GetName().Version}";
	}


	private async void queryEditControl_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F5)
			await RunQuery();
	}

	private async void runButton_Click(object sender, EventArgs e) => await RunQuery();

	private async Task RunQuery()
	{
		tabControl1.TabPages.Clear();
		var selectedText = queryEditControl.SelectedText;
		if (string.IsNullOrWhiteSpace(selectedText))
			selectedText = queryEditControl.Text;
		var queries = selectedText.Split([";"], StringSplitOptions.RemoveEmptyEntries);
		await ExecuteQueries(queries);
	}

	private async Task ExecuteQueries(string[] queries)
	{
		SqlConnection connection = new(ConnectionStrings.Local);
		await connection.OpenAsync();

		foreach (var query in queries)
		{
			await using SqlCommand command = new(query.Trim(), connection);
			try
			{
				await using var reader = await command.ExecuteReaderAsync();
				if (reader.HasRows)
				{
					DataTable dt = new();

					for (var i = 0; i < reader.FieldCount; i++)
						dt.Columns.Add(reader.GetName(i));

					while (reader.Read())
					{
						var row = dt.NewRow();
						for (var i = 0; i < reader.FieldCount; i++)
							row[i] = reader[i];
						dt.Rows.Add(row);
					}

					TabPage tabPage = new($"Query {queries.ToList().IndexOf(query) + 1}");
					DataGridView dgv = new()
					{
						Dock = DockStyle.Fill,
						DataSource = dt,
						ReadOnly = true,
						AllowUserToAddRows = false,
						AllowUserToDeleteRows = false
					};
					tabPage.Controls.Add(dgv);
					tabControl1.TabPages.Add(tabPage);
				}
			}
			catch (SqlException ex)
			{
				TabPage tabPage = new($"Query {queries.ToList().IndexOf(query) + 1}");
				TextBox textBox = new()
				{
					Dock = DockStyle.Fill,
					Multiline = true,
					Text = $"""
					        Error executing query:
					        {query}

					        {ex.Message}
					        """,
					ReadOnly = true
				};
				tabPage.Controls.Add(textBox);
				tabControl1.TabPages.Add(tabPage);
			}
		}
	}
}
