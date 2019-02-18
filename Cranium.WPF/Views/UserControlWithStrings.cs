using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Cranium.WPF.Services.Strings;
using Cranium.WPF.ViewModels;

namespace Cranium.WPF.Views
{
    public class UserControlWithStrings : UserControl
    {
        public UserControlWithStrings()
        {
            DataContextChanged += OnDataContextChanged;
        }

        public IViewModelBase ViewModel => DataContext as IViewModelBase;

        protected virtual void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (ViewModel == null)
                return;

            ViewModel.PropertyChanged += OnViewModelPropertyChanged;
            OnStringsChanged(ViewModel.Strings, null);
        }

        protected virtual void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(IViewModelBase.Strings):
                    ViewModel.Strings.PropertyChanged += (s, p) => OnStringsChanged(ViewModel.Strings, e.PropertyName);
                    OnStringsChanged(ViewModel.Strings, null);
                    break;
            }
        }

        protected virtual void OnStringsChanged(Strings strings, string propertyName)
        {
        }
    }
}