using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EcoFarm
{
    public class CommandHelper<T> : ICommand
    {
        private Action<T> executeMethod;
        private Func<T, bool> canExecuteMethod;

        public CommandHelper(Action<T> executeMethod, Func<T, bool> canExecuteMethod = null)
        {
            this.executeMethod = executeMethod;
            this.canExecuteMethod = canExecuteMethod;
        }

        public event EventHandler CanExecuteChanged;

        bool ICommand.CanExecute(object parameter)
        {
            canExecuteMethod?.Invoke((T)parameter);

            if (executeMethod != null)
                return true;

            return false;
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged(this, EventArgs.Empty);
        }

        void ICommand.Execute(object parameter)
        {
            if (parameter is T t)
                executeMethod?.Invoke(t);
            else if (typeof(T) == typeof(bool) && parameter?.ToString() != null && bool.TryParse(parameter.ToString(), out bool boolResult))
                executeMethod?.Invoke((T)(object)boolResult);
            else if (typeof(T) == typeof(int) && parameter?.ToString() != null && int.TryParse(parameter.ToString(), out int intResult))
                executeMethod?.Invoke((T)(object)intResult);
            else if (typeof(T) == typeof(double) && parameter?.ToString() != null && double.TryParse(parameter.ToString(), out double doubleResult))
                executeMethod?.Invoke((T)(object)doubleResult);
            else
                executeMethod?.Invoke(default(T));
        }
    }

    public class CommandHelper : ICommand
    {
        protected Action<object> _execute;
        protected Func<object, bool> _canExecute;

        public CommandHelper(Action<object> exe = null, Func<object, bool> canExecute = null)
        {
            this._execute = exe;
            this._canExecute = canExecute;
        }

        public CommandHelper(CommandHelper source)
        {
            this._execute = source._execute;
            this._canExecute = source._canExecute;
        }

        public Action<object> ExecuteMethod
        {
            get { return _execute; }
        }

        public Func<object, bool> CanExecuteMethod
        {
            get { return _canExecute; }
            set { _canExecute = value; }
        }

        bool System.Windows.Input.ICommand.CanExecute(object parameter)
        {
            if (this._canExecute != null)
            {
                try
                {
                    return this._canExecute(parameter);
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void OnCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }

        void System.Windows.Input.ICommand.Execute(object parameter)
        {
            if (this._execute != null)
            {
                this._execute(parameter);
            }
        }
    }

}
