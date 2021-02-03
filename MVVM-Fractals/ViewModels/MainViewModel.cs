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

		#region private helper methods

		#region Output-Methods
		private BitmapImage ConvertToBitmapImage( Bitmap bmp ) {
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
		private Bitmap RenderFractal( double fractalMinWidth, double fractalMaxWidth, double fractalMinHeight, double fractalMaxHeight ) {
			var image = new Bitmap( ImageWidth, ImageHeight );
			for( int width = 0; width < ImageWidth; width++ ) {
				for( int height = 0; height < ImageHeight; height++ ) {
					double x = Map( width, 0, ImageWidth, fractalMinWidth, fractalMaxWidth );
					double y = Map( height, 0, ImageHeight, fractalMinHeight, fractalMaxHeight );
					image.SetPixel( width, height, MapToColor( CalculatePoint( x, y ) ) );
				}
			}
			return image;
		}
		#endregion

		#region Math-Methods
		private double Map( double value, double oldMin, double oldMax, double newMin, double newMax ) {
			double oldSize = Math.Abs( oldMax - oldMin );
			double newSize = Math.Abs( newMax - newMin );
			if( newSize > oldSize )
				return newMin + ((value - oldMin) * ((oldMax - oldMin) / (newMax - newMin)));
			if( newSize < oldSize )
				return newMin + ((value - oldMin) * (newMax - newMin) / (oldMax - oldMin));
			else //if( newSize == oldSize )
				return newMin - oldMin + value;
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
		private Color MapToColor( int value ) {
			if( value == _Itterations )
				return Color.Black;
			int val = value == 0 ? 0 : (byte)(255 / (100 / value));
			return Color.FromArgb( 255, val, val, val );
		}
		#endregion

		public static Color FromHSL( double luminosity, double saturation, double hue ) {
			double r = 0, g = 0, b = 0;
			if( luminosity != 0 ) {
				if( saturation == 0 )
					r = g = b = luminosity;
				else {
					double temp2 = (luminosity < 0.5)
						? luminosity * (1.0 + saturation)
						: luminosity + saturation - (luminosity * saturation);
					double temp1 = (2.0 * luminosity) - temp2;

					r = ColorCalc( temp2, hue + (1.0 / 3.0), temp1 );
					g = ColorCalc( temp2, hue, temp1 );
					b = ColorCalc( temp2, hue - (1.0 / 3.0), temp1 );
				}
			}
			return Color.FromArgb( (int)(255 * r), (int)(255 * g), (int)(255 * b) );
		}

		public static Color ToRGB( double hue, double saturation, double luminosity ) {
			byte r, g, b;
			if( saturation == 0 )
				r = g = b = (byte)Math.Round( luminosity * 255.0 );
			else {
				double t1, t2;
				double th = hue / 6.0;

				if( luminosity < 0.5 )
					t2 = luminosity * (1.0 + saturation);
				else
					t2 = luminosity + saturation - (luminosity * saturation);
				t1 = (2.0 * luminosity) - t2;

				double tr, tg, tb;
				tr = th + (1.0 / 3.0);
				tg = th;
				tb = th - (1.0 / 3.0);

				tr = ColorCalc( tr, t1, t2 );
				tg = ColorCalc( tg, t1, t2 );
				tb = ColorCalc( tb, t1, t2 );
				r = (byte)Math.Round( tr * 255.0 );
				g = (byte)Math.Round( tg * 255.0 );
				b = (byte)Math.Round( tb * 255.0 );
			}
			return Color.FromArgb( r, g, b );
		}

		private static Color ToRGBA( double x, double saturation, double z, double w ) {
			double r, g, b;
			if( saturation == 0.0 )
				r = g = b = z;
			else {
				double q = z < 0.5 ? z * (1.0 + saturation) : z + saturation - (z * saturation);
				double p = (2.0 * z) - q;
				r = ColorCalc( p, q, x + (1.0 / 3.0) );
				g = ColorCalc( p, q, x );
				b = ColorCalc( p, q, x - (1.0 / 3.0) );
			}
			return Color.FromArgb( (int)(r * 255), (int)(g * 255), (int)(b * 255), (int)(w * 255) );
		}

		private static double ColorCalc( double c, double t1, double t2 ) {
			if( c < 0.0 )
				c += 1.0;
			else if( c > 1.0 )
				c -= 1.0;
			if( c < 1.0 / 6.0 )
				return t1 + ((t2 - t1) * 6.0 * c);
			if( c < 1.0 / 2.0 )
				return t2;
			if( c < 2.0 / 3.0 )
				return t1 + ((t2 - t1) * ((2.0 / 3.0) - c) * 6.0);
			return t1;
		}

		#endregion

		#region obsolete
		private Bitmap RenderSimple() { //first implementation
			var image = new Bitmap( ImageWidth, ImageHeight );
			for( int width = 0; width < ImageWidth; width++ ) {
				for( int height = 0; height < ImageHeight; height++ ) {
					double x = (width - (ImageWidth / 2)) / (ImageWidth / 4.0);
					double y = (height - (ImageHeight / 2)) / (ImageHeight / 4.0);
					image.SetPixel( width, height, MapToColor( CalculatePoint( x, y ) ) );
				}
			}
			return image;
		}
		#endregion

	}
}
