using System;

namespace MVVM_Fractals
{

	// An Exampleclass for my personal understanding of Complex numbers
	internal class Complex {

		#region public properties
		public double Real { get; private set; }
		public double Imaginary { get; private set; }
		#endregion

		#region constructor 
		// private, because it is an exampleclass
		private Complex( double real, double imaginary ) {
			Real = real;
			Imaginary = imaginary;
		}
		#endregion

		#region methods
		internal void Square() {
			double temp = Real * Real - Imaginary * Imaginary;
			Imaginary = 2 * Real * Imaginary;
			Real = temp;
		}

		internal double Magnitude()
		{
			return Math.Sqrt(Real * Real + Imaginary * Imaginary);
		}

		internal void Add( Complex c ) {
			Real += c.Real;
			Imaginary += c.Imaginary;
		}
		#endregion

		#region operators
		public static Complex operator +(Complex a, Complex b)
		{
			return new Complex(a.Real + b.Real, a.Imaginary + b.Imaginary);
		}
		#endregion

	}

}
