﻿using System.Windows;
using System.Windows.Controls;

namespace PubEntryWPF.Admin;

/// <summary>
/// Interaction logic for LocationPage.xaml
/// </summary>
public partial class LocationPage : Page
{
	public LocationPage() => InitializeComponent();

	private async void Page_Loaded(object sender, RoutedEventArgs e) => await LoadData();

	private async Task LoadData()
	{
		if (locationDataGrid is null) return;

		var nameSearch = searchTextBox.Text.Trim();

		var locations = await CommonData.LoadTableData<LocationModel>(Table.Location);

		bool showActive = showActiveCheckBox?.IsChecked ?? false;
		bool showInactive = showInactiveCheckBox?.IsChecked ?? false;

		locationDataGrid.ItemsSource = locations
			.Where(item => string.IsNullOrEmpty(nameSearch) || item.Name.Contains(nameSearch, StringComparison.CurrentCultureIgnoreCase))
			.Where(item => (showActive && item.Status) || (showInactive && !item.Status))
			.OrderBy(item => !item.Status)
			.ToList();

		foreach (var column in locationDataGrid.Columns)
		{
			column.MinWidth = 100;
			column.IsReadOnly = true;
			if (column.Header.ToString() == "Id") column.MinWidth = 50;
		}

		UpdateFields();
	}

	private async void searchTextBox_TextChanged(object sender, TextChangedEventArgs e) => await LoadData();

	private async void showCheckBox_CheckedChanged(object sender, RoutedEventArgs e) => await LoadData();

	private void locationDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e) => UpdateFields();

	private void nameTextBox_TextChanged(object sender, TextChangedEventArgs e) => UpdateButtonField();

	private void UpdateFields()
	{
		if (locationDataGrid.SelectedItem is LocationModel selectedLocation)
		{
			nameTextBox.Text = selectedLocation.Name;
			statusCheckBox.IsChecked = selectedLocation.Status;
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
		if (locationDataGrid.SelectedItem is null) saveButton.Content = "Save";
		else saveButton.Content = "Update";

		if (!string.IsNullOrEmpty(nameTextBox.Text)) saveButton.IsEnabled = true;
		else saveButton.IsEnabled = false;
	}

	private async void saveButton_Click(object sender, RoutedEventArgs e)
	{
		if (string.IsNullOrEmpty(nameTextBox.Text.Trim()))
		{
			MessageBox.Show("Please enter a Location Name", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			return;
		}

		LocationModel locationModel = new()
		{
			Name = nameTextBox.Text,
			Status = (bool)statusCheckBox.IsChecked
		};

		if (locationDataGrid.SelectedItem is LocationModel selectedLocation)
		{
			locationModel.Id = selectedLocation.Id;
			await LocationData.UpdateLocation(locationModel);
		}
		else await LocationData.InsertLocation(locationModel);

		await LoadData();
	}
}