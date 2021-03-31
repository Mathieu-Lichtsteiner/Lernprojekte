using System;

namespace MVVM_Fractals.Behaviour {
	internal interface IMouseCaptureProxy : IMouseMoveProxy {
		event EventHandler? Capture;
		event EventHandler? Release;

		void OnMouseDown( object sender, MouseCaptureEventArgs e );
		void OnMouseUp( object sender, MouseCaptureEventArgs e );
	}
}
