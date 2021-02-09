using Data.Comments;
using Data.Users;
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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace EQCN.Model {
	public sealed partial class SingleCommentViewer: UserControl {
		public Comment comment { get; private set; }
		public SingleCommentViewer(Comment comment) {
			this.InitializeComponent();
			this.comment = comment;
		}

		private async void UserControl_Loading(FrameworkElement sender, object args) {
			AuthorNameTextBlock.Text = comment.AuthorName;
			DetailTextBlock.Text = comment.Date.Value.Date.ToString();
			var style = new PostDisplayer.ParagraphStyle() { normalTextSize = 24 };
			PostDisplayer.AddToStackPanel(ContentHolderStackPanel, PostDisplayer.GetCompleteParagraphs(comment.Content.Rendered), style);
			//ContentTextBlock.Text = ;
			string url = comment.AuthorAvatarUrls["96"];
			WriteableBitmap source = await User.GetUserAvator(url);
			if(source == null) {
				BitmapImage bitmapImage = new BitmapImage {
					UriSource = new Uri(AvatorImage.BaseUri, @"..\Assets\avator_what.jpg")
				};
				AvatorImage.Source = bitmapImage;
			} else {
				AvatorImage.Source = source;
			}
		}
	}
}
