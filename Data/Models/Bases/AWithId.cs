using Prism.Mvvm;

namespace Data.Models.Bases
{
    public abstract class AWithId : BindableBase, IWithId
    {
        private long _id;

        public long Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }
    }
}
