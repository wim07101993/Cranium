using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Cranium.WPF.Data.Category;

namespace Cranium.WPF.Helpers.Controls
{
    public abstract class AModelEditingControl<T> : Control where T : class 
    {
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

        public static readonly DependencyProperty StringsProperty = DependencyProperty.Register(
            nameof(Strings),
            typeof(Strings.Strings),
            typeof(AModelEditingControl<T>),
            new PropertyMetadata(default(Strings.Strings)));


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

        public Strings.Strings Strings
        {
            get => (Strings.Strings) GetValue(StringsProperty);
            set => SetValue(StringsProperty, value);
        }


        private static void OnModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is AModelEditingControl<T> c))
                return;

            c.OnModelChanged(e.OldValue as T, e.NewValue as T);
        }

        protected virtual void OnModelChanged(T oldValue, T newValue)
        {
        }
    }
}