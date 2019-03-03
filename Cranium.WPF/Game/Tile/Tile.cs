using System.Collections.ObjectModel;
using Cranium.WPF.Helpers;
using MongoDB.Bson;

namespace Cranium.WPF.Game.Tile
{
    public class Tile : AWithId
    {
        public Tile(ObjectId id, ObjectId categoryId)
        {
            Id = id;
            CategoryId = categoryId;
        }


        public ObjectId CategoryId { get; }

        public ObservableCollection<Player.Player> Players { get; } = new ObservableCollection<Player.Player>();
    }
}
