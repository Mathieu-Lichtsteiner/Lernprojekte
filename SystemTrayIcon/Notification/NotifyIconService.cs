using System;
using System.Drawing;
using Forms = System.Windows.Forms;

namespace SystemTrayIcon {
	public class NotifyIconService : INotificationService {

		#region private fields
		private readonly Forms.NotifyIcon _SystemTrayIcon;
		private NotificationType _ShownNotificationType;
		#endregion

		#region constructor
		public NotifyIconService() {
			_SystemTrayIcon = new Forms.NotifyIcon {
				Icon = new Icon( "Resources/welding-mask.ico" ),
				Text = "Testing the NotifyIcon!",
				ContextMenuStrip = new Forms.ContextMenuStrip(),
			};

			_SystemTrayIcon.Click += MainClicked;
			_SystemTrayIcon.BalloonTipClicked += NotifyIconClicked;
			_SystemTrayIcon.ContextMenuStrip.Items.Add( "Hello World!", null, HelloWorldClicked );

			_SystemTrayIcon.Visible = true;
		}
		#endregion

		#region public events
		public event Action<NotificationType> NotificationClicked;
		public event Action<StatusType> StatusClicked;
		#endregion

		#region public methods
		public void Notify( string message, string caption, NotificationType type, int duration, Forms.ToolTipIcon icon ) {

		}
		public void Dispose() => _SystemTrayIcon.Dispose();
		#endregion

		#region private helper methods
		private void MainClicked( object sender, EventArgs e )
			=> StatusClicked?.Invoke( StatusType.Main );
		private void HelloWorldClicked( object sender, EventArgs e )
			=> StatusClicked?.Invoke( StatusType.HelloWorld );
		private void NotifyIconClicked( object sender, EventArgs e )
			=> NotificationClicked?.Invoke( _ShownNotificationType );
		#endregion

	}
}
