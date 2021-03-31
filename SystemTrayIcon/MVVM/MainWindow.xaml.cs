using System.Windows;

namespace SystemTrayIcon
{
	public partial class MainWindow : Window {

		public MainWindow(MainViewModel viewModel) : this()
		{
			DataContext = viewModel;
		}

		public MainWindow()
		{
			InitializeComponent();
		}
	}
}
