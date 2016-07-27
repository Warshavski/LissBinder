using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using Escyug.LissBinder.Models.Drugs;
using Escyug.LissBinder.Models.Services.Exceptions;
using Escyug.LissBinder.Models.Utils;
using Escyug.LissBinder.Models.Services.Common;

namespace Escyug.LissBinder.Models.Services
{
    public sealed class DictionaryService : IDictionaryService
    {
        private readonly ApiContext _apiContext;

        public DictionaryService(ApiContext apiContext)
        {
            _apiContext = apiContext;
        }

        public async Task<IEnumerable<DictionaryDrug>> GetDrugsAsync(string drugName)
        {
            var responseAddress = "api/dictionary/" + drugName;
            var accessToken = _apiContext.Token.AccessToken;

            try
            {
                var dictionaryDrugsList =
                    await HttpHelper.GetEntityAsync<IEnumerable<DictionaryDrug>>(_apiContext.ApiUri, responseAddress, accessToken);

                return dictionaryDrugsList;
            }
            catch (HttpRequestException ex)
            {
                throw new ServiceException(ex.Message);
            }
        }

    }
}
