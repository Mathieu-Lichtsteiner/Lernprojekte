using System.ComponentModel;

namespace SystemTrayIcon {
	public class ObservableObject : INotifyPropertyChanged {
		public event PropertyChangedEventHandler? PropertyChanged;
	}
}
