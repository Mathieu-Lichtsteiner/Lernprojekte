using System;
using System.Windows;

namespace MVVM_Fractals
{

	internal struct Area
	{

		#region public properties
		public double Left { get; }
		public double Right { get; }
		public double Bottom { get; }
		public double Top { get; }
		public Point Center { get; }
		public double Width { get; }
		public double Height { get; }
		#endregion

		#region constructor
		public Area(double left, double right, double bottom, double top)
		{
			Left = left;
			Right = right;
			Bottom = bottom;
			Top = top;
			Width = Math.Abs(right - left);
			Height = Math.Abs(top - bottom);
			Center = new Point(Left + Width / 2.0, Bottom + Height / 2.0);
		}
		public Area(Point center, double width, double height)
		{
			Left = center.X - width / 2.0;
			Right = center.X + width / 2.0;
			Bottom = center.Y - height / 2.0;
			Top = center.Y + height / 2.0;
			Width = width;
			Height = height;
			Center = center;
		}
		#endregion

		#region public factories
		public static Area Move(Area oldArea, Point center)
		{
			return (Math.Abs(center.X) > 2.0 || Math.Abs(center.Y) > 2.0)
					   ? throw new ArgumentException("The point is outside the boundries of a Mandelbrot-Set!", nameof(center))
					   : new Area(center, oldArea.Width, oldArea.Height);
		}

		public static Area ZoomIn(Area oldArea, double zoomFactor)
		{
			return ZoomIn(oldArea, oldArea.Center, zoomFactor);
		}

		public static Area ZoomIn(Area oldArea, Point center, double zoomFactor)
		{
			return (zoomFactor == 0.0)
					   ? throw new ArgumentException("Can't allow zero, because it would result in an infinite Area!", nameof(zoomFactor))
					   : new Area(center, oldArea.Width / zoomFactor, oldArea.Height / zoomFactor);
		}

		public static Area ZoomOut(Area oldArea, double zoomFactor)
		{
			return ZoomOut(oldArea, oldArea.Center, zoomFactor);
		}

		public static Area ZoomOut(Area oldArea, Point center, double zoomFactor)
		{
			return (zoomFactor == 0.0)
					   ? throw new ArgumentException("Can't allow zero, because it would result in an Area of size zero!", nameof(zoomFactor))
					   : new Area(center, oldArea.Width * zoomFactor, oldArea.Height * zoomFactor);
		}
		#endregion

		#region public methods
		public bool Contains(Point center)
		{
			return Left < center.X && center.X < Right && Bottom < center.Y && center.Y < Top;
		}

		public bool Contains(double x, double y)
		{
			return Left < x && x < Right && Bottom < y && y < Top;
		}
		#endregion

		#region operators
		public static Area operator +(Area one, Area two)
		{
			return new Area(one.Left + two.Left, one.Right + two.Right, one.Bottom + two.Bottom, one.Top + two.Top);
		}

		public static Area operator -(Area one, Area two)
		{
			return new Area(one.Left - two.Left, one.Right - two.Right, one.Bottom - two.Bottom, one.Top - two.Top);
		}

		public static Area operator *(Area one, Area two)
		{
			return new Area(one.Left * two.Left, one.Right * two.Right, one.Bottom * two.Bottom, one.Top * two.Top);
		}

		public static Area operator /(Area one, Area two)
		{
			return new Area(one.Left / two.Left, one.Right / two.Right, one.Bottom / two.Bottom, one.Top / two.Top);
		}

		public static bool operator <(Area one, Area two)
		{
			return one.Width * one.Height < two.Width * two.Height;
		}

		public static bool operator >(Area one, Area two)
		{
			return one.Width * one.Height > two.Width * two.Height;
		}

		public static bool operator <=(Area one, Area two)
		{
			return one.Width * one.Height <= two.Width * two.Height;
		}

		public static bool operator >=(Area one, Area two)
		{
			return one.Width * one.Height >= two.Width * two.Height;
		}

		public static bool operator ==(Area one, Area two)
		{
			return one.Left == two.Left && one.Right == two.Right && one.Bottom == two.Bottom && one.Top == two.Top;
		}

		public static bool operator !=(Area one, Area two)
		{
			return one.Left != two.Left || one.Right != two.Right || one.Bottom != two.Bottom || one.Top != two.Top;
		}

		public override bool Equals(object? obj)
		{
			return obj is Area area ? this == area : false;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
		#endregion

		#region overrides
		public override string ToString()
		{
			return $"{Top}, {Left}, {Right}, {Bottom}; {Width}*{Height} (T,L,R,B; w*h)";
		}
		#endregion

	}
}
