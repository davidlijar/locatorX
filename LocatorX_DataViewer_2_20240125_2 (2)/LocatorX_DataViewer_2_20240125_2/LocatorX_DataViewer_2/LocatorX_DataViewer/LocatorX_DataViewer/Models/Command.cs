using System;
using System.Windows.Input;

namespace Controller_Tester.Standard_Simulation
{
    public class Command : ICommand
    {
        private readonly Action _action;
        private bool _isEnabled;

        public Command(Action action, bool isEnabled = true)
        {
            _action = action;
            _isEnabled = isEnabled;
        }

        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                _isEnabled = value;
                this.RaiseCanExecuteChanged();
            }
        }

        public bool CanExecute(object parameter)
        {
            return _isEnabled;
        }

        public void Execute(object parameter)
        {
            if (_action != null)
                _action();
        }

        public event EventHandler CanExecuteChanged;

        protected virtual void RaiseCanExecuteChanged()
        {
            var handler = CanExecuteChanged;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }
    }
}