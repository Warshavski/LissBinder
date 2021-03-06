﻿using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;

using Escyug.LissBinder.Models.Drugs;
using Escyug.LissBinder.Models.Services.Exceptions;
using Escyug.LissBinder.Models.Utils;
using Escyug.LissBinder.Models.Services.Common;

namespace Escyug.LissBinder.Models.Services
{
    public sealed class PharmacyService : IPharmacyService
    {
        private readonly ApiContext _apiContext;

        public PharmacyService(ApiContext apiContext)
        {
            _apiContext = apiContext;
        }

        public async Task<IEnumerable<PharmacyDrug>> GetDrugsAsync(string drugName)
        {
            var responseAddress = "api/drugs/" + drugName;

            var accessToken = _apiContext.Token.AccessToken;

            try
            {
                var pharmacyDrugsList =
                    await HttpHelper.GetEntityAsync<List<PharmacyDrug>>(_apiContext.ApiUri, responseAddress, accessToken);

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
                var isBindSuccessful = await HttpHelper.PostEntityAsync<bool, Binding>(_apiContext.ApiUri, responseAddress, binding);

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
