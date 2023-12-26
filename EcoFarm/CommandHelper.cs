using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EcoFarm
{
    public class CommandHelper<T> : System.Windows.Input.ICommand where T : class
    {
        protected Action<T> _execute;
        protected Func<T, bool> _canExecute;

        public CommandHelper(Action<T> exe = null, Func<T, bool> canExecute = null)
        {
            this._execute = exe;
            this._canExecute = canExecute;
        }

        public Action<T> ExecuteMethod
        {
            get { return _execute; }
        }

        public Func<T, bool> CanExecuteMethod
        {
            get { return _canExecute; }
        }

        bool System.Windows.Input.ICommand.CanExecute(object parameter)
        {
            if (this._canExecute != null)
            {
                try
                {
                    return this._canExecute(parameter as T);
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
                this._execute(parameter as T);
            }
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
