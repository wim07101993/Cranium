using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using Cranium.WPF.Data.Answer;
using Cranium.WPF.Data.Files;
using Cranium.WPF.Helpers.Extensions;
using Cranium.WPF.Helpers.ViewModels;
using Cranium.WPF.Strings;
using Prism.Commands;

namespace Cranium.WPF.Game.Question
{
    public class AnswerViewModel : AModelContainerViewModel<Answer>
    {
        #region FIELDS

        private static readonly Random Random = new Random();

        private static readonly string AppDataDir =
            $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}/{Assembly.GetCallingAssembly().GetName()}";

        private readonly IFileService _fileService;
        private readonly MediaPlayer _mediaPlayer = new MediaPlayer();

        private string _musicAttachment;
        private string _videoAttachment;
        private ImageSource _imageAttachment;

        #endregion FIELDS


        #region CONSTRCUTOR

        public AnswerViewModel(IStringsProvider stringsProvider, IFileService fileService)
            : base(stringsProvider)
        {
            _fileService = fileService;

            PlayCommand = new DelegateCommand(Play);
            PauseCommand = new DelegateCommand(Pause);
            StopCommand = new DelegateCommand(Stop);
        }

        #endregion CONSTRUCTOR


        #region PROPERTIES

        public ImageSource ImageAttachment
        {
            get => _imageAttachment;
            set => SetProperty(ref _imageAttachment, value);
        }

        public string MusicAttachment
        {
            get => _musicAttachment;
            set => SetProperty(ref _musicAttachment, value);
        }

        public string VideoAttachment
        {
            get => _videoAttachment;
            set => SetProperty(ref _videoAttachment, value);
        }

        public ICommand PlayCommand { get; }
        public ICommand PauseCommand { get; }
        public ICommand StopCommand { get; }

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
            switch (Model.AttachmentType)
            {
                case EAttachmentType.None:
                    break;
                case EAttachmentType.Image:
                    await UpdateImageAttachmentAsync();
                    break;
                case EAttachmentType.Music:
                    await UpdateMusicAttachmentAsync();
                    break;
                case EAttachmentType.Video:
                    await UpdateVideoAttachmentAsync();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private async Task UpdateImageAttachmentAsync()
        {
            try
            {
                var bytes = await _fileService.GetOneAsync(Model.Attachment);
                ImageAttachment = bytes.ToImage();
            }
            catch (Exception e)
            {
                // TODO
            }
        }

        private async Task UpdateMusicAttachmentAsync()
        {
            try
            {
                var bytes = await _fileService.GetOneAsync(Model.Attachment);
                var path = $"{AppDataDir}/{Random.Next()}";
                var file = File.Open(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                file.Write(bytes, 0, bytes.Length);
                MusicAttachment = path;
            }
            catch (Exception e)
            {
                // TODO
            }
        }

        private async Task UpdateVideoAttachmentAsync()
        {
            try
            {
                var bytes = await _fileService.GetOneAsync(Model.Attachment);
                var path = $"{AppDataDir}/{Random.Next()}";
                var file = File.Open(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                file.Write(bytes, 0, bytes.Length);
                VideoAttachment = path;
            }
            catch (Exception e)
            {
                // TODO
            }
        }

        public void Play() => _mediaPlayer.Play();
        public void Pause() => _mediaPlayer.Pause();
        public void Stop() => _mediaPlayer.Stop();

        #endregion METHODS
    }
}