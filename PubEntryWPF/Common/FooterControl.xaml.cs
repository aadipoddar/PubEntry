using System.Diagnostics;
using System.Windows.Controls;

namespace PubEntryWPF.Common;

/// <summary>
/// Interaction logic for FooterControl.xaml
/// </summary>
public partial class FooterControl : UserControl
{
	public FooterControl() => InitializeComponent();

	private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
	{
		Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true });
		e.Handled = true;
	}
}
