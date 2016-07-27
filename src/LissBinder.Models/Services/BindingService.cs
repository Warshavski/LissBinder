using System.Net.Http;
using System.Threading.Tasks;

using Escyug.LissBinder.Models.Drugs;
using Escyug.LissBinder.Models.Services.Common;
using Escyug.LissBinder.Models.Services.Exceptions;
using Escyug.LissBinder.Models.Utils;

namespace Escyug.LissBinder.Models.Services
{
    public class BindingService : IBindingService
    {
        private readonly string _apiUri;

        public BindingService(string apiUri)
        {
            _apiUri = apiUri;
        }

        public async Task<bool> BindAsync(Binding binding)
        {
            return await CallWebApiAsync(binding);
        }

        public async Task<bool> BindAsync(PharmacyDrug pharmacyDrug, DictionaryDrug dictionaryDrug)
        {
            var binding = new Binding(pharmacyDrug, dictionaryDrug);

            return await CallWebApiAsync(binding);
        }

        private async Task<bool> CallWebApiAsync(Binding binding)
        {
            try
            {
                var responseAddress = "api/bind";
                var token = ApiContext.Token.AccessToken;

                var isBindSuccessful = await HttpHelper.PostEntityAsync<bool, Binding>(
                    _apiUri, responseAddress, token, binding);

                return isBindSuccessful;
            }
            catch (HttpRequestException ex)
            {
                //*** write to the log file
                throw new ServiceException(ex.Message);
            }
        }
    }
}
