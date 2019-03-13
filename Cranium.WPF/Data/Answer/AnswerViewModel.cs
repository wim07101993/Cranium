using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Cranium.WPF.Data.Attachment;
using Cranium.WPF.Helpers.ViewModels;
using Cranium.WPF.Strings;
using Microsoft.Win32;
using Prism.Commands;

namespace Cranium.WPF.Data.Answer
{
    public class AnswerViewModel : AModelContainerViewModel<Answer>
    {
        private readonly IAttachmentService _fileService;


        public AnswerViewModel(IStringsProvider stringsProvider, IAttachmentService fileService)
            : base(stringsProvider)
        {
            _fileService = fileService;
            PickFileCommand = new DelegateCommand(async () => await PickFileAsync());
        }


        public ICommand PickFileCommand { get; }


        public async Task PickFileAsync()
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

            try
            {
                if (Model.Attachment != default)
                    await _fileService.RemoveAsync(Model.Attachment);
            }
            catch (Exception e)
            {
                // TODO
            }

            var stream = File.OpenRead(dialog.FileName);
            var fileName = Path.GetFileName(dialog.FileName);

            var fileId = await _fileService.CreateAsync(stream, fileName);
            Model.Attachment = fileId;

            var extension = Path.GetExtension(fileName);
            if (_fileService.ImageExtensions.Any(x => x == extension))
                Model.AttachmentType = EAttachmentType.Image;
            else if (_fileService.MusicExtensions.Any(x => x == extension))
                Model.AttachmentType = EAttachmentType.Music;
            else if (_fileService.VideoExtensions.Any(x => x == extension))
                Model.AttachmentType = EAttachmentType.Video;
        }
    }
}