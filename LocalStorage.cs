using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Storage;
using Windows.UI.Xaml;

namespace EQCN {
	
	namespace LocalStorage {
		public partial class AppSettings {
			[JsonProperty("AppTheme")]
			public string AppTheme { get; set; }
		}
		public enum AppTheme { Dark, Light };
		public partial class AppSettings {
			public static AppSettings FromJson(string json) => JsonConvert.DeserializeObject<AppSettings>(json, Converter.Settings);

			public static async void WriteSettings(AppSettings settings) {

				StorageFolder local = await Package.Current.InstalledLocation.GetFolderAsync("Local");
				StorageFile file = await local.GetFileAsync("Settings.txt");
				string json = Serialize.ToJson(settings);
				//await FileIO.WriteTextAsync(file,json,Windows.Storage.Streams.UnicodeEncoding.Utf8);
				await FileIO.AppendTextAsync(file, json, Windows.Storage.Streams.UnicodeEncoding.Utf8);
			}

			public static async Task<AppSettings> GetSettings() {
				StorageFolder local = await Package.Current.InstalledLocation.GetFolderAsync("Local");
				StorageFile file = await local.GetFileAsync("Settings.txt");
				string[] lines = File.ReadAllLines(file.Path, Encoding.UTF8);
				string content = "";
				lines.ToList().ForEach((l) => content += l);
				AppSettings settings = FromJson(content);
				return settings;
			}
		}

		public static class Serialize {
			public static string ToJson(this AppSettings self) => JsonConvert.SerializeObject(self, Converter.Settings);
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

	namespace AppSetting {
		public sealed class AppSettings {
			public ApplicationTheme theme;//light=0,dark=1

			public AppSettings(ApplicationTheme theme) {
				this.theme = theme;
			}

			public static AppSettings GetSettings() {
				ApplicationDataContainer c = ApplicationData.Current.LocalSettings;
				//c.Values["theme"] = "Dark";
				int theme = (int)c.Values["theme"];
				return new AppSettings((ApplicationTheme)theme);
			}
			public static void WriteSettings(AppSettings appSettings) {
				ApplicationDataContainer c = ApplicationData.Current.LocalSettings;
				c.Values["theme"] = (int)appSettings.theme;
			}
		}
	}
}
