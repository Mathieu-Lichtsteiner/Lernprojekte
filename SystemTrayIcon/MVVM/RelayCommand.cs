using System;
using System.Windows.Input;

namespace SystemTrayIcon
{
	public class RelayCommand : ICommand {

		private readonly Action<object> _Execute;

		public RelayCommand(Action<object> execute)
		{
			_Execute = execute;
		}

		public event EventHandler CanExecuteChanged;
		public bool CanExecute(object parameter)
		{
			return true;
		}

		public void Execute(object parameter)
		{
			_Execute(parameter);
		}
	}
}
