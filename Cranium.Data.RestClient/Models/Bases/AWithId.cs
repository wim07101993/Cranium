using System;
using Prism.Mvvm;

namespace Cranium.Data.RestClient.Models.Bases
{
    public abstract class AWithId : BindableBase, IWithId
    {
        private Guid _id;

        public Guid Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }
    }
}