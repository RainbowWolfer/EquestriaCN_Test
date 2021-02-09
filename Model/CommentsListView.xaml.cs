using Data.Comments;
using Data.Users;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace EQCN.Model {
	public sealed partial class CommentsListView: UserControl {
		public ListView _MainListView => MainListView;
		public int postID;
		public Comment[] comments { get; private set; }
		public CommentsListView(int postID) {
			this.InitializeComponent();
			this.postID = postID;
			LoadAllComments();
		}

		private void RefreshButton_Click(object sender, RoutedEventArgs e) {
			LoadAllComments();
		}
		private async void LoadAllComments() {
			CommentsProgressRing.IsActive = true;
			CommentsProgressRing.Visibility = Visibility.Visible;
			comments = (await Comment.GetComments(postID)).ToArray();
			TitleTextBlock.Text = "评论（" + comments.Length + "）";
			MainListView.Items.Clear();
			foreach(Comment comment in comments) {
				MainListView.Items.Add(new SingleCommentViewer(comment));
			}
			CommentsProgressRing.Visibility = Visibility.Collapsed;
			CommentsProgressRing.IsActive = false;
		}
	}
}
