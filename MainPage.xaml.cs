using Data;
using Data.Posts;
using Data.Users;
using Data.Categories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Windows.UI.Popups;
using EQCN.Pages;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.System;
using EQCN.Model;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;

namespace EQCN {
	public sealed partial class MainPage: Page {
		public static MainPage Instance;

		public bool isLoaded = false;
		public ObservableCollection<Section> pivotSections;
		public ObservableCollection<Section> listviewSections;
		public Category[] categories;
		public bool isViewingPost = false;
		public PivotItem currentPivotItem { get; set; }

		public AdjustableSize adjustSize;

		public ObservableCollection<HistoryAction> historyActions { get; private set; }
		public bool canGoBack => historyActions.Count > 1;
		public void GoBack() {
			if(!canGoBack) {
				return;
			}
			HistoryAction action = historyActions[historyActions.Count - 2];
			if(action.actionType == HistoryAction.ActionType.LoadPage) {
				//currentPivotItem.Content = action.historyObject;
			} else if(action.actionType == HistoryAction.ActionType.PivotItemChange) {
				PivotItem pivotItem = action.historyObject as PivotItem;
				MainPivot.SelectedItem = pivotItem;
			}
			historyActions.Remove(action);
			BackButton.Visibility = canGoBack ? Visibility.Visible : Visibility.Collapsed;
		}

		public Grid _MainGrid => MainGrid;
		public TextBlock _VersionTextBlock => VersionTextBlock;

		public MainPage() {
			this.InitializeComponent();
			Instance = this;
			currentPivotItem = null;
			pivotSections = new ObservableCollection<Section>();
			listviewSections = new ObservableCollection<Section>();
			historyActions = new ObservableCollection<HistoryAction>();
			BackButton.Visibility = Visibility.Collapsed;
			//WideLayoutFrame.Navigate(typeof(SettingsPage));
			adjustSize = new AdjustableSize((h, w) => {//narrow
				HamburgerButton.Visibility = Visibility.Visible;
				OptionsButton.Visibility = Visibility.Collapsed;
				MyMask.Visibility = Visibility.Visible;
				//DeleteCatrgoriesInPivot();
			}, (h, w) => {//wide
				HamburgerButton.Visibility = Visibility.Collapsed;
				OptionsButton.Visibility = Visibility.Visible;
				CategoriesSplitView.IsPaneOpen = false;
				//LoadCategoriesToPivot(false);
				MyMask.Visibility = Visibility.Collapsed;
			});
		}
		private void Page_Loaded(object sender, RoutedEventArgs e) {
			adjustSize.InitializeSize(_MainGrid.ActualHeight, _MainGrid.ActualWidth);
		}
		private async void Page_Loading(FrameworkElement sender, object args) {
			StackPanel stackPanel = new StackPanel();
			ProgressRing progressRing = new ProgressRing() { IsActive = true, Height = 100, Width = 100, HorizontalAlignment = HorizontalAlignment.Center };
			stackPanel.Children.Add(progressRing);
			stackPanel.Children.Add(new TextBlock() { Text = "Welcome Home" + '\n' + "Equestrian", FontSize = 36, HorizontalAlignment = HorizontalAlignment.Center, HorizontalTextAlignment = TextAlignment.Center });
			ContentDialog dialog = new ContentDialog() {
				IsPrimaryButtonEnabled = false,
				IsSecondaryButtonEnabled = false,
			};
			Wait(dialog);
			dialog.Content = stackPanel;
			categories = (await Category.GetCategories()).ToArray();
			await LoadPages();
			dialog.Hide();
			isLoaded = true;

			MainPivot.SelectionChanged += MainPivot_SelectionChanged;
			LoadCategoriesToPivot();

			//ListView
			ListViewItem indexItem = new ListViewItem {
				Content = new TextBlock() { Text = "首页" }
			};
			listviewSections.Add(new Section(null, indexItem, new Index()));
			CategoriesListView.Items.Add(indexItem);
			foreach(Category c in categories) {
				ListViewItem listViewItem = new ListViewItem {
					Content = new TextBlock() { Text = c.Name }
				};
				listviewSections.Add(new Section(null, listViewItem, new PageDisplayer(c)));
				CategoriesListView.Items.Add(listViewItem);
			}
			//CategoriesListView.Items.Add(new Rectangle() { Height = 5, HorizontalAlignment = HorizontalAlignment.Stretch, Margin = new Thickness(5) });

			ListViewItem settingsItem = new ListViewItem {
				Content = new TextBlock() { Text = "设置", Name = "Settings" }
			};

			listviewSections.Add(new Section(null, settingsItem, new SettingsPage()));
			CategoriesListView.Items.Add(settingsItem);

			ListViewItem aboutItem = new ListViewItem {
				Content = new TextBlock() { Text = "关于", Name = "About" }
			};
			listviewSections.Add(new Section(null, aboutItem, null));
			CategoriesListView.Items.Add(aboutItem);

			ListViewItem officialItem = new ListViewItem {
				Content = new TextBlock() { Text = "官网", Name = "Official" }
			};
			listviewSections.Add(new Section(null, officialItem, null));
			CategoriesListView.Items.Add(officialItem);

			BackButton.Visibility = Visibility.Collapsed;
		}
		private async void Wait(ContentDialog dialog) {
			await dialog.ShowAsync();
		}
		private async Task LoadPages() {
			foreach(Category ca in categories) {
				ca.posts = await Post.GetPostsByCategory((int)ca.Id);
				LoadEachPosts(ca);
			}
		}
		private async void LoadEachPosts(Category category) {
			foreach(Post post in category.posts) {
				post.author_User = await Data.Users.User.GetUser((int)post.Author);
				post.categories_List = new List<CategoryID>();
				foreach(long id in post.Categories) {
					post.categories_List.Add((CategoryID)id);
				}
				if(post.BetterFeaturedImage != null) {
					post.thumbnail = await ImageConverter.GetWriteableBitmapAsync(Method.GetThumbnail(post.BetterFeaturedImage.MediaDetails.Sizes));
				} else {
					post.thumbnail = null;
				}
			}

		}
		private void LoadCategoriesToPivot() {
			PivotItem previous = currentPivotItem;
			MainPivot.Items.Clear();
			pivotSections.Clear();
			PivotItem itemIndex = new PivotItem() { Header = "首页", FontSize = 20 };
			pivotSections.Add(new Section(null, itemIndex, new Index()));
			MainPivot.Items.Add(itemIndex);
			currentPivotItem = itemIndex;
			foreach(Category c in categories) {
				PivotItem item = new PivotItem() { Header = c.Name };
				pivotSections.Add(new Section(c, item, new PageDisplayer(c)));
				MainPivot.Items.Add(item);
			}
		}
		private void MainPivot_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			Pivot pivot = sender as Pivot;
			PivotItem item = pivot.SelectedItem as PivotItem;
			currentPivotItem = item;
			foreach(Section section in pivotSections) {
				if(section.holder == currentPivotItem) {
					HistoryAction action = new HistoryAction(section.holder, HistoryAction.ActionType.PivotItemChange);
					historyActions.Add(action);
					currentPivotItem.Content = section.displayPage;
					BackButton.Visibility = canGoBack ? Visibility.Visible : Visibility.Collapsed;
					break;
				}
			}
		}
		private void CategoriesListView_ItemClick(object sender, ItemClickEventArgs e) {
			ListView listView = sender as ListView;
			CategoriesSplitView.IsPaneOpen = false;
			ListViewItem current = null;
			foreach(ListViewItem item in listView.Items) {
				if(item.Content == e.ClickedItem) {
					current = item;
					break;
				}
			}
			if(current == null) {
				throw new Exception();
			}
			foreach(Section sec in listviewSections) {
				if(sec.holder == current) {
					if(sec.displayPage == null) {
						string name = (sec.holder.Content as TextBlock).Name;
						if(name == "About") {
							AboutFlyoutItem_Click(null, null);
						} else if(name == "Official") {
							OfficialFlyoutItem_Click(null, null);
						}
					} else {
						//currentPivotItem = current;
						currentPivotItem.Content = sec.displayPage;
					}
				}
			}


		}

		public void LoadPost(Post post) {
			Page page = new PostDisplayer(post);
			HistoryAction action = new HistoryAction(page, HistoryAction.ActionType.LoadPage);
			historyActions.Add(action);
			currentPivotItem.Content = action.historyObject;
			BackButton.Visibility = canGoBack ? Visibility.Visible : Visibility.Collapsed;
			isViewingPost = true;
		}

		public void UnloadPost() {
			isViewingPost = false;

		}

		private void SettingsBtn_Click(object sender, RoutedEventArgs e) {
			SettingsSpiltView.IsPaneOpen = !SettingsSpiltView.IsPaneOpen;
		}

		private void SettingsFlyoutItem_Click(object sender, RoutedEventArgs e) {
			if(currentPivotItem != null) {
				//Page page = new SettingsPage();
				//HistoryAction action = new HistoryAction(page, HistoryAction.ActionType.LoadPage);
				//historyActions.Add(action);
				//currentPivotItem.Content = action.historyObject;
				//BackButton.Visibility = canGoBack ? Visibility.Visible : Visibility.Collapsed;
				currentPivotItem.Content = new SettingsPage();
			}
		}

		private void AboutFlyoutItem_Click(object sender, RoutedEventArgs e) {
			SettingsSpiltView.IsPaneOpen = true;
		}

		private async void OfficialFlyoutItem_Click(object sender, RoutedEventArgs e) {
			bool success = await Launcher.LaunchUriAsync(new Uri("https://www.equestriacn.com/"));
		}

		private async void SearchButton_Click(object sender, RoutedEventArgs e) {
			ContentDialog contentDialog = new ContentDialog() {
				FullSizeDesired = true,
				Title = "搜索文章",
				IsSecondaryButtonEnabled = true,
				IsPrimaryButtonEnabled = true,
				SecondaryButtonText = "返回",
				PrimaryButtonText = "确定",
			};
			contentDialog.Content = new SearchSection();
			ContentDialogResult result = await contentDialog.ShowAsync();
			if(result == ContentDialogResult.Primary) {
				Post post = (contentDialog.Content as SearchSection).selectedPost;
				if(post != null) {
					LoadPost(post);
				}
			}
		}

		private void BackButton_Click(object sender, RoutedEventArgs e) {
			GoBack();
		}

		private void HamburgerButton_Click(object sender, RoutedEventArgs e) {
			CategoriesSplitView.IsPaneOpen = !CategoriesSplitView.IsPaneOpen;
		}



		private void MainGrid_SizeChanged(object sender, SizeChangedEventArgs e) {
			if(isLoaded) {
				adjustSize.AdjustSize(e.NewSize, e.PreviousSize);
			}
		}

	}
	public class Section {
		public Section(Category category, ContentControl holder, Page displayPage) {
			this.category = category;
			this.holder = holder;
			this.displayPage = displayPage;
		}

		public ContentControl holder { get; set; }
		public Category category { get; set; }
		public Page displayPage { get; set; }
		public List<Post> posts { get; set; }
	}
	public class HistoryAction {
		public object historyObject { get; set; }
		public ActionType actionType;

		public HistoryAction(object historyObject, ActionType actionType) {
			this.historyObject = historyObject;
			this.actionType = actionType;
		}

		public enum ActionType {
			PivotItemChange = 0, LoadPage = 1
		}
	}
}
