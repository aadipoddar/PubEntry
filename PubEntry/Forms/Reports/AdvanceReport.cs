using System.Reflection;

namespace PubEntry.Forms.Reports;

public partial class AdvanceReport : Form
{
	private readonly DateTime _takenOn;
	private readonly int _locationId;

	public AdvanceReport(DateTime takenOn, int locationId)
	{
		_takenOn = takenOn;
		_locationId = locationId;

		InitializeComponent();
	}

	private void AdvanceReport_Load(object sender, EventArgs e) => LoadData();

	private async void LoadData()
	{
		advanceDataGridView.DataSource = await AdvanceData.LoadAdvancesByTakenOnLocation(_takenOn, _locationId);
		foreach (DataGridViewColumn column in advanceDataGridView.Columns)
			if (new[] { 0, 7, 8, 10, 11, 13 }.Contains(column.Index))
				column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

		totalDataGridView.DataSource = await AdvanceData.LoadAdvancePaymentModeTotalsByTakenOn(_takenOn, _locationId);
		foreach (DataGridViewColumn column in totalDataGridView.Columns)
		{
			if (column.Index == 0)
				column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			else column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
		}

		versionLabel.Text = $"Version: {Assembly.GetExecutingAssembly().GetName().Version}";
	}
}
