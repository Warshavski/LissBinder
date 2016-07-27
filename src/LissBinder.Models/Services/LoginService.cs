using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Escyug.LissBinder.Models.Services.Exceptions;
using Escyug.LissBinder.Models.Utils;
using Escyug.LissBinder.Models.Services.Common;

namespace Escyug.LissBinder.Models.Services
{
    public class LoginService : ILoginService
    {
        private readonly ApiContext _apiContext;

        public LoginService(ApiContext apiContext)
        {
            _apiContext = apiContext;
        }

        public async Task<User> SignInAsync(string login, string password)
        {
            User user = null;

            var token = await GetTokenAsync(login, password);
            
            if (token != null)
            {
                user = await GetUserInfoAsync(token);
            }

            return user;
        }

        private async Task<ServiceToken> GetTokenAsync(string login, string password)
        {
            try
            {
                var form = new Dictionary<string, string>  
               {  
                   {"grant_type", "password"},  
                   {"username", login},  
                   {"password", password},  
               };

                var responseAddress = "/api/account/auth";
                var responseHeader = "application/x-www-form-urlencoded";
                //var token = 
                //    await HttpHelper.PostEntityAsync<ServiceToken, Dictionary<string, string>>(_apiUri, responseAddress, responseHeader, form);

                //return token;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_apiContext.ApiUri);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(responseHeader));

                    var wat = await client.PostAsync(responseAddress, new FormUrlEncodedContent(form));
                    if (wat.IsSuccessStatusCode)
                    {
                        var token = await wat.Content.ReadAsAsync<ServiceToken>();

                        _apiContext.Token = token;

                        return token;
                    }
                    else
                    {
                        return null;
                    }
                }

                //var tokenResponse = client.PostAsync(baseAddress + "accesstoken", new FormUrlEncodedContent(form)).Result;  

            }
            catch (HttpRequestException ex)
            {
                //*** write to the log file
                throw new ServiceException(ex.Message);
            }
        }

        private async Task<User> GetUserInfoAsync(ServiceToken token)
        {
            try
            {
                var responseAddress = "/api/account/info";
                var responseHeader = "application/json";

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_apiContext.ApiUri);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(responseHeader));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

                    var userInfo = await client.GetAsync(responseAddress);
                    if (userInfo.IsSuccessStatusCode)
                    {
                        var user = await userInfo.Content.ReadAsAsync<User>();

                        return user;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                //*** write to the log file
                throw new ServiceException(ex.Message);
            }
        }

    }
}
