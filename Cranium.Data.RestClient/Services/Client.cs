using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Cranium.Data.RestClient.Attributes;
using Cranium.Data.RestClient.Exceptions;
using Cranium.Data.RestClient.Models.Bases;
using Flurl;
using Flurl.Http;
using Shared.Extensions;

namespace Cranium.Data.RestClient.Services
{
    public class Client : IClient
    {
        private readonly IClientSettings _clientSettings;


        public Client(IClientSettings clientSettings)
        {
            _clientSettings = clientSettings;
            // allow all certificates
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, errors) => true;
        }

        #region generic

        public async Task<IList<T>> GetAsync<T>(int skip = 0, int take = -1) where T : class, IWithId
            => await GetUrl<T>()
                .SetQueryParam("skip", skip)
                .SetQueryParam("take", take)
                .GetJsonAsync<List<T>>();

        public async Task<T> GetAsync<T>(Guid id) where T : class, IWithId
            => await GetUrl<T>()
                .AppendPathSegment(id)
                .GetJsonAsync<T>();

        public async Task<T> CreateAsync<T>(T t) where T : class, IWithId
        {
            var response = await GetUrl<T>().PostJsonAsync(t);

            CheckStatusCode(response);

            return await response
                .Content
                .ReadAsStringAsync()
                .DeserializeJsonAsync<T>();
        }

        public async Task<T> UpdateAsync<T>(T t) where T : class, IWithId
        {
            var response = await GetUrl<T>()
                .AppendPathSegment(t.Id)
                .PutJsonAsync(t);

            CheckStatusCode(response);

            return await response
                .Content
                .ReadAsStringAsync()
                .DeserializeJsonAsync<T>();
        }

        public async Task<Guid> DeleteAsync<T>(Guid id) where T : class, IWithId
        {
            var response = await GetUrl<T>()
                .AppendPathSegment(id)
                .DeleteAsync();

            CheckStatusCode(response);

            var content = await response
                .Content
                .ReadAsStringAsync();

            content = content.Remove(content.Length - 1).Remove(0,1);
            return Guid.Parse(content);
        }

        private string GetUrl<T>()
        {
            var attribute = typeof(T).GetCustomAttribute<HasControllerAttribute>();
            if (attribute == null)
                throw new InvalidOperationException($"Cannot get controller from the model {typeof(T).Name}");

            return $"{_clientSettings.HostName}/{attribute.ControllerName}";
        }

        private static void CheckStatusCode(HttpResponseMessage response)
        {
            if ((int)response.StatusCode >= 400 && (int)response.StatusCode < 500)
                throw new Http400Exception(response);
            if ((int)response.StatusCode >= 500 && (int)response.StatusCode < 600)
                throw new Http500Exception(response);
        }

        #endregion generic
    }
}