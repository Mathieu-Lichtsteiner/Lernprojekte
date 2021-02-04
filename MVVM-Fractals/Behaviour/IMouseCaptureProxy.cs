using System;

namespace MVVM_Fractals.Behaviour {
	internal interface IMouseCaptureProxy {
		event EventHandler? Capture;
		event EventHandler? Release;

		void OnMouseDown( object sender, MouseCaptureEventArgs e );
		void OnMouseMove( object sender, MouseCaptureEventArgs e );
		void OnMouseUp( object sender, MouseCaptureEventArgs e );
	}
}
