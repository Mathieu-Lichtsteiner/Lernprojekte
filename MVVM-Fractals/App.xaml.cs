using System.Windows;

namespace MVVM_Fractals {
	public partial class App : Application {

		#region private fields
		private readonly MainWindow _MainWindow;
		private readonly MainViewModel _MainViewModel;
		#endregion

		#region constructor
		public App() {
			_MainViewModel = new MainViewModel();
			_MainWindow = new MainWindow( _MainViewModel );
		}
		#endregion

		#region OnStartup
		protected override void OnStartup( StartupEventArgs e ) {
			_MainWindow.Show();

			base.OnStartup( e );
		}
		#endregion

		#region OnExit
		protected override void OnExit( ExitEventArgs e )
			=> base.OnExit( e );
		#endregion

	}
}
