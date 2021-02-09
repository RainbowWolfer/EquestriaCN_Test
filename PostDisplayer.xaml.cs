using Data;
using Data.Comments;
using Data.Posts;
using EQCN.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace EQCN {
	public sealed partial class PostDisplayer: Page {
		public Post post { get; set; }

		public AdjustableSize adjustableSize;

		public string subTitle => "作者：" + post.author_User.Name + " 时间：" + post.Date.Value;
		public string categoies {
			get {
				string result = "";
				post.categories_List.ForEach((c) => result += Method.ConvertID2Category(c) + " ");
				return result;
			}
		}

		public Paragraph[] paragraphs { get; private set; }
		public Comment[] comments { get; private set; }

		private CommentsListView sideCommentsListView { get; set; }
		private CommentsListView belowCommentsListView { get; set; }

		public PostDisplayer(Post post) {
			this.InitializeComponent();
			this.post = post;
			adjustableSize = new AdjustableSize((h, w) => {
				SideCommentViewerHolder.Visibility = Visibility.Collapsed;
				CommentViewerHolder.Visibility = Visibility.Visible;
				Grid.SetRow(RightDetailTextBlock, 1);
			}, (h, w) => {
				SideCommentViewerHolder.Visibility = Visibility.Visible;
				CommentViewerHolder.Visibility = Visibility.Collapsed;
				Grid.SetRow(RightDetailTextBlock, 0);
			});
			MainPage.Instance.SizeChanged += (s, c) => AdjustSize(c.NewSize.Width, c.NewSize.Height, c.PreviousSize.Width, c.PreviousSize.Height);
			adjustableSize.InitializeSize(MainPage.Instance._MainGrid.ActualHeight, MainPage.Instance._MainGrid.ActualWidth);

			MainStackPanel.Height = MainPage.Instance._MainGrid.ActualHeight;
			MainScrollviewer.Height = MainStackPanel.Height - 300;

			string content = Regex.Replace(post.Content.Rendered, "<!--(.|\n)*?-->", "");
			content = Regex.Replace(content, @"\n", "");
			paragraphs = GetCompleteParagraphs(content);
			UpdateContent(paragraphs);

			LoadImageAsync(AvatorImage, (int)post.Author, AvatorLoadingRing);


			sideCommentsListView = new CommentsListView((int)post.Id);
			belowCommentsListView = new CommentsListView((int)post.Id);
			AdjustSize(MainPage.Instance._MainGrid.ActualWidth, MainPage.Instance._MainGrid.ActualHeight, MainPage.Instance._MainGrid.ActualWidth, MainPage.Instance._MainGrid.ActualHeight);
			SideCommentViewerHolder.Children.Add(sideCommentsListView);
			CommentViewerHolder.Children.Add(belowCommentsListView);
		}
		private void AdjustSize(double newWidth, double newHeight, double oldWidth, double oldHeight) {
			MainStackPanel.Height = newHeight;
			MainScrollviewer.Height = newHeight - 230;
			sideCommentsListView._MainListView.Height = newHeight - 280;
			if(newWidth <= AdjustableSize.BoundaryWidth && oldWidth >= AdjustableSize.BoundaryWidth) {//narrow
				SideCommentViewerHolder.Visibility = Visibility.Collapsed;
				CommentViewerHolder.Visibility = Visibility.Visible;
				Grid.SetRow(RightDetailTextBlock, 1);
			} else if(newWidth >= AdjustableSize.BoundaryWidth && oldWidth <= AdjustableSize.BoundaryWidth) {//wide
				SideCommentViewerHolder.Visibility = Visibility.Visible;
				CommentViewerHolder.Visibility = Visibility.Collapsed;
				Grid.SetRow(RightDetailTextBlock, 0);
			}
		}

		protected override void OnNavigatedTo(NavigationEventArgs e) {
			base.OnNavigatedTo(e);

			throw new Exception();
		}

		public void UpdateContent(Paragraph[] paragraphs) {
			ParagraphStyle style = new ParagraphStyle() {
				normalTextSize = 20
			};
			AddToStackPanel(ContentStackPanel, paragraphs, style);
		}
		public class ParagraphStyle {
			public int normalTextSize = 18;
			public int hyperLinkTextSize = 16;
		}
		public static void AddToStackPanel(StackPanel parent, Paragraph[] paragraphs, ParagraphStyle style) {
			foreach(Paragraph p in paragraphs) {
				StackPanel stack = new StackPanel() { Orientation = Orientation.Vertical };
				switch(p.tag) {
					case "img":
						string address;
						SourceSet sourceSet = null;
						WriteableBitmap source = null;
						int height = 200;
						int width = 200;

						Grid grid = new Grid() { HorizontalAlignment = HorizontalAlignment.Left };
						ProgressRing progressRing = new ProgressRing() { IsActive = true, Height = height / 2, Width = width / 2 };
						Image image = new Image() { Height = height, Width = width };

						grid.Children.Add(image);
						grid.Children.Add(progressRing);
						stack.Children.Add(grid);

						foreach(Property pro in p.properties) {
							if(pro.name == "src" && !string.IsNullOrEmpty(pro.content)) {
								address = pro.content;
								LoadImageAsync(image, address, progressRing);
								continue;
							} else if(pro.name == "height" && int.TryParse(p.properties.ToList().Find((proper) => proper.name == "height").content, out height)) {
								continue;
							} else if(pro.name == "width" && int.TryParse(p.properties.ToList().Find((proper) => proper.name == "width").content, out width)) {
								continue;
							} else if(pro.name == "srcset" && !string.IsNullOrEmpty(pro.content)) {
								sourceSet = new SourceSet(pro.content);
								continue;
							}
						}
						//progressRing.IsActive = false;
						image.Source = source;
						image.Width = width;
						image.Height = height;
						image.RightTapped += (sender, e) => {
							MenuFlyout myFlyout = new MenuFlyout();
							if(sourceSet != null) {
								for(int i = 0; i < sourceSet.sets.Length; i++) {
									SourceSet.Set ss = sourceSet.sets[i];
									MenuFlyoutItem fly = new MenuFlyoutItem { Text = "复制图片地址： 品质 " + ss.quality + "w" };
									fly.Click += (s, c) => {
										DataPackage dataPackage = new DataPackage();
										dataPackage.SetText(ss.address);
										Clipboard.SetContent(dataPackage);
									};
									myFlyout.Items.Add(fly);
								}
							} else {
								//myFlyout.Items.Add(new TextBlock() { Text = "" });
							}
							myFlyout.Placement = FlyoutPlacementMode.Left;
							if(myFlyout.Items.Count != 0) {
								FrameworkElement senderElement = sender as FrameworkElement;
								myFlyout.ShowAt(sender as UIElement, e.GetPosition(sender as UIElement));
							}
						};
						break;
					case "a":
						foreach(Property pro in p.properties) {
							if(pro.name == "href" && !string.IsNullOrEmpty(pro.content)) {
								HyperlinkButton hyperlinkButton = new HyperlinkButton { Content = pro.content, FontSize = style.hyperLinkTextSize };
								hyperlinkButton.Click += async (s, c) => {
									bool success = await Launcher.LaunchUriAsync(new Uri(pro.content));
								};
								stack.Children.Add(hyperlinkButton);
								continue;
							}
						}
						break;
					default:
						stack.Children.Add(new TextBlock() { Text = p.fixedContent, TextWrapping = TextWrapping.WrapWholeWords, FontSize = style.normalTextSize });
						break;
				}

				parent.Children.Add(stack);
				if(p.children != null) {
					if(p.children.Length != 0) {
						AddToStackPanel(stack, p.children, style);
					}
				}
			}
		}
		private async static void LoadImageAsync(Image image, string url, ProgressRing progressRing) {
			if(progressRing != null) {
				progressRing.IsActive = true;
				progressRing.Visibility = Visibility.Visible;
			}
			await Task.Delay(500);
			image.Source = await ImageConverter.GetWriteableBitmapAsync(url);
			if(progressRing != null) {
				progressRing.IsActive = false;
				progressRing.Visibility = Visibility.Collapsed;
			}
		}
		private async static void LoadImageAsync(Image image, int authorID, ProgressRing progressRing) {
			if(progressRing != null) {
				progressRing.IsActive = true;
				progressRing.Visibility = Visibility.Visible;
			}
			await Task.Delay(500);
			image.Source = await Data.Users.User.GetUserAvator(authorID);
			if(progressRing != null) {
				progressRing.IsActive = false;
				progressRing.Visibility = Visibility.Collapsed;
			}
		}

		public static Paragraph[] GetCompleteParagraphs(string source) {
			Paragraph[] paragraphs = GetParallelParagraphs(source);
			CalculateChilrenParagrah(paragraphs);
			return paragraphs;
		}

		public static Paragraph[] GetParallelParagraphs(string source) {
			List<Paragraph> paragraphs = new List<Paragraph>();
			for(int i = 0; i < source.Length; i++) {
				bool insideOpen = false;//<p></p>
				bool insideClose = false;//<p/>
				int index = i + 1;
				if(source[i] == '<') {
					while(index < source.Length - 1) {
						index++;
						if(source[index] == '>') {
							insideOpen = true;
							break;
						} else if(source[index] == '/' && source[index + 1] == '>') {
							insideClose = true;
							break;
						}
					}
					string startTag = GetSubString(source, i, index);//<tag>
					int spaceIndex = startTag.ToList().FindIndex((cha) => cha == ' ');
					string startIdentifier;//tag
					if(spaceIndex == -1) {
						startIdentifier = GetSubString(startTag, 1, startTag.Length - 2).Trim();
					} else {
						startIdentifier = GetSubString(startTag, 1, spaceIndex).Trim();
					}
					string content;
					if(insideOpen) {
						//1.没有检测到<span>
						//2.检测到<span>时，每次检测到，计数i+1，检测到</span>时，计数j+1，之后往后检测，
						//如果是<span>，则重复，每次当i==j时才跳出到3
						//3.如果是</span>则是返回这个正确的结尾
						int currentIndex = index;
						int a = 0;
						int b = 0;
						bool lastEnd = false;
						while(currentIndex++ < source.Length - startIdentifier.Length) {
							char c = source[currentIndex];
							if(c == '<') {
								if(source[currentIndex + 1] == '/') {//</span>
									string cIdentifier = GetSubString(source, currentIndex + 2, currentIndex + startIdentifier.Length + 1);
									if(cIdentifier == startIdentifier) {
										if(a == b) {
											lastEnd = true;
										}
										if(lastEnd) {
											break;
										}
										b++;
									}
								} else {//<span>
									string cIdentifier = GetSubString(source, currentIndex + 1, currentIndex + startIdentifier.Length);
									if(cIdentifier == startIdentifier) {
										a++;
										lastEnd = false;
									}
								}
							}
						}
						if(index + 1 > currentIndex - 1) {
							content = "";
						} else {
							content = GetSubString(source, index + 1, currentIndex - 1);
						}

						paragraphs.Add(new Paragraph(startIdentifier, GetProperties(startTag), content) { isClosed = false });
						i = currentIndex;
						continue;
					} else if(insideClose) {
						int currentIndex = index;
						while(currentIndex++ < source.Length - 1) {
							char current = source[currentIndex];
							if(current == '>') {
								break;
							}
						}
						if(currentIndex - 2 < index) {
							content = "";
						} else {
							content = GetSubString(source, index, currentIndex - 2);
						}
						paragraphs.Add(new Paragraph(startIdentifier, GetProperties(startTag), content) { isClosed = true });
						i = currentIndex;
						continue;
					}
				}
			}
			return paragraphs.ToArray();
		}
		public static void CalculateChilrenParagrah(params Paragraph[] paragraphs) {
			foreach(Paragraph p in paragraphs) {
				Paragraph[] pc = GetParallelParagraphs(p.fullContent);
				if(pc.Length != 0) {
					p.children = pc;
					foreach(Paragraph pp in p.children) {
						pp.parent = p;
					}
					CalculateChilrenParagrah(p.children);
				} else {
					p.children = new Paragraph[0];
				}
			}
		}
		public static Property[] GetProperties(string source) {
			string s1 = new string(new char[] { (char)34, (char)34 });
			string s2 = new string(new char[] { (char)34, (char)32, (char)34 });
			Console.WriteLine(s1);
			Console.WriteLine(s2);
			source = Regex.Replace(source, s1, s2);
			List<Property> properties = new List<Property>();
			int startIndex = 0;
			bool found = false;
			for(int i = 0; i < source.Length; i++) {
				if(source[i] == ' ') {
					startIndex = i + 1;
					found = true;
					break;
				}
			}
			if(!found) {
				return properties.ToArray();
			}

			for(int i = startIndex; i < source.Length; i++) {
				int nameStart = i;
				int index = nameStart;
				while(index++ < source.Length - 1) {
					if(source[index] == '=' && source[index + 1] == '"') {
						string name = GetSubString(source, nameStart, index - 1);
						string content = "";
						int contentStart = index + 2;
						index += 2;
						char c = source[index];
						while(index++ < source.Length - 1) {
							if(source[index] == '"') {
								content = GetSubString(source, contentStart, index - 1);
								properties.Add(new Property(name.Trim(), content));
								i = index;
								goto exitInnerLoop;
							}
						}
					}
				}
				exitInnerLoop:
				;
			}
			return properties.ToArray();
		}
		public static string GetSubString(string source, int start, int end) {
			if(start > end || end > source.Length) {
				throw new Exception();
			}
			string result = "";
			for(int i = start; i <= end; i++) {
				result += source[i];
			}
			return result;
		}

		public class Paragraph {
			public bool isClosed;
			public string tag;
			public Property[] properties;
			public string fullContent;
			public string fixedContent {
				get {
					if(GetParallelParagraphs(this.fullContent).Length == 0 && children != null) {
						return fullContent;
					} else {
						string c = fullContent;
						foreach(Paragraph p in children) {
							c = Regex.Replace(c, p.fullContent, "");
						}
						c = Regex.Replace(c, "<(.|\n)*?>", "");
						return c;
					}
				}
			}

			public Paragraph[] children { get; set; }
			public Paragraph parent { get; set; }

			public Paragraph(string tag, Property[] properties, string content) {
				this.tag = tag;
				this.properties = properties;
				this.fullContent = content;
			}
		}
		public class Property {
			public string name;
			public string content;

			public Property(string name, string content) {
				this.name = name;
				this.content = content;
			}
		}
		public class SourceSet {
			public class Set {
				public string address;
				public int quality;
				public Set(string address, int quality) {
					this.address = address;
					this.quality = quality;
				}
			}
			public string srcset { get; private set; }
			public Set[] sets { get; private set; }

			public SourceSet(string srcset) {
				this.srcset = srcset;

				string[] split = Regex.Split(srcset, ",");
				List<Set> ss = new List<Set>();
				foreach(string s in split) {
					int wIndex = s.LastIndexOf('w');
					int spaceIndex = s.LastIndexOf(' ');
					if(int.TryParse(GetSubString(s, spaceIndex, wIndex - 1), out int result)) {
						string address = GetSubString(s, 0, spaceIndex - 1);
						ss.Add(new Set(address, result));
					}
				}
				this.sets = ss.ToArray();
				for(int a = 0; a < sets.Length - 1; a++) {
					for(int b = 0; b < sets.Length - 1 - a; b++) {
						if(sets[b].quality > sets[b + 1].quality) {
							var temp = sets[b + 1];
							sets[b + 1] = sets[b];
							sets[b] = temp;
						}
					}
				}
			}
		}
	}
}
