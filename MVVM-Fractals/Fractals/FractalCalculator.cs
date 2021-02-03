using System;
using System.Drawing;

namespace MVVM_Fractals {
	internal abstract class FractalCalculator {

		#region public properties
		public int ImageWidth { get; init; }
		public int ImageHeight { get; init; }
		public int Itterations { get; init; }
		public int? ColorMinValue { get; set; }
		public int? ColorMaxValue { get; set; }
		public Func<int, Color> ColorMapper { get; set; }
		#endregion

		#region constructor
		public FractalCalculator( int imageWidth, int imageHeight, Func<int, Color> colorMapper, int itterations = 100 ) {
			ImageWidth = imageWidth;
			ImageHeight = imageHeight;
			Itterations = itterations;
			ColorMapper = colorMapper;
		}
		public FractalCalculator( int imageWidth, int imageHeight, Func<int, Color> colorMapper, int itterations = 100, int? colorMinValue = null, int? colorMaxValue = null ) {
			ImageWidth = imageWidth;
			ImageHeight = imageHeight;
			Itterations = itterations;
			ColorMapper = colorMapper;
			ColorMinValue = colorMinValue;
			ColorMaxValue = colorMaxValue;
		}
		#endregion

		#region public methods
		internal Bitmap RenderFractal( double fractalMinWidth, double fractalMaxWidth, double fractalMinHeight, double fractalMaxHeight ) {
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

		#region abstract methods
		protected abstract int CalculatePoint( double real, double imaginary );
		#endregion

		#region conversion
		private Color MapToColor( int value ) {
			if( value == Itterations )
				return Color.Black;
			return ColorMapper( (int)Map( value, 1, Itterations - 1, ColorMinValue ?? 1.0, ColorMaxValue ?? Itterations ) );
		}
		protected static double Map( double value, double oldMin, double oldMax, double newMin, double newMax ) {
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
