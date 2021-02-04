using System;
using System.Windows;

namespace MVVM_Fractals {

	internal struct Area {

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
		public Area( double left, double right, double bottom, double top ) {
			Left = left;
			Right = right;
			Bottom = bottom;
			Top = top;
			Width = Math.Abs( right - left );
			Height = Math.Abs( top - bottom );
			Center = new Point( Left + Width / 2.0, Bottom + Height / 2.0 );
		}
		public Area( Point center, double width, double height ) {
			Left = center.X - width / 2.0;
			Right = center.X + width / 2.0;
			Bottom = center.Y - height / 2.0;
			Top = center.Y + height / 2.0;
			Width = width;
			Height = height;
			Center = center;
		}
		#endregion

	}
}
