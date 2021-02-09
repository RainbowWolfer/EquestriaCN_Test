using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

namespace EQCN.Pages {
	public sealed partial class SettingsPage: Page {
		private List<Rectangle> rectangles;
		public SettingsPage() {
			this.InitializeComponent();
			rectangles = new List<Rectangle>();
		}

		private void ThemeColorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {

		}

		private void Rectangle_Loaded(object sender, RoutedEventArgs e) {
			rectangles.Add((sender as Rectangle));
			(sender as Rectangle).Width = MainGridView.ActualWidth - 10;
		}

		private void MainGridView_SizeChanged(object sender, SizeChangedEventArgs e) {
			foreach(Rectangle r in rectangles) {
				r.Width = MainGridView.ActualWidth - 10;
			}
		}

		private void myLightThemeButton_Click(object sender, RoutedEventArgs e) {

		}

		private void myDarkThemebutton_Click(object sender, RoutedEventArgs e) {

		}

		private void myWindowsDefaultThemeButton_Click(object sender, RoutedEventArgs e) {

		}

		private void ChangeAddressButton_Click(object sender, RoutedEventArgs e) {

		}
	}
}
