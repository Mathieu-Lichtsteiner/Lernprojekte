﻿using MVVM_Fractals.Behaviour;
using MVVM_Fractals.Fractals;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;

namespace MVVM_Fractals {
	internal class MainViewModel : ObservableObject, IMouseCaptureProxy {

		#region private fields
		private readonly FractalCalculator _Calculator;
		private readonly Area _DefaultArea = new Area( -2.05, 0.55, -1.3, 1.3 );
		private Area _CurrentArea;
		private BitmapImage? _Fractal;
		private Stack<BitmapImage> _GeneratedFractals = new Stack<BitmapImage>();

		private readonly Func<int, Color> _Eins = value => ColorConverter.Method1( value, 255, 0.5 );
		private readonly Func<int, Color> _Zwei = value => ColorConverter.Method2( value, 255, 0.5 );
		private readonly Func<int, Color> _Drei = value => ColorConverter.Method3( value, 255, 0.5 );
		private readonly Func<int, Color> _Vier = value => { int val = value == 0 ? 0 : (byte)(255 / (100 / value)); return Color.FromArgb( 255, val, val, val ); };
		#endregion

		#region public properties
		public BitmapImage Fractal {
			get => _GeneratedFractals.Peek();
			private set {
				if( value == _Fractal )
					return;
				_GeneratedFractals.Push( value );
				RaisePropertyChanged( nameof( Fractal ) );
			}
		}
		public int ImageWidth { get; init; } = 1000;
		public int ImageHeight
			=> (int)Math.Round( ImageWidth * (_DefaultArea.Width / _DefaultArea.Height) );
		public Area CurrentArea {
			get => _CurrentArea;
			set {
				_CurrentArea = value;
				Fractal = ConvertToBitmapImage( _Calculator.RenderFractal( CurrentArea ) );
			}
		}
		#endregion

		#region public events
		public event EventHandler? Capture;
		public event EventHandler? Release;
		#endregion

		#region constructor
		public MainViewModel() {
			_Calculator = new MandelbrotCalculator( ImageWidth, ImageHeight, _Zwei, 100 );
			CurrentArea = new Area( -2.05, 0.55, -1.3, 1.3 );
		}
		#endregion

		#region public methods
		public void OnMouseDown( object sender, MouseCaptureEventArgs e ) {
			var center = new System.Windows.Point(
				MyMath.Map( e.X, 0, ImageWidth, CurrentArea.Left, CurrentArea.Right ),
				MyMath.Map( e.Y, 0, ImageHeight, CurrentArea.Bottom, CurrentArea.Top ) );
			if( e.LeftButton )
				CurrentArea = Area.ZoomIn( CurrentArea, center );
			else if( e.RightButton ) {
				if( _GeneratedFractals.Count > 1 && _GeneratedFractals.TryPop( out _ ) )
					RaisePropertyChanged( nameof( Fractal ) );
				//var newArea = Area.ZoomOut( CurrentArea, center );
				//CurrentArea = newArea >= _DefaultArea ? _DefaultArea : newArea;
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
