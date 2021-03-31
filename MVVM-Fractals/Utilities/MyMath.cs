using System;

namespace MVVM_Fractals
{
	internal static class MyMath {
		public static double Map( double value, double oldMin, double oldMax, double newMin, double newMax ) {
			double oldSize = Math.Abs( oldMax - oldMin );
			double newSize = Math.Abs( newMax - newMin );
			if( newSize > oldSize )
			{
				return newMin + ((value - oldMin) * ((oldMax - oldMin) / (newMax - newMin)));
			}

			if ( newSize < oldSize )
			{
				return newMin + ((value - oldMin) * (newMax - newMin) / (oldMax - oldMin));
			}
			else //if( newSize == oldSize )
			{
				return newMin - oldMin + value;
			}
		}
	}
}
