using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Cranium.WPF.Extensions;
using Cranium.WPF.Models;
using Cranium.WPF.Services.Game;
using Cranium.WPF.Services.Mongo;
using Cranium.WPF.Services.Strings;
using Cranium.WPF.ViewModels.Implementations;
using Prism.Commands;

namespace Cranium.WPF.ViewModels.Game.Implementations
{
    public class QuestionViewModel : AViewModelBase, IQuestionViewModel
    {
        #region FIELDS

        private readonly IQuestionService _questionService;
        private readonly IGameService _gameService;

        private Question _question;
        private BitmapImage _imageAttachment;

        private bool _isAnswerCorrect;
        private bool _hasAnswered;

        #endregion FIELDS


        #region CONSTRUCTOR

        public QuestionViewModel(
            IStringsProvider stringsProvider, IQuestionService questionService, IGameService gameService)
            : base(stringsProvider)
        {
            _questionService = questionService;
            _gameService = gameService;

            _gameService.GameChangedEvent += OnGameChanged;
            _gameService.PlayerChangedEvent += OnPlayerChanged;

            AnswerCommand = new DelegateCommand<bool?>(async x => await Answer(x == true));
            SelectCategoryCommand = new DelegateCommand<Category>(async x => await GetNewQuestionAsync(x));
        }

        #endregion CONSTRUCTOR


        #region PROPERTIES

        public Question Question
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

        public ICommand AnswerCommand { get; }

        public bool IsAnswerCorrect
        {
            get => _isAnswerCorrect;
            set => SetProperty(ref _isAnswerCorrect, value);
        }

        public IEnumerable<Answer> CorrectAnswers
            => _question?.Answers.Where(x => x.IsCorrect);

        public ICommand SelectCategoryCommand { get; }

        public IEnumerable<Category> Categories
            => _gameService
                .Game?
                .Questions?
                .Select(x => x.QuestionType.Category)
                .Distinct();

        public bool HasAnswered
        {
            get => _hasAnswered;
            set => SetProperty(ref _hasAnswered, value);
        }
        
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
            }
            catch (Exception e)
            {
                // TODO
            }
        }

        private async Task GameChangedAsync()
        {
            RaisePropertyChanged(nameof(Categories));
            await GetNewQuestionAsync();
        }
        
        private void OnPlayerChanged(object sender, EventArgs e)
        {
            var _ = GetNewQuestionAsync();
        }

        private void OnGameChanged(object sender, EventArgs e)
        {
            var _ = GameChangedAsync();
        }

        #endregion METHODS
    }
}