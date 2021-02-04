using System;
using System.Drawing;
using System.Threading.Tasks;

namespace MVVM_Fractals {
	internal abstract class FractalCalculator {

		#region public properties
		public int ImageWidth { get; init; }
		public int ImageHeight { get; init; }
		public int Itterations { get; set; }
		public int? ColorMinValue { get; set; }
		public int? ColorMaxValue { get; set; }
		public Func<int, Color> ColorMapper { get; set; }
		public Area DefaultArea { get; set; }
		public Area CurrentArea { get; set; }
		#endregion

		#region constructor
		public FractalCalculator( int imageWidth, int imageHeight, Func<int, Color> colorMapper, Area? defaultArea = null, int itterations = 100 ) {
			ImageWidth = imageWidth;
			ImageHeight = imageHeight;
			ColorMapper = colorMapper;
			DefaultArea = defaultArea ?? new Area( -2.05, 0.55, -1.3, 1.3 );
			Itterations = itterations;
		}
		public FractalCalculator( int imageWidth, int imageHeight, Func<int, Color> colorMapper, Area? defaultArea = null, int itterations = 100, int? colorMinValue = null, int? colorMaxValue = null ) {
			ImageWidth = imageWidth;
			ImageHeight = imageHeight;
			ColorMapper = colorMapper;
			DefaultArea = defaultArea ?? new Area( -2.05, 0.55, -1.3, 1.3 );
			Itterations = itterations;
			ColorMinValue = colorMinValue;
			ColorMaxValue = colorMaxValue;
		}
		#endregion

		#region public methods
		internal Bitmap RenderFractalParallel( Area area ) {
			var image = new Bitmap( ImageWidth, ImageHeight );
			var array = new Color[ImageWidth, ImageHeight];
			Parallel.For( 0, ImageWidth, width =>
				Parallel.For( 0, ImageHeight, height => {
					double x = MyMath.Map( width, 0, ImageWidth, area.Left, area.Right );
					double y = MyMath.Map( height, 0, ImageHeight, area.Bottom, area.Top );
					array[width, height] = MapToColor( CalculatePoint( x, y ) );
				} ) );

			for( int width = 0; width < ImageWidth; width++ )
				for( int height = 0; height < ImageHeight; height++ )
					image.SetPixel( width, height, array[width, height] );
			return image;
		}
		internal Bitmap RenderFractal( Area area ) {
			var image = new Bitmap( ImageWidth, ImageHeight );
			for( int width = 0; width < ImageWidth; width++ )
				for( int height = 0; height < ImageHeight; height++ ) {
					double x = MyMath.Map( width, 0, ImageWidth, area.Left, area.Right );
					double y = MyMath.Map( height, 0, ImageHeight, area.Bottom, area.Top );
					image.SetPixel( width, height, MapToColor( CalculatePoint( x, y ) ) );
				}
			return image;
		}
		internal Bitmap RenderFractal( double fractalMinWidth, double fractalMaxWidth, double fractalMinHeight, double fractalMaxHeight ) {
			var image = new Bitmap( ImageWidth, ImageHeight );
			for( int width = 0; width < ImageWidth; width++ )
				for( int height = 0; height < ImageHeight; height++ ) {
					double x = MyMath.Map( width, 0, ImageWidth, fractalMinWidth, fractalMaxWidth );
					double y = MyMath.Map( height, 0, ImageHeight, fractalMinHeight, fractalMaxHeight );
					image.SetPixel( width, height, MapToColor( CalculatePoint( x, y ) ) );
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
			return ColorMapper( (int)MyMath.Map( value, 1, Itterations - 1, ColorMinValue ?? 1.0, ColorMaxValue ?? Itterations ) );
		}
		#endregion

	}
}
