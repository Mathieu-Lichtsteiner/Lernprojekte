﻿using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace MVVM_Fractals.Behaviour
{
	internal class MouseCaptureBehavior : Behavior<FrameworkElement> {

		#region OnAttached/Detaching (Register Eventhandlers)			
		protected override void OnAttached() {
			base.OnAttached();
			AssociatedObject.PreviewMouseDown += OnMouseDown;
			AssociatedObject.PreviewMouseMove += OnMouseMove;
			AssociatedObject.PreviewMouseUp += OnMouseUp;
		}
		protected override void OnDetaching() {
			base.OnDetaching();
			AssociatedObject.PreviewMouseDown -= OnMouseDown;
			AssociatedObject.PreviewMouseMove -= OnMouseMove;
			AssociatedObject.PreviewMouseUp -= OnMouseUp;
		}
		#endregion

		#region mouse-position (DependencyProperty)
		public static readonly DependencyProperty MouePointProperty = DependencyProperty.Register(
			nameof( MousePoint ),
			typeof( Point ),
			typeof( MouseCaptureBehavior ),
			new PropertyMetadata( default( Point ) ) );
		public static readonly DependencyProperty MouseXProperty = DependencyProperty.Register(
			nameof( MouseX ),
			typeof( double ),
			typeof( MouseCaptureBehavior ),
			new PropertyMetadata( default( double ) ) );
		public static readonly DependencyProperty MouseYProperty = DependencyProperty.Register(
			nameof( MouseY ),
			typeof( double ),
			typeof( MouseCaptureBehavior ),
			new PropertyMetadata( default( double ) ) );
		public double MouseX {
			get => (double)GetValue( MouseXProperty );
			set => SetValue( MouseXProperty, value );
		}
		public double MouseY {
			get => (double)GetValue( MouseYProperty );
			set => SetValue( MouseYProperty, value );
		}
		public Point MousePoint {
			get => (Point)GetValue( MouePointProperty );
			set => SetValue( MouePointProperty, value );
		}
		#endregion

		#region proxy (AttachedProperty) for MouseClicks
		public static readonly DependencyProperty ProxyProperty = DependencyProperty.RegisterAttached(
			"Proxy",
			typeof( IMouseCaptureProxy ),
			typeof( MouseCaptureBehavior ),
			new PropertyMetadata( null, OnProxyChanged ) );

		public static void SetProxy(DependencyObject source, IMouseCaptureProxy value)
		{
			source.SetValue(ProxyProperty, value);
		}

		public static IMouseCaptureProxy GetProxy(DependencyObject source)
		{
			return (IMouseCaptureProxy)source.GetValue(ProxyProperty);
		}

		private static void OnProxyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e ) {
			if( e.OldValue is IMouseCaptureProxy oldVal ) {
				oldVal.Capture -= OnCapture;
				oldVal.Release -= OnRelease;
			}
			if( e.NewValue is IMouseCaptureProxy newVal ) {
				newVal.Capture += OnCapture;
				newVal.Release += OnRelease;
			}
		}
		#endregion

		#region Click-Eventhandlers
		private static void OnCapture(object? sender, EventArgs e)
		{
			(sender as MouseCaptureBehavior)?.AssociatedObject.CaptureMouse();
		}

		private static void OnRelease(object? sender, EventArgs e)
		{
			(sender as MouseCaptureBehavior)?.AssociatedObject.ReleaseMouseCapture();
		}

		private void OnMouseDown( object sender, MouseButtonEventArgs e ) {
			if( GetProxy( this ) is IMouseCaptureProxy proxy ) {
				Point pos = e.GetPosition( AssociatedObject );
				MouseCaptureEventArgs? args = new MouseCaptureEventArgs {
					X = pos.X,
					Y = pos.Y,
					LeftButton = (e.LeftButton == MouseButtonState.Pressed),
					RightButton = (e.RightButton == MouseButtonState.Pressed)
				};
				proxy.OnMouseDown( this, args );
			}
		}
		private void OnMouseMove( object sender, MouseEventArgs e ) {
			MousePoint = e.GetPosition( AssociatedObject );
			MouseX = MousePoint.X;
			MouseY = MousePoint.Y;
			if( GetProxy( this ) is IMouseCaptureProxy proxy ) {
				Point pos = e.GetPosition( AssociatedObject );
				MouseCaptureEventArgs? args = new MouseCaptureEventArgs {
					X = pos.X,
					Y = pos.Y,
					LeftButton = (e.LeftButton == MouseButtonState.Pressed),
					RightButton = (e.RightButton == MouseButtonState.Pressed)
				};
				proxy.OnMouseMove( this, args );
			}
		}
		private void OnMouseUp( object sender, MouseButtonEventArgs e ) {
			if( GetProxy( this ) is IMouseCaptureProxy proxy ) {
				Point pos = e.GetPosition( AssociatedObject );
				MouseCaptureEventArgs? args = new MouseCaptureEventArgs {
					X = pos.X,
					Y = pos.Y,
					LeftButton = (e.LeftButton == MouseButtonState.Pressed),
					RightButton = (e.RightButton == MouseButtonState.Pressed)
				};
				proxy.OnMouseUp( this, args );
			}
		}
		#endregion

	}
}
