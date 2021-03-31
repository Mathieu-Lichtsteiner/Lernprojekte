using System;
using System.Drawing;

namespace MVVM_Fractals
{
	internal static class ColorConverter {

		#region ColorMethods
		internal static Color Method1( double hue, double saturation, double luminosity ) {
			byte r = 0, g = 0, b = 0;
			if( luminosity != 0.0 ) {
				if( saturation == 0 )
				{
					r = g = b = (byte)Math.Round( luminosity * 255.0 );
				}
				else {
					double q = (luminosity < 0.5) ? luminosity * (1.0 + saturation) : luminosity + saturation - (luminosity * saturation);
					double p = (2.0 * luminosity) - q;

					r = ColorCalc( q, hue + (1.0 / 3.0), p );
					g = ColorCalc( q, hue, p );
					b = ColorCalc( q, hue - (1.0 / 3.0), p );
				}
			}
			return Color.FromArgb( r, g, b );
		}
		internal static Color Method2( double hue, double saturation, double luminosity ) {
			byte r = 0, g = 0, b = 0;
			if( luminosity > 0.0 ) {
				if( saturation == 0.0 )
				{
					r = g = b = (byte)Math.Round( luminosity * 255.0 );
				}
				else {
					double q = (luminosity < 0.5) ? luminosity * (1.0 + saturation) : luminosity + saturation - (luminosity * saturation);
					double p = (2.0 * luminosity) - q;

					r = ColorCalc( p, q, hue + (1.0 / 3.0) );
					g = ColorCalc( p, q, hue );
					b = ColorCalc( p, q, hue - (1.0 / 3.0) );
				}
			}
			return Color.FromArgb( r, g, b );
		}
		internal static Color Method3( double hue, double saturation, double luminosity ) {
			byte r = 0, g = 0, b = 0;
			if( luminosity > 0.0 ) {
				if( saturation == 0 )
				{
					r = g = b = (byte)Math.Round( luminosity * 255.0 );
				}
				else {
					double q = (luminosity < 0.5) ? luminosity * (1.0 + saturation) : luminosity + saturation - (luminosity * saturation);
					double p = (2.0 * luminosity) - q;
					double tHue = hue / 6.0;

					r = ColorCalc( tHue + (1.0 / 3.0), p, q );
					g = ColorCalc( tHue, p, q );
					b = ColorCalc( tHue - (1.0 / 3.0), p, q );
				}
			}
			return Color.FromArgb( r, g, b );
		}
		#endregion

		#region private helpermethods
		private static byte ColorCalc( double c, double t1, double t2 ) {
			double val = t1;
			if( c < 0.0 )
			{
				c += 1.0;
			}
			else if( c > 1.0 )
			{
				c -= 1.0;
			}

			if ( c < 1.0 / 6.0 )
			{
				val = t1 + ((t2 - t1) * 6.0 * c);
			}

			if ( c < 1.0 / 2.0 )
			{
				val = t2;
			}

			if ( c < 2.0 / 3.0 )
			{
				val = t1 + ((t2 - t1) * ((2.0 / 3.0) - c) * 6.0);
			}

			return (byte)Math.Round( val * 255.0 );
		}
		#endregion
	}
}
