using System.Drawing;

// from Rich Newman

namespace MVVM_Fractals {
	public class HSLColor {

		#region private fields
		private double _Hue = 1.0;
		private double _Saturation = 1.0;
		private double _Luminosity = 1.0;
		private const double _Scale = 240.0;
		#endregion

		#region public properties
		public double Hue {
			get => _Hue * _Scale;
			set => _Hue = CheckRange( value / _Scale );
		}
		public double Saturation {
			get => _Saturation * _Scale;
			set => _Saturation = CheckRange( value / _Scale );
		}
		public double Luminosity {
			get => _Luminosity * _Scale;
			set => _Luminosity = CheckRange( value / _Scale );
		}
		#endregion

		#region constructor
		public HSLColor() { }
		public HSLColor( Color color ) {
			SetRGB( color.R, color.G, color.B );
		}
		public HSLColor( int red, int green, int blue ) {
			SetRGB( red, green, blue );
		}
		public HSLColor( double hue, double saturation, double luminosity ) {
			Hue = hue;
			Saturation = saturation;
			Luminosity = luminosity;
		}
		#endregion

		#region Casts to/from System.Drawing.Color
		public static implicit operator Color( HSLColor hslColor ) {
			double r = 0, g = 0, b = 0;
			if( hslColor._Luminosity != 0 ) {
				if( hslColor._Saturation == 0 )
					r = g = b = hslColor._Luminosity;
				else {
					double temp2 = hslColor._Luminosity < 0.5
						? hslColor._Luminosity * (1.0 + hslColor._Saturation)
						: hslColor._Luminosity + hslColor._Saturation - (hslColor._Luminosity * hslColor._Saturation);
					double temp1 = (2.0 * hslColor._Luminosity) - temp2;

					r = GetColorComponent( temp1, temp2, hslColor._Hue + (1.0 / 3.0) );
					g = GetColorComponent( temp1, temp2, hslColor._Hue );
					b = GetColorComponent( temp1, temp2, hslColor._Hue - (1.0 / 3.0) );
				}
			}
			return Color.FromArgb( (int)(255 * r), (int)(255 * g), (int)(255 * b) );
		}
		public static implicit operator HSLColor( Color color ) {
			var hslColor = new HSLColor {
				_Hue = color.GetHue() / 360.0, // we store hue as 0-1 as opposed to 0-360 
				_Luminosity = color.GetBrightness(),
				_Saturation = color.GetSaturation()
			};
			return hslColor;
		}
		#endregion

		#region public methods
		public override string ToString()
			=> string.Format( "H: {0:#0.##} S: {1:#0.##} L: {2:#0.##}", Hue, Saturation, Luminosity );
		public string ToRGBString() {
			var color = (Color)this;
			return string.Format( "R: {0:#0.##} G: {1:#0.##} B: {2:#0.##}", color.R, color.G, color.B );
		}
		public void SetRGB( int red, int green, int blue ) {
			var hslColor = (HSLColor)Color.FromArgb( red, green, blue );
			this._Hue = hslColor._Hue;
			this._Saturation = hslColor._Saturation;
			this._Luminosity = hslColor._Luminosity;
		}
		#endregion

		#region private helpermethods
		private static double GetColorComponent( double t1, double t2, double c ) {
			if( c < 0.0 )
				c += 1.0;
			else if( c > 1.0 )
				c -= 1.0;
			if( c < 1.0 / 6.0 )
				return t1 + ((t2 - t1) * 6.0 * c);
			else if( c < 0.5 )
				return t2;
			else if( c < 2.0 / 3.0 )
				return t1 + ((t2 - t1) * ((2.0 / 3.0) - c) * 6.0);
			else
				return t1;
		}
		private static double CheckRange( double value ) {
			if( value < 0.0 )
				value = 0.0;
			else if( value > 1.0 )
				value = 1.0;
			return value;
		}
		#endregion

	}
}
