using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace PubEntryWPF.Transaction.Advance;

/// <summary>
/// Interaction logic for AdvanceId.xaml
/// </summary>
public partial class AdvanceId : Page
{
	private readonly Frame _parentFrame;

	public AdvanceId(Frame parentFrame)
	{
		InitializeComponent();
		_parentFrame = parentFrame;
	}

	private void textBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
	{
		Regex regex = new("[^0-9]+");
		e.Handled = regex.IsMatch(e.Text);
	}

	private bool ValidateForm() => !string.IsNullOrEmpty(advanceIdTextBox.Text);

	private async void loadButton_Click(object sender, RoutedEventArgs e)
	{
		if (!ValidateForm())
		{
			MessageBox.Show("Please enter Advance Id", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			return;
		}

		var advance = await CommonData.LoadTableDataById<AdvanceModel>(Table.Advance, int.Parse(advanceIdTextBox.Text));

		if (advance is null || advance.TransactionId != 0)
		{
			MessageBox.Show("Invalid Advance Id", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			return;
		}

		_parentFrame.Content = new UpdateAdvance(advance, _parentFrame);
	}
}
