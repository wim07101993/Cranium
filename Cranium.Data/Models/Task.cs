using System.Collections.ObjectModel;

namespace Cranium.Data.Models
{
    public class Task : AWithId
    {
        #region FIELDS

        private string _value;
        private string _tip;
        private TaskType _taskType;
        private ObservableCollection<Attachment> _attachments;
        private ObservableCollection<Answer> _answers;

        #endregion FIELDS


        #region PROPERTIES

        public string Value
        {
            get => _value;
            set => SetProperty(ref _value, value);
        }

        public string Tip
        {
            get => _tip;
            set => SetProperty(ref _tip, value);
        }

        public TaskType TaskType
        {
            get => _taskType;
            set => SetProperty(ref _taskType, value);
        }

        public ObservableCollection<Attachment> Attachments
        {
            get => _attachments;
            internal set => SetProperty(ref _attachments, value);
        }

        public ObservableCollection<Answer> Answers
        {
            get => _answers;
            internal set => SetProperty(ref _answers, value);
        }

        #endregion PROPERTIES
    }
}
