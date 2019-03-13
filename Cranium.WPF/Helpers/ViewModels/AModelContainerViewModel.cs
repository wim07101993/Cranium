using System.ComponentModel;
using System.Threading.Tasks;
using Cranium.WPF.Strings;

namespace Cranium.WPF.Helpers.ViewModels
{
    public class AModelContainerViewModel<T> : AViewModelBase
        where T : INotifyPropertyChanged
    {
        #region FIELDS

        private T _model;

        #endregion FIELDS


        #region CONSTRUCTOR

        public AModelContainerViewModel(IStringsProvider stringsProvider) : base(stringsProvider)
        {
        }

        #endregion CONSTRUCTOR


        #region PROPERTIES

        public T Model
        {
            get => _model;
            set
            {
                if (Equals(_model, value))
                    return;

                if (_model != null)
                    _model.PropertyChanged -= OnModelPropertyChanged;

                if (SetProperty(ref _model, value))
                    OnModelChangedAsync();

                if (_model != null)
                    _model.PropertyChanged += OnModelPropertyChanged;
            }
        }

        #endregion PROPERTIES


        #region METHODS

        protected virtual void OnModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var _ = OnModelPropertyChangedAsync((T)sender, e.PropertyName);
        }

        protected virtual Task OnModelPropertyChangedAsync(T model, string propertyName) => Task.CompletedTask;
        protected virtual Task OnModelChangedAsync() => Task.CompletedTask;

        #endregion METHODS
    }
}