using System.Windows;
using System.Windows.Controls;

namespace PubEntryWPF.Admin
{
	/// <summary>
	/// Interaction logic for ReservationTypePage.xaml
	/// </summary>
	public partial class ReservationTypePage : Page
	{
		public ReservationTypePage()
		{
			InitializeComponent();
		}

		private async void Page_Loaded(object sender, RoutedEventArgs e) => await LoadData();

		private async Task LoadData()
		{
			if (reservationDataGrid is null) return;

			var nameSearch = searchTextBox.Text.Trim();

			var reservations = await CommonData.LoadTableData<ReservationTypeModel>(Table.ReservationType);

			bool showActive = showActiveCheckBox?.IsChecked ?? false;
			bool showInactive = showInactiveCheckBox?.IsChecked ?? false;

			reservationDataGrid.ItemsSource = reservations
				.Where(item => string.IsNullOrEmpty(nameSearch) || item.Name.Contains(nameSearch, StringComparison.CurrentCultureIgnoreCase))
				.Where(item => (showActive && item.Status) || (showInactive && !item.Status))
				.OrderBy(item => !item.Status)
				.ToList();

			foreach (var column in reservationDataGrid.Columns)
			{
				column.MinWidth = 100;
				column.IsReadOnly = true;
				if (column.Header.ToString() == "Id") column.MinWidth = 50;
			}

			UpdateFields();
		}

		private async void searchTextBox_TextChanged(object sender, TextChangedEventArgs e) => await LoadData();

		private async void showCheckBox_CheckedChanged(object sender, RoutedEventArgs e) => await LoadData();

		private void reservationDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e) => UpdateFields();

		private void nameTextBox_TextChanged(object sender, TextChangedEventArgs e) => UpdateButtonField();

		private void UpdateFields()
		{
			if (reservationDataGrid.SelectedItem is ReservationTypeModel selectedReservation)
			{
				nameTextBox.Text = selectedReservation.Name;
				statusCheckBox.IsChecked = selectedReservation.Status;
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
			if (reservationDataGrid.SelectedItem is null) saveButton.Content = "Save";
			else saveButton.Content = "Update";

			if (!string.IsNullOrEmpty(nameTextBox.Text)) saveButton.IsEnabled = true;
			else saveButton.IsEnabled = false;
		}

		private async void saveButton_Click(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrEmpty(nameTextBox.Text.Trim()))
			{
				MessageBox.Show("Please enter a Reservation name", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			ReservationTypeModel reservationTypeModel = new()
			{
				Name = nameTextBox.Text,
				Status = (bool)statusCheckBox.IsChecked
			};

			if (reservationDataGrid.SelectedItem is ReservationTypeModel selectedReservation)
			{
				reservationTypeModel.Id = selectedReservation.Id;
				await ReservationTypeData.UpdateReservationType(reservationTypeModel);
			}
			else await ReservationTypeData.InsertReservationType(reservationTypeModel);

			await LoadData();
		}
	}
}
