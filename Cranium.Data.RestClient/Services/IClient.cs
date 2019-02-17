using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cranium.Data.RestClient.Models.Bases;

namespace Cranium.Data.RestClient.Services
{
    public interface IClient
    {
        #region generic

        Task<IList<T>> GetAsync<T>(int skip = 0, int take = -1) where T : class, IWithId;
        Task<T> GetAsync<T>(Guid id) where T : class, IWithId;

        Task<T> CreateAsync<T>(T t) where T : class, IWithId;
        Task<T> UpdateAsync<T>(T t) where T : class, IWithId;
        Task<Guid> DeleteAsync<T>(Guid id) where T : class, IWithId;

        #endregion generic
    }
}