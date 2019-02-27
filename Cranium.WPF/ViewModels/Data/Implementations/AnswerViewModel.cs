using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
    public class AnswerViewModel : AViewModelBase, IAnswerViewModel
    {
        private static readonly IReadOnlyList<string> ImageExtensions = new[] {".bmp", ".jpg", ".gif", ".png"};
        private static readonly IReadOnlyList<string> MusicExtensions = new[] {".mp3", ".m4a", ".wma"};
        private static readonly IReadOnlyList<string> VideoExtensions = new[] {".mp4", ".wmv", ".webm"};

        private readonly IFileService _fileService;

        private Answer _model;


        public AnswerViewModel(IStringsProvider stringsProvider, IFileService fileService)
            : base(stringsProvider)
        {
            _fileService = fileService;
            PickFileCommand = new DelegateCommand(async () => await PickFileAsync());
        }


        public Answer Model
        {
            get => _model;
            set => SetProperty(ref _model, value);
        }

        public ICommand PickFileCommand { get; }


        public async Task PickFileAsync()
        {
            var dialog = new OpenFileDialog
            {
                Multiselect = false,
                Title = Strings.SelectAnImageFile,
                Filter = $"{GenerateImageFilter()}|" +
                         $"{GenerateMusicFilter()}|" +
                         $"{GenerateVideoFilter()}"
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
            if (ImageExtensions.Any(x => x == extension))
                Model.AttachmentType = EAttachmentType.Image;
            else if (MusicExtensions.Any(x => x == extension))
                Model.AttachmentType = EAttachmentType.Music;
            else if (VideoExtensions.Any(x => x == extension))
                Model.AttachmentType = EAttachmentType.Video;
        }

        private string GenerateImageFilter()
        {
            var imageExtensions = new StringBuilder("Image files (");

            foreach (var imageExtension in ImageExtensions)
                imageExtensions.Append($"*{imageExtension};");

            imageExtensions.Append(")|");

            foreach (var imageExtension in ImageExtensions)
                imageExtensions.Append($"*{imageExtension};");

            return imageExtensions.ToString();
        }

        private string GenerateMusicFilter()
        {
            var musicExtensions = new StringBuilder("Music files (");

            foreach (var musicExtension in MusicExtensions)
                musicExtensions.Append($"*{musicExtension};");

            musicExtensions.Append(")|");

            foreach (var musicExtension in MusicExtensions)
                musicExtensions.Append($"*{musicExtension};");

            return musicExtensions.ToString();
        }

        private string GenerateVideoFilter()
        {
            var videoExtensions = new StringBuilder("Video files (");

            foreach (var videoExtension in VideoExtensions)
                videoExtensions.Append($"*{videoExtension};");

            videoExtensions.Append(")|");

            foreach (var videoExtension in VideoExtensions)
                videoExtensions.Append($"*{videoExtension};");

            return videoExtensions.ToString();
        }
    }
}