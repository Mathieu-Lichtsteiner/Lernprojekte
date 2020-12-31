using System;
using System.Windows.Forms;

namespace SystemTrayIcon {

	public enum StatusType {
		Main,
		HelloWorld
	}
	public enum NotificationType {

	}

	public interface INotificationService : IDisposable {

		event Action<NotificationType> NotificationClicked;
		event Action<StatusType> StatusClicked;
		void Notify( string message, string caption, NotificationType type, int duration, ToolTipIcon icon );

	}
}
