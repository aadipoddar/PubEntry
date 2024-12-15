using System.Diagnostics;
using System.Globalization;

using PubEntryLibrary.Data;
using PubEntryLibrary.Models;

using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using System.Data;

namespace PubEntry.Reports;

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

				Size = new Size(1000, 500),
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
				Size = new Size(100, 50),
				Location = new Point(350, y += 50),
				Text = "Print",
			};

			button.Click += printButton_Click;
			button.Show();

			Controls.Add(dateLabel);
			Controls.Add(button);

			Size = new Size(850, 800);
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
				Size = new Size(100, 50),
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

			Size = new Size(850, 800);
		}
	}

	#region Printing
	public PdfPageTemplateElement AddHeader(PdfDocument doc, string title, string description)
	{
		RectangleF rect = new RectangleF(0, 0, doc.Pages[0].GetClientSize().Width, 50);

		PdfPageTemplateElement header = new PdfPageTemplateElement(rect);
		PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 24);
		float doubleHeight = font.Height * 2;
		Color activeColor = System.Drawing.Color.FromArgb(44, 71, 120);
		SizeF imageSize = new SizeF(110f, 35f);

		PdfSolidBrush brush = new PdfSolidBrush(activeColor);

		PdfPen pen = new PdfPen(System.Drawing.Color.DarkBlue, 3f);
		font = new PdfStandardFont(PdfFontFamily.Helvetica, 16, PdfFontStyle.Bold);

		PdfStringFormat format = new PdfStringFormat();
		format.Alignment = PdfTextAlignment.Center;
		format.LineAlignment = PdfVerticalAlignment.Middle;

		header.Graphics.DrawString(title, font, brush, new RectangleF(0, 0, header.Width, header.Height), format);
		brush = new PdfSolidBrush(System.Drawing.Color.Gray);
		font = new PdfStandardFont(PdfFontFamily.Helvetica, 6, PdfFontStyle.Bold);

		format = new PdfStringFormat();
		format.Alignment = PdfTextAlignment.Left;
		format.LineAlignment = PdfVerticalAlignment.Bottom;

		header.Graphics.DrawString(description, font, brush, new RectangleF(0, 0, header.Width, header.Height - 8), format);

		pen = new PdfPen(System.Drawing.Color.DarkBlue, 0.7f);
		header.Graphics.DrawLine(pen, 0, 0, header.Width, 0);
		pen = new PdfPen(System.Drawing.Color.DarkBlue, 2f);
		header.Graphics.DrawLine(pen, 0, 03, header.Width + 3, 03);
		pen = new PdfPen(System.Drawing.Color.DarkBlue, 2f);
		header.Graphics.DrawLine(pen, 0, header.Height - 3, header.Width, header.Height - 3);
		header.Graphics.DrawLine(pen, 0, header.Height, header.Width, header.Height);

		return header;
	}

	public PdfPageTemplateElement AddFooter(PdfDocument doc)
	{
		RectangleF rect = new RectangleF(0, 0, doc.Pages[0].GetClientSize().Width, 50);

		PdfPageTemplateElement footer = new PdfPageTemplateElement(rect);
		PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 7, PdfFontStyle.Bold);

		PdfSolidBrush brush = new PdfSolidBrush(System.Drawing.Color.Black);

		PdfPageNumberField pageNumber = new PdfPageNumberField(font, brush);

		PdfPageCountField count = new PdfPageCountField(font, brush);

		PdfCompositeField compositeField = new PdfCompositeField(font, brush, "Page {0} of {1}", pageNumber, count);
		compositeField.Bounds = footer.Bounds;

		compositeField.Draw(footer.Graphics, new PointF(470, 40));

		return footer;
	}

	private void printButton_Click(object sender, EventArgs e)
	{
		if (detailedReport) DetailedReport();

		else SummaryReport();
	}

	private void SummaryReport()
	{
		PdfDocument pdfDocument = new PdfDocument();
		PdfPage pdfPage = pdfDocument.Pages.Add();

		pdfDocument.Template.Top = AddHeader(pdfDocument, $"{GetFormatedDate()} - {GetFormatedDate(false)}", "Summary Report");
		pdfDocument.Template.Bottom = AddFooter(pdfDocument);

		PdfLayoutFormat layoutFormat = new PdfLayoutFormat();
		layoutFormat.Layout = PdfLayoutType.Paginate;
		layoutFormat.Break = PdfLayoutBreakType.FitPage;

		PdfStandardFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 25, PdfFontStyle.Bold);

		PdfLayoutResult result = null;
		PdfTextElement textElement;

		string text;
		float textWidth, pageWidth, textX;
		int grandTotalMale = 0, grandTotalFemale = 0, grandTotalCash = 0, grandTotalCard = 0, grandTotalUPI = 0, grandTotalAmex = 0;
		var locations = Task.Run(async () => await CommonData.LoadTableData<LocationModel>("LocationTable")).Result.ToList();

		foreach (var location in locations)
		{
			int totalMale = 0, totalFemale = 0, totalCash = 0, totalCard = 0, totalUPI = 0, totalAmex = 0;
			List<TransactionModel> transactions = GetTransactionsByLocationId(location.Id);

			font = new PdfStandardFont(PdfFontFamily.Helvetica, 25, PdfFontStyle.Bold);

			text = $"{location.Name}";
			textWidth = font.MeasureString(text).Width;
			pageWidth = pdfPage.GetClientSize().Width;
			textX = (pageWidth - textWidth) / 2f;

			textElement = new PdfTextElement(text, font);

			if (result == null) result = textElement.Draw(pdfPage, new PointF(textX, 20), layoutFormat);

			else result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Bottom + 20), layoutFormat);

			foreach (var transaction in transactions)
			{
				totalMale += transaction.Male;
				totalFemale += transaction.Female;
				totalCash += transaction.Cash;
				totalCard += transaction.Card;
				totalUPI += transaction.UPI;
				totalAmex += transaction.Amex;
			}

			font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);

			textElement = new PdfTextElement($"Total People: {totalMale + totalFemale}", font);
			result = textElement.Draw(result.Page, new PointF(10, result.Bounds.Bottom + 10), layoutFormat);

			text = $"Total Amount: {totalCash + totalCard + totalUPI + totalAmex}";
			textWidth = font.MeasureString(text).Width;
			pageWidth = pdfPage.GetClientSize().Width;
			textX = pageWidth - textWidth;
			textElement = new PdfTextElement(text, font);
			result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Top), layoutFormat);

			textElement = new PdfTextElement($"Male: {totalMale}", font);
			result = textElement.Draw(result.Page, new PointF(10, result.Bounds.Bottom + 10), layoutFormat);

			text = $"Cash: {totalCash}";
			textWidth = font.MeasureString(text).Width;
			pageWidth = pdfPage.GetClientSize().Width;
			textX = pageWidth - textWidth;
			textElement = new PdfTextElement(text, font);
			result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Top), layoutFormat);

			textElement = new PdfTextElement($"Female: {totalFemale}", font);
			result = textElement.Draw(result.Page, new PointF(10, result.Bounds.Bottom + 10), layoutFormat);

			text = $"Card: {totalCard}";
			textWidth = font.MeasureString(text).Width;
			pageWidth = pdfPage.GetClientSize().Width;
			textX = pageWidth - textWidth;
			textElement = new PdfTextElement(text, font);
			result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Top), layoutFormat);

			text = $"UPI: {totalUPI}";
			textWidth = font.MeasureString(text).Width;
			pageWidth = pdfPage.GetClientSize().Width;
			textX = pageWidth - textWidth;
			textElement = new PdfTextElement(text, font);
			result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Bottom + 10), layoutFormat);

			text = $"Amex: {totalAmex}";
			textWidth = font.MeasureString(text).Width;
			pageWidth = pdfPage.GetClientSize().Width;
			textX = pageWidth - textWidth;
			textElement = new PdfTextElement(text, font);
			result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Bottom + 10), layoutFormat);

			grandTotalMale += totalMale;
			grandTotalFemale += totalFemale;
			grandTotalCash += totalCash;
			grandTotalCard += totalCard;
			grandTotalUPI += totalUPI;
			grandTotalAmex += totalAmex;
		}

		font = new PdfStandardFont(PdfFontFamily.Helvetica, 25, PdfFontStyle.Bold);
		text = "Grand Total";
		textWidth = font.MeasureString(text).Width;
		pageWidth = pdfPage.GetClientSize().Width;
		textX = (pageWidth - textWidth) / 2f;
		textElement = new PdfTextElement(text, font);
		result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Bottom + 20), layoutFormat);

		font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);

		textElement = new PdfTextElement($"Total People: {grandTotalMale + grandTotalFemale}", font);
		result = textElement.Draw(result.Page, new PointF(10, result.Bounds.Bottom + 10), layoutFormat);

		text = $"Total Amount: {grandTotalCash + grandTotalCard + grandTotalUPI + grandTotalAmex}";
		textWidth = font.MeasureString(text).Width;
		pageWidth = pdfPage.GetClientSize().Width;
		textX = pageWidth - textWidth;
		textElement = new PdfTextElement(text, font);
		result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Top), layoutFormat);

		textElement = new PdfTextElement($"Male: {grandTotalMale}", font);
		result = textElement.Draw(result.Page, new PointF(10, result.Bounds.Bottom + 10), layoutFormat);

		text = $"Cash: {grandTotalCash}";
		textWidth = font.MeasureString(text).Width;
		pageWidth = pdfPage.GetClientSize().Width;
		textX = pageWidth - textWidth;
		textElement = new PdfTextElement(text, font);
		result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Top), layoutFormat);

		textElement = new PdfTextElement($"Female: {grandTotalFemale}", font);
		result = textElement.Draw(result.Page, new PointF(10, result.Bounds.Bottom + 10), layoutFormat);

		text = $"Card: {grandTotalCard}";
		textWidth = font.MeasureString(text).Width;
		pageWidth = pdfPage.GetClientSize().Width;
		textX = pageWidth - textWidth;
		textElement = new PdfTextElement(text, font);
		result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Top), layoutFormat);

		text = $"UPI: {grandTotalUPI}";
		textWidth = font.MeasureString(text).Width;
		pageWidth = pdfPage.GetClientSize().Width;
		textX = pageWidth - textWidth;
		textElement = new PdfTextElement(text, font);
		result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Bottom + 10), layoutFormat);

		text = $"Amex: {grandTotalAmex}";
		textWidth = font.MeasureString(text).Width;
		pageWidth = pdfPage.GetClientSize().Width;
		textX = pageWidth - textWidth;
		textElement = new PdfTextElement(text, font);
		result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Bottom + 10), layoutFormat);

		pdfDocument.Save($"{Path.GetTempPath()}\\Table.pdf");
		pdfDocument.Close(true);
		Process.Start(new ProcessStartInfo($"{Path.GetTempPath()}\\Table.pdf") { UseShellExecute = true });
	}

	private void DetailedReport()
	{
		PdfDocument pdfDocument = new PdfDocument();
		PdfPage pdfPage = pdfDocument.Pages.Add();

		pdfDocument.Template.Top = AddHeader(pdfDocument, $"{GetFormatedDate()} - {GetFormatedDate(false)}", "Summary Report");
		pdfDocument.Template.Bottom = AddFooter(pdfDocument);

		PdfLayoutFormat layoutFormat = new PdfLayoutFormat();
		layoutFormat.Layout = PdfLayoutType.Paginate;
		layoutFormat.Break = PdfLayoutBreakType.FitPage;

		PdfStandardFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 25, PdfFontStyle.Bold);

		PdfLayoutResult result = null;
		PdfTextElement textElement;

		int totalMale = 0, totalFemale = 0, totalCash = 0, totalCard = 0, totalUPI = 0, totalAmex = 0;
		List<TransactionModel> transactions = GetTransactionsByLocationId(locationId);

		font = new PdfStandardFont(PdfFontFamily.Helvetica, 25, PdfFontStyle.Bold);

		string text = $"{Task.Run(async () => await CommonData.GetById<LocationModel>("LocationTable", locationId)).Result.FirstOrDefault().Name}";
		float textWidth = font.MeasureString(text).Width;
		float pageWidth = pdfPage.GetClientSize().Width;
		float textX = (pageWidth - textWidth) / 2f;

		textElement = new PdfTextElement(text, font);

		if (result == null) result = textElement.Draw(pdfPage, new PointF(textX, 20), layoutFormat);

		else result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Bottom + 20), layoutFormat);

		PdfGrid pdfGrid = new PdfGrid();

		DataTable dataTable = new DataTable();

		dataTable.Columns.Add("Id", typeof(int));
		dataTable.Columns.Add("Name", typeof(string));
		dataTable.Columns.Add("Number", typeof(string));
		dataTable.Columns.Add("Male", typeof(int));
		dataTable.Columns.Add("Female", typeof(int));
		dataTable.Columns.Add("Cash", typeof(int));
		dataTable.Columns.Add("Card", typeof(int));
		dataTable.Columns.Add("UPI", typeof(int));
		dataTable.Columns.Add("Amex", typeof(int));
		dataTable.Columns.Add("Entered By", typeof(string));
		dataTable.Columns.Add("Date Time", typeof(DateTime));

		int i = 1;
		foreach (var transaction in transactions)
		{
			var person = Task.Run(async () => await CommonData.GetById<PersonModel>("PersonTable", transaction.PersonId)).Result.FirstOrDefault();
			string employeeName = Task.Run(async () => await CommonData.GetById<EmployeeModel>("EmployeeTable", transaction.EmployeeId)).Result.FirstOrDefault().Name;

			dataTable.Rows.Add(i, $"{person.Name}", $"{person.Number}", transaction.Male, transaction.Female, transaction.Cash, transaction.Card, transaction.UPI, transaction.Amex, $"{employeeName}", $"{transaction.DateTime}");

			totalMale += transaction.Male;
			totalFemale += transaction.Female;
			totalCash += transaction.Cash;
			totalCard += transaction.Card;
			totalUPI += transaction.UPI;
			totalAmex += transaction.Amex;

			i++;
		}

		pdfGrid.DataSource = dataTable;

		pdfGrid.Columns[0].Width = 30;
		pdfGrid.Columns[3].Width = 30;
		pdfGrid.Columns[4].Width = 30;
		pdfGrid.Columns[8].Width = 30;

		foreach (PdfGridRow row in pdfGrid.Rows)
		{
			foreach (PdfGridCell cell in row.Cells)
			{
				PdfGridCellStyle cellStyle = new PdfGridCellStyle
				{
					CellPadding = new PdfPaddings(5, 5, 5, 5)
				};
				cell.Style = cellStyle;
			}
		}

		pdfGrid.ApplyBuiltinStyle(PdfGridBuiltinStyle.GridTable4Accent1);

		foreach (PdfGridColumn column in pdfGrid.Columns)
			column.Format = new PdfStringFormat(PdfTextAlignment.Right, PdfVerticalAlignment.Middle);

		result = pdfGrid.Draw(result.Page, new PointF(10, result.Bounds.Bottom + 20), layoutFormat);

		font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);

		textElement = new PdfTextElement($"Total People: {totalMale + totalFemale}", font);
		result = textElement.Draw(result.Page, new PointF(10, result.Bounds.Bottom + 10), layoutFormat);

		text = $"Total Amount: {totalCash + totalCard + totalUPI + totalAmex}";
		textWidth = font.MeasureString(text).Width;
		pageWidth = pdfPage.GetClientSize().Width;
		textX = pageWidth - textWidth;
		textElement = new PdfTextElement(text, font);
		result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Top), layoutFormat);

		textElement = new PdfTextElement($"Male: {totalMale}", font);
		result = textElement.Draw(result.Page, new PointF(10, result.Bounds.Bottom + 10), layoutFormat);

		text = $"Cash: {totalCash}";
		textWidth = font.MeasureString(text).Width;
		pageWidth = pdfPage.GetClientSize().Width;
		textX = pageWidth - textWidth;
		textElement = new PdfTextElement(text, font);
		result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Top), layoutFormat);

		textElement = new PdfTextElement($"Female: {totalFemale}", font);
		result = textElement.Draw(result.Page, new PointF(10, result.Bounds.Bottom + 10), layoutFormat);

		text = $"Card: {totalCard}";
		textWidth = font.MeasureString(text).Width;
		pageWidth = pdfPage.GetClientSize().Width;
		textX = pageWidth - textWidth;
		textElement = new PdfTextElement(text, font);
		result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Top), layoutFormat);

		text = $"UPI: {totalUPI}";
		textWidth = font.MeasureString(text).Width;
		pageWidth = pdfPage.GetClientSize().Width;
		textX = pageWidth - textWidth;
		textElement = new PdfTextElement(text, font);
		result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Bottom + 10), layoutFormat);

		text = $"Amex: {totalAmex}";
		textWidth = font.MeasureString(text).Width;
		pageWidth = pdfPage.GetClientSize().Width;
		textX = pageWidth - textWidth;
		textElement = new PdfTextElement(text, font);
		result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Bottom + 10), layoutFormat);

		pdfDocument.Save($"{Path.GetTempPath()}\\Table.pdf");
		pdfDocument.Close(true);
		Process.Start(new ProcessStartInfo($"{Path.GetTempPath()}\\Table.pdf") { UseShellExecute = true });
	}
	#endregion
}
