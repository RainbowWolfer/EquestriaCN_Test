using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Windows.UI.Xaml.Media.Imaging;
using Data.Users;
using Data.Posts;
using EQCN;
using Windows.UI.Xaml.Media;

namespace Data {
	namespace Posts {
		public partial class Post {
			public User author_User { get; set; }
			//public WriteableBitmap authorAvator_small;
			//public WriteableBitmap authorAvator_middle;
			//public WriteableBitmap authorAvator_big;
			public ImageSource thumbnail;
			public List<CategoryID> categories_List;

			public string tags_string {
				get {
					if(Tags == null || Tags.Count == 0) {
						return "no tags";
					}
					string result = "";
					Tags.ForEach((t) => result += t);
					return result;
				}
			}

			[JsonProperty("id", NullValueHandling = NullValueHandling.Include)]
			public long? Id { get; set; }
			[JsonProperty("date", NullValueHandling = NullValueHandling.Include)]
			public DateTimeOffset? Date { get; set; }
			[JsonProperty("date_gmt", NullValueHandling = NullValueHandling.Include)]
			public DateTimeOffset? DateGmt { get; set; }
			[JsonProperty("guid", NullValueHandling = NullValueHandling.Include)]
			public Guid Guid { get; set; }
			[JsonProperty("modified", NullValueHandling = NullValueHandling.Include)]
			public DateTimeOffset? Modified { get; set; }
			[JsonProperty("modified_gmt", NullValueHandling = NullValueHandling.Include)]
			public DateTimeOffset? ModifiedGmt { get; set; }
			[JsonProperty("slug", NullValueHandling = NullValueHandling.Include)]
			public string Slug { get; set; }
			[JsonProperty("status", NullValueHandling = NullValueHandling.Include)]
			public Status? Status { get; set; }
			[JsonProperty("type", NullValueHandling = NullValueHandling.Include)]
			public TypeEnum? Type { get; set; }
			[JsonProperty("link", NullValueHandling = NullValueHandling.Include)]
			public string Link { get; set; }
			[JsonProperty("title", NullValueHandling = NullValueHandling.Include)]
			public Guid Title { get; set; }
			[JsonProperty("content", NullValueHandling = NullValueHandling.Include)]
			public Content Content { get; set; }
			[JsonProperty("excerpt", NullValueHandling = NullValueHandling.Include)]
			public Content Excerpt { get; set; }
			[JsonProperty("author", NullValueHandling = NullValueHandling.Include)]
			public long? Author { get; set; }
			[JsonProperty("featured_media", NullValueHandling = NullValueHandling.Include)]
			public long? FeaturedMedia { get; set; }
			[JsonProperty("comment_status", NullValueHandling = NullValueHandling.Include)]
			public CommentStatus? CommentStatus { get; set; }
			[JsonProperty("ping_status", NullValueHandling = NullValueHandling.Include)]
			public PingStatus? PingStatus { get; set; }
			[JsonProperty("sticky", NullValueHandling = NullValueHandling.Include)]
			public bool? Sticky { get; set; }
			[JsonProperty("template", NullValueHandling = NullValueHandling.Include)]
			public Template? Template { get; set; }
			[JsonProperty("format", NullValueHandling = NullValueHandling.Include)]
			public Format? Format { get; set; }
			[JsonProperty("meta", NullValueHandling = NullValueHandling.Include)]
			public List<string> Meta { get; set; }
			[JsonProperty("categories", NullValueHandling = NullValueHandling.Include)]
			public List<long> Categories { get; set; }
			[JsonProperty("tags", NullValueHandling = NullValueHandling.Include)]
			public List<string> Tags { get; set; }
			[JsonProperty("better_featured_image", NullValueHandling = NullValueHandling.Include)]
			public BetterFeaturedImage BetterFeaturedImage { get; set; }
			[JsonProperty("_links", NullValueHandling = NullValueHandling.Include)]
			public Links Links { get; set; }
		}
		public partial class BetterFeaturedImage {
			[JsonProperty("id", NullValueHandling = NullValueHandling.Include)]
			public long? Id { get; set; }
			[JsonProperty("alt_text", NullValueHandling = NullValueHandling.Include)]
			public Template? AltText { get; set; }
			[JsonProperty("caption", NullValueHandling = NullValueHandling.Include)]
			public Template? Caption { get; set; }
			[JsonProperty("description", NullValueHandling = NullValueHandling.Include)]
			public Template? Description { get; set; }
			[JsonProperty("media_type", NullValueHandling = NullValueHandling.Include)]
			public MediaType? MediaType { get; set; }
			[JsonProperty("media_details", NullValueHandling = NullValueHandling.Include)]
			public MediaDetails MediaDetails { get; set; }
			[JsonProperty("post", NullValueHandling = NullValueHandling.Include)]
			public long? Post { get; set; }
			[JsonProperty("source_url", NullValueHandling = NullValueHandling.Include)]
			public string SourceUrl { get; set; }
		}
		public partial class MediaDetails {
			[JsonProperty("width", NullValueHandling = NullValueHandling.Include)]
			public long? Width { get; set; }
			[JsonProperty("height", NullValueHandling = NullValueHandling.Include)]
			public long? Height { get; set; }
			[JsonProperty("file", NullValueHandling = NullValueHandling.Include)]
			public string File { get; set; }
			[JsonProperty("sizes", NullValueHandling = NullValueHandling.Include)]
			public Sizes Sizes { get; set; }
			[JsonProperty("image_meta", NullValueHandling = NullValueHandling.Include)]
			public ImageMeta ImageMeta { get; set; }
		}
		public partial class ImageMeta {
			[JsonProperty("aperture", NullValueHandling = NullValueHandling.Include)]
			public string Aperture { get; set; }
			[JsonProperty("credit", NullValueHandling = NullValueHandling.Include)]
			public Template? Credit { get; set; }
			[JsonProperty("camera", NullValueHandling = NullValueHandling.Include)]
			public Template? Camera { get; set; }
			[JsonProperty("caption", NullValueHandling = NullValueHandling.Include)]
			public Template? Caption { get; set; }
			[JsonProperty("created_timestamp", NullValueHandling = NullValueHandling.Include)]
			public string CreatedTimestamp { get; set; }
			[JsonProperty("copyright", NullValueHandling = NullValueHandling.Include)]
			public Template? Copyright { get; set; }
			[JsonProperty("focal_length", NullValueHandling = NullValueHandling.Include)]
			public string FocalLength { get; set; }
			[JsonProperty("iso", NullValueHandling = NullValueHandling.Include)]
			public string Iso { get; set; }
			[JsonProperty("shutter_speed", NullValueHandling = NullValueHandling.Include)]
			public string ShutterSpeed { get; set; }
			[JsonProperty("title", NullValueHandling = NullValueHandling.Include)]
			public Template? Title { get; set; }
			[JsonProperty("orientation", NullValueHandling = NullValueHandling.Include)]
			public string Orientation { get; set; }
			[JsonProperty("keywords", NullValueHandling = NullValueHandling.Include)]
			public List<object> Keywords { get; set; }
		}
		public partial class Sizes {
			[JsonProperty("thumbnail", NullValueHandling = NullValueHandling.Include)]
			public GalleryBlock Thumbnail { get; set; }
			[JsonProperty("medium", NullValueHandling = NullValueHandling.Include)]
			public GalleryBlock Medium { get; set; }
			[JsonProperty("post-thumbnail", NullValueHandling = NullValueHandling.Include)]
			public GalleryBlock PostThumbnail { get; set; }
			[JsonProperty("main-slider", NullValueHandling = NullValueHandling.Include)]
			public GalleryBlock MainSlider { get; set; }
			[JsonProperty("main-block", NullValueHandling = NullValueHandling.Include)]
			public GalleryBlock MainBlock { get; set; }
			[JsonProperty("slider-small", NullValueHandling = NullValueHandling.Include)]
			public GalleryBlock SliderSmall { get; set; }
			[JsonProperty("gallery-block", NullValueHandling = NullValueHandling.Include)]
			public GalleryBlock GalleryBlock { get; set; }
			[JsonProperty("grid-overlay", NullValueHandling = NullValueHandling.Include)]
			public GalleryBlock GridOverlay { get; set; }
			[JsonProperty("large", NullValueHandling = NullValueHandling.Include)]
			public GalleryBlock Large { get; set; }
			[JsonProperty("main-full", NullValueHandling = NullValueHandling.Include)]
			public GalleryBlock MainFull { get; set; }
		}
		public partial class GalleryBlock {
			[JsonProperty("file", NullValueHandling = NullValueHandling.Include)]
			public string File { get; set; }
			[JsonProperty("width", NullValueHandling = NullValueHandling.Include)]
			public long? Width { get; set; }
			[JsonProperty("height", NullValueHandling = NullValueHandling.Include)]
			public long? Height { get; set; }
			[JsonProperty("mime-type", NullValueHandling = NullValueHandling.Include)]
			public MimeType? MimeType { get; set; }
			[JsonProperty("source_url", NullValueHandling = NullValueHandling.Include)]
			public string SourceUrl { get; set; }
		}
		public partial class Content {
			[JsonProperty("rendered", NullValueHandling = NullValueHandling.Include)]
			public string Rendered { get; set; }
			[JsonProperty("protected", NullValueHandling = NullValueHandling.Include)]
			public bool? Protected { get; set; }
		}
		public partial class Guid {
			[JsonProperty("rendered", NullValueHandling = NullValueHandling.Include)]
			public string Rendered { get; set; }
		}
		public partial class Links {
			[JsonProperty("self", NullValueHandling = NullValueHandling.Include)]
			public List<About> Self { get; set; }
			[JsonProperty("collection", NullValueHandling = NullValueHandling.Include)]
			public List<About> Collection { get; set; }
			[JsonProperty("about", NullValueHandling = NullValueHandling.Include)]
			public List<About> About { get; set; }
			[JsonProperty("author", NullValueHandling = NullValueHandling.Include)]
			public List<Author> Author { get; set; }
			[JsonProperty("replies", NullValueHandling = NullValueHandling.Include)]
			public List<Author> Replies { get; set; }
			[JsonProperty("version-history", NullValueHandling = NullValueHandling.Include)]
			public List<About> VersionHistory { get; set; }
			[JsonProperty("wp:featuredmedia", NullValueHandling = NullValueHandling.Include)]
			public List<Author> WpFeaturedmedia { get; set; }
			[JsonProperty("wp:attachment", NullValueHandling = NullValueHandling.Include)]
			public List<About> WpAttachment { get; set; }
			[JsonProperty("wp:term", NullValueHandling = NullValueHandling.Include)]
			public List<WpTerm> WpTerm { get; set; }
			[JsonProperty("curies", NullValueHandling = NullValueHandling.Include)]
			public List<Cury> Curies { get; set; }
		}
		public partial class About {
			[JsonProperty("href", NullValueHandling = NullValueHandling.Include)]
			public string Href { get; set; }
		}
		public partial class Author {
			[JsonProperty("embeddable", NullValueHandling = NullValueHandling.Include)]
			public bool? Embeddable { get; set; }
			[JsonProperty("href", NullValueHandling = NullValueHandling.Include)]
			public string Href { get; set; }
		}
		public partial class Cury {
			[JsonProperty("name", NullValueHandling = NullValueHandling.Include)]
			public Name? Name { get; set; }
			[JsonProperty("href", NullValueHandling = NullValueHandling.Include)]
			public Href? Href { get; set; }
			[JsonProperty("templated", NullValueHandling = NullValueHandling.Include)]
			public bool? Templated { get; set; }
		}
		public partial class WpTerm {
			[JsonProperty("taxonomy", NullValueHandling = NullValueHandling.Include)]
			public Taxonomy? Taxonomy { get; set; }
			[JsonProperty("embeddable", NullValueHandling = NullValueHandling.Include)]
			public bool? Embeddable { get; set; }
			[JsonProperty("href", NullValueHandling = NullValueHandling.Include)]
			public string Href { get; set; }
		}
		public enum Template { Empty, Sethisto };
		public enum MimeType { ImageGif, ImageJpeg, ImagePng };
		public enum MediaType { Image };
		public enum CommentStatus { Open };
		public enum Format { Standard, Video };
		public enum Href { HttpsApiWOrgRel };
		public enum Name { Wp };
		public enum Taxonomy { Category, PostTag };
		public enum PingStatus { Closed, Open };
		public enum Status { Publish };
		public enum TypeEnum { Post };
		public partial class Post {
			public static List<Post> FromJson(string json) => JsonConvert.DeserializeObject<List<Post>>(json, Posts.Converter.Settings);
			public static async Task<List<Post>> GetPostsByCategory(int categoryID) {
				string uri = "http://www.equestriacn.com/?rest_route=/wp/v2/posts&categories=" + categoryID;
				HttpResponseMessage responseMessage = await new HttpClient().GetAsync(new Uri(uri));
				string result = await responseMessage.Content.ReadAsStringAsync();
				return FromJson(result);
			}
			public static async Task<List<Post>> GetPosts() {
				string uri = "http://www.equestriacn.com/?rest_route=/wp/v2/posts";
				HttpResponseMessage responseMessage = await new HttpClient().GetAsync(new Uri(uri));
				string result = await responseMessage.Content.ReadAsStringAsync();
				return FromJson(result);
			}
			public static async Task<List<Post>> GetPostsByContent(string searchContent) {
				string uri = "http://www.equestriacn.com/?rest_route=/wp/v2/posts&search=" + searchContent;
				HttpResponseMessage responseMessage = await new HttpClient().GetAsync(new Uri(uri));
				string result = await responseMessage.Content.ReadAsStringAsync();
				return FromJson(result);
			}
		}
		public static class Serialize {
			public static string ToJson(this List<Post> self) => JsonConvert.SerializeObject(self, Posts.Converter.Settings);
		}
		internal static class Converter {
			public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings {
				MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
				DateParseHandling = DateParseHandling.None,
				Converters = {
					new MediaTypeConverter(),
					new CommentStatusConverter(),
					new FormatConverter(),
					new PingStatusConverter(),
					new HrefConverter(),
					new StatusConverter(),
					new NameConverter(),
					new TypeEnumConverter(),
					new TaxonomyConverter(),
					new TemplateConverter(),
					new MimeTypeConverter(),
					new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
				},
			};
		}
		internal class MediaTypeConverter: JsonConverter {
			public override bool CanConvert(Type t) => t == typeof(MediaType) || t == typeof(MediaType?);

			public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer) {
				if(reader.TokenType == JsonToken.Null)
					return null;
				var value = serializer.Deserialize<string>(reader);
				if(value == "image") {
					return MediaType.Image;
				}
				throw new Exception("Cannot unmarshal type MediaType");
			}

			public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer) {
				var value = (MediaType)untypedValue;
				if(value == MediaType.Image) {
					serializer.Serialize(writer, "image");
					return;
				}
				throw new Exception("Cannot marshal type MediaType");
			}
		}
		internal class CommentStatusConverter: JsonConverter {
			public override bool CanConvert(Type t) => t == typeof(CommentStatus) || t == typeof(CommentStatus?);

			public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer) {
				if(reader.TokenType == JsonToken.Null)
					return null;
				var value = serializer.Deserialize<string>(reader);
				if(value == "open") {
					return CommentStatus.Open;
				}
				throw new Exception("Cannot unmarshal type CommentStatus");
			}

			public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer) {
				var value = (CommentStatus)untypedValue;
				if(value == CommentStatus.Open) {
					serializer.Serialize(writer, "open");
					return;
				}
				throw new Exception("Cannot marshal type CommentStatus");
			}
		}
		internal class FormatConverter: JsonConverter {
			public override bool CanConvert(Type t) => t == typeof(Format) || t == typeof(Format?);

			public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer) {
				if(reader.TokenType == JsonToken.Null)
					return null;
				var value = serializer.Deserialize<string>(reader);
				if(value == "standard") {
					return Format.Standard;
				} else if(value == "video") {
					return Format.Video;
				}
				throw new Exception("Cannot unmarshal type Format");
			}

			public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer) {
				var value = (Format)untypedValue;
				if(value == Format.Standard) {
					serializer.Serialize(writer, "standard");
					return;
				}
				throw new Exception("Cannot marshal type Format");
			}
		}
		internal class PingStatusConverter: JsonConverter {
			public override bool CanConvert(Type t) => t == typeof(PingStatus) || t == typeof(PingStatus?);

			public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer) {
				if(reader.TokenType == JsonToken.Null)
					return null;
				var value = serializer.Deserialize<string>(reader);
				if(value == "closed") {
					return PingStatus.Closed;
				} else if(value == "open") {
					return PingStatus.Open;
				}
				throw new Exception("Cannot unmarshal type PingStatus");
			}

			public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer) {
				var value = (PingStatus)untypedValue;
				if(value == PingStatus.Closed) {
					serializer.Serialize(writer, "closed");
					return;
				}
				throw new Exception("Cannot marshal type PingStatus");
			}
		}
		internal class HrefConverter: JsonConverter {
			public override bool CanConvert(Type t) => t == typeof(Href) || t == typeof(Href?);

			public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer) {
				if(reader.TokenType == JsonToken.Null)
					return null;
				var value = serializer.Deserialize<string>(reader);
				if(value == "https://api.w.org/{rel}") {
					return Href.HttpsApiWOrgRel;
				}
				throw new Exception("Cannot unmarshal type Href");
			}

			public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer) {
				var value = (Href)untypedValue;
				if(value == Href.HttpsApiWOrgRel) {
					serializer.Serialize(writer, "https://api.w.org/{rel}");
					return;
				}
				throw new Exception("Cannot marshal type Href");
			}
		}
		internal class StatusConverter: JsonConverter {
			public override bool CanConvert(Type t) => t == typeof(Status) || t == typeof(Status?);

			public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer) {
				if(reader.TokenType == JsonToken.Null)
					return null;
				var value = serializer.Deserialize<string>(reader);
				if(value == "publish") {
					return Status.Publish;
				}
				throw new Exception("Cannot unmarshal type Status");
			}

			public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer) {
				var value = (Status)untypedValue;
				if(value == Status.Publish) {
					serializer.Serialize(writer, "publish");
					return;
				}
				throw new Exception("Cannot marshal type Status");
			}
		}
		internal class NameConverter: JsonConverter {
			public override bool CanConvert(Type t) => t == typeof(Name) || t == typeof(Name?);

			public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer) {
				if(reader.TokenType == JsonToken.Null)
					return null;
				var value = serializer.Deserialize<string>(reader);
				if(value == "wp") {
					return Name.Wp;
				}
				throw new Exception("Cannot unmarshal type Name");
			}

			public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer) {
				var value = (Name)untypedValue;
				if(value == Name.Wp) {
					serializer.Serialize(writer, "wp");
					return;
				}
				throw new Exception("Cannot marshal type Name");
			}
		}
		internal class TypeEnumConverter: JsonConverter {
			public override bool CanConvert(Type t) => t == typeof(TypeEnum) || t == typeof(TypeEnum?);

			public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer) {
				if(reader.TokenType == JsonToken.Null)
					return null;
				var value = serializer.Deserialize<string>(reader);
				if(value == "post") {
					return TypeEnum.Post;
				}
				throw new Exception("Cannot unmarshal type TypeEnum");
			}

			public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer) {
				var value = (TypeEnum)untypedValue;
				if(value == TypeEnum.Post) {
					serializer.Serialize(writer, "post");
					return;
				}
				throw new Exception("Cannot marshal type TypeEnum");
			}
		}
		internal class TaxonomyConverter: JsonConverter {
			public override bool CanConvert(Type t) => t == typeof(Taxonomy) || t == typeof(Taxonomy?);

			public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer) {
				if(reader.TokenType == JsonToken.Null)
					return null;
				var value = serializer.Deserialize<string>(reader);
				switch(value) {
					case "category":
						return Taxonomy.Category;
					case "post_tag":
						return Taxonomy.PostTag;
				}
				throw new Exception("Cannot unmarshal type Taxonomy");
			}

			public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer) {
				var value = (Taxonomy)untypedValue;
				switch(value) {
					case Taxonomy.Category:
						serializer.Serialize(writer, "category");
						return;
					case Taxonomy.PostTag:
						serializer.Serialize(writer, "post_tag");
						return;
				}
				throw new Exception("Cannot marshal type Taxonomy");
			}
		}
		internal class TemplateConverter: JsonConverter {
			public override bool CanConvert(Type t) => t == typeof(Template) || t == typeof(Template?);

			public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer) {
				if(reader.TokenType == JsonToken.Null)
					return null;
				var value = serializer.Deserialize<string>(reader);
				if(value == "") {
					return Template.Empty;
				} else if(value == "Sethisto") {
					return Template.Sethisto;
				}
				//throw new Exception("Cannot unmarshal type Template");
				return Template.Empty;
			}

			public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer) {
				var value = (Template)untypedValue;
				if(value == Template.Empty) {
					serializer.Serialize(writer, "");
					return;
				}
				throw new Exception("Cannot marshal type Template");
			}
		}
		internal class MimeTypeConverter: JsonConverter {
			public override bool CanConvert(Type t) => t == typeof(MimeType) || t == typeof(MimeType?);

			public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer) {
				if(reader.TokenType == JsonToken.Null)
					return null;
				var value = serializer.Deserialize<string>(reader);
				switch(value) {
					case "image/gif":
						return MimeType.ImageGif;
					case "image/jpeg":
						return MimeType.ImageJpeg;
					case "image/png":
						return MimeType.ImagePng;
				}
				throw new Exception("Cannot unmarshal type MimeType");
			}

			public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer) {
				var value = (MimeType)untypedValue;
				switch(value) {
					case MimeType.ImageGif:
						serializer.Serialize(writer, "image/gif");
						return;
					case MimeType.ImageJpeg:
						serializer.Serialize(writer, "image/jpeg");
						return;
					case MimeType.ImagePng:
						serializer.Serialize(writer, "image/png");
						return;
				}
				throw new Exception("Cannot marshal type MimeType");
			}
		}
	}
	namespace Categories {
		//http://www.equestriacn.com/?rest_route=/wp/v2/categories
		public partial class Category {
			public List<Post> posts;

			[JsonProperty("id", NullValueHandling = NullValueHandling.Include)]
			public long? Id { get; set; }

			[JsonProperty("count", NullValueHandling = NullValueHandling.Include)]
			public long? Count { get; set; }

			[JsonProperty("description", NullValueHandling = NullValueHandling.Include)]
			public string Description { get; set; }

			[JsonProperty("link", NullValueHandling = NullValueHandling.Include)]
			public string Link { get; set; }

			[JsonProperty("name", NullValueHandling = NullValueHandling.Include)]
			public string Name { get; set; }

			[JsonProperty("slug", NullValueHandling = NullValueHandling.Include)]
			public string Slug { get; set; }

			[JsonProperty("taxonomy", NullValueHandling = NullValueHandling.Include)]
			public string Taxonomy { get; set; }

			[JsonProperty("parent", NullValueHandling = NullValueHandling.Include)]
			public long? Parent { get; set; }

			[JsonProperty("_links", NullValueHandling = NullValueHandling.Include)]
			public Links Links { get; set; }
		}
		public partial class Links {
			[JsonProperty("self")]
			public List<About> Self { get; set; }

			[JsonProperty("collection")]
			public List<About> Collection { get; set; }

			[JsonProperty("about")]
			public List<About> About { get; set; }

			[JsonProperty("up")]
			public List<Up> Up { get; set; }

			[JsonProperty("https://api.w.org/post_type")]
			public List<About> HttpsApiWOrgPostType { get; set; }
		}
		public partial class About {
			[JsonProperty("href")]
			public string Href { get; set; }
		}
		public partial class Up {
			[JsonProperty("embeddable")]
			public bool Embeddable { get; set; }

			[JsonProperty("href")]
			public string Href { get; set; }
		}
		public partial class Category {
			public static List<Category> FromJson(string json) => JsonConvert.DeserializeObject<List<Category>>(json, Categories.Converter.Settings);
			public static async Task<List<Category>> GetCategories() {
				string uri = "http://www.equestriacn.com/?rest_route=/wp/v2/categories";
				HttpResponseMessage responseMessage = await new HttpClient().GetAsync(new Uri(uri));
				string result = await responseMessage.Content.ReadAsStringAsync();
				return FromJson(result);
			}
		}
		public static class Serialize {
			public static string ToJson(this List<Category> self) => JsonConvert.SerializeObject(self, Categories.Converter.Settings);
		}
		internal static class Converter {
			public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings {
				MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
				DateParseHandling = DateParseHandling.None,
				Converters = {
					new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
				},
			};
		}
	}
	namespace Users {
		public partial class User {
			[JsonProperty("id", NullValueHandling = NullValueHandling.Include)]
			public long? Id { get; set; }

			[JsonProperty("name", NullValueHandling = NullValueHandling.Include)]
			public string Name { get; set; }

			[JsonProperty("url", NullValueHandling = NullValueHandling.Include)]
			public string Url { get; set; }

			[JsonProperty("description", NullValueHandling = NullValueHandling.Include)]
			public string Description { get; set; }

			[JsonProperty("link", NullValueHandling = NullValueHandling.Include)]
			public string Link { get; set; }

			[JsonProperty("slug", NullValueHandling = NullValueHandling.Include)]
			public string Slug { get; set; }

			[JsonProperty("avatar_urls", NullValueHandling = NullValueHandling.Include)]
			public Dictionary<string, string> AvatarUrls { get; set; }

			[JsonProperty("meta", NullValueHandling = NullValueHandling.Include)]
			public List<object> Meta { get; set; }

			[JsonProperty("_links", NullValueHandling = NullValueHandling.Include)]
			public Links Links { get; set; }
		}
		public partial class Links {
			[JsonProperty("self", NullValueHandling = NullValueHandling.Include)]
			public List<Collection> Self { get; set; }

			[JsonProperty("collection", NullValueHandling = NullValueHandling.Include)]
			public List<Collection> Collection { get; set; }
		}
		public partial class Collection {
			[JsonProperty("href", NullValueHandling = NullValueHandling.Include)]
			public string Href { get; set; }
		}
		public partial class User {
			public static User FromJson(string json) => JsonConvert.DeserializeObject<User>(json, Users.Converter.Settings);
			public static async Task<User> GetUser(int userID) {
				string uri = "http://www.equestriacn.com/?rest_route=/wp/v2/users/" + userID;
				HttpResponseMessage responseMessage = await new HttpClient().GetAsync(new Uri(uri));
				string result = await responseMessage.Content.ReadAsStringAsync();
				return FromJson(result);
			}
			public static async Task<WriteableBitmap> GetUserAvator(int userID) {
				string uri = "http://www.equestriacn.com/?get_wp_connect_avatar=" + userID;
				return await ImageConverter.GetWriteableBitmapAsync(uri);
			}
			public static async Task<WriteableBitmap> GetUserAvator(string url) {
				//HttpResponseMessage responseMessage = await new HttpClient().GetAsync(new Uri(url));
				//string result = await responseMessage.Content.ReadAsStringAsync();
				return await ImageConverter.GetWriteableBitmapAsync(url);
			}
		}
		public static class Serialize {
			public static string ToJson(this User self) => JsonConvert.SerializeObject(self, Users.Converter.Settings);
		}
		internal static class Converter {
			public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings {
				MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
				DateParseHandling = DateParseHandling.None,
				Converters = {
					new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
				},
			};
		}
	}
	namespace Comments {
		public partial class Comment {
			[JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
			public long? Id { get; set; }
			[JsonProperty("post", NullValueHandling = NullValueHandling.Ignore)]
			public long? Post { get; set; }
			[JsonProperty("parent", NullValueHandling = NullValueHandling.Ignore)]
			public long? Parent { get; set; }
			[JsonProperty("author", NullValueHandling = NullValueHandling.Ignore)]
			public long? Author { get; set; }
			[JsonProperty("author_name", NullValueHandling = NullValueHandling.Ignore)]
			public string AuthorName { get; set; }
			[JsonProperty("author_url", NullValueHandling = NullValueHandling.Ignore)]
			public string AuthorUrl { get; set; }
			[JsonProperty("date", NullValueHandling = NullValueHandling.Ignore)]
			public DateTimeOffset? Date { get; set; }
			[JsonProperty("date_gmt", NullValueHandling = NullValueHandling.Ignore)]
			public DateTimeOffset? DateGmt { get; set; }
			[JsonProperty("content", NullValueHandling = NullValueHandling.Ignore)]
			public Content Content { get; set; }
			[JsonProperty("link", NullValueHandling = NullValueHandling.Ignore)]
			public string Link { get; set; }
			[JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
			public Status? Status { get; set; }
			[JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
			public TypeEnum? Type { get; set; }
			[JsonProperty("author_avatar_urls", NullValueHandling = NullValueHandling.Ignore)]
			public Dictionary<string, string> AuthorAvatarUrls { get; set; }
			[JsonProperty("meta", NullValueHandling = NullValueHandling.Ignore)]
			public List<object> Meta { get; set; }
			[JsonProperty("hi", NullValueHandling = NullValueHandling.Ignore)]
			public HiUnion? Hi { get; set; }
			[JsonProperty("_links", NullValueHandling = NullValueHandling.Ignore)]
			public Links Links { get; set; }
		}
		public partial class Content {
			[JsonProperty("rendered", NullValueHandling = NullValueHandling.Ignore)]
			public string Rendered { get; set; }
		}
		public partial class HiClass {
			[JsonProperty("author", NullValueHandling = NullValueHandling.Ignore)]
			public HiAuthor Author { get; set; }
		}
		public partial class HiAuthor {
			[JsonProperty("ID", NullValueHandling = NullValueHandling.Ignore)]
			public long? Id { get; set; }
			[JsonProperty("user_login", NullValueHandling = NullValueHandling.Ignore)]
			public string UserLogin { get; set; }
			[JsonProperty("display_name", NullValueHandling = NullValueHandling.Ignore)]
			public string DisplayName { get; set; }
			[JsonProperty("role", NullValueHandling = NullValueHandling.Ignore)]
			public string Role { get; set; }
			[JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
			public string Description { get; set; }
		}
		public partial class Links {
			[JsonProperty("self", NullValueHandling = NullValueHandling.Ignore)]
			public List<Collection> Self { get; set; }
			[JsonProperty("collection", NullValueHandling = NullValueHandling.Ignore)]
			public List<Collection> Collection { get; set; }
			[JsonProperty("author", NullValueHandling = NullValueHandling.Ignore)]
			public List<AuthorElement> Author { get; set; }
			[JsonProperty("up", NullValueHandling = NullValueHandling.Ignore)]
			public List<Up> Up { get; set; }
		}
		public partial class AuthorElement {
			[JsonProperty("embeddable", NullValueHandling = NullValueHandling.Ignore)]
			public bool? Embeddable { get; set; }
			[JsonProperty("href", NullValueHandling = NullValueHandling.Ignore)]
			public string Href { get; set; }
		}
		public partial class Collection {
			[JsonProperty("href", NullValueHandling = NullValueHandling.Ignore)]
			public string Href { get; set; }
		}
		public partial class Up {
			[JsonProperty("embeddable", NullValueHandling = NullValueHandling.Ignore)]
			public bool? Embeddable { get; set; }
			[JsonProperty("post_type", NullValueHandling = NullValueHandling.Ignore)]
			public PostType? PostType { get; set; }
			[JsonProperty("href", NullValueHandling = NullValueHandling.Ignore)]
			public string Href { get; set; }
		}
		public enum PostType { Post };
		public enum Status { Approved };
		public enum TypeEnum { Comment };
		public partial struct HiUnion {
			public List<object> AnythingArray;
			public HiClass HiClass;
			public bool IsNull => AnythingArray == null && HiClass == null;
		}
		public partial class Comment {
			public static List<Comment> FromJson(string json) => JsonConvert.DeserializeObject<List<Comment>>(json, Comments.Converter.Settings);
			public static async Task<List<Comment>> GetComments(int postID) {
				string uri = "http://www.equestriacn.com/?rest_route=/wp/v2/comments&post=" + postID;
				HttpResponseMessage responseMessage = await new HttpClient().GetAsync(new Uri(uri));
				string result = await responseMessage.Content.ReadAsStringAsync();
				return FromJson(result);
			}
		}
		public static class Serialize {
			public static string ToJson(this List<Comment> self) => JsonConvert.SerializeObject(self, Comments.Converter.Settings);
		}
		internal static class Converter {
			public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings {
				MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
				DateParseHandling = DateParseHandling.None,
				Converters = {
					PostTypeConverter.Singleton,
					HiUnionConverter.Singleton,
					StatusConverter.Singleton,
					TypeEnumConverter.Singleton,
					new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
				},
			};
		}
		internal class PostTypeConverter: JsonConverter {
			public override bool CanConvert(Type t) => t == typeof(PostType) || t == typeof(PostType?);

			public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer) {
				if(reader.TokenType == JsonToken.Null)
					return null;
				var value = serializer.Deserialize<string>(reader);
				if(value == "post") {
					return PostType.Post;
				}
				throw new Exception("Cannot unmarshal type PostType");
			}

			public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer) {
				if(untypedValue == null) {
					serializer.Serialize(writer, null);
					return;
				}
				var value = (PostType)untypedValue;
				if(value == PostType.Post) {
					serializer.Serialize(writer, "post");
					return;
				}
				throw new Exception("Cannot marshal type PostType");
			}

			public static readonly PostTypeConverter Singleton = new PostTypeConverter();
		}
		internal class HiUnionConverter: JsonConverter {
			public override bool CanConvert(Type t) => t == typeof(HiUnion) || t == typeof(HiUnion?);

			public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer) {
				switch(reader.TokenType) {
					case JsonToken.StartObject:
						var objectValue = serializer.Deserialize<HiClass>(reader);
						return new HiUnion { HiClass = objectValue };
					case JsonToken.StartArray:
						var arrayValue = serializer.Deserialize<List<object>>(reader);
						return new HiUnion { AnythingArray = arrayValue };
				}
				throw new Exception("Cannot unmarshal type HiUnion");
			}

			public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer) {
				var value = (HiUnion)untypedValue;
				if(value.AnythingArray != null) {
					serializer.Serialize(writer, value.AnythingArray);
					return;
				}
				if(value.HiClass != null) {
					serializer.Serialize(writer, value.HiClass);
					return;
				}
				throw new Exception("Cannot marshal type HiUnion");
			}

			public static readonly HiUnionConverter Singleton = new HiUnionConverter();
		}
		internal class StatusConverter: JsonConverter {
			public override bool CanConvert(Type t) => t == typeof(Status) || t == typeof(Status?);

			public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer) {
				if(reader.TokenType == JsonToken.Null)
					return null;
				var value = serializer.Deserialize<string>(reader);
				if(value == "approved") {
					return Status.Approved;
				}
				throw new Exception("Cannot unmarshal type Status");
			}

			public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer) {
				if(untypedValue == null) {
					serializer.Serialize(writer, null);
					return;
				}
				var value = (Status)untypedValue;
				if(value == Status.Approved) {
					serializer.Serialize(writer, "approved");
					return;
				}
				throw new Exception("Cannot marshal type Status");
			}

			public static readonly StatusConverter Singleton = new StatusConverter();
		}
		internal class TypeEnumConverter: JsonConverter {
			public override bool CanConvert(Type t) => t == typeof(TypeEnum) || t == typeof(TypeEnum?);

			public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer) {
				if(reader.TokenType == JsonToken.Null)
					return null;
				var value = serializer.Deserialize<string>(reader);
				if(value == "comment") {
					return TypeEnum.Comment;
				}
				throw new Exception("Cannot unmarshal type TypeEnum");
			}

			public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer) {
				if(untypedValue == null) {
					serializer.Serialize(writer, null);
					return;
				}
				var value = (TypeEnum)untypedValue;
				if(value == TypeEnum.Comment) {
					serializer.Serialize(writer, "comment");
					return;
				}
				throw new Exception("Cannot marshal type TypeEnum");
			}

			public static readonly TypeEnumConverter Singleton = new TypeEnumConverter();
		}
	}

	public enum CategoryID {
		OtherTranslation = 2,
		EpisodeFeed = 3,
		Offical = 5,
		FandomRecommendation = 7,
		News = 64,
		FandomFeed = 66,
		Fiction = 236,
		Animation = 238,
		PMV = 393,
		Pictures = 461,
	}
	public static partial class Method {
		public static string ConvertID2Category(CategoryID ID) {
			switch(ID) {
				case CategoryID.OtherTranslation:
					return "其他翻译";
				case CategoryID.EpisodeFeed:
					return "剧集资讯";
				case CategoryID.Offical:
					return "官方资讯";
				case CategoryID.FandomRecommendation:
					return "同人推荐";
				case CategoryID.News:
					return "新闻";
				case CategoryID.FandomFeed:
					return "同人资讯";
				case CategoryID.Fiction:
					return "小说推荐";
				case CategoryID.Animation:
					return "动画推荐";
				case CategoryID.PMV:
					return "PMV推荐";
				case CategoryID.Pictures:
					return "图片推荐";
				default:
					return "未分类";
			}
		}
		public static string ConvertExtendedASCII(string HTML) {
			string retVal = "";
			char[] s = HTML.ToCharArray();
			foreach(char c in s) {
				//retVal += Convert.ToInt32(c) > 127 ? "&#" + Convert.ToInt32(c) + ";" : c;
				if(Convert.ToInt32(c) > 127) {
					retVal += "&#" + Convert.ToInt32(c) + ";";
				} else {
					retVal += c;
				}
			}
			return retVal;
		}
		public static string GetThumbnail(Sizes sizes) {
			if(sizes.Large != null) {
				return sizes.Large.SourceUrl;
			} else if(sizes.Medium != null) {
				return sizes.Medium.SourceUrl;
			} else if(sizes.Thumbnail != null) {
				return sizes.Thumbnail.SourceUrl;
			} else if(sizes.PostThumbnail != null) {
				return sizes.PostThumbnail.SourceUrl;
			} else {
				return "";
			}
		}
	}
	public class HTMLConverter {
		public string source;

		public HTMLConverter(string source) {
			this.source = source;
		}
	}
}