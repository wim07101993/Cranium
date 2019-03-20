using System.Collections.ObjectModel;

namespace Cranium.Data.Models
{
    public class Tile : AWithId
    {
        #region FIELDS

        private Category _category;
        private ObservableCollection<Team> _teams;

        #endregion FIELDS


        #region PROPERTIES

        public Category Category
        {
            get => _category;
            set => SetProperty(ref _category, value);
        }

        public ObservableCollection<Team> Teams
        {
            get => _teams;
            internal set => SetProperty(ref _teams, value);
        }

        #endregion PROPERTIES
    }
}