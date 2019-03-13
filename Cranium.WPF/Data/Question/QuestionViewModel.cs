using System;
using System.Collections.ObjectModel;
using System.IO;
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

        private readonly IAttachmentService _attachmentService;
        private readonly IQuestionService _questionService;

        private BitmapImage _imageAttachment;
        private string _attachmentPath;

        #endregion FIELDS


        #region CONSTRUCTOR

        public QuestionViewModel(IUnityContainer unityContainer)
            : base(unityContainer.Resolve<IStringsProvider>())
        {
            _attachmentService = unityContainer.Resolve<IAttachmentService>();
            _questionService = unityContainer.Resolve<IQuestionService>();
            AnswersViewModel = unityContainer.Resolve<AnswersViewModel>();

            ChangeAttachmentCommand = new DelegateCommand(() =>
            {
                var _ = ChangeAttachmentAsync();
            });

            var __ = GetAttachmentAsync();
        }

        #endregion CONSTRUCTOR


        #region PROPERTIES
        
        public BitmapImage ImageAttachment
        {
            get => _imageAttachment;
            private set => SetProperty(ref _imageAttachment, value);
        }   

        public ICommand ChangeAttachmentCommand { get; }

        public AnswersViewModel AnswersViewModel { get; }

        public string AttachmentPath
        {
            get => _attachmentPath;
            set => SetProperty(ref _attachmentPath, value);
        }

        #endregion PROPERTIES


        #region METHODS

        public async Task GetAttachmentAsync()
        {
            if (Model == null)
            {
                ImageAttachment = null;
                return;
            }

            try
            {
                var bytes = await _questionService.GetAttachmentAsync(Model);
                if (Model.AttachmentType == EAttachmentType.Image)
                    ImageAttachment = bytes.ToImage();
            }
            catch (Exception e)
            {
                // TODO
            }
        }

        public async Task ChangeAttachmentAsync()
        {
            await Task.CompletedTask;

            var dialog = new OpenFileDialog
            {
                Multiselect = false,
                Title = Strings.SelectAnImageFile,
                Filter = $"{_attachmentService.GenerateImageFilter()}|" +
                         $"{_attachmentService.GenerateMusicFilter()}|" +
                         $"{_attachmentService.GenerateVideoFilter()}"
            };
            dialog.ShowDialog();

            if (string.IsNullOrWhiteSpace(dialog.FileName))
                return;

            AttachmentPath = dialog.FileName;

            using (var file = File.Open(AttachmentPath, FileMode.Open, FileAccess.Read))
                ImageAttachment = file.ToImage();
        }

        protected override async Task OnModelChangedAsync()
        {
            await GetAttachmentAsync();

            if (Model == null)
                AnswersViewModel.Models = null;
            else
            {
                if (Model.Answers == null)
                    Model.Answers = new ObservableCollection<Answer.Answer>();

                AnswersViewModel.Models = Model.Answers;
            }
        }
        
        #endregion METHODS
    }
}