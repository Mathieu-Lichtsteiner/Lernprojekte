using System.ComponentModel;

namespace MVVM_Fractals {
	internal class ObservableObject : INotifyPropertyChanged {

		#region public events
		public event PropertyChangedEventHandler? PropertyChanged;
		#endregion

		#region protected methods
		protected void RaisePropertyChanged()
			=> RaisePropertyChanged( null );
		protected void RaisePropertyChanged( string? propertyName )
			=> PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( propertyName ) );
		#endregion
	}
}