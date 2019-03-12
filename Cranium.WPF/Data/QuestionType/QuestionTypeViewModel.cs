using System.Threading.Tasks;
using Cranium.WPF.Helpers.ViewModels;
using Cranium.WPF.Strings;
using Prism.Events;

namespace Cranium.WPF.Data.QuestionType
{
    public sealed class QuestionTypeViewModel : AModelContainerViewModel<QuestionType>
    {
        #region FIELDS

        private readonly IQuestionTypeService _questionTypeService;
        private readonly IEventAggregator _eventAggregator;
        
        #endregion FIELDS


        #region CONSTRUCTOR

        public QuestionTypeViewModel(
            IStringsProvider stringsProvider, IQuestionTypeService questionTypeService,
            IEventAggregator eventAggregator)
            : base(stringsProvider)
        {
            _questionTypeService = questionTypeService;
            _eventAggregator = eventAggregator;
        }

        #endregion CONSTRUCTOR


        #region PROPERTIES
        
        #endregion PROPERTIES


        #region METHODS

        protected override async Task OnModelPropertyChangedAsync(QuestionType model, string propertyName)
        {
            if (model == null)
                return;

            switch (propertyName)
            {
                case nameof(QuestionType.Category):
                    await _questionTypeService.UpdateAsync(model);
                    break;
                case nameof(QuestionType.Explanation):
                    await _questionTypeService.UpdateAsync(model);
                    break;
                case nameof(QuestionType.Name):
                    await _questionTypeService.UpdateAsync(model);
                    break;
            }

            _eventAggregator.GetEvent<QuestionTypeChangedEvent>().Publish(model);
        }

        #endregion METHODS
    }
}