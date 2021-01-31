using System.Windows;

namespace MVVM_Fractals {
	public partial class MainWindow : Window {

		internal MainWindow( MainViewModel mainViewModel ) : this()
			=> DataContext = mainViewModel;
		public MainWindow()
			=> InitializeComponent();

	}
}
