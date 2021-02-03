using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;
using Media = System.Windows.Media;

namespace MVVM_Fractals {
	internal class MainViewModel : ObservableObject {

		#region private fields
		private readonly int _Itterations = 100;
		#endregion

		#region public properties
		public Media.ImageSource Fractal { get; private set; }
		public int ImageWidth { get; init; } = 1000;
		public int ImageHeight { get; init; }
		#endregion

		#region constructor
		public MainViewModel() {
			double xStart = -2.05;
			double xEnd = 0.55;
			double yStart = -1.3;
			double yEnd = 1.3;
			ImageHeight = (int)Math.Round( ImageWidth * ((xEnd - xStart) / (yEnd - yStart)) );
			Fractal = ConvertToBitmapImage( RenderFractal( xStart, xEnd, yStart, yEnd ) );
		}
		#endregion

		#region private methods
		internal Bitmap RenderFractal( double fractalMinWidth, double fractalMaxWidth, double fractalMinHeight, double fractalMaxHeight ) {
			var image = new Bitmap( ImageWidth, ImageHeight );
			for( int width = 0; width < ImageWidth; width++ ) {
				for( int height = 0; height < ImageHeight; height++ ) {
					double x = Map( width, 0, ImageWidth, fractalMinWidth, fractalMaxWidth );
					double y = Map( height, 0, ImageHeight, fractalMinHeight, fractalMaxHeight );
					image.SetPixel( width, height, MapToColor( CalculatePoint( x, y ), _Itterations ) );
				}
			}
			return image;
		}
		private int CalculatePoint( double real, double imaginary ) {
			// For each number c: square, add c for i times
			double constReal = real;
			double constImaginary = imaginary;
			for( int i = 1; i <= _Itterations; i++ ) {
				// Square Complex c : temp = Real * Real - Imaginary * Imaginary; c.Imaginary = 2 * Real * Imaginary; Real = temp;
				double temp = (real * real) - (imaginary * imaginary) + constReal;
				imaginary = (2 * real * imaginary) + constImaginary;
				real = temp;
				// Calculate magnitude (pythagoras) and if bigger than 2 it will explode (2*2=4)
				if( (real * real) + (imaginary * imaginary) > 4.0 )
					return i;
			}
			return _Itterations;
		}
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
		private static Color MapToColor( int value, int itterations ) {
			if( value == itterations )
				return Color.Black;
			var der = ColorConverter.Method1( value, 255, 0.5 );
			var die = ColorConverter.Method2( value, 255, 0.5 );
			var das = ColorConverter.Method3( value, 255, 0.5 );
			return die;

			int val = value == 0 ? 0 : (byte)(255 / (100 / value));
			return Color.FromArgb( 255, val, val, val );
		}
		private static double Map( double value, double oldMin, double oldMax, double newMin, double newMax ) {
			double oldSize = Math.Abs( oldMax - oldMin );
			double newSize = Math.Abs( newMax - newMin );
			if( newSize > oldSize )
				return newMin + ((value - oldMin) * ((oldMax - oldMin) / (newMax - newMin)));
			if( newSize < oldSize )
				return newMin + ((value - oldMin) * (newMax - newMin) / (oldMax - oldMin));
			else //if( newSize == oldSize )
				return newMin - oldMin + value;
		}
		#endregion

	}
}
