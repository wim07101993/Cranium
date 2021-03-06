﻿using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Cranium.WPF.Data.QuestionType;
using Cranium.WPF.Helpers.Extensions;
using Cranium.WPF.Helpers.ViewModels;
using Prism.Events;
using Unity;

namespace Cranium.WPF.Data.Question
{
    public sealed class QuestionsViewModel : ACollectionViewModel<Question, QuestionViewModel>
    {
        private readonly IQuestionTypeService _questionTypeService;


        public QuestionsViewModel(IUnityContainer unityContainer) : base(unityContainer)
        {
            _questionTypeService = unityContainer.Resolve<IQuestionTypeService>();
            unityContainer.Resolve<IEventAggregator>()
                .GetEvent<QuestionTypeChangedEvent>()
                .Subscribe(UpdateQuestionTypes);

            var _ = UpdateCollectionAsync();
        }


        public ObservableCollection<QuestionType.QuestionType> QuestionTypes {get;} = new ObservableCollection<QuestionType.QuestionType>();


        public override async Task UpdateCollectionAsync()
        {
            await base.UpdateCollectionAsync();

            var questionTypes = await _questionTypeService.GetAsync();
            QuestionTypes.Clear();
            QuestionTypes.Add(questionTypes);
        }

        private void UpdateQuestionTypes(QuestionType.QuestionType questionType)
        {
            var i = QuestionTypes.FindFirstIndex(x => questionType.Id == x.Id);
            if (i < 0)
                return;

            QuestionTypes[i].Category = questionType.Category;
            QuestionTypes[i].Explanation = questionType.Explanation;
            QuestionTypes[i].Name = questionType.Name;
        }
    }
}