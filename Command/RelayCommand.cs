using System;
using System.Windows.Input;

namespace GoninDigital.Command
{
    public class RelayCommand : ICommand
    {
        private readonly Action _action;
        private Func<bool> _canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public RelayCommand(Action action, Func<bool> canExecute = null)
        {
            _action = action;
            _canExecute = canExecute;
        }
        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute.Invoke();
        }
        public void Execute(object parameter)
        {
            _action.Invoke();
        }
    }
}
