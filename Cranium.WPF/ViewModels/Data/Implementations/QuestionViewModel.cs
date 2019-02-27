﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using Unity;

namespace Cranium.WPF.ViewModels.Data.Implementations
{
    public class QuestionViewModel : AViewModelBase, IQuestionViewModel
    {
        #region FIELDS

        private static readonly IReadOnlyList<string> ImageExtensions = new[] { ".bmp", ".jpg", ".gif", ".png" };
        private static readonly IReadOnlyList<string> MusicExtensions = new[] { ".mp3", ".m4a", ".wma" };
        private static readonly IReadOnlyList<string> VideoExtensions = new[] { ".mp4", ".wmv", ".webm" };

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
                case nameof(Question.Attachment):
                    await _questionService.UpdatePropertyAsync(item.Id, x => x.Attachment, item.Attachment);
                    await _questionService.UpdatePropertyAsync(item.Id, x => x.AttachmentType, item.AttachmentType);
                    break;
            }
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

        #endregion METHODS
    }
}