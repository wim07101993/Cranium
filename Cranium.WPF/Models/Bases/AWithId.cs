﻿using MongoDB.Bson;
using Prism.Mvvm;

namespace Cranium.WPF.Models.Bases
{
    public abstract class AWithId : BindableBase, IWithId
    {
        private ObjectId _id;

        public ObjectId Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }
    }
}