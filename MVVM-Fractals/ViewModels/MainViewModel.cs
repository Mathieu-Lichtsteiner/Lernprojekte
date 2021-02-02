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
		private readonly int _ImageWidth = 800;
		private readonly int _ImageHeight = 800;
		private readonly double _Zoom = 1.4;
		#endregion

		#region public properties
		public Media.ImageSource OutputPicture1 { get; private set; }
		public Media.ImageSource OutputPicture2 { get; private set; }
		#endregion

		#region constructor
		public MainViewModel() {
			OutputPicture1 = ConvertToBitmapImage( RenderSimple() );
			OutputPicture2 = ConvertToBitmapImage( RenderFractal() );
		}
		#endregion

		#region private helper methods
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
		private Bitmap RenderSimple() { //first implementation
			var image = new Bitmap( _ImageWidth, _ImageHeight );
			for( int width = 0; width < _ImageWidth; width++ ) {
				for( int height = 0; height < _ImageHeight; height++ ) {
					double x = (width - _ImageWidth / 2) / (_ImageWidth / 4.0);
					double y = (height - _ImageHeight / 2) / (_ImageHeight / 4.0);
					image.SetPixel( width, height, MapToColor( CalculatePoint( x, y ) ) );
				}
			}
			return image;
		}
		private Bitmap RenderFractal( double centerReal = 0.0, double centerImaginary = 0.0 ) {
			centerReal = _Zoom == 1.4 ? centerReal - 0.2 : centerReal;
			centerImaginary = _Zoom == 1.4 ? centerImaginary - 0.6 : centerImaginary;
			var image = new Bitmap( _ImageWidth, _ImageHeight );
			for( int width = 0; width < _ImageWidth; width++ ) {
				for( int height = 0; height < _ImageHeight; height++ ) {
					double x = (4.0 * width / _Zoom - 2.0 * _ImageWidth) / _ImageWidth + centerReal;
					double y = (4.0 * height / _Zoom - 2.0 * _ImageHeight) / _ImageHeight - centerImaginary;
					image.SetPixel( width, height, MapToColor( CalculatePoint( x, y ) ) );
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
				double temp = real * real - imaginary * imaginary + constReal;
				imaginary = 2 * real * imaginary + constImaginary;
				real = temp;
				// Calculate magnitude (pythagoras) and if bigger than 2 it will explode (2*2=4)
				if( real * real + imaginary * imaginary > 4.0 )
					return i;
			}
			return _Itterations;
		}
		private Color MapToColor( int value ) {
			if( value > _Itterations )
				return Color.Black;
			var val = value == 0 ? 0 : (byte)(255 / (100 / value));
			return Color.FromArgb( 255, val, val, val );
		}
		private double Map( double value, double oldMin, double oldMax, double newMin, double newMax ) {
			var oldSize = Math.Abs( oldMax - oldMin );
			var newSize = Math.Abs( newMax - newMin );

			if( newSize > oldSize )
				return newMin + ((oldMin + value) * (newMax - newMin) / (oldMax - oldMin));
			if( newSize < oldSize )
				return newMin + ((oldMin + value) * (oldMax - oldMin) / (newMax - newMin));
			else //if( newSize == oldSize )
				return (newMin - oldMin) + value;
		}
		#endregion

	}
}
