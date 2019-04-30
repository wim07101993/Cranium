namespace Cranium.Data.Models
{
    public class TaskType : AWithId
    {
        #region FIELDS

        private string _name;
        private string _explanation;
        private Category _category;
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

        public Category Category
        {
            get => _category;
            set => SetProperty(ref _category, value);
        }

        #endregion PROPERTIES
    }
}
