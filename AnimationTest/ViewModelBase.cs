using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Windows.Input;

namespace AnimationTest
{
    public static class PropertyHelper
    {
        // http://blogs.msdn.com/b/csharpfaq/archive/2010/03/11/how-can-i-get-objects-and-property-values-from-expression-trees.aspx
        /// <summary>
        /// 引数で渡されたプロパティから当該プロパティの名前を返します。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="e"></param>
        /// <returns></returns>
        public static string GetName<T>(Expression<Func<T>> e)
        {
            var member = (MemberExpression)e.Body;
            return member.Member.Name;
        }
    }

    /// <summary>
    /// デリゲートを呼び出すことによって、コマンドを他のオブジェクトに中継する。CanExecute メソッドの既定値は 'true'。
    /// </summary>
    public class RelayCommand : ICommand
    {
        #region fields

        private readonly Action<object> execute;
        private readonly Predicate<object> canExecute;

        #endregion // Fields

        #region Constructor

        /// <summary>
        /// 実行可否判定のないコマンドを作成
        /// </summary>
        /// <param name="execute"></param>
        public RelayCommand(Action<object> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// コマンドを作成
        /// </summary>
        /// <param name="execute"></param>
        /// <param name="canExecute"></param>
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("param: execute");

            this.execute = execute;
            this.canExecute = canExecute;
        }

        #endregion // Constructor

        #region ICommand Members

        public bool CanExecute(object parameter)
        {
            return canExecute == null ? true : canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            execute(parameter);
        }

        #endregion // ICommand Members
    }

    /// <summary>
    /// プロパティ変更通知を実装したビューモデルの基底クラス
    /// </summary>
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ViewModelBase() { }

        protected void RaisePropertyChanged<T>(Expression<Func<T>> e)
        {
            RaisePropertyChanged(PropertyHelper.GetName(e));
        }

        protected void RaisePropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
