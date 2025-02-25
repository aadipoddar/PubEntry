using System.Windows;
using System.Windows.Controls;

namespace PubEntryWPF.Admin;

/// <summary>
/// Interaction logic for UserPage.xaml
/// </summary>
public partial class UserPage : Page
{
	public UserPage() => InitializeComponent();

	private async void Page_Loaded(object sender, RoutedEventArgs e) => await LoadData();

	private async Task LoadData()
	{
		locationComboBox.ItemsSource = await CommonData.LoadTableData<LocationModel>(Table.Location);
		locationComboBox.DisplayMemberPath = nameof(LocationModel.Name);
		locationComboBox.SelectedValuePath = nameof(LocationModel.Id);
		locationComboBox.SelectedIndex = 0;

		searchLocationComboBox.ItemsSource = await CommonData.LoadTableData<LocationModel>(Table.Location);
		searchLocationComboBox.DisplayMemberPath = nameof(LocationModel.Name);
		searchLocationComboBox.SelectedValuePath = nameof(LocationModel.Id);
		searchLocationComboBox.SelectedIndex = -1;

		await ApplySearchFilter();
	}

	private async Task ApplySearchFilter()
	{
		if (userDataGrid is null || searchLocationComboBox is null) return;

		var nameSearch = searchTextBox.Text.Trim();

		List<UserLocationModel> users = [];

		foreach (var user in await CommonData.LoadTableData<UserModel>(Table.User))
			users.Add(new UserLocationModel
			{
				Id = user.Id,
				Name = user.Name,
				Password = user.Password,
				LocationId = user.LocationId,
				Location = (await CommonData.LoadTableDataById<LocationModel>(Table.Location, user.LocationId)).Name,
				Admin = user.Admin,
				Status = user.Status
			});

		bool showActive = showActiveCheckBox?.IsChecked ?? false;
		bool showInactive = showInactiveCheckBox?.IsChecked ?? false;
		bool showAdmin = showAdminCheckBox?.IsChecked ?? false;
		bool showNonAdmin = showNonAdminCheckBox?.IsChecked ?? false;

		userDataGrid.ItemsSource = users
			.Where(item => string.IsNullOrEmpty(nameSearch) || item.Name.Contains(nameSearch, StringComparison.CurrentCultureIgnoreCase))
			.Where(item => searchLocationComboBox.SelectedIndex == -1 || item.LocationId == (int)searchLocationComboBox.SelectedValue)
			.Where(item => (showActive && item.Status) || (showInactive && !item.Status))
			.Where(item => (showAdmin && item.Admin) || (showNonAdmin && !item.Admin))
			.OrderBy(item => !item.Status)
			.ToList();

		foreach (var column in userDataGrid.Columns)
		{
			column.MinWidth = 100;
			column.IsReadOnly = true;
			if (column.Header.ToString() == "Id") column.MinWidth = 50;
		}

		UpdateFields();
	}

	private async void searchTextBox_TextChanged(object sender, TextChangedEventArgs e) => await ApplySearchFilter();

	private async void searchLocationComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) => await ApplySearchFilter();

	private async void showCheckBox_CheckedChanged(object sender, RoutedEventArgs e) => await ApplySearchFilter();

	private void userDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e) => UpdateFields();

	private void nameTextBox_TextChanged(object sender, TextChangedEventArgs e) => UpdateButtonField();

	private void UpdateFields()
	{
		if (userDataGrid.SelectedItem is UserLocationModel selectedUser)
		{
			nameTextBox.Text = selectedUser.Name;
			passwordBox.Password = selectedUser.Password;
			statusCheckBox.IsChecked = selectedUser.Status;
			adminCheckBox.IsChecked = selectedUser.Admin;
			locationComboBox.SelectedValue = selectedUser.LocationId;
			saveButton.Content = "Update";
			saveButton.IsEnabled = true;
		}

		else
		{
			nameTextBox.Clear();
			passwordBox.Clear();
			statusCheckBox.IsChecked = true;
			adminCheckBox.IsChecked = false;
			saveButton.Content = "Save";
			saveButton.IsEnabled = false;
		}

		UpdateButtonField();
	}

	private void UpdateButtonField()
	{
		if (userDataGrid.SelectedItem is null) saveButton.Content = "Save";
		else saveButton.Content = "Update";

		if (!string.IsNullOrEmpty(nameTextBox.Text)) saveButton.IsEnabled = true;
		else saveButton.IsEnabled = false;
	}

	private bool ValidateForm()
	{
		if (string.IsNullOrEmpty(nameTextBox.Text.Trim()))
		{
			MessageBox.Show("Please enter a User Name", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			return false;
		}

		if (string.IsNullOrEmpty(passwordBox.Password.Trim()))
		{
			MessageBox.Show("Please enter a Password", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			return false;
		}

		return true;
	}

	private async void saveButton_Click(object sender, RoutedEventArgs e)
	{
		if (!ValidateForm()) return;

		UserModel userModel = new()
		{
			Name = nameTextBox.Text,
			Password = passwordBox.Password,
			LocationId = (int)locationComboBox.SelectedValue,
			Admin = (bool)adminCheckBox.IsChecked,
			Status = (bool)statusCheckBox.IsChecked
		};

		if (userDataGrid.SelectedItem is UserLocationModel selectedUser)
		{
			userModel.Id = selectedUser.Id;
			await UserData.UpdateUser(userModel);
		}
		else await UserData.InsertUser(userModel);

		await ApplySearchFilter();
	}
}