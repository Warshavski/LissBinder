using Escyug.LissBinder.Models.Services.Exceptions;
using Escyug.LissBinder.Models.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Escyug.LissBinder.Models.Services
{
    public class LoginService : ILoginService
    {
        private readonly string _apiUri;

        public LoginService(string apiUri)
        {
            _apiUri = apiUri;
        }

        public async Task<ServiceToken> SignInAsync(string login, string password)
        {
            //*** better create hash on client side and then create one more on server

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
                    client.BaseAddress = new Uri(_apiUri);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(responseHeader));

                    var wat = await client.PostAsync(responseAddress, new FormUrlEncodedContent(form));
                    if (wat.IsSuccessStatusCode)
                    {
                        var token = await wat.Content.ReadAsAsync<ServiceToken>();
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

    }
}
