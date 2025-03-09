using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace PubEntryWPF.Transaction;

/// <summary>
/// Interaction logic for SlipId.xaml
/// </summary>
public partial class SlipIdPage : Page
{
	private readonly Frame _parentFrame;

	public SlipIdPage(Frame parentFrame)
	{
		InitializeComponent();
		_parentFrame = parentFrame;
	}

	private void textBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
	{
		Regex regex = new("[^0-9]+");
		e.Handled = regex.IsMatch(e.Text);
	}

	private bool ValidateForm() => !string.IsNullOrEmpty(slipIdTextBox.Text);

	private async void loadButton_Click(object sender, RoutedEventArgs e)
	{
		if (!ValidateForm())
		{
			MessageBox.Show("Please enter Transaction Id", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			return;
		}

		var transaction = await CommonData.LoadTableDataById<TransactionModel>(TableNames.Transaction, int.Parse(slipIdTextBox.Text));

		if (transaction is null)
		{
			MessageBox.Show("Invalid Transaction Id", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			return;
		}

		_parentFrame.Content = new UpdateTransactionPage(transaction, _parentFrame);
	}

	private async void Page_Loaded(object sender, RoutedEventArgs e) => await LoadData();

	private async Task LoadData()
	{
		//var transactions = await CommonData.LoadTableData<TransactionPrintModel>(Views.Transactions);
		//transactionDataGrid.ItemsSource = transactions;
	}
}
