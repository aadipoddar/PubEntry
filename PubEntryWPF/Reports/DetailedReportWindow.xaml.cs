using System.Windows;

namespace PubEntryWPF.Reports
{
	/// <summary>
	/// Interaction logic for DetailedReportWindow.xaml
	/// </summary>
	public partial class DetailedReportWindow : Window
	{
		public DetailedReportWindow(DateTime fromDateTime, DateTime toDateTime, int locationId)
		{
			InitializeComponent();
		}
	}
}
