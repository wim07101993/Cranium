using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Cranium.WPF.Data.Answer;
using Cranium.WPF.Data.Files;
using Cranium.WPF.Helpers.Extensions;
using Cranium.WPF.Helpers.ViewModels;
using Cranium.WPF.Strings;
using Microsoft.Win32;
using Prism.Commands;
using Unity;

namespace Cranium.WPF.Data.Question
{
    public class QuestionViewModel : AModelContainerViewModel<Question>
    {
        #region FIELDS

        private readonly IFileService _fileService;
        private readonly IQuestionService _questionService;

        private byte[] _attachment;

        #endregion FIELDS


        #region CONSTRUCTOR

        public QuestionViewModel(IUnityContainer unityContainer)
            : base(unityContainer.Resolve<IStringsProvider>())
        {
            _fileService = unityContainer.Resolve<IFileService>();
            _questionService = unityContainer.Resolve<IQuestionService>();
            AnswersViewModel = unityContainer.Resolve<AnswersViewModel>();

            ChangeAttachmentCommand = new DelegateCommand(async () => await ChangeAttachmentAsync());

            var _ = GetAttachmentFromDbAsync();
            AnswersViewModel.AnyAnswerChanged += OnAnyAnswerChanged;
        }

        #endregion CONSTRUCTOR


        #region PROPERTIES
        
        public byte[] Attachment
        {
            get => _attachment;
            private set
            {
                SetProperty(ref _attachment, value);
                RaisePropertyChanged(nameof(ImageSource));
            }
        }

        public BitmapImage ImageSource
            => Model?.AttachmentType == EAttachmentType.Image 
            ? Attachment?.ToImage() 
            : null;

        public ICommand ChangeAttachmentCommand { get; }

        public AnswersViewModel AnswersViewModel { get; }

        #endregion PROPERTIES


        #region METHODS

        public async Task GetAttachmentFromDbAsync()
        {
            if (Model == null)
            {
                Attachment = null;
                return;
            }

            try
            {
                Attachment = await _questionService.GetAttachmentAsync(Model);
            }
            catch (Exception e)
            {
                // TODO
            }
        }

        public async Task ChangeAttachmentAsync()
        {
            var dialog = new OpenFileDialog
            {
                Multiselect = false,
                Title = Strings.SelectAnImageFile,
                Filter = $"{_fileService.GenerateImageFilter()}|" +
                         $"{_fileService.GenerateMusicFilter()}|" +
                         $"{_fileService.GenerateVideoFilter()}"
            };
            dialog.ShowDialog();

            if (string.IsNullOrWhiteSpace(dialog.FileName))
                return;

            var stream = File.OpenRead(dialog.FileName);
            var fileName = Path.GetFileName(dialog.FileName);

            var attachmentType = EAttachmentType.None;
            var extension = Path.GetExtension(fileName);
            if (_fileService.ImageExtensions.Any(x => x == extension))
                attachmentType = EAttachmentType.Image;
            else if (_fileService.MusicExtensions.Any(x => x == extension))
                attachmentType = EAttachmentType.Music;
            else if (_fileService.VideoExtensions.Any(x => x == extension))
                attachmentType = EAttachmentType.Video;

            try
            {
                Model.Attachment = await _questionService.UpdateAttachment(Model, stream, fileName, attachmentType);
                Model.AttachmentType = attachmentType;
            }
            catch (Exception e)
            {
                // TODO
            }
        }

        protected override async Task OnModelChangedAsync()
        {
            await GetAttachmentFromDbAsync();

            if (Model == null)
                AnswersViewModel.Models = null;
            else
            {
                if (Model.Answers == null)
                    Model.Answers = new ObservableCollection<Answer.Answer>();

                AnswersViewModel.Models = Model.Answers;
            }
        }

        private void OnAnyAnswerChanged(object sender, EventArgs e)
        {
            var _ = OnModelPropertyChangedAsync(Model, nameof(Question.Answers));
        }

        protected override async Task OnModelPropertyChangedAsync(Question item, string propertyName)
        {
            if (item == null)
                return;

            switch (propertyName)
            {
                case nameof(Question.Task):
                    await _questionService.UpdateAsync(item);
                    break;
                case nameof(Question.QuestionType):
                    await _questionService.UpdateAsync(item);
                    break;
                case nameof(Question.Answers):
                    await _questionService.UpdateAsync(item);
                    break;
                case nameof(Question.Tip):
                    await _questionService.UpdateAsync(item);
                    break;
                case nameof(Question.Attachment):
                    await GetAttachmentFromDbAsync();
                    break;
            }
        }

        #endregion METHODS
    }
}