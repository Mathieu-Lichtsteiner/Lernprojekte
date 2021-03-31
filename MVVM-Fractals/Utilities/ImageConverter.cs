using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;

namespace MVVM_Fractals
{
	internal static class ImageConverter {

		#region Bitmap Conversion
		public static BitmapImage BitmapToBitmapImage( Bitmap bmp ) {
			using(MemoryStream? memory = new MemoryStream() ) {
				bmp.Save( memory, ImageFormat.Png );
				memory.Position = 0;

				BitmapImage? bitmapImage = new BitmapImage();
				bitmapImage.BeginInit();
				bitmapImage.StreamSource = memory;
				bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
				bitmapImage.EndInit();
				bitmapImage.Freeze();

				return bitmapImage;
			}
		}
		#endregion

	}
}
