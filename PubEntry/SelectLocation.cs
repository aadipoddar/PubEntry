using PubEntryLibrary.Data;
using PubEntryLibrary.Models;

using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace PubEntry;

public partial class SelectLocation : Form
{
	public SelectLocation()
	{
		InitializeComponent();

		Task task = LoadComboBox();
	}

	public async Task LoadComboBox()
	{
		locationComboBox.DataSource = null;
		employeeComboBox.DataSource = null;

		var locations = (await DataAccess.LoadTableData<LocationModel>("LocationTable")).ToList();
		var employees = (await DataAccess.LoadTableData<EmployeeModel>("EmployeeTable")).ToList();

		locationComboBox.DataSource = locations;
		locationComboBox.DisplayMember = "Name";

		employeeComboBox.DataSource = employees;
		employeeComboBox.DisplayMember = "Name";
	}

	private void goButton_Click(object sender, EventArgs e)
	{
		var employeeId = employeeComboBox.SelectedIndex;
		if (Task.Run(async () => await DataAccess.GetEmployeePasswordById(employeeId)).Result == passwordTextBox.Text)
		{
			MainForm mainForm = new(locationComboBox.SelectedIndex, employeeComboBox.SelectedIndex);
			mainForm.Show();
			Hide();
		}

		else
			MessageBox.Show("Incorrect Password");
	}

	private void finalPrintButton_Click(object sender, EventArgs e)
	{
		//Document.Create(container =>
		//{
		//	container.Page(page =>
		//	{
		//		page.Size(PageSizes.A4);
		//		page.Margin(2, Unit.Centimetre);
		//		page.PageColor(Colors.White);
		//		page.DefaultTextStyle(x => x.FontSize(20));


		//		page.Header()
		//			.Text("Report")
		//			.SemiBold().FontSize(36).FontColor(Colors.Blue.Medium);

		//		page.Content()
		//			.PaddingVertical(1, Unit.Centimetre)
		//			.Column(x =>
		//			{
		//				x.Spacing(20);

		//				var locations = Task.Run(async () => await DataAccess.LoadTableData<LocationModel>("LocationTable")).Result.ToList();

		//				foreach (var location in locations)
		//				{
		//					x.Item().Text(location.Name).SemiBold().FontSize(30).FontColor(Colors.Red.Medium);

		//					var people = Task.Run(async () => await DataAccess.GetPeopleByLocation(location.Id)).Result;

		//					if (people.Count != 0)
		//					{
		//						foreach (var person in people)
		//						{
		//							x.Item().Row(row =>
		//							{
		//								row.RelativeItem().Text($"{person.Id}").FontSize(10);
		//								row.RelativeItem().Text($"{person.Name}").FontSize(10);
		//								row.RelativeItem().Text($"{person.Number}").FontSize(10);
		//								row.RelativeItem().Text($"{person.EntryTime}").FontSize(10);
		//								row.RelativeItem().Text($"{Task.Run(async () => await DataAccess.GetEmployeeNameById(person.EmployeeId)).Result}").FontSize(10);
		//								row.RelativeItem().Text($"{Task.Run(async () => await DataAccess.GetModOfPaymentName(person.ModeOfPayment)).Result}").FontSize(10);
		//								row.RelativeItem().Text($"{person.Amount}").FontSize(10);
		//							});
		//						}
		//					}
		//				}
		//			});
		//	});
		//}).GeneratePdfAndShow();
	}
}
