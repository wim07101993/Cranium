using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Cranium.WPF.Models;
using Cranium.WPF.Services.Mongo;
using Cranium.WPF.Services.Strings;
using Cranium.WPF.ViewModels.Implementations;
using Microsoft.Win32;
using Prism.Commands;

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

        public QuestionViewModel(IStringsProvider stringsProvider, IFileService fileService, IQuestionService questionService)
            : base(stringsProvider)
        {
            _fileService = fileService;
            _questionService = questionService;
            ChangeAttachmentCommand = new DelegateCommand(async () => await ChangeAttachmentAsync());
            GetAttachmentFromDb();
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

                if (value != null)
                    value.PropertyChanged -= OnQuestionPropertyChanged;

                SetProperty(ref _model, value);

                GetAttachmentFromDb();

                if (value != null)
                    value.PropertyChanged += OnQuestionPropertyChanged;
            }
        }

        public byte[] Attachment
        {
            get => _attachment;
            private set => SetProperty(ref _attachment, value);
        }

        public ICommand ChangeAttachmentCommand { get; }

        #endregion PROPERTIES


        #region METHODS

        public async Task GetAttachmentFromDb()
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
            OnQuestionPropertyChangedAsync(sender as Question, e.PropertyName);
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
                    await _questionService.UpdatePropertyAsync(item.Id, x => x.Answers, item.Answers);
                    break;
                case nameof(Question.Tip):
                    await _questionService.UpdatePropertyAsync(item.Id, x => x.Tip, item.Tip);
                    break;
            }

        }

        #endregion METHODS
    }
}
