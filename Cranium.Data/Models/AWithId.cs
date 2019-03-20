using System;
using Prism.Mvvm;
using Shared;

namespace Cranium.Data.Models
{
    public class AWithId : BindableBase, IWithId
    {
        private Guid _id;

        public Guid Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }
    }
}
