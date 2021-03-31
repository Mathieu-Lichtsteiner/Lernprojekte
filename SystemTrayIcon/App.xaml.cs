using System.Windows;

namespace SystemTrayIcon
{
	public partial class App : Application {

		#region private fields
		private readonly INotificationService _NotificationService;
		#endregion

		#region constructor
		public App() {
			_NotificationService = new NotifyIconService();
			_NotificationService.NotificationClicked += OnNotifyIconClicked;
			_NotificationService.StatusClicked += OnStatusClicked;
		}
		#endregion

		#region overriden methods
		protected override void OnStartup( StartupEventArgs e ) {
			MainWindow = new MainWindow( new MainViewModel() );
			MainWindow.Show();
			base.OnStartup( e );
		}
		protected override void OnExit( ExitEventArgs e ) {
			_NotificationService.Dispose();
			_NotificationService.NotificationClicked -= OnNotifyIconClicked;
			_NotificationService.StatusClicked -= OnStatusClicked;
			base.OnExit( e );
		}
		#endregion

		#region private helper methods
		private void OnNotifyIconClicked(NotificationType type)
		{
			ShowMainWindow();
		}

		private void OnStatusClicked( StatusType type ) {
			if( type == StatusType.Main )
			{
				ShowMainWindow();
			}
			else
			{
				MessageBox.Show( "Tray Icon Clicked!", "Hello World!", MessageBoxButton.OK, MessageBoxImage.Information );
			}
		}

		private void ShowMainWindow() {
			Point pos = new( SystemParameters.WorkArea.Width / 2.0, SystemParameters.WorkArea.Height / 2.0 );
			MainWindow.Left = pos.X - (MainWindow.Width / 2.0);
			MainWindow.Top = pos.Y - (MainWindow.Height / 2.0);
			MainWindow.Activate();
		}
		#endregion
	}
}
