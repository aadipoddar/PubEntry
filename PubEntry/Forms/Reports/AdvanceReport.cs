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
		totalDataGridView.DataSource = await AdvanceData.LoadTotalsByAdvanceTakenON(_takenOn, _locationId);
		foreach (DataGridViewColumn column in totalDataGridView.Columns)
		{
			if (column.Index == 0)
				column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			else column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
		}

		advanceDataGridView.DataSource = await PrintData.LoadAdvancesByTakenOnAndLocation(_takenOn, _locationId);
		foreach (DataGridViewColumn column in advanceDataGridView.Columns)
			if (new[] { 0, 7, 8, 10, 11, 13 }.Contains(column.Index))
				column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
	}
}
