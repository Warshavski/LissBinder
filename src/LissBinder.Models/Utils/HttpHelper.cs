using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Escyug.LissBinder.Models.Utils
{
    public static class HttpHelper
    {
        private const string DEFAULT_MEDIA_TYPE_HEADER = "application/json";

        public static async Task<TEntity> GetEntityAsync<TEntity>(string apiUri, string responseAddress)
        {
            return await GetEntityAsync<TEntity>(apiUri, responseAddress, DEFAULT_MEDIA_TYPE_HEADER);
        }

        public static async Task<TEntity> GetEntityAsync<TEntity>(string apiUri, 
            string responseAddress, string mediaTypeHeader)
        {
            // async wep api method call
            using (var client = new HttpClient())
            {
                // HttpClient setup
                client.BaseAddress = new Uri(apiUri);
                client.DefaultRequestHeaders.Accept.Clear();
                
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaTypeHeader));

                // call web api controller method
                var response = await client.GetAsync(responseAddress);
                if (response.IsSuccessStatusCode)
                {
                    var entity = await response.Content.ReadAsAsync<TEntity>();
                    return entity;
                }
                else
                {
                    return default(TEntity);
                }
            }
        }

         public static async Task<bool> PostEntityAsync<TValue>(string apiUri, string responseAddress,
            TValue postValue)
        {
            return await PostEntityAsync<TValue>(apiUri, responseAddress, DEFAULT_MEDIA_TYPE_HEADER, postValue);
        }

        public static async Task<bool> PostEntityAsync<TValue>(string apiUri, string responseAddress,
            string mediaTypeHeader, TValue postValue)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaTypeHeader));

                var wat = await client.PostAsJsonAsync(responseAddress, postValue);
                if (wat.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
