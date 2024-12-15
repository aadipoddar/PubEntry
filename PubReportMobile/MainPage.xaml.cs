using System.Globalization;

using PubEntryLibrary.Data;
using PubEntryLibrary.Models;

using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace PubReportMobile;

public partial class MainPage : ContentPage
{
	LocationModel selectedLocation;
	int selectedLocationId;

	public MainPage()
	{
		InitializeComponent();

		LoadTextBoxes();
	}

	public DateTime FormDate { get; set; } = DateTime.Now;
	public DateTime ToDate { get; set; } = DateTime.Now;
	public int FromTime { get; set; } = 5;
	public int ToTime { get; set; } = 24;

	private void LoadTextBoxes()
	{
		if (DateTime.Now.Hour > 4)
			fromTimePicker.Time = TimeSpan.FromHours(int.Parse(DateTime.Now.Hour.ToString()) - 3);

		else fromTimePicker.Time = TimeSpan.FromHours(int.Parse(DateTime.Now.Hour.ToString()));

		toTimePicker.Time = TimeSpan.FromHours(int.Parse(DateTime.Now.Hour.ToString()));

		var locations = Task.Run(async () => await CommonData.LoadTableData<LocationModel>("LocationTable")).Result.ToList();

		locationPicker.ItemsSource = locations;
		locationPicker.ItemDisplayBinding = new Binding(nameof(LocationModel.Name));
		locationPicker.SelectedIndex = 0;

		locationPicker.SelectedIndexChanged += (sender, args) =>
		{
			if (locationPicker.SelectedIndex != -1)
			{
				selectedLocation = locations[locationPicker.SelectedIndex];
				selectedLocationId = selectedLocation.Id;
			}
		};
	}

	#region GetData
	private void GetDateTime(out string fromDateTime, out string toDateTime)
	{
		fromDateTime = fromDatePicker.Date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
		toDateTime = toDatePicker.Date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

		fromDateTime = fromDateTime + $" {fromTimePicker.Time}";
		toDateTime = toDateTime + $" {toTimePicker.Time}";
	}

	private string GetFormatedDate(bool getFromDate = true)
	{
		if (getFromDate)
			return fromDatePicker.Date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) + $" {fromTimePicker.Time}";

		else
			return toDatePicker.Date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) + $" {toTimePicker.Time}";
	}

	private List<TransactionModel> GetTransactionsByLocationId(int locationId)
	{
		string fromDateTime, toDateTime;
		GetDateTime(out fromDateTime, out toDateTime);
		return Task.Run(async () => await TransactionData.GetTransactionsByDateRangeAndLocation(fromDateTime, toDateTime, locationId)).Result;
	}
	#endregion

	private void SummaryReportButtonClicked(object sender, EventArgs e)
	{
		Document.Create(container =>
		{
			container.Page(page =>
			{
				page.Size(PageSizes.A4);
				page.Margin(2, Unit.Centimetre);
				page.PageColor(QuestPDF.Helpers.Colors.White);
				page.DefaultTextStyle(x => x.FontSize(20));

				int grandTotalMale = 0, grandTotalFemale = 0, grandTotalCash = 0, grandTotalCard = 0, grandTotalUPI = 0, grandTotalAmex = 0;
				var locations = Task.Run(async () => await CommonData.LoadTableData<LocationModel>("LocationTable")).Result.ToList();

				page.Header()
					.Text($"{GetFormatedDate()} - {GetFormatedDate(false)}")
					.Bold().FontSize(25).AlignCenter();

				page.Content()
					.PaddingVertical(1, Unit.Centimetre)
					.Column(x =>
					{
						x.Spacing(20);

						foreach (var location in locations)
						{
							int totalMale = 0, totalFemale = 0, totalCash = 0, totalCard = 0, totalUPI = 0, totalAmex = 0;
							List<TransactionModel> transactions = GetTransactionsByLocationId(location.Id);

							x.Item().Text($"** {location.Name} **").AlignCenter().FontSize(20).SemiBold();

							foreach (var transaction in transactions)
							{
								totalMale += transaction.Male;
								totalFemale += transaction.Female;
								totalCash += transaction.Cash;
								totalCard += transaction.Card;
								totalUPI += transaction.UPI;
								totalAmex += transaction.Amex;
							}

							x.Item().LineHorizontal(1);

							x.Item().Row(row =>
							{
								row.RelativeItem().Column(c =>
								{
									c.Item().Text($"Total Persons: {totalMale + totalFemale}").AlignLeft().FontSize(12);
									c.Item().Text($"Male: {totalMale}").AlignLeft().FontSize(12);
									c.Item().Text($"Female: {totalFemale}").AlignLeft().FontSize(12);
								});

								row.RelativeItem().Column(c =>
								{
									c.Item().Text($"Total Amount: {totalCash + totalCard + totalUPI + totalAmex}").AlignRight().FontSize(12);
									c.Item().Text($"Cash: {totalCash}").AlignRight().FontSize(12);
									c.Item().Text($"Card: {totalCard}").AlignRight().FontSize(12);
									c.Item().Text($"UPI: {totalUPI}").AlignRight().FontSize(12);
									c.Item().Text($"Amex: {totalAmex}").AlignRight().FontSize(12);
								});
							});

							x.Item().LineHorizontal(1);

							grandTotalMale += totalMale;
							grandTotalFemale += totalFemale;
							grandTotalCash += totalCash;
							grandTotalCard += totalCard;
							grandTotalUPI += totalUPI;
							grandTotalAmex += totalAmex;
						}

						x.Item().Text("** Grand Total **").AlignCenter().FontSize(20).SemiBold();

						x.Item().LineHorizontal(1);

						x.Item().Row(row =>
						{
							row.RelativeItem().Column(c =>
							{
								c.Item().Text($"Grand Total Persons: {grandTotalMale + grandTotalFemale}").AlignLeft().FontSize(12);
								c.Item().Text($"Male: {grandTotalMale}").AlignLeft().FontSize(12);
								c.Item().Text($"Female: {grandTotalFemale}").AlignLeft().FontSize(12);
							});

							row.RelativeItem().Column(c =>
							{
								c.Item().Text($"Grand Total Amount: {grandTotalCash + grandTotalCard + grandTotalUPI + grandTotalAmex}").AlignRight().FontSize(12);
								c.Item().Text($"Cash: {grandTotalCash}").AlignRight().FontSize(12);
								c.Item().Text($"Card: {grandTotalCard}").AlignRight().FontSize(12);
								c.Item().Text($"UPI: {grandTotalUPI}").AlignRight().FontSize(12);
								c.Item().Text($"Amex: {grandTotalAmex}").AlignRight().FontSize(12);
							});
						});
					});
			});
		}).GeneratePdfAndShow();
	}

	private void DetailReportButtonClicked(object sender, EventArgs e)
	{
		Document.Create(container =>
		{
			container.Page(page =>
			{
				page.Size(PageSizes.A4);
				page.Margin(2, Unit.Centimetre);
				page.PageColor(QuestPDF.Helpers.Colors.White);
				page.DefaultTextStyle(x => x.FontSize(20));

				page.Header()
					.Text($"{GetFormatedDate()} - {GetFormatedDate(false)}")
					.Bold().FontSize(25).AlignCenter();

				page.Content()
					.PaddingVertical(1, Unit.Centimetre)
					.Column(x =>
					{
						x.Spacing(20);

						int totalMale = 0, totalFemale = 0, totalCash = 0, totalCard = 0, totalUPI = 0, totalAmex = 0;
						List<TransactionModel> transactions = GetTransactionsByLocationId(selectedLocationId + 1);

						x.Item().Text($"** {Task.Run(async () => await CommonData.GetById<LocationModel>("LocationTable", selectedLocationId + 1)).Result.FirstOrDefault()?.Name} **").AlignCenter().FontSize(20).SemiBold();

						ComposeTable(x, transactions);

						foreach (var transaction in transactions)
						{
							totalMale += transaction.Male;
							totalFemale += transaction.Female;
							totalCash += transaction.Cash;
							totalCard += transaction.Card;
							totalUPI += transaction.UPI;
							totalAmex += transaction.Amex;
						}

						x.Item().LineHorizontal(1);

						x.Item().Row(row =>
						{
							row.RelativeItem().Column(c =>
							{
								c.Item().Text($"Total Persons: {totalMale + totalFemale}").AlignLeft().FontSize(12);
								c.Item().Text($"Male: {totalMale}").AlignLeft().FontSize(12);
								c.Item().Text($"Female: {totalFemale}").AlignLeft().FontSize(12);
							});

							row.RelativeItem().Column(c =>
							{
								c.Item().Text($"Total Amount: {totalCash + totalCard + totalUPI + totalAmex}").AlignRight().FontSize(12);
								c.Item().Text($"Cash: {totalCash}").AlignRight().FontSize(12);
								c.Item().Text($"Card: {totalCard}").AlignRight().FontSize(12);
								c.Item().Text($"UPI: {totalUPI}").AlignRight().FontSize(12);
								c.Item().Text($"Amex: {totalAmex}").AlignRight().FontSize(12);
							});
						});
					});
			});
		}).GeneratePdfAndShow();
	}

	private static void ComposeTable(ColumnDescriptor x, List<TransactionModel> transactions)
	{
		x.Item().Table(table =>
		{
			table.ColumnsDefinition(columns =>
			{
				columns.RelativeColumn();
				columns.RelativeColumn();
				columns.RelativeColumn();
				columns.RelativeColumn();
				columns.RelativeColumn();
				columns.RelativeColumn();
				columns.RelativeColumn();
				columns.RelativeColumn();
				columns.RelativeColumn();
				columns.RelativeColumn();
			});

			table.Header(header =>
			{
				header.Cell().Element(CellStyle).AlignCenter().Text("Name");
				header.Cell().Element(CellStyle).AlignCenter().Text("Number");
				header.Cell().Element(CellStyle).AlignCenter().Text("Male");
				header.Cell().Element(CellStyle).AlignCenter().Text("Female");
				header.Cell().Element(CellStyle).AlignRight().Text("Cash");
				header.Cell().Element(CellStyle).AlignRight().Text("Card");
				header.Cell().Element(CellStyle).AlignRight().Text("UPI");
				header.Cell().Element(CellStyle).AlignRight().Text("Amex");
				header.Cell().Element(CellStyle).AlignCenter().Text("Entered By");
				header.Cell().Element(CellStyle).AlignCenter().Text("Date Time");

				static QuestPDF.Infrastructure.IContainer CellStyle(QuestPDF.Infrastructure.IContainer container)
				{
					return container.DefaultTextStyle(x => x.SemiBold().FontSize(10)).PaddingVertical(2).BorderBottom(1).BorderColor(QuestPDF.Helpers.Colors.Black);
				}
			});

			foreach (var transaction in transactions)
			{
				var person = Task.Run(async () => await CommonData.GetById<PersonModel>("PersonTable", transaction.PersonId)).Result.FirstOrDefault();
				string reservationTypeName = Task.Run(async () => await CommonData.GetById<ReservationTypeModel>("ReservationTypeTable", transaction.ReservationType)).Result.FirstOrDefault().Name;
				string employeeName = Task.Run(async () => await CommonData.GetById<EmployeeModel>("EmployeeTable", transaction.EmployeeId)).Result.FirstOrDefault().Name;

				table.Cell().Element(CellStyle).AlignCenter().Text($"{person.Name}");
				table.Cell().Element(CellStyle).AlignCenter().Text($"{person.Number}");
				table.Cell().Element(CellStyle).AlignCenter().Text($"{transaction.Male}");
				table.Cell().Element(CellStyle).AlignCenter().Text($"{transaction.Female}");
				table.Cell().Element(CellStyle).AlignRight().Text($"{transaction.Cash}");
				table.Cell().Element(CellStyle).AlignRight().Text($"{transaction.Card}");
				table.Cell().Element(CellStyle).AlignRight().Text($"{transaction.UPI}");
				table.Cell().Element(CellStyle).AlignRight().Text($"{transaction.Amex}");
				table.Cell().Element(CellStyle).AlignCenter().Text($"{employeeName}");
				table.Cell().Element(CellStyle).AlignCenter().Text($"{transaction.DateTime.ToString("HH/mm/yy HH:mm")}");

				static QuestPDF.Infrastructure.IContainer CellStyle(QuestPDF.Infrastructure.IContainer container)
				{
					return container.DefaultTextStyle(x => x.FontSize(6).NormalWeight()).PaddingVertical(1);
				}
			}
		});
	}
}
