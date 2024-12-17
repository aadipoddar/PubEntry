using PubEntryLibrary.Data;
using PubEntryLibrary.Models;

using SampleBrowser.Maui.Base;
#if PDFSB
using SampleBrowser.Maui.Pdf.Services;
#else
using SampleBrowser.Maui.Services;
#endif
using Syncfusion.Drawing;
using Syncfusion.Maui.Popup;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;

using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;

using Color = Syncfusion.Drawing.Color;
using PointF = Syncfusion.Drawing.PointF;
using SizeF = Syncfusion.Drawing.SizeF;

namespace SampleBrowser.Maui.Pdf.Pdf
{
	public partial class Invoice : SampleView
	{
		#region Constructor
		int selectedLocationId = 0;

		public DateTime FormDate { get; set; } = DateTime.Now;
		public DateTime ToDate { get; set; } = DateTime.Now;
		public int FromTime { get; set; } = 5;
		public int ToTime { get; set; } = 24;

		public Invoice()
		{
			InitializeComponent();

			LoadTextBoxes();
		}

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
					selectedLocationId = locationPicker.SelectedIndex;
			};
		}
		#endregion

		private bool ValidateTime()
		{
			if (fromDatePicker.Date > toDatePicker.Date) return false;

			if (fromTimePicker.Time > toTimePicker.Time)
				if (fromDatePicker.Date == toDatePicker.Date)
					return false;

			return true;
		}

		#region Events
		private void SummaryReportButtonClicked(object sender, EventArgs e)
		{
			if (!ValidateTime())
				errorPopup.Show();
			else
			{
				waitPopup.Show();
				Task.Run(() => PrintSummary());
			}
		}

		private void DetailReportButtonClicked(object sender, EventArgs e)
		{
			if (!ValidateTime())
				errorPopup.Show();
			else
			{
				waitPopup.Show();
				Task.Run(() => PrintDetail());
			}
		}
		#endregion

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

		#region Printing
		RectangleF TotalPriceCellBounds = RectangleF.Empty;
		RectangleF QuantityCellBounds = RectangleF.Empty;

		public PdfPageTemplateElement AddHeader(PdfDocument doc, string title, string description)
		{
			RectangleF rect = new RectangleF(0, 0, doc.Pages[0].GetClientSize().Width, 50);

			PdfPageTemplateElement header = new PdfPageTemplateElement(rect);
			PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 24);
			float doubleHeight = font.Height * 2;
			Color activeColor = Color.FromArgb(44, 71, 120);
			SizeF imageSize = new SizeF(110f, 35f);

			PdfSolidBrush brush = new PdfSolidBrush(activeColor);

			PdfPen pen = new PdfPen(Color.DarkBlue, 3f);
			font = new PdfStandardFont(PdfFontFamily.Helvetica, 16, PdfFontStyle.Bold);

			PdfStringFormat format = new PdfStringFormat();
			format.Alignment = PdfTextAlignment.Center;
			format.LineAlignment = PdfVerticalAlignment.Middle;

			header.Graphics.DrawString(title, font, brush, new RectangleF(0, 0, header.Width, header.Height), format);
			brush = new PdfSolidBrush(Color.Gray);
			font = new PdfStandardFont(PdfFontFamily.Helvetica, 6, PdfFontStyle.Bold);

			format = new PdfStringFormat();
			format.Alignment = PdfTextAlignment.Left;
			format.LineAlignment = PdfVerticalAlignment.Bottom;

			header.Graphics.DrawString(description, font, brush, new RectangleF(0, 0, header.Width, header.Height - 8), format);

			pen = new PdfPen(Color.DarkBlue, 0.7f);
			header.Graphics.DrawLine(pen, 0, 0, header.Width, 0);
			pen = new PdfPen(Color.DarkBlue, 2f);
			header.Graphics.DrawLine(pen, 0, 03, header.Width + 3, 03);
			pen = new PdfPen(Color.DarkBlue, 2f);
			header.Graphics.DrawLine(pen, 0, header.Height - 3, header.Width, header.Height - 3);
			header.Graphics.DrawLine(pen, 0, header.Height, header.Width, header.Height);

			return header;
		}

		public PdfPageTemplateElement AddFooter(PdfDocument doc)
		{
			RectangleF rect = new RectangleF(0, 0, doc.Pages[0].GetClientSize().Width, 50);

			PdfPageTemplateElement footer = new PdfPageTemplateElement(rect);
			PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 7, PdfFontStyle.Bold);

			PdfSolidBrush brush = new PdfSolidBrush(Color.Black);

			PdfPageNumberField pageNumber = new PdfPageNumberField(font, brush);

			PdfPageCountField count = new PdfPageCountField(font, brush);

			PdfCompositeField compositeField = new PdfCompositeField(font, brush, "Page {0} of {1}", pageNumber, count);
			compositeField.Bounds = footer.Bounds;

			compositeField.Draw(footer.Graphics, new PointF(470, 40));

			return footer;
		}

		private void PrintSummary()
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

			using MemoryStream ms = new();
			pdfDocument.Save(ms);
			pdfDocument.Close(true);
			ms.Position = 0;
			SaveService saveService = new();
			saveService.SaveAndView("Invoice.pdf", "application/pdf", ms);
		}

		private void PrintDetail()
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
			List<TransactionModel> transactions = GetTransactionsByLocationId(selectedLocationId + 1);

			font = new PdfStandardFont(PdfFontFamily.Helvetica, 25, PdfFontStyle.Bold);

			string text = $"{Task.Run(async () => await CommonData.GetById<LocationModel>("LocationTable", selectedLocationId + 1)).Result.FirstOrDefault().Name}";
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
			dataTable.Columns.Add("Loyalty", typeof(string));
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

				dataTable.Rows.Add(i, $"{person.Name}", $"{person.Number}", person.Loyalty == 1 ? "Y" : "N", transaction.Male, transaction.Female, transaction.Cash, transaction.Card, transaction.UPI, transaction.Amex, $"{employeeName}", $"{transaction.DateTime}");

				totalMale += transaction.Male;
				totalFemale += transaction.Female;
				totalCash += transaction.Cash;
				totalCard += transaction.Card;
				totalUPI += transaction.UPI;
				totalAmex += transaction.Amex;

				i++;
			}

			pdfGrid.DataSource = dataTable;

			pdfGrid.Columns[0].Width = 20;
			pdfGrid.Columns[2].Width = 60;
			pdfGrid.Columns[3].Width = 30;
			pdfGrid.Columns[4].Width = 30;
			pdfGrid.Columns[5].Width = 30;
			pdfGrid.Columns[9].Width = 30;
			pdfGrid.Columns[10].Width = 40;

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

			using MemoryStream ms = new();
			pdfDocument.Save(ms);
			pdfDocument.Close(true);
			ms.Position = 0;
			SaveService saveService = new();
			saveService.SaveAndView("Invoice.pdf", "application/pdf", ms);
		}
		#endregion
	}
}
