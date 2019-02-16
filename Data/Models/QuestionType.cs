using Data.Models.Bases;

namespace Data.Models
{
    public class QuestionType : AWithId
    {
        #region FIELDS

        private string _name;
        private string _explanation;
        private bool _hasMultipleAnswers;

        #endregion FIELDS


        #region PROPERTIES

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public string Explanation
        {
            get => _explanation;
            set => SetProperty(ref _explanation, value);
        }

        public bool HasMultipleAnswers
        {
            get => _hasMultipleAnswers;
            set => SetProperty(ref _hasMultipleAnswers, value);
        }

        #endregion PROPERTIES
    }
}