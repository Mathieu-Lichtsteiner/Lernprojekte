using MVVM_Fractals.Behaviour;
using MVVM_Fractals.Fractals;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Media.Imaging;

namespace MVVM_Fractals {
	internal class MainViewModel : ObservableObject, IMouseCaptureProxy {

		#region private fields
		private readonly FractalCalculator _Calculator;
		private readonly Area _DefaultArea = new Area( -2.05, 0.55, -1.3, 1.3 );
		private Stack<Bitmap> _GeneratedFractals = new Stack<Bitmap>();

		private readonly Func<int, Color> _Eins = value => ColorConverter.Method1( value, 255, 0.5 );
		private readonly Func<int, Color> _Zwei = value => ColorConverter.Method2( value, 255, 0.5 );
		private readonly Func<int, Color> _Drei = value => ColorConverter.Method3( value, 255, 0.5 );
		private readonly Func<int, Color> _Vier = value => { int val = value == 0 ? 0 : (byte)(255 / (100 / value)); return Color.FromArgb( 255, val, val, val ); };
		#endregion

		#region public properties
		public BitmapImage Fractal
			=> ImageConverter.BitmapToBitmapImage( _GeneratedFractals.Peek() );
		public int ImageWidth { get; init; } = 1000;
		public int ImageHeight
			=> (int)Math.Round( ImageWidth * (_DefaultArea.Width / _DefaultArea.Height) );
		#endregion

		#region public events
		public event EventHandler? Capture;
		public event EventHandler? Release;
		#endregion

		#region constructor
		public MainViewModel()
			=> _Calculator = new MandelbrotCalculator( ImageWidth, ImageHeight, _Zwei, _DefaultArea, 100 );
		#endregion

		#region public methods
		public void OnMouseDown( object sender, MouseCaptureEventArgs e ) {
			if( e.LeftButton ) {
				_Calculator.ZoomInUnMapped( e.X, e.Y );
				_GeneratedFractals.Push( _Calculator.RenderFractal() );
				RaisePropertyChanged( nameof( Fractal ) );
			}
			else if( e.RightButton && _GeneratedFractals.Count > 1 && _GeneratedFractals.TryPop( out _ ) )
				RaisePropertyChanged( nameof( Fractal ) );
		}
		public void OnMouseMove( object sender, MouseCaptureEventArgs e ) { }
		public void OnMouseUp( object sender, MouseCaptureEventArgs e ) { }
		#endregion

	}
}
