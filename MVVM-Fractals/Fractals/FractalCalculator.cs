using System;
using System.Drawing;
using System.Threading.Tasks;

namespace MVVM_Fractals
{
	internal abstract class FractalCalculator
	{

		#region public properties
		public int ImageWidth { get; init; }
		public int ImageHeight { get; init; }
		public int Itterations { get; private set; }
		public int? ColorMinValue { get; set; }
		public int? ColorMaxValue { get; set; }
		public Func<int, Color> ColorMapper { get; set; }
		public double ZoomFactor { get; init; }
		public Area DefaultArea { get; init; }
		public Area CurrentArea { get; set; }
		#endregion

		#region constructor
		public FractalCalculator(int imageWidth, int imageHeight, Func<int, Color> colorMapper, Area? defaultArea, int itterations, double zoomFactor)
		{
			ImageWidth = imageWidth;
			ImageHeight = imageHeight;
			ColorMapper = colorMapper;
			DefaultArea = defaultArea ?? new Area(-2.05, 0.55, -1.3, 1.3);
			CurrentArea = DefaultArea;
			Itterations = itterations;
			ZoomFactor = zoomFactor;
		}
		public FractalCalculator(int imageWidth, int imageHeight, Func<int, Color> colorMapper, Area? defaultArea = null, int itterations = 60, double zoomFactor = 1.5, int? colorMinValue = null, int? colorMaxValue = null)
			: this(imageWidth, imageHeight, colorMapper, defaultArea, itterations, zoomFactor)
		{
			ColorMinValue = colorMinValue;
			ColorMaxValue = colorMaxValue;
		}
		#endregion

		#region abstract methods
		protected abstract int CalculatePoint(double real, double imaginary);
		#endregion

		#region public methods
		internal Bitmap RenderFractal()
		{
			Bitmap? image = new Bitmap(ImageWidth, ImageHeight);
			Color[,]? array = new Color[ImageWidth, ImageHeight];
			Parallel.For(0, ImageWidth * ImageHeight,
				pos =>
				{
					int width = pos / ImageWidth;
					int height = pos % ImageHeight;
					double x = MyMath.Map(width, 0, ImageWidth, CurrentArea.Left, CurrentArea.Right);
					double y = MyMath.Map(height, 0, ImageHeight, CurrentArea.Bottom, CurrentArea.Top);
					array[width, height] = MapToColor(CalculatePoint(x, y));
				});

			for (int width = 0; width < ImageWidth; width++)
			{
				for (int height = 0; height < ImageHeight; height++)
				{
					image.SetPixel(width, height, array[width, height]);
				}
			}

			return image;
		}
		public void ZoomIn(double centerX, double centerY)
		{
			if (CurrentArea.Contains(centerX, centerY) is false)
			{
				centerX = MyMath.Map(centerX, 0, ImageWidth, CurrentArea.Left, CurrentArea.Right);
				centerY = MyMath.Map(centerY, 0, ImageHeight, CurrentArea.Bottom, CurrentArea.Top);
			}
			Itterations = (int)(Itterations * Math.Sqrt(ZoomFactor));
			CurrentArea = Area.ZoomIn(CurrentArea, new System.Windows.Point(centerX, centerY), ZoomFactor);
		}
		public void ZoomOut(double centerX, double centerY)
		{
			if (CurrentArea.Contains(centerX, centerY) is false)
			{
				centerX = MyMath.Map(centerX, 0, ImageWidth, CurrentArea.Left, CurrentArea.Right);
				centerY = MyMath.Map(centerY, 0, ImageHeight, CurrentArea.Bottom, CurrentArea.Top);
			}
			Itterations = (int)(Itterations / Math.Sqrt(ZoomFactor));
			CurrentArea = Area.ZoomOut(CurrentArea, new System.Windows.Point(centerX, centerY), ZoomFactor);
		}
		#endregion

		#region conversion
		private Color MapToColor(int value)
		{
			if (value == Itterations)
			{
				return Color.Black;
			}

			return ColorMapper((int)MyMath.Map(value, 1, Itterations - 1, ColorMinValue ?? 1.0, ColorMaxValue ?? Itterations));
		}
		#endregion

	}
}
