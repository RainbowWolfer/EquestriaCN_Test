using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using EQCN.AppSetting;

namespace EQCN {
	public sealed partial class App: Application {
		public App() {
			this.InitializeComponent();
			this.Suspending += OnSuspending;
		}

		protected override void OnLaunched(LaunchActivatedEventArgs e) {
			//LocalStorage.AppSettings.WriteSettings(new LocalStorage.AppSettings() { AppTheme = "" });
			if(!(Window.Current.Content is Frame rootFrame)) {
				rootFrame = new Frame();

				rootFrame.NavigationFailed += OnNavigationFailed;

				if(e.PreviousExecutionState == ApplicationExecutionState.Terminated) {
					//TODO: Load state from previously suspended application
				}

				Window.Current.Content = rootFrame;
			}

			if(e.PrelaunchActivated == false) {
				if(rootFrame.Content == null) {
					rootFrame.Navigate(typeof(MainPage), e.Arguments);
				}
				Window.Current.Activate();
			}

			SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;

			MainPage.Instance._VersionTextBlock.Text = string.Format("版本: {0}.{1}.{2}.{3}",
					Package.Current.Id.Version.Major,
					Package.Current.Id.Version.Minor,
					Package.Current.Id.Version.Build,
					Package.Current.Id.Version.Revision);

			AppSettings.WriteSettings(new AppSettings(ApplicationTheme.Dark));
			var v = AppSettings.GetSettings();
		}
		void OnNavigationFailed(object sender, NavigationFailedEventArgs e) {
			throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
		}

		private void OnSuspending(object sender, SuspendingEventArgs e) {
			var deferral = e.SuspendingOperation.GetDeferral();
			//TODO: Save application state and stop any background activity
			deferral.Complete();
		}
	}
	public interface IAdjustSize {
		void AdjustSize(double newHeight, double newWidth, double oldHeight, double oldWidth);
		void InitializeSize(double height, double width);
	}
}
