using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Cranium.Data.Models
{
    public class Task : AWithId
    {
        #region FIELDS

        private string _value;
        private string _tip;
        private TaskType _taskType;

        #endregion FIELDS


        #region CONSTRUCTOR

        public Task() : this(null, null)
        {
        }

        public Task(IEnumerable<Attachment> attachments, IEnumerable<Solution> solutions)
        {
            Attachments = attachments == null
                ? new ObservableCollection<Attachment>()
                : new ObservableCollection<Attachment>(attachments);

            Solutions = solutions == null 
                ? new ObservableCollection<Solution>()
                : new ObservableCollection<Solution>(solutions);
        }

        #endregion CONSTRCUTOR


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

        public ObservableCollection<Attachment> Attachments { get; }

        public ObservableCollection<Solution> Solutions { get; }

        #endregion PROPERTIES
    }
}
