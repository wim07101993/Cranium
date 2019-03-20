using System.Collections.ObjectModel;

namespace Cranium.Data.Models
{
    public class Answer : AWithId
    {
        #region FIELDS

        private bool _isCorrect;
        private string _value;
        private string _info;
        private ObservableCollection<Attachment> _attachments;

        #endregion FIELDS


        #region PROPERTIES

        public bool IsCorrect
        {
            get => _isCorrect;
            set => SetProperty(ref _isCorrect, value);
        }

        public string Value
        {
            get => _value;
            set => SetProperty(ref _value, value);
        }

        public string Info
        {
            get => _info;
            set => SetProperty(ref _info, value);
        }

        public ObservableCollection<Attachment> Attachments
        {
            get => _attachments;
            internal set => SetProperty(ref _attachments, value);
        }

        #endregion PROPERTIES
    }
}
