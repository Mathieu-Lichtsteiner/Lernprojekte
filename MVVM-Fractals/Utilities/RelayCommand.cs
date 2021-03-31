using System;
using System.Windows.Input;

namespace MVVM_Fractals
{
	internal class RelayCommand : ICommand {

		private readonly Action<object?> _Execute;
		private readonly Predicate<object?>? _CanExecute;

		public RelayCommand( Action<object?> execute, Predicate<object?>? canExecute = null ) {
			_Execute = execute;
			_CanExecute = canExecute;
		}

		public event EventHandler? CanExecuteChanged;
		public bool CanExecute(object? parameter)
		{
			return _CanExecute?.Invoke(parameter) ?? true;
		}

		public void Execute(object? parameter)
		{
			_Execute(parameter);
		}
	}
}
