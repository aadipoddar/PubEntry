using System.Windows;
using System.Windows.Controls;

namespace PubEntryWPF.Admin;

/// <summary>
/// Interaction logic for PaymentModePage.xaml
/// </summary>
public partial class PaymentModePage : Page
{
	public PaymentModePage() => InitializeComponent();

	private async void Page_Loaded(object sender, RoutedEventArgs e) => await LoadData();

	private async Task LoadData()
	{
		if (paymentDataGrid is null) return;

		var nameSearch = searchTextBox.Text.Trim();

		var paymentModes = await CommonData.LoadTableData<PaymentModeModel>(Table.PaymentMode);

		bool showActive = showActiveCheckBox?.IsChecked ?? false;
		bool showInactive = showInactiveCheckBox?.IsChecked ?? false;

		paymentDataGrid.ItemsSource = paymentModes
			.Where(item => string.IsNullOrEmpty(nameSearch) || item.Name.Contains(nameSearch, StringComparison.CurrentCultureIgnoreCase))
			.Where(item => (showActive && item.Status) || (showInactive && !item.Status))
			.OrderBy(item => !item.Status)
			.ToList();

		foreach (var column in paymentDataGrid.Columns)
		{
			column.MinWidth = 100;
			column.IsReadOnly = true;
			if (column.Header.ToString() == "Id") column.MinWidth = 50;
		}

		UpdateFields();
	}

	private async void searchTextBox_TextChanged(object sender, TextChangedEventArgs e) => await LoadData();

	private async void showCheckBox_CheckedChanged(object sender, RoutedEventArgs e) => await LoadData();

	private void paymentDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e) => UpdateFields();

	private void nameTextBox_TextChanged(object sender, TextChangedEventArgs e) => UpdateButtonField();

	private void UpdateFields()
	{
		if (paymentDataGrid.SelectedItem is PaymentModeModel selectedPayemntMode)
		{
			nameTextBox.Text = selectedPayemntMode.Name;
			statusCheckBox.IsChecked = selectedPayemntMode.Status;
			saveButton.Content = "Update";
			saveButton.IsEnabled = true;
		}

		else
		{
			nameTextBox.Clear();
			statusCheckBox.IsChecked = true;
			saveButton.Content = "Save";
			saveButton.IsEnabled = false;
		}

		UpdateButtonField();
	}

	private void UpdateButtonField()
	{
		if (paymentDataGrid.SelectedItem is null) saveButton.Content = "Save";
		else saveButton.Content = "Update";

		if (!string.IsNullOrEmpty(nameTextBox.Text)) saveButton.IsEnabled = true;
		else saveButton.IsEnabled = false;
	}

	private async void saveButton_Click(object sender, RoutedEventArgs e)
	{
		if (string.IsNullOrEmpty(nameTextBox.Text.Trim()))
		{
			MessageBox.Show("Please enter a Payment Mode Name", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			return;
		}

		PaymentModeModel paymentModeModel = new()
		{
			Name = nameTextBox.Text,
			Status = (bool)statusCheckBox.IsChecked
		};

		if (paymentDataGrid.SelectedItem is PaymentModeModel selectedPaymentMode)
		{
			paymentModeModel.Id = selectedPaymentMode.Id;
			await PaymentModeData.UpdatePaymentMode(paymentModeModel);
		}
		else await PaymentModeData.InsertPaymentMode(paymentModeModel);

		await LoadData();
	}
}
