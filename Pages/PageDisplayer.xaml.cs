using Data;
using Data.Categories;
using Data.Posts;
using Data.Users;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Imaging;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace EQCN.Pages {
	public sealed partial class PageDisplayer: Page {
		public string title { get; private set; }
		public Category category;
		public Post[] posts;

		public AdjustableSize adjustableSize;

		//public ObservableCollection<PostItemInfo> items;

		public PageDisplayer(Category category) {
			this.InitializeComponent();
			//items = new ObservableCollection<PostItemInfo>();
			this.category = category;
			this.posts = category.posts.ToArray();
			adjustableSize = new AdjustableSize((h, w) => {
				MainGridView.Height = MainPage.Instance._MainGrid.ActualHeight - 200;
				MainGridView.Visibility = Visibility.Collapsed;
				NarrowListView.Visibility = Visibility.Visible;
			}, (h, w) => {
				MainGridView.Height = MainPage.Instance._MainGrid.ActualHeight - 200;
				MainGridView.Visibility = Visibility.Visible;
				NarrowListView.Visibility = Visibility.Collapsed;
			});
			this.title = Method.ConvertID2Category((CategoryID)category.Id);

			adjustableSize.InitializeSize(MainPage.Instance._MainGrid.ActualHeight, MainPage.Instance._MainGrid.ActualWidth);
			MainPage.Instance.SizeChanged += (s, c) => {
				adjustableSize.AdjustSize(c.NewSize, c.PreviousSize);
			};
		}

		private void MainGridView_ItemClick(object sender, ItemClickEventArgs e) {
			Post post = e.ClickedItem as Post;
			MainPage.Instance.LoadPost(post);
		}

		private void Grid_PointerEntered(object sender, PointerRoutedEventArgs e) {
			Grid g = sender as Grid;
			StackPanel s = g.Children.ToList().Find((c) => c is StackPanel) as StackPanel;
			s.Height = 200;
		}

		private void Grid_PointerExited(object sender, PointerRoutedEventArgs e) {
			Grid g = sender as Grid;
			StackPanel s = g.Children.ToList().Find((c) => c is StackPanel) as StackPanel;
			s.Height = 100;
		}

		private void ThumbnailImage_Loaded(object sender, RoutedEventArgs e) {
			Image image = sender as Image;
			if(image.Source == null) {
				BitmapImage bitmapImage = new BitmapImage {
					UriSource = new Uri(image.BaseUri, @"..\Assets\BlankImage.jpg")
				};
				image.Source = bitmapImage;
			}
		}

		private void ThumbnailImage_Loading(FrameworkElement sender, object args) {

		}
	}
	[Obsolete("", true)]
	public class PostItemInfo {
		public PostItemInfo(Post post, ImageSource source) {
			this.post = post;
			this.imagePath = source;
		}
		public Post post { get; set; }

		public ImageSource imagePath;

	}
}