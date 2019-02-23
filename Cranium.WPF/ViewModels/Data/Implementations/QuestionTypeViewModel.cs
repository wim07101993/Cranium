using System.ComponentModel;
using System.Threading.Tasks;
using Cranium.WPF.Models;
using Cranium.WPF.Services.Mongo;
using Cranium.WPF.Services.Strings;
using Cranium.WPF.ViewModels.Implementations;

namespace Cranium.WPF.ViewModels.Data.Implementations
{
    public class QuestionTypeViewModel : AViewModelBase, IQuestionTypeViewModel
    {
        #region FIELDS

        private readonly IQuestionTypeService _questionTypeService;

        private QuestionType _model;
        #endregion FIELDS


        #region CONSTRUCTOR

        public QuestionTypeViewModel(IStringsProvider stringsProvider, IQuestionTypeService questionTypeService)
            : base(stringsProvider)
        {
            _questionTypeService = questionTypeService;
        }

        #endregion CONSTRUCTOR


        #region PROPERTIES

        public QuestionType Model
        {
            get => _model;
            set
            {
                if (Equals(_model, value))
                    return;

                if (value != null)
                    value.PropertyChanged -= OnQuestionPropertyChanged;

                SetProperty(ref _model, value);
                
                if (value != null)
                    value.PropertyChanged += OnQuestionPropertyChanged;
            }
        }
        
        #endregion PROPERTIES


        #region METHODS
        
        private void OnQuestionPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnQuestionPropertyChangedAsync(sender as QuestionType, e.PropertyName);
        }

        private async Task OnQuestionPropertyChangedAsync(QuestionType item, string propertyName)
        {
            if (item == null)
                return;

            switch (propertyName)
            {
                case nameof(QuestionType.Category):
                    await _questionTypeService.UpdatePropertyAsync(item.Id, x => x.Category, item.Category);
                    break;
                case nameof(QuestionType.Explanation):
                    await _questionTypeService.UpdatePropertyAsync(item.Id, x => x.Explanation, item.Explanation);
                    break;
                case nameof(QuestionType.Name):
                    await _questionTypeService.UpdatePropertyAsync(item.Id, x => x.Name, item.Name);
                    break;
            }

        }

        #endregion METHODS
    }
}
