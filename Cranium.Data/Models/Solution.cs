using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Cranium.Data.Models
{
    public class Solution : AWithId
    {
        #region FIELDS

        private bool _isCorrect;
        private string _value;
        private string _info;

        #endregion FIELDS


        #region CONSTRUCTOR

        public Solution() : this(null)
        {
        }

        public Solution(IEnumerable<Attachment> attachments)
        {
            Attachments = attachments == null 
                ? new ObservableCollection<Attachment>()
                : new ObservableCollection<Attachment>(attachments);
        }

        #endregion CONSTRCUTOR


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

        public ObservableCollection<Attachment> Attachments { get; }

        #endregion PROPERTIES
    }
}
