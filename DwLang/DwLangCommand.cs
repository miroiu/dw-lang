using System;
using System.Windows.Input;

namespace DwLang
{
    public class DwLangCommand : ICommand
    {
        private readonly Action _action;
        private readonly Func<bool> _canExecute;

        public event EventHandler CanExecuteChanged;

        public DwLangCommand(Action action, Func<bool> canExecute = default)
        {
            _action = action;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
            => _canExecute?.Invoke() ?? true;

        public void Execute(object parameter)
            => _action.Invoke();
    }
}
