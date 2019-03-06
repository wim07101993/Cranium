using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Cranium.WPF.Data.Answer;
using Cranium.WPF.Data.Category;
using Cranium.WPF.Data.Files;
using Cranium.WPF.Data.Question;
using Cranium.WPF.Helpers.Extensions;
using Cranium.WPF.Helpers.ViewModels;
using Cranium.WPF.Strings;
using Prism.Commands;

namespace Cranium.WPF.Game.Question
{
    public class QuestionViewModel : AModelContainerViewModel<Data.Question.Question>
    {
        #region FIELDS

        private readonly IQuestionService _questionService;
        private readonly IGameService _gameService;

        private Data.Question.Question _question;
        private BitmapImage _imageAttachment;

        private bool _isAnswerCorrect;
        private bool _hasAnswered;

        #endregion FIELDS


        #region CONSTRUCTOR

        public QuestionViewModel(
            IStringsProvider stringsProvider, IQuestionService questionService,
            IGameService gameService)
            : base(stringsProvider)
        {
            _questionService = questionService;
            _gameService = gameService;

            _gameService.GameChanged += GameChangedAsync;
            _gameService.PlayerChanged += OnPlayerChanged;

            AnswerCommand = new DelegateCommand<bool?>(async x => await Answer(x == true));
            SelectCategoryCommand = new DelegateCommand<Category>(async x => await GetNewQuestionAsync(x));
        }

        #endregion CONSTRUCTOR


        #region PROPERTIES

        public Data.Question.Question Question
        {
            get => _question;
            set
            {
                if (!SetProperty(ref _question, value))
                    return;

                var _ = UpdateAttachmentAsync();
                RaisePropertyChanged(nameof(CorrectAnswers));
            }
        }

        public BitmapImage ImageAttachment
        {
            get => _imageAttachment;
            private set => SetProperty(ref _imageAttachment, value);
        }

        #region category

        public IEnumerable<Category> Categories
            => _gameService.Categories.Where(x => !x.IsSpecial);

        public bool NeedsToChooseCategory
            => Category?.IsSpecial == true && Question == null;

        public Category Category
            => _gameService.TileOfCurrentPlayer != null
                ? _gameService.Categories.FirstOrDefault(x => x.Id == _gameService.TileOfCurrentPlayer.CategoryId)
                : null;

        public ICommand SelectCategoryCommand { get; }

        #endregion category

        #region answer

        public ICommand AnswerCommand { get; }

        public ICommand GetNewQuestionCommand { get; }

        public bool IsAnswerCorrect
        {
            get => _isAnswerCorrect;
            set => SetProperty(ref _isAnswerCorrect, value);
        }

        public IEnumerable<Answer> CorrectAnswers
            => _question?.Answers.Where(x => x.IsCorrect);

        public bool HasAnswered
        {
            get => _hasAnswered;
            set => SetProperty(ref _hasAnswered, value);
        }

        #endregion answer

        #endregion PROPERTIES


        #region METHODS

        public Task Answer(bool correct)
        {
            HasAnswered = true;
            IsAnswerCorrect = correct;
            return Task.CompletedTask;
        }

        private async Task UpdateAttachmentAsync()
        {
            var attachment = await _questionService.GetAttachmentAsync(Question.Id);

            switch (Question.AttachmentType)
            {
                case EAttachmentType.Image:
                    ImageAttachment = attachment.ToImage();
                    break;
            }
        }

        private async Task GetNewQuestionAsync()
        {
            try
            {
                Question = await _gameService.GetQuestionAsync();
            }
            catch (Exception e)
            {
                // TODO
            }
        }

        private async Task GetNewQuestionAsync(Category category)
        {
            try
            {
                Question = await _gameService.GetQuestionAsync(category.Id);
                RaisePropertyChanged(nameof(NeedsToChooseCategory));
            }
            catch (Exception e)
            {
                // TODO
            }
        }

        private async Task OnPlayerChanged(object sender)
        {
            RaisePropertyChanged(nameof(NeedsToChooseCategory));
            RaisePropertyChanged(nameof(Category));
        }

        private Task GameChangedAsync(object sender)
        {
            RaisePropertyChanged(nameof(Categories));
            return Task.CompletedTask;
        }

        #endregion METHODS
    }
}