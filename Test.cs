using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Web.Http;

namespace EQCN {
	public class ImageConverter {
		public async static Task<IBuffer> GetBufferAsync(string url) {
			HttpClient httpClient = new HttpClient();
			IBuffer ResultStr = await httpClient.GetBufferAsync(new Uri(url));
			return ResultStr;
		}
		public async static Task<WriteableBitmap> GetWriteableBitmapAsync(string url) {
			try {
				IBuffer buffer = await GetBufferAsync(url);
				if(buffer != null) {
					BitmapImage bi = new BitmapImage();
					WriteableBitmap wb = null;
					Stream stream2Write;
					using(InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream()) {
						stream2Write = stream.AsStreamForWrite();
						await stream2Write.WriteAsync(buffer.ToArray(), 0, (int)buffer.Length);
						await stream2Write.FlushAsync();
						stream.Seek(0);
						await bi.SetSourceAsync(stream);
						wb = new WriteableBitmap(bi.PixelWidth, bi.PixelHeight);
						stream.Seek(0);
						await wb.SetSourceAsync(stream);
						return wb;
					}
				} else {
					return null;
				}
			} catch {
				return null;
			}
		}
	}
}
