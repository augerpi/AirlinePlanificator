//  Original author - Josh Smith - http://msdn.microsoft.com/en-us/magazine/dd419663.aspx#id0090030

using System;
using System.Diagnostics;
using System.Windows.Input;

namespace AirlinePlanificator.ViewModels.Utilities
{
    /// <summary>
    /// A command whose sole purpose is to relay its functionality to other objects by invoking delegates. The default return value for the CanExecute method is 'true'.
    /// </summary>
    public class RelayCommand<TParameter, TReturnValue> : ICommand
    {

        #region Declarations

        protected readonly Predicate<TParameter> _canExecute;
        protected readonly Func<TParameter, TReturnValue> _execute;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand&lt;T&gt;"/> class and the command can always be executed.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        public RelayCommand(Func<TParameter, TReturnValue> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public RelayCommand(Func<TParameter, TReturnValue> execute, Predicate<TParameter> canExecute)
        {

            if (execute == null)
                throw new ArgumentNullException("execute");
            _execute = execute;
            _canExecute = canExecute;
        }

        #endregion

        #region ICommand Members

        public event EventHandler CanExecuteChanged
        {
            add
            {

                if (_canExecute != null)
                    CommandManager.RequerySuggested += value;
            }
            remove
            {

                if (_canExecute != null)
                    CommandManager.RequerySuggested -= value;
            }
        }

        [DebuggerStepThrough]
        public virtual bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute((TParameter)parameter);
        }

        [DebuggerStepThrough]
        public virtual TReturnValue Execute(object parameter)
        {
            return _execute((TParameter)parameter);
        }
        void ICommand.Execute(object parameter)
        {
            throw new NotImplementedException("You cannot use the ICommand.Execute methode with this generic class RelayCommand because the class specify a return value for the method Execute. You must cast the object in the right generic class ( RelayCommand<TParameter, TReturnValue>).");
        }

        #endregion

    }

        /// <summary>
    /// A command whose sole purpose is to relay its functionality to other objects by invoking delegates. The default return value for the CanExecute method is 'true'.
    /// </summary>
    public class RelayCommand<TParameter> : RelayCommand<TParameter, bool>, ICommand
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand&lt;T&gt;"/> class and the command can always be executed.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        public RelayCommand(Action<TParameter> execute)
            : base(parameter=>{ execute(parameter); return true; }, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public RelayCommand(Action<TParameter> execute, Predicate<TParameter> canExecute)
            : base(parameter=>{ execute(parameter); return true; }, canExecute)
        {
        }

        #endregion

        #region ICommand Members

        [DebuggerStepThrough]
        public new Boolean CanExecute(object parameter)
        {
            return _canExecute == null ? true : base.CanExecute(null);
        }

        [DebuggerStepThrough]
        public new void Execute(object parameter)
        {
            base.Execute((TParameter)parameter);
        }
        void ICommand.Execute(object parameter)
        {
            Execute((TParameter)parameter);
        }

        #endregion
    }

    /// <summary>
    /// A command whose sole purpose is to relay its functionality to other objects by invoking delegates. The default return value for the CanExecute method is 'true'.
    /// </summary>
    public class RelayCommand : RelayCommand<object, bool>, ICommand
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand&lt;T&gt;"/> class and the command can always be executed.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        public RelayCommand(Action execute)
            : base(parameter => { execute(); return true; }, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public RelayCommand(Action execute, Func<Boolean> canExecute)
            : base(parameter => { execute(); return true; }, parameter => canExecute())
        {
        }

        #endregion

        #region ICommand Members

        [DebuggerStepThrough]
        public bool CanExecute()
        {
            return _canExecute == null || base.CanExecute(null);
        }

        [DebuggerStepThrough]
        public void Execute()
        {
            base.Execute(null);
        }

        void ICommand.Execute(object parameter)
        {
            Execute();
        }

        #endregion
    }
}
