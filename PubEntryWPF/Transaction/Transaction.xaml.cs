using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows;

namespace PubEntryWPF.Transaction
{
	/// <summary>
	/// Interaction logic for Transaction.xaml
	/// </summary>
	public partial class Transaction : Window
	{
		private readonly Dashboard _dashboard;

		public Transaction(Dashboard dashboard)
		{
			InitializeComponent();
			_dashboard = dashboard;
		}

		private async void Window_Loaded(object sender, RoutedEventArgs e)
		{
			reservationComboBox.ItemsSource = await CommonData.LoadTableData<ReservationTypeModel>(Table.ReservationType);
			reservationComboBox.DisplayMemberPath = nameof(ReservationTypeModel.Name);
			reservationComboBox.SelectedValuePath = nameof(ReservationTypeModel.Id);
			reservationComboBox.SelectedIndex = 0;

			richTextBoxFooter.Text = $"Version: {Assembly.GetExecutingAssembly().GetName().Version}";
		}

		private void Window_Closed(object sender, EventArgs e)
		{
			Close();
			_dashboard.Show();
		}

		private void maleTextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
		{

		}

		private void textBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
		{
			Regex regex = new("[^0-9]+");
			e.Handled = regex.IsMatch(e.Text);
		}
	}
}
