using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;

using Escyug.LissBinder.Models.Drugs;
using Escyug.LissBinder.Models.Services.Exceptions;
using Escyug.LissBinder.Models.Utils;

namespace Escyug.LissBinder.Models.Services
{
    public sealed class PharmacyService : IPharmacyService
    {
        private readonly string _apiUri;

        public PharmacyService(string apiUri)
        {
            _apiUri = apiUri;
        }

        public async Task<IEnumerable<PharmacyDrug>> GetDrugsAsync(string drugName)
        {
            var responseAddress = "api/drugs/1/" + drugName;
            try
            {
                var pharmacyDrugsList =
                    await HttpHelper.GetEntityAsync<List<PharmacyDrug>>(_apiUri, responseAddress);

                return pharmacyDrugsList;
            }
            catch (HttpRequestException ex)
            {
                //*** write to the log file
                throw new ServiceException(ex.Message);
            }
        }

        //*** create binding presenter
        public async Task<bool> BindPharmacyDrugAsync(PharmacyDrug pharmacyDrug, DictionaryDrug dictionaryDrug, int phamacyId)
        {
            var binding = new Binding(pharmacyDrug, dictionaryDrug,phamacyId);

            try
            {
                var responseAddress = "api/bind";
                var isBindSuccessful = await HttpHelper.PostEntityAsync<bool, Binding>(_apiUri, responseAddress, binding);

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
