using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Cranium.WPF.Extensions;
using Cranium.WPF.Models;
using Cranium.WPF.Services.Mongo;
using Cranium.WPF.Services.Strings;
using Cranium.WPF.ViewModels.Implementations;
using Microsoft.Win32;
using Prism.Commands;
using Shared.Extensions;
using Unity;

namespace Cranium.WPF.ViewModels.Data.Implementations
{
    public class QuestionViewModel : AViewModelBase, IQuestionViewModel
    {
        #region FIELDS

        private readonly IFileService _fileService;
        private readonly IQuestionService _questionService;

        private Question _model;
        private byte[] _attachment;

        #endregion FIELDS


        #region CONSTRUCTOR

        public QuestionViewModel(IUnityContainer unityContainer)
            : base(unityContainer.Resolve<IStringsProvider>())
        {
            _fileService = unityContainer.Resolve<IFileService>();
            _questionService = unityContainer.Resolve<IQuestionService>();
            AnswersViewModel = unityContainer.Resolve<IAnswersViewModel>();

            ChangeAttachmentCommand = new DelegateCommand(async () => await ChangeAttachmentAsync());

            var _ = GetAttachmentFromDbAsync();
            AnswersViewModel.AnyAnswerChanged += OnAnyAnswerChanged;
        }

        #endregion CONSTRUCTOR


        #region PROPERTIES

        public Question Model
        {
            get => _model;
            set
            {
                if (Equals(_model, value))
                    return;

                if (_model != null)
                    value.PropertyChanged -= OnQuestionPropertyChanged;

                SetProperty(ref _model, value);

                var _ = GetAttachmentFromDbAsync();
                
                
                if (value == null)
                    AnswersViewModel.Models = null;
                else
                {
                    if (value.Answers == null)
                        value.Answers = new ObservableCollection<Answer>();

                    AnswersViewModel.Models = value.Answers;
                    value.PropertyChanged += OnQuestionPropertyChanged;
                }
            }
        }

        public byte[] Attachment
        {
            get => _attachment;
            private set => SetProperty(ref _attachment, value);
        }

        public ICommand ChangeAttachmentCommand { get; }

        public IAnswersViewModel AnswersViewModel { get; }

        #endregion PROPERTIES


        #region METHODS

        public async Task GetAttachmentFromDbAsync()
        {
            if (Model == null)
            {
                Attachment = null;
                return;
            }

            using (var stream = new MemoryStream())
            {
                await _fileService.GetOneAsync(_model.Attachment, stream);

                Attachment = new byte[stream.Length];
                stream.Read(Attachment, 0, Attachment.Length);
            }
        }

        public async Task ChangeAttachmentAsync()
        {
            var dialog = new OpenFileDialog
            {
                Multiselect = false,
                Title = Strings.SelectAnAttachment
            };
            dialog.ShowDialog();

            if (string.IsNullOrWhiteSpace(dialog.FileName))
                return;

            if (Model.Attachment != default)
                await _fileService.RemoveAsync(Model.Attachment);

            var stream = File.OpenRead(dialog.FileName);
            var fileName = Path.GetFileName(dialog.FileName);

            var imgId = await _fileService.CreateAsync(stream, fileName);
            Model.Attachment = imgId;
        }

        private void OnQuestionPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var _ = OnQuestionPropertyChangedAsync(sender as Question, e.PropertyName);
        }
        
        private void OnAnyAnswerChanged(object sender, EventArgs e)
        {
            var _ = OnQuestionPropertyChangedAsync(Model, nameof(Question.Answers));
        }

        private async Task OnQuestionPropertyChangedAsync(Question item, string propertyName)
        {
            if (item == null)
                return;

            switch (propertyName)
            {
                case nameof(Question.Task):
                    await _questionService.UpdatePropertyAsync(item.Id, x => x.Task, item.Task);
                    break;
                case nameof(Question.QuestionType):
                    await _questionService.UpdatePropertyAsync(item.Id, x => x.QuestionType, item.QuestionType);
                    break;
                case nameof(Question.Answers):
                    await _questionService.UpdatePropertyAsync(item.Id, x => x.Answers, AnswersViewModel.Models);
                    break;
                case nameof(Question.Tip):
                    await _questionService.UpdatePropertyAsync(item.Id, x => x.Tip, item.Tip);
                    break;
            }
        }

        #endregion METHODS
    }
}