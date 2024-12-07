using PubEntryLibrary.Data;
using PubEntryLibrary.Models;

using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace PubEntry;

public partial class MainForm : Form
{
	int employee, location;
	PersonModel person = new PersonModel();

	public MainForm(int location, int employee)
	{
		InitializeComponent();

		this.employee = employee;
		this.location = location;

		Task task = LoadComboBox();
	}

	private async Task LoadComboBox()
	{
		paymentMethodComboBox.DataSource = null;

		var methods = (await DataAccess.LoadTableData<PaymentMethodModel>("PaymentMethodTable")).ToList();

		paymentMethodComboBox.DataSource = methods;
		paymentMethodComboBox.DisplayMember = "Name";
	}

	private async void insertButton_Click(object sender, EventArgs e)
	{
		var people = (await DataAccess.LoadTableData<PersonModel>("PersonTable")).ToList();

		if (people.Count != 0)
			person.Id = people.LastOrDefault().Id + 1;
		else
			person.Id = 1;

		person.Name = nameTextBox.Text;
		person.Number = numberTextBox.Text;
		person.Amount = (int)Convert.ToInt64(amountTextBox.Text);
		person.ModeOfPayment = paymentMethodComboBox.SelectedIndex + 1;
		person.EntryTime = DateTime.Now;
		person.EmployeeId = employee;
		person.LocationId = location;

		await DataAccess.InsertPersonTableData(person);

		Print();

		ClearForm();
	}

	private void Print()
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
					.Text($"{Task.Run(async () => await DataAccess.GetLocationNameById(person.LocationId)).Result}")
					.SemiBold().FontSize(36).FontColor(Colors.Blue.Medium);

				page.Content()
					.PaddingVertical(1, Unit.Centimetre)
					.Column(x =>
					{
						x.Spacing(20);

						x.Item().Text($"Bill Id / Guest Id = #{person.Id}");
						x.Item().Text($"Employee Name = {Task.Run(async () => await DataAccess.GetEmployeeNameById(person.EmployeeId)).Result}");
						x.Item().Text($"Guest Name = {person.Name}");
						x.Item().Text($"Guest Number = {person.Number}");
						x.Item().Text($"Amount Paid = {person.Amount}");
						x.Item().Text($"Mode of Payment = {Task.Run(async () => await DataAccess.GetModOfPaymentName(person.ModeOfPayment)).Result}");
						x.Item().Text($"Time Entered = {person.EntryTime}");
					});

				page.Footer()
					.AlignCenter()
					.Text(x =>
					{
						x.Span($"Thank you for Visiting {Task.Run(async () => await DataAccess.GetLocationNameById(person.LocationId)).Result}");
					});
			});
		}).GeneratePdfAndShow();
	}

	private void ClearForm()
	{
		nameTextBox.Text = string.Empty;
		numberTextBox.Text = string.Empty;
		amountTextBox.Text = string.Empty;
	}

	private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
	{
		Application.Exit();
	}

	private void numberTextBox_TextChanged(object sender, EventArgs e)
	{
		var foundPerson = Task.Run(async () => await DataAccess.GetPersonByNumber(numberTextBox.Text)).Result.FirstOrDefault();
		if (foundPerson != null)
		{
			nameTextBox.Text = foundPerson.Name;
			numberTextBox.Text = foundPerson.Number;
			amountTextBox.Text = foundPerson.Amount.ToString();
			paymentMethodComboBox.SelectedIndex = foundPerson.ModeOfPayment - 1;

			insertButton.Enabled = false;
		}

		else
		{
			insertButton.Enabled = true;
		}
	}
}
