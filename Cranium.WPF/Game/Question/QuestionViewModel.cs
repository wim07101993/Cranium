using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Cranium.WPF.Data.Category;
using Cranium.WPF.Data.Files;
using Cranium.WPF.Data.Question;
using Cranium.WPF.Helpers.Extensions;
using Cranium.WPF.Helpers.ViewModels;
using Cranium.WPF.Strings;
using Prism.Commands;
using Unity;

namespace Cranium.WPF.Game.Question
{
    public class QuestionViewModel : AModelContainerViewModel<Data.Question.Question>
    {
        #region FIELDS

        private readonly IQuestionService _questionService;
        private readonly IGameService _gameService;
        private readonly IUnityContainer _unityContainer;

        private Data.Question.Question _question;
        private BitmapImage _imageAttachment;

        private bool _isAnswerCorrect;
        private bool _hasAnswered;

        private Category _category;

        #endregion FIELDS


        #region CONSTRUCTOR

        public QuestionViewModel(IUnityContainer unityContainer)
            : base(unityContainer.Resolve<IStringsProvider>())
        {
            _unityContainer = unityContainer;
            _questionService = _unityContainer.Resolve<IQuestionService>();
            _gameService = _unityContainer.Resolve<IGameService>();

            _gameService.GameChanged += GameChangedAsync;
            _gameService.PlayerChanged += OnPlayerChanged;
            ((INotifyCollectionChanged)_gameService.Categories).CollectionChanged += (s,e) => RaisePropertyChanged(nameof(Categories));

            SelectCategoryCommand = new DelegateCommand<Category>(x => Category = x);

            AnswerCommand = new DelegateCommand<bool?>(async x => await Answer(x == true));
            GetNewQuestionCommand = new DelegateCommand(async () => await GetNewQuestionAsync(Category));
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
                RaisePropertyChanged(nameof(Answers));
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

        public Category Category
        {
            get => _category;
            set => SetProperty(ref _category, value);
        }

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

        public IEnumerable<AnswerViewModel> Answers
            => _question?.Answers.Select(x => 
            {
                var vm = _unityContainer.Resolve<AnswerViewModel>();
                vm.Model = x;
                return vm;
            });

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
            try
            {
                var attachment = await _questionService.GetAttachmentAsync(Question.Id);

                switch (Question.AttachmentType)
                {
                    case EAttachmentType.Image:
                        ImageAttachment = attachment.ToImage();
                        break;
                }
            }
            catch (Exception e)
            {
                // TODO
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

        private Task OnPlayerChanged(object sender)
        {
            ResetQuestion();

            var tile = _gameService.TileOfCurrentPlayer;
                
            Category = tile != null
                ? _gameService.Categories.FirstOrDefault(x => x.Id == tile.CategoryId)
                : null;

            return Task.CompletedTask;
        }

        private Task GameChangedAsync(object sender)
        {
            ResetQuestion();
            return Task.CompletedTask;
        }

        private void ResetQuestion()
        {
            Category = null;
            Question = null;
            IsAnswerCorrect = false;
            HasAnswered = false;
        }
        
        #endregion METHODS
    }
}