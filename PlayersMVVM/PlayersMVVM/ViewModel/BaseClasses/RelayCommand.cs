using System;
using System.Windows.Input;

namespace PlayersMVVM.ViewModel.BaseClasses
{
    class RelayCommand : ICommand
    {
        readonly Action<object> _execute; //delegata dla metod o 1 agrumencie object - nic nie zwraca
        readonly Predicate<object> _canExecute; // delegata dla metod zwracajacych bool dla argumentu object

        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null) throw new ArgumentNullException(nameof(execute));
            else _execute = execute;
            _canExecute = canExecute;
        }

        //sprawdza możliwość wykonania polecenia
        public bool CanExecute(object parameter) => _canExecute == null ? true : _canExecute(parameter);
        public event EventHandler CanExecuteChanged
        {
            add { if (_canExecute != null) CommandManager.RequerySuggested += value; } //dołącza metode
            remove { if (_canExecute != null) CommandManager.RequerySuggested -= value; } //usuwa metodę
        }

        //wykonuje polecenie
        public void Execute(object parameter) { _execute(parameter); }
    }
}
