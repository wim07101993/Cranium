using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Cranium.WPF.Data.Category;

namespace Cranium.WPF.Helpers.Controls
{
    public abstract class AModelEditingControl<T> : Control where T : class 
    {
        #region DEPENDENCY PROPERTIES

        public static readonly DependencyProperty ModelProperty = DependencyProperty.Register(
            nameof(Model),
            typeof(T),
            typeof(AModelEditingControl<T>),
            new PropertyMetadata(default(Category), OnModelChanged));

        public static readonly DependencyProperty DeleteCommandProperty = DependencyProperty.Register(
            nameof(DeleteCommand),
            typeof(ICommand),
            typeof(AModelEditingControl<T>),
            new PropertyMetadata(default(ICommand)));

        public static readonly DependencyProperty SaveCommandProperty = DependencyProperty.Register(
          nameof(SaveCommand),
          typeof(ICommand),
          typeof(AModelEditingControl<T>),
          new PropertyMetadata(default(ICommand)));

        public static readonly DependencyProperty StringsProperty = DependencyProperty.Register(
            nameof(Strings),
            typeof(Strings.Strings),
            typeof(AModelEditingControl<T>),
            new PropertyMetadata(default(Strings.Strings)));

        #endregion DEPENDENCY PROPERTIES


        #region PROPERTIES

        public T Model
        {
            get => (T) GetValue(ModelProperty);
            set => SetValue(ModelProperty, value);
        }

        public ICommand DeleteCommand
        {
            get => (ICommand) GetValue(DeleteCommandProperty);
            set => SetValue(DeleteCommandProperty, value);
        }

        public ICommand SaveCommand
        {
            get => (ICommand)GetValue(SaveCommandProperty);
            set => SetValue(SaveCommandProperty, value);
        }

        public Strings.Strings Strings
        {
            get => (Strings.Strings) GetValue(StringsProperty);
            set => SetValue(StringsProperty, value);
        }

        #endregion PROPERTIES


        #region METHODS

        private static void OnModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is AModelEditingControl<T> c))
                return;

            c.OnModelChanged(e.OldValue as T, e.NewValue as T);
        }

        protected virtual void OnModelChanged(T oldValue, T newValue)
        {
        }

        #endregion METHODS
    }
}