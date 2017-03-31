using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace _02___ViewModel
{
    public class TemplateCommand : ICommand
    {
        Action<Object> _execute;
        Func<Object, bool> _canExecute;
        public TemplateCommand(Action<Object> execute,
                                Func<Object, bool> canExecute = null)
        {
            if (execute == null)
                throw new ApplicationException("Ты шо, барыга, думаешь не давать мне делегат? :)");
            _execute = execute;

            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
                return true;
            return _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}
