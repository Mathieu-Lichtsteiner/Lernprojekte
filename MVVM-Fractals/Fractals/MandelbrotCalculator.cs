﻿using System;
using System.Drawing;

namespace MVVM_Fractals.Fractals {
	internal class MandelbrotCalculator : FractalCalculator {

		#region constructor
		public MandelbrotCalculator( int imageWidth, int imageHeight, Func<int, Color> colorMapper, int itterations = 100 )
			: base( imageWidth, imageHeight, colorMapper, itterations ) { }
		public MandelbrotCalculator( int imageWidth, int imageHeight, Func<int, Color> colorMapper, int itterations = 100, int? colorMinValue = null, int? colorMaxValue = null )
			: base( imageWidth, imageHeight, colorMapper, itterations, colorMinValue, colorMaxValue ) { }
		#endregion

		#region overriden methods
		protected override int CalculatePoint( double real, double imaginary ) {
			// For each number c: square, add c for i times
			double constReal = real;
			double constImaginary = imaginary;
			for( int i = 1; i <= Itterations; i++ ) {
				// Square Complex c : temp = Real * Real - Imaginary * Imaginary; c.Imaginary = 2 * Real * Imaginary; Real = temp;
				double temp = (real * real) - (imaginary * imaginary) + constReal;
				imaginary = (2 * real * imaginary) + constImaginary;
				real = temp;
				// Calculate magnitude (pythagoras) and if bigger than 2 it will explode (2*2=4)
				if( (real * real) + (imaginary * imaginary) > 4.0 )
					return i;
			}
			return Itterations;
		}
		#endregion

	}
}
