using System.Windows;

namespace PubEntryWPF;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class Dashboard : Window
{
	public Dashboard()
	{
		InitializeComponent();
		UpdateCheck();
	}

	private static async void UpdateCheck()
	{
		try
		{
			var isUpdateAvailable = await AadiSoftUpdater.AadiSoftUpdater.CheckForUpdates("aadipoddar", $"{Secrets.DatabaseName}", Assembly.GetExecutingAssembly().GetName().Version.ToString());

			if (!isUpdateAvailable) return;
			if (MessageBox.Show("New Version Available. Do you want to update?", "Update Available", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
				await AadiSoftUpdater.AadiSoftUpdater.UpdateApp("aadipoddar", $"{Secrets.DatabaseName}", "PubEntrySetup", "477557B4-2908-4106-B360-D2D114F02452");
		}
		catch (Exception)
		{
			MessageBox.Show("No Internet Connection", "Network Error", MessageBoxButton.OK, MessageBoxImage.Error);
		}
	}

	private async void Window_Loaded(object sender, RoutedEventArgs e) => await LoadLocationComboBox();

	private async Task LoadLocationComboBox()
	{
		locationComboBox.ItemsSource = await CommonData.LoadTableDataByStatus<LocationModel>(Table.Location);
		locationComboBox.DisplayMemberPath = nameof(LocationModel.Name);
		locationComboBox.SelectedValuePath = nameof(LocationModel.Id);
		locationComboBox.SelectedIndex = 0;

		richTextBoxFooter.Text = $"Version: {Assembly.GetExecutingAssembly().GetName().Version}";
	}

	private async Task LoadUserComboBox()
	{
		userComboBox.ItemsSource = await UserData.LoadUsersByLocationId(int.Parse(locationComboBox.SelectedValue.ToString()));
		userComboBox.DisplayMemberPath = nameof(UserModel.Name);
		userComboBox.SelectedValuePath = nameof(UserModel.Id);
		userComboBox.SelectedIndex = 0;
	}

	private async void locationComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e) => await LoadUserComboBox();

	private void userComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
	{
		passwordBox.Clear();
		passwordBox.Focus();

		if ((userComboBox.SelectedItem as UserModel).Admin)
			adminButton.Visibility = Visibility.Visible;
		else adminButton.Visibility = Visibility.Collapsed;
	}

	private bool ValidatePassword()
	{
		if ((userComboBox.SelectedItem as UserModel)?.Password == passwordBox.Password)
		{
			passwordBox.Clear();
			return true;
		}

		passwordBox.Clear();
		return false;
	}

	private void entryButton_Click(object sender, RoutedEventArgs e)
	{
		if (!ValidatePassword())
		{
			MessageBox.Show("Incorrect Password", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			return;
		}

		Transaction.Transaction transaction = new(this, int.Parse(locationComboBox.SelectedValue.ToString()), int.Parse(userComboBox.SelectedValue.ToString()));
		Hide();
		transaction.ShowDialog();
	}

	private void advanceButton_Click(object sender, RoutedEventArgs e)
	{
		if (!ValidatePassword())
		{
			MessageBox.Show("Incorrect Password", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			return;
		}

		Transaction.Advance.Advance advance = new(this, int.Parse(locationComboBox.SelectedValue.ToString()), int.Parse(userComboBox.SelectedValue.ToString()));
		Hide();
		advance.ShowDialog();
	}

	private void reportsButton_Click(object sender, RoutedEventArgs e)
	{

	}

	private void adminButton_Click(object sender, RoutedEventArgs e)
	{

	}
}