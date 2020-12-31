using System.Collections.Specialized;

namespace SystemTrayIcon {
	public class ObservableObject : INotifyCollectionChanged {
		public event NotifyCollectionChangedEventHandler CollectionChanged;
	}
}
