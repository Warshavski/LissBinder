using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using Escyug.LissBinder.Models.Drugs;
using Escyug.LissBinder.Models.Services.Exceptions;
using Escyug.LissBinder.Models.Utils;

namespace Escyug.LissBinder.Models.Services
{
    public sealed class DictionaryService : IDictionaryService
    {
        private readonly string _apiUri;

        public DictionaryService(string apiUri)
        {
            _apiUri = apiUri;
        }

        public async Task<IEnumerable<DictionaryDrug>> GetDrugsAsync(string drugName)
        {
            var responseAddress = "api/dictionary/" + drugName;
            var accessToken = ApiContext.Token.AccessToken;

            try
            {
                var dictionaryDrugsList =
                    await HttpHelper.GetEntityAsync<IEnumerable<DictionaryDrug>>(_apiUri, responseAddress, accessToken);

                return dictionaryDrugsList;
            }
            catch (HttpRequestException ex)
            {
                throw new ServiceException(ex.Message);
            }
        }

    }
}
