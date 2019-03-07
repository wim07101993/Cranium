using System.Threading.Tasks;
using System.Windows.Media;
using Cranium.WPF.Data.Answer;
using Cranium.WPF.Data.Files;
using Cranium.WPF.Helpers.Extensions;
using Cranium.WPF.Helpers.ViewModels;
using Cranium.WPF.Strings;

namespace Cranium.WPF.Game.Question
{
    public class AnswerViewModel : AModelContainerViewModel<Data.Answer.Answer>
    {
        #region FIELDS

        private readonly IFileService _fileService;

        private byte[] _attachment;

        #endregion FIELDS


        #region CONSTRCUTOR

        public AnswerViewModel(IStringsProvider stringsProvider, IFileService fileService) 
            : base(stringsProvider)
        {
            _fileService = fileService;
        }

        #endregion CONSTRUCTOR


        #region PROPERTIES

        public ImageSource ImageAttachment => Attachmnet?.ToImage();

        public byte[] Attachmnet
        {
            get => _attachment;
            set
            {
                if (SetProperty(ref _attachment, value))
                    return ;
                RaisePropertyChanged(nameof(ImageAttachment));
            }
        }

        #endregion PROPERTIES


        #region METHODS

        protected override async Task OnModelPropertyChangedAsync(Answer model, string propertyName)
        {
            await base.OnModelPropertyChangedAsync(model, propertyName);

            switch (propertyName)
            {
                case nameof(Answer.AttachmentType):
                    await UpdateAttachmentAsync();
                    break;
            }
        }

        private async Task UpdateAttachmentAsync()
        {
            if (Model.Attachment != default)
                Attachmnet = await _fileService.GetOneAsync(Model.Attachment);
        }

        #endregion METHODS
    }
}
