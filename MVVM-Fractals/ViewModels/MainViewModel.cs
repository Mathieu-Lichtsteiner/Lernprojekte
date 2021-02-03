using MVVM_Fractals.Fractals;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;
using Media = System.Windows.Media;

namespace MVVM_Fractals {
	internal class MainViewModel : ObservableObject {

		#region private fields
		private readonly FractalCalculator _Calculator;
		Func<int, Color> _Eins = value => ColorConverter.Method1( value, 255, 0.5 );
		Func<int, Color> _Zwei = value => ColorConverter.Method2( value, 255, 0.5 );
		Func<int, Color> _Drei = value => ColorConverter.Method3( value, 255, 0.5 );
		Func<int, Color> _Vier = value => {
			int val = value == 0 ? 0 : (byte)(255 / (100 / value));
			return Color.FromArgb( 255, val, val, val );
		};
		#endregion

		#region public properties
		public Media.ImageSource Fractal { get; private set; }
		public int ImageWidth { get; init; } = 1000;
		public int ImageHeight
			=> (int)Math.Round( ImageWidth * ((XEnd - XStart) / (YEnd - YStart)) );

		public double XStart { get; private set; } = -2.05;
		public double XEnd { get; private set; } = 0.55;
		public double YStart { get; private set; } = -1.3;
		public double YEnd { get; private set; } = 1.3;
		#endregion

		#region constructor
		public MainViewModel() {
			_Calculator = new MandelbrotCalculator( ImageWidth, ImageHeight, _Zwei, 100 );
			Fractal = ConvertToBitmapImage( _Calculator.RenderFractal( XStart, XEnd, YStart, YEnd ) );
		}
		#endregion

		#region public methods
		public void UpdateFractal()
			=> Fractal = ConvertToBitmapImage( _Calculator.RenderFractal( XStart, XEnd, YStart, YEnd ) );
		#endregion

		#region conversion
		private static BitmapImage ConvertToBitmapImage( Bitmap bmp ) {
			using( var memory = new MemoryStream() ) {
				bmp.Save( memory, ImageFormat.Png );
				memory.Position = 0;

				var bitmapImage = new BitmapImage();
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
