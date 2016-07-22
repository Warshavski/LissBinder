using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Escyug.LissBinder.Models.Utils
{
    public static class HttpHelper
    {
        private const string DEFAULT_MEDIA_TYPE_HEADER = "application/json";

       
        // HTTP GET SECTION
        //---------------------------------------------------------------------

        public static async Task<TEntity> GetEntityAsync<TEntity>(string apiUri, string responseAddress)
        {
            return await GetEntityAsync<TEntity>(apiUri, responseAddress, DEFAULT_MEDIA_TYPE_HEADER);
        }

        public static async Task<TEntity> GetEntityAsync<TEntity>(string apiUri, 
            string responseAddress, string mediaTypeHeader)
        {
            // async wep api method call
            using (var client = CreateHttpClient(apiUri, mediaTypeHeader))
            {
                
                // call web api controller method
                var response = await client.GetAsync(responseAddress);
                var result = await ReadHttpResponseMessageAsync<TEntity>(response);

                return result;
            }
        }


        // HTTP POST SECTION
        //---------------------------------------------------------------------

        public static async Task<TResult> PostEntityAsync<TResult, TValue>(string apiUri, string responseAddress,
            TValue postValue)
        {
            return await PostEntityAsync<TResult, TValue>(apiUri, responseAddress, DEFAULT_MEDIA_TYPE_HEADER, postValue);
        }

        public static async Task<TResult> PostEntityAsync<TResult, TValue>(string apiUri, string requestAddress,
            string mediaTypeHeader, TValue postValue)
        {
            using (var client = CreateHttpClient(apiUri, mediaTypeHeader))
            {
                var postResult = await client.PostAsJsonAsync<TValue>(requestAddress, postValue);
                var result = await ReadHttpResponseMessageAsync<TResult>(postResult);

                return result;
            }
        }

        
        public static async Task<TResult> PostEntityAsJsonAsync<TResult, TValue>(string apiUri, string requestAddress,
            TValue postValue)
        {
            var result = await PostEntityAsJsonAsync<TResult, TValue>(apiUri, requestAddress, DEFAULT_MEDIA_TYPE_HEADER, postValue);
            return result;
        }

        public static async Task<TResult> PostEntityAsJsonAsync<TResult, TValue>(string apiUri, string requestAddress,
            string mediaTypeHeader, TValue postValue)
        {
            using (HttpClient client = CreateHttpClient(apiUri, mediaTypeHeader))
            {
                var postResult = await client.PostAsJsonAsync<TValue>(requestAddress, postValue);
                var result = await ReadHttpResponseMessageAsync<TResult>(postResult);

                return result;
            }
        }


        // COMON HELPER METHODS SECTION
        //---------------------------------------------------------------------

        private static HttpClient CreateHttpClient(string uri, string mediaTypeHeader)
        {
            var httpClient = new HttpClient();

            // HttpClient setup
            httpClient.BaseAddress = new Uri(uri);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaTypeHeader));

            return httpClient;
        }

        private static async Task<TValue> ReadHttpResponseMessageAsync<TValue>(HttpResponseMessage message)
        {
            if (message.IsSuccessStatusCode)
            {
                return await message.Content.ReadAsAsync<TValue>();
            }
            else
            {
                return default(TValue);
            }
        }
    }
}
