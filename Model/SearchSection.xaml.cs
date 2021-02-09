using Data;
using Data.Posts;
using Data.Users;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
	public sealed partial class SearchSection: UserControl {
		public ObservableCollection<SingleSearchResult> results;
		public Post selectedPost { get; private set; }
		public SearchSection() {
			this.InitializeComponent();
			results = new ObservableCollection<SingleSearchResult>();
			TipTextBlock.Visibility = Visibility.Collapsed;
			MyProgressBar.Visibility = Visibility.Collapsed;
		}

		private void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args) {
			results.Clear();
			if(!string.IsNullOrEmpty(sender.Text)) {
				UpdateResults(sender.Text);
			}
		}
		private async void UpdateResults(string content) {
			TipTextBlock.Visibility = Visibility.Collapsed;
			MyProgressBar.Visibility = Visibility.Visible;
			Post[] posts = (await Post.GetPostsByContent(content)).ToArray();
			if(posts.Length == 0) {
				MyProgressBar.Visibility = Visibility.Collapsed;
				TipTextBlock.Visibility = Visibility.Visible;
				return;
			}
			foreach(Post p in posts) {
				bool isExisted = false;
				foreach(SingleSearchResult result in results) {
					if((int)result.post.Id == (int)p.Id) {
						isExisted = true;
					}
				}
				if(isExisted) {
					continue;
				}
				User author = await User.GetUser((int)p.Author);
				p.author_User = author;
				p.categories_List = new List<CategoryID>();
				foreach(long id in p.Categories) {
					p.categories_List.Add((CategoryID)id);
				}
				results.Add(new SingleSearchResult(p));
			}
			MyProgressBar.Visibility = Visibility.Collapsed;
		}

		private void ContentListView_ItemClick(object sender, ItemClickEventArgs e) {
			SingleSearchResult result = e.ClickedItem as SingleSearchResult;
			//MainPage.Instance.LoadPost(result.post);
			selectedPost = result.post;
		}
	}
}
