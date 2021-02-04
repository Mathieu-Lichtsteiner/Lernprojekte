﻿using MVVM_Fractals.Behaviour;
using MVVM_Fractals.Fractals;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;

namespace MVVM_Fractals {
	internal class MainViewModel : ObservableObject, IMouseCaptureProxy {

		#region private fields
		private readonly FractalCalculator _Calculator;
		Func<int, Color> _Eins = value => ColorConverter.Method1( value, 255, 0.5 );
		Func<int, Color> _Zwei = value => ColorConverter.Method2( value, 255, 0.5 );
		Func<int, Color> _Drei = value => ColorConverter.Method3( value, 255, 0.5 );
		Func<int, Color> _Vier = value => {
			int val = value == 0 ? 0 : (byte)(255 / (100 / value));
			return Color.FromArgb( 255, val, val, val );
		};
		#endregion

		#region public properties
		public System.Windows.Media.ImageSource Fractal { get; private set; }
		public int ImageWidth { get; init; } = 1000;
		public int ImageHeight
			=> (int)Math.Round( ImageWidth * (Area.Width / Area.Height) );

		public Area Area { get; private set; }
			= new Area( -2.05, 0.55, -1.3, 1.3 );
		#endregion

		#region public events
		public event EventHandler? Capture;
		public event EventHandler? Release;
		#endregion

		#region constructor
		public MainViewModel() {
			_Calculator = new MandelbrotCalculator( ImageWidth, ImageHeight, _Zwei, 100 );
			Fractal = ConvertToBitmapImage( _Calculator.RenderFractal( Area ) );
		}
		#endregion

		#region public methods
		public void OnMouseDown( object sender, MouseCaptureEventArgs e ) {
			var centerX = MyMath.Map( e.X, 0, ImageWidth, Area.Left, Area.Right );
			var centerY = MyMath.Map( e.Y, 0, ImageHeight, Area.Bottom, Area.Top );
			if( e.LeftButton ) {
				Debug.WriteLine( $"Left Click {e.X}, {e.Y}" );
			}
			else if( e.RightButton ) {
				Debug.WriteLine( $"Right Click {e.X}, {e.Y}" );
			}
		}
		public void OnMouseMove( object sender, MouseCaptureEventArgs e ) { }
		public void OnMouseUp( object sender, MouseCaptureEventArgs e ) { }
		#endregion

		#region conversion
		private static BitmapImage ConvertToBitmapImage( Bitmap bmp ) {
			using( var memory = new MemoryStream() ) {
				bmp.Save( memory, ImageFormat.Png );
				memory.Position = 0;

				var bitmapImage = new BitmapImage();
				bitmapImage.BeginInit();
				bitmapImage.StreamSource = memory;
				bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
				bitmapImage.EndInit();
				bitmapImage.Freeze();

				return bitmapImage;
			}
		}
		#endregion

	}
}
