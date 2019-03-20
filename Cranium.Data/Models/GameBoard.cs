using Shared;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Cranium.Data.Models
{
    public class GameBoard : ObservableCollection<Tile>, IWithId, INotifyPropertyChanged
    {
        private Guid _id;

        public Guid Id
        {
            get => _id;
            set
            {
                if (Equals(_id, value))
                    return;

                _id = value;

                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Id)));
            }
        }
    }
}
