﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace PubEntryWPF;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
	protected override void OnStartup(StartupEventArgs e)
	{
		Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(Secrets.SyncfusionLicense);

		// Select the text in a TextBox when it receives focus.
		EventManager.RegisterClassHandler(typeof(TextBox), TextBox.PreviewMouseLeftButtonDownEvent,
			new MouseButtonEventHandler(SelectivelyIgnoreMouseButton));
		EventManager.RegisterClassHandler(typeof(TextBox), TextBox.GotKeyboardFocusEvent,
			new RoutedEventHandler(SelectAllText));
		EventManager.RegisterClassHandler(typeof(TextBox), TextBox.MouseDoubleClickEvent,
			new RoutedEventHandler(SelectAllText));
		base.OnStartup(e);
	}

	void SelectivelyIgnoreMouseButton(object sender, MouseButtonEventArgs e)
	{
		// Find the TextBox
		DependencyObject parent = e.OriginalSource as UIElement;
		while (parent != null && parent is not TextBox)
			parent = VisualTreeHelper.GetParent(parent);

		if (parent != null)
		{
			var textBox = (TextBox)parent;
			if (!textBox.IsKeyboardFocusWithin)
			{
				// If the text box is not yet focused, give it the focus and
				// stop further processing of this click event.
				textBox.Focus();
				e.Handled = true;
			}
		}
	}

	void SelectAllText(object sender, RoutedEventArgs e)
	{
		if (e.OriginalSource is TextBox textBox)
			textBox.SelectAll();
	}
}
