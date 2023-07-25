using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PZ3_NetworkService.Ostalo
{
    
    //GIM Dodata mogucnost da se moze slati parametar komadni preko RelayCommand klase iz MVVMLight Nuget-a
    #region Classes
    public class MyICommand : ICommand
    {
        Action _TargetExecuteMethod;
        Func<bool> _TargetCanExecuteMethod;

        public MyICommand(Action executeMethod)
        {
            _TargetExecuteMethod = executeMethod;
        }

        public MyICommand(Action executeMethod, Func<bool> canExecuteMethod)
        {
            _TargetExecuteMethod = executeMethod;
            _TargetCanExecuteMethod = canExecuteMethod;
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged(this, EventArgs.Empty);
        }

        bool ICommand.CanExecute(object parameter)
        {

            if (_TargetCanExecuteMethod != null)
            {
                return _TargetCanExecuteMethod();
            }

            if (_TargetExecuteMethod != null)
            {
                return true;
            }

            return false;
        }

        public event EventHandler CanExecuteChanged = delegate { };

        void ICommand.Execute(object parameter)
        {
            if (_TargetExecuteMethod != null)
            {
                _TargetExecuteMethod();
            }
        }
    }

    //omogucuje prosledjivanje parametara komadi- EventArgs
    public class MyICommand<T> : RelayCommand<T>
    {
        #region Constructors
        public MyICommand(Action<T> execute, bool keepTargetAlive = false) : base(execute, keepTargetAlive) { }

        public MyICommand(Action<T> execute, Func<T, bool> canExecute, bool keepTargetAlive = false) : base(execute, canExecute, keepTargetAlive) { }
        #endregion

        #region Override Methods
        public override void Execute(object parameter)
        {
            base.Execute(parameter);
        }
        #endregion
    }
    #endregion
}
