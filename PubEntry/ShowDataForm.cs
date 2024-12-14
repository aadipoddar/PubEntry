using System.Diagnostics.Eventing.Reader;
using System.Globalization;

using PubEntryLibrary.Data;
using PubEntryLibrary.Models;

using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace PubEntry;

public partial class ShowDataForm : Form
{
	DateTimePicker fromDateTimePicker, toDateTimePicker;
	TextBox fromTimeTextBox, toTimeTextBox;
	bool detailedReport = false;
	int locationId;

	public ShowDataForm(DateTimePicker fromDateTimePicker, DateTimePicker toDateTimePicker, TextBox fromTimeTextBox, TextBox toTimeTextBox, int locationId = 0, bool detailedReport = false)
	{
		this.fromDateTimePicker = fromDateTimePicker;
		this.toDateTimePicker = toDateTimePicker;
		this.fromTimeTextBox = fromTimeTextBox;
		this.toTimeTextBox = toTimeTextBox;
		this.locationId = locationId;
		this.detailedReport = detailedReport;
		AutoScroll = true;

		InitializeComponent();
		LoadComponents(detailedReport);
	}

	#region GetData
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
		return Task.Run(async () => await TransactionData.GetTransactionsByDateRangeAndLocation(fromDateTime, toDateTime, locationId)).Result;
	}
	#endregion

	private void printButton_Click(object sender, EventArgs e)
	{
		if (detailedReport)
		{
			Document.Create(container =>
			{
				container.Page(page =>
				{
					page.Size(PageSizes.A4);
					page.Margin(2, Unit.Centimetre);
					page.PageColor(Colors.White);
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
							List<TransactionModel> transactions = GetTransactionsByLocationId(locationId);

							x.Item().Text($"** {Task.Run(async () => await CommonData.GetById<LocationModel>("LocationTable", locationId)).Result.FirstOrDefault().Name} **").AlignCenter().FontSize(20).SemiBold();

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

		else
			Document.Create(container =>
			{
				container.Page(page =>
				{
					page.Size(PageSizes.A4);
					page.Margin(2, Unit.Centimetre);
					page.PageColor(Colors.White);
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

				static IContainer CellStyle(IContainer container)
				{
					return container.DefaultTextStyle(x => x.SemiBold().FontSize(10)).PaddingVertical(2).BorderBottom(1).BorderColor(Colors.Black);
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

				static IContainer CellStyle(IContainer container)
				{
					return container.DefaultTextStyle(x => x.FontSize(6).NormalWeight()).PaddingVertical(1);
				}
			}
		});
	}

	private void LoadComponents(bool detailedReport = false)
	{
		if (detailedReport)
		{
			Font font = new("Courier New", 9);
			int y = 0;

			Label dateLabel = new()
			{
				Text = $"{GetFormatedDate()} - {GetFormatedDate(false)}",
				Font = new Font("Courier New", 12, FontStyle.Bold),
				AutoSize = true,
				Location = new Point(250, y += 20)
			};

			int totalMale = 0, totalFemale = 0, totalCash = 0, totalCard = 0, totalUPI = 0, totalAmex = 0;
			List<TransactionModel> transactions = GetTransactionsByLocationId(locationId);

			Label locationLabel = new()
			{
				Text = $"** {Task.Run(async () => await CommonData.GetById<LocationModel>("LocationTable", locationId)).Result.FirstOrDefault().Name} **",
				Font = new Font("Courier New", 12, FontStyle.Bold),
				AutoSize = true,
				Location = new Point(350, y += 20)
			};

			Label dashLinesLabel1 = new()
			{
				Text = "------------------------------------------------------------------------------------------------------------------",
				Font = font,
				AutoSize = true,
				Location = new Point(5, y += 20)
			};

			#region DataGridView
			DataGridView dataGridView = new DataGridView()
			{
				Columns = {
						new DataGridViewTextBoxColumn { Name = "Name" },
						new DataGridViewTextBoxColumn { Name = "Number" },
						new DataGridViewTextBoxColumn { Name = "Male" },
						new DataGridViewTextBoxColumn { Name = "Female" },
						new DataGridViewTextBoxColumn { Name = "Cash", DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight }},
						new DataGridViewTextBoxColumn { Name = "Card", DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight } },
						new DataGridViewTextBoxColumn { Name = "UPI" , DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight }},
						new DataGridViewTextBoxColumn { Name = "Amex" , DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight }},
						new DataGridViewTextBoxColumn { Name = "Entered By" },
						new DataGridViewTextBoxColumn { Name = "Date Time" },
					},

				Size = new System.Drawing.Size(1000, 500),
				AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells,
				Location = new Point(10, y += 20),
				ScrollBars = ScrollBars.Both,
				ReadOnly = true,
				AutoGenerateColumns = false,
				AllowUserToAddRows = false,
				AllowUserToOrderColumns = false,
				AllowUserToDeleteRows = false,
			};

			foreach (var transaction in transactions)
			{
				var person = Task.Run(async () => await CommonData.GetById<PersonModel>("PersonTable", transaction.PersonId)).Result.FirstOrDefault();
				string reservationTypeName = Task.Run(async () => await CommonData.GetById<ReservationTypeModel>("ReservationTypeTable", transaction.ReservationType)).Result.FirstOrDefault().Name;
				string employeeName = Task.Run(async () => await CommonData.GetById<EmployeeModel>("EmployeeTable", transaction.EmployeeId)).Result.FirstOrDefault().Name;

				dataGridView.Rows.Add(
					person.Name,
					person.Number,
					transaction.Male,
					transaction.Female,
					transaction.Cash,
					transaction.Card,
					transaction.UPI,
					transaction.Amex,
					employeeName,
					transaction.DateTime
				);

				totalMale += transaction.Male;
				totalFemale += transaction.Female;
				totalCash += transaction.Cash;
				totalCard += transaction.Card;
				totalUPI += transaction.UPI;
				totalAmex += transaction.Amex;
			};

			y += dataGridView.Height;
			#endregion

			#region Totals
			Label totalPersonLabel = new()
			{
				Text = $"Total Persons: {totalMale + totalFemale}",
				Font = font,
				AutoSize = true,
				Location = new Point(100, y += 20)
			};

			Label totalAmountLabel = new()
			{
				Text = $"Total Amount: {totalCash + totalCard + totalUPI + totalAmex}",
				Font = font,
				AutoSize = true,
				Location = new Point(600, y)
			};

			Label totalMaleLabel = new()
			{
				Text = $"Male: {totalMale}",
				Font = font,
				AutoSize = true,
				Location = new Point(100, y += 15)
			};

			Label totalCashLabel = new()
			{
				Text = $"Cash: {totalCash}",
				Font = font,
				AutoSize = true,
				Location = new Point(600, y)
			};

			Label totalFemaleLabel = new()
			{
				Text = $"Female: {totalFemale}",
				Font = font,
				AutoSize = true,
				Location = new Point(100, y += 15)
			};

			Label totalCardLabel = new()
			{
				Text = $"Card: {totalCard}",
				Font = font,
				AutoSize = true,
				Location = new Point(600, y)
			};

			Label totalUPILabel = new()
			{
				Text = $"UPI: {totalUPI}",
				Font = font,
				AutoSize = true,
				Location = new Point(600, y += 15)
			};

			Label totalAmexLabel = new()
			{
				Text = $"Amex: {totalAmex}",
				Font = font,
				AutoSize = true,
				Location = new Point(600, y += 15)
			};
			#endregion

			Controls.Add(locationLabel);
			Controls.Add(dashLinesLabel1);
			Controls.Add(dataGridView);
			Controls.Add(totalPersonLabel);
			Controls.Add(totalAmountLabel);
			Controls.Add(totalMaleLabel);
			Controls.Add(totalFemaleLabel);
			Controls.Add(totalCashLabel);
			Controls.Add(totalCardLabel);
			Controls.Add(totalUPILabel);
			Controls.Add(totalAmexLabel);

			Button button = new()
			{
				Size = new System.Drawing.Size(100, 50),
				Location = new Point(350, y += 50),
				Text = "Print",
			};

			button.Click += printButton_Click;
			button.Show();

			Controls.Add(dateLabel);
			Controls.Add(button);

			Size = new System.Drawing.Size(850, 800);
		}

		else
		{
			Font font = new("Courier New", 9);
			int y = 0;
			int grandTotalMale = 0, grandTotalFemale = 0, grandTotalCash = 0, grandTotalCard = 0, grandTotalUPI = 0, grandTotalAmex = 0;
			var locations = Task.Run(async () => await CommonData.LoadTableData<LocationModel>("LocationTable")).Result.ToList();

			Label dateLabel = new()
			{
				Text = $"{GetFormatedDate()} - {GetFormatedDate(false)}",
				Font = new Font("Courier New", 12, FontStyle.Bold),
				AutoSize = true,
				Location = new Point(250, y += 20)
			};

			foreach (var location in locations)
			{
				int totalMale = 0, totalFemale = 0, totalCash = 0, totalCard = 0, totalUPI = 0, totalAmex = 0;
				List<TransactionModel> transactions = GetTransactionsByLocationId(location.Id);

				Label locationLabel = new()
				{
					Text = $"** {location.Name} **",
					Font = new Font("Courier New", 12, FontStyle.Bold),
					AutoSize = true,
					Location = new Point(350, y += 20)
				};

				Label dashLinesLabel1 = new()
				{
					Text = "------------------------------------------------------------------------------------------------------------------",
					Font = font,
					AutoSize = true,
					Location = new Point(5, y += 20)
				};


				foreach (var transaction in transactions)
				{
					var person = Task.Run(async () => await CommonData.GetById<PersonModel>("PersonTable", transaction.PersonId)).Result.FirstOrDefault();
					string reservationTypeName = Task.Run(async () => await CommonData.GetById<ReservationTypeModel>("ReservationTypeTable", transaction.ReservationType)).Result.FirstOrDefault().Name;
					string employeeName = Task.Run(async () => await CommonData.GetById<EmployeeModel>("EmployeeTable", transaction.EmployeeId)).Result.FirstOrDefault().Name;

					totalMale += transaction.Male;
					totalFemale += transaction.Female;
					totalCash += transaction.Cash;
					totalCard += transaction.Card;
					totalUPI += transaction.UPI;
					totalAmex += transaction.Amex;
				};

				#region Totals
				Label totalPersonLabel = new()
				{
					Text = $"Total Persons: {totalMale + totalFemale}",
					Font = font,
					AutoSize = true,
					Location = new Point(100, y += 20)
				};

				Label totalAmountLabel = new()
				{
					Text = $"Total Amount: {totalCash + totalCard + totalUPI + totalAmex}",
					Font = font,
					AutoSize = true,
					Location = new Point(600, y)
				};

				Label totalMaleLabel = new()
				{
					Text = $"Male: {totalMale}",
					Font = font,
					AutoSize = true,
					Location = new Point(100, y += 15)
				};

				Label totalCashLabel = new()
				{
					Text = $"Cash: {totalCash}",
					Font = font,
					AutoSize = true,
					Location = new Point(600, y)
				};

				Label totalFemaleLabel = new()
				{
					Text = $"Female: {totalFemale}",
					Font = font,
					AutoSize = true,
					Location = new Point(100, y += 15)
				};

				Label totalCardLabel = new()
				{
					Text = $"Card: {totalCard}",
					Font = font,
					AutoSize = true,
					Location = new Point(600, y)
				};

				Label totalUPILabel = new()
				{
					Text = $"UPI: {totalUPI}",
					Font = font,
					AutoSize = true,
					Location = new Point(600, y += 15)
				};

				Label totalAmexLabel = new()
				{
					Text = $"Amex: {totalAmex}",
					Font = font,
					AutoSize = true,
					Location = new Point(600, y += 15)
				};

				grandTotalMale += totalMale;
				grandTotalFemale += totalFemale;
				grandTotalCash += totalCash;
				grandTotalCard += totalCard;
				grandTotalUPI += totalUPI;
				grandTotalAmex += totalAmex;
				#endregion

				Controls.Add(locationLabel);
				Controls.Add(dashLinesLabel1);
				Controls.Add(totalPersonLabel);
				Controls.Add(totalAmountLabel);
				Controls.Add(totalMaleLabel);
				Controls.Add(totalFemaleLabel);
				Controls.Add(totalCashLabel);
				Controls.Add(totalCardLabel);
				Controls.Add(totalUPILabel);
				Controls.Add(totalAmexLabel);
			}

			#region GrandTotals
			Label grandTotalLabel = new()
			{
				Text = "** Grand total **",
				Font = new Font("Courier New", 12, FontStyle.Bold),
				AutoSize = true,
				Location = new Point(350, y += 20)
			};

			Label dashLinesLabel2 = new()
			{
				Text = "------------------------------------------------------------------------------------------------------------------",
				Font = font,
				AutoSize = true,
				Location = new Point(5, y += 20)
			};

			Label grandTotalPersonLabel = new()
			{
				Text = $"Total Persons: {grandTotalMale + grandTotalFemale}",
				Font = font,
				AutoSize = true,
				Location = new Point(100, y += 20)
			};

			Label grandTotalAmountLabel = new()
			{
				Text = $"Total Amount: {grandTotalCash + grandTotalCard + grandTotalUPI + grandTotalAmex}",
				Font = font,
				AutoSize = true,
				Location = new Point(600, y)
			};

			Label grandTotalMaleLabel = new()
			{
				Text = $"Male: {grandTotalMale}",
				Font = font,
				AutoSize = true,
				Location = new Point(100, y += 15)
			};

			Label grandTotalCashLabel = new()
			{
				Text = $"Cash: {grandTotalCash}",
				Font = font,
				AutoSize = true,
				Location = new Point(600, y)
			};

			Label grandTotalFemaleLabel = new()
			{
				Text = $"Female: {grandTotalFemale}",
				Font = font,
				AutoSize = true,
				Location = new Point(100, y += 15)
			};

			Label grandTotalCardLabel = new()
			{
				Text = $"Card: {grandTotalCard}",
				Font = font,
				AutoSize = true,
				Location = new Point(600, y)
			};

			Label grandTotalUPILabel = new()
			{
				Text = $"UPI: {grandTotalUPI}",
				Font = font,
				AutoSize = true,
				Location = new Point(600, y += 15)
			};

			Label grandTotalAmexLabel = new()
			{
				Text = $"Amex: {grandTotalAmex}",
				Font = font,
				AutoSize = true,
				Location = new Point(600, y += 15)
			};
			#endregion

			Button button = new()
			{
				Size = new System.Drawing.Size(100, 50),
				Location = new Point(350, y += 50),
				Text = "Print",
			};

			button.Click += printButton_Click;
			button.Show();

			Controls.Add(dateLabel);
			Controls.Add(grandTotalLabel);
			Controls.Add(dashLinesLabel2);
			Controls.Add(grandTotalPersonLabel);
			Controls.Add(grandTotalAmountLabel);
			Controls.Add(grandTotalMaleLabel);
			Controls.Add(grandTotalFemaleLabel);
			Controls.Add(grandTotalCashLabel);
			Controls.Add(grandTotalCardLabel);
			Controls.Add(grandTotalUPILabel);
			Controls.Add(grandTotalAmexLabel);
			Controls.Add(button);

			Size = new System.Drawing.Size(850, 800);
		}
	}
}
