using System.Drawing.Printing;
using System.Globalization;

using PubEntryLibrary.Data;
using PubEntryLibrary.Models;

namespace PubEntry;

public partial class ShowDataForm : Form
{
	DateTimePicker fromDateTimePicker, toDateTimePicker;
	TextBox fromTimeTextBox, toTimeTextBox;

	public ShowDataForm(DateTimePicker fromDateTimePicker, DateTimePicker toDateTimePicker, TextBox fromTimeTextBox, TextBox toTimeTextBox)
	{
		this.fromDateTimePicker = fromDateTimePicker;
		this.toDateTimePicker = toDateTimePicker;
		this.fromTimeTextBox = fromTimeTextBox;
		this.toTimeTextBox = toTimeTextBox;

		InitializeComponent();
	}

	private void GetDateTime(out string fromDateTime, out string toDateTime)
	{
		fromDateTime = fromDateTimePicker.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
		toDateTime = toDateTimePicker.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

		fromDateTime = fromDateTime + $" {fromTimeTextBox.Text}:00:00";
		toDateTime = toDateTime + $" {toTimeTextBox.Text}:00:00";
	}

	private string GetFormatedDate(bool getFromDate = true)
	{
		if (getFromDate)
			return fromDateTimePicker.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) + $" {fromTimeTextBox.Text}:00";

		else
			return toDateTimePicker.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) + $" {toTimeTextBox.Text}:00";
	}

	private List<TransactionModel> GetTransactionsByLocationId(int locationId)
	{
		string fromDateTime, toDateTime;
		GetDateTime(out fromDateTime, out toDateTime);
		return Task.Run(async () => await DataAccess.GetTransactionsByDateRangeAndLocation(fromDateTime, toDateTime, locationId)).Result;
	}

	private int MakingGraphics(Graphics g)
	{
		Font font = new Font("Courier New", 9);

		int y = 0;
		int grandTotalMale = 0, grandTotalFemale = 0, grandTotalCash = 0, grandTotalCard = 0, grandTotalUPI = 0;
		var locations = Task.Run(async () => await DataAccess.LoadTableData<LocationModel>("LocationTable")).Result.ToList();

		g.DrawString($"{GetFormatedDate()} - {GetFormatedDate(false)}", new Font("Courier New", 12, FontStyle.Bold), Brushes.Black, 250, y += 20);

		foreach (var location in locations)
		{
			int totalMale = 0, totalFemale = 0, totalCash = 0, totalCard = 0, totalUPI = 0;
			List<TransactionModel> transactions = GetTransactionsByLocationId(location.Id);

			g.DrawString($"** {location.Name} **", new Font("Courier New", 12, FontStyle.Bold), Brushes.Black, 350, y += 20);

			g.DrawString("---------------------------------", font, Brushes.Black, 300, y += 15);

			foreach (var transaction in transactions)
			{
				totalMale += transaction.Male;
				totalFemale += transaction.Female;
				totalCash += transaction.Cash;
				totalCard += transaction.Card;
				totalUPI += transaction.UPI;
			}

			g.DrawString($"Total Persons: {totalMale + totalFemale}", font, Brushes.Black, 100, y += 15);
			g.DrawString($"Total Amount: {totalCash + totalCard + totalUPI}", font, Brushes.Black, 600, y);

			g.DrawString($"Male: {totalMale}", font, Brushes.Black, 100, y += 15);
			g.DrawString($"Cash: {totalCash}", font, Brushes.Black, 600, y);

			g.DrawString($"Female: {totalFemale}", font, Brushes.Black, 100, y += 15);
			g.DrawString($"Card: {totalCard}", font, Brushes.Black, 600, y);

			g.DrawString($"UPI: {totalUPI}", font, Brushes.Black, 600, y += 15);

			grandTotalMale += totalMale;
			grandTotalFemale += totalFemale;
			grandTotalCash += totalCash;
			grandTotalCard += totalCard;
			grandTotalUPI += totalUPI;
		}

		g.DrawString("** Grand Total **", new Font("Courier New", 12, FontStyle.Bold), Brushes.Black, 350, y += 20);

		g.DrawString("---------------------------------", font, Brushes.Black, 300, y += 15);

		g.DrawString($"Grand Total Persons: {grandTotalMale + grandTotalFemale}", font, Brushes.Black, 100, y += 15);
		g.DrawString($"Grand Total Amount: {grandTotalCash + grandTotalCard + grandTotalUPI}", font, Brushes.Black, 600, y);

		g.DrawString($"Male: {grandTotalMale}", font, Brushes.Black, 100, y += 15);
		g.DrawString($"Cash: {grandTotalCash}", font, Brushes.Black, 600, y);

		g.DrawString($"Female: {grandTotalFemale}", font, Brushes.Black, 100, y += 15);
		g.DrawString($"Card: {grandTotalCard}", font, Brushes.Black, 600, y);

		g.DrawString($"UPI: {grandTotalUPI}", font, Brushes.Black, 600, y += 15);
		return y;
	}

	private void ShowDataForm_Paint(object sender, PaintEventArgs e)
	{
		Graphics g = e.Graphics;
		int y = MakingGraphics(g);

		Button button = new Button();
		button.Size = new Size(100, 50);
		button.Location = new Point(300, y += 30);
		button.Text = "Print";
		button.Click += printButton_Click;
		button.Show();
		Controls.Add(button);

		Size = new Size(1000, y + 100);
	}

	private void printButton_Click(object sender, EventArgs e)
	{
		PrintDialog printDialog = new PrintDialog();
		printDialog.Document = printDocument;

		printDocument.Print();
	}

	private void printDocument_PrintPage(object sender, PrintPageEventArgs e)
	{
		Graphics g = e.Graphics;
		int y = MakingGraphics(g);
	}
}
