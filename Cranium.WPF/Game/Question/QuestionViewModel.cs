using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Cranium.WPF.Data.Category;
using Cranium.WPF.Data.Files;
using Cranium.WPF.Data.Question;
using Cranium.WPF.Helpers;
using Cranium.WPF.Helpers.Extensions;
using Cranium.WPF.Helpers.ViewModels;
using Cranium.WPF.Strings;
using Prism.Commands;
using Prism.Events;
using Unity;

namespace Cranium.WPF.Game.Question
{
    public class QuestionViewModel : AModelContainerViewModel<Data.Question.Question>
    {
        #region FIELDS

        private readonly IQuestionService _questionService;
        private readonly IGameService _gameService;
        private readonly IUnityContainer _unityContainer;
        private readonly IEventAggregator _eventAggregator;
        private readonly IFileService _fileService;

        private BitmapImage _imageAttachment;

        private bool _isAnswerCorrect;
        private bool _hasAnswered;

        private Category _category;
        private BitmapImage _categoryImage;

        #endregion FIELDS


        #region CONSTRUCTOR

        public QuestionViewModel(IUnityContainer unityContainer)
            : base(unityContainer.Resolve<IStringsProvider>())
        {
            _unityContainer = unityContainer;
            _questionService = _unityContainer.Resolve<IQuestionService>();
            _gameService = _unityContainer.Resolve<IGameService>();
            _eventAggregator = _unityContainer.Resolve<IEventAggregator>();
            _fileService = _unityContainer.Resolve<IFileService>();

            _gameService.GameChanged += GameChangedAsync;
            _gameService.PlayerChanged += OnPlayerChanged;
            ((INotifyCollectionChanged) _gameService.Categories).CollectionChanged +=
                (s, e) => RaisePropertyChanged(nameof(Categories));

            SelectCategoryCommand = new DelegateCommand<Category>(x => Category = x);

            AnswerCommand = new DelegateCommand<bool?>(x =>
            {
                var _ = Answer(x == true);
            });
            GetNewQuestionCommand = new DelegateCommand(() =>
            {
                var _ = GetNewQuestionAsync(Category);
            });
        }

        #endregion CONSTRUCTOR


        #region PROPERTIES

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
            set
            {
                if (value?.IsSpecial == true)
                    value = null;

                if (!SetProperty(ref _category, value))
                    return;

                if (value != null)
                {
                    var _ = GetNewQuestionAsync(value);
                }
            }
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
            => Model?.Answers.Select(x =>
            {
                var vm = _unityContainer.Resolve<AnswerViewModel>();
                vm.Model = x;
                return vm;
            });

        public bool HasAnswered
        {
            get => _hasAnswered;
            set
            {
                if (!SetProperty(ref _hasAnswered, value))
                    return;

                if (_hasAnswered)
                    QuestionAnswered?.Invoke(this);
            }
        }

        public BitmapImage CategoryImage
        {
            get => _categoryImage;
            set => SetProperty(ref _categoryImage, value);
        }

        public bool ShowAnswers
            => Model?.Answers?.Count > 1;

        public bool HasContentToShow
            => Model != null && (
                   ShowAnswers ||
                   !string.IsNullOrWhiteSpace(Model?.Task) ||
                   !string.IsNullOrWhiteSpace(Model?.Tip) ||
                   Model.AttachmentType == EAttachmentType.Image
               );

        #endregion answer

        #endregion PROPERTIES


        #region METHODS

        public Task Answer(bool correct)
        {
            Category = null;
            Model = null;
            IsAnswerCorrect = correct;
            HasAnswered = true;
            return Task.CompletedTask;
        }

        private async Task UpdateAttachmentsAsync()
        {
            if (Model == null || Model.AttachmentType != EAttachmentType.Image)
            {
                ImageAttachment = null;
                return;
            }

            try
            {
                var attachment = await _questionService.GetAttachmentAsync(Model);

                switch (Model.AttachmentType)
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

        private async Task UpdateCategoryImageAsync()
        {
            if (Category == null)
            {
                CategoryImage = null;
                return;
            }

            try
            {
                var categoryImageBytes = await _fileService.GetOneAsync(Category.Image);
                CategoryImage = categoryImageBytes.ToImage();
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
                Model = await _gameService.GetQuestionAsync(category.Id);
            }
            catch (Exception e)
            {
                // TODO
            }
        }

        private Task OnPlayerChanged(IGameService sender)
        {
            ResetQuestion();

            var tile = _gameService.TileOfCurrentPlayer;

            Category = tile != null
                ? _gameService.Categories.FirstOrDefault(x => x.Id == tile.CategoryId)
                : null;

            return Task.CompletedTask;
        }

        private Task GameChangedAsync(IGameService sender)
        {
            ResetQuestion();
            return Task.CompletedTask;
        }

        private void ResetQuestion()
        {
            Category = null;
            Model = null;
            IsAnswerCorrect = false;
            HasAnswered = false;
        }

        protected override async Task OnModelChangedAsync()
        {
            await base.OnModelChangedAsync();

            await UpdateAttachmentsAsync();
            await UpdateCategoryImageAsync();
            RaisePropertyChanged(nameof(Answers));
            RaisePropertyChanged(nameof(ShowAnswers));
            RaisePropertyChanged(nameof(HasContentToShow));

            if (Model == null)
                _eventAggregator.GetEvent<HideQuestionEvent>().Publish();
            else
                _eventAggregator.GetEvent<ShowQuestionEvent>().Publish(this);
        }

        #endregion METHODS


        #region EVENTS

        public event AsyncEventHandler<QuestionViewModel> QuestionAnswered;

        #endregion EVENTS
    }
}