﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Escyug.LissBinder.Models.Drugs;
using Escyug.LissBinder.Models.Repositories;
using System.Threading.Tasks;

namespace Escyug.LissBinder.Web.Api.Controllers
{
    public class DictionaryController : ApiController
    {
        private readonly IDictionaryDrugsRepository _dictionaryRepository;

        public DictionaryController(IDictionaryDrugsRepository dictionaryRepository)
        {
            _dictionaryRepository = dictionaryRepository;
        }

        [Route("api/dictionary/{name}")]
        [HttpGet]
        public async Task<IEnumerable<DictionaryDrug>> GetAsync(string name)
        {
            var dictionary = await _dictionaryRepository.GetDrugsByNameAsync(name);
            return dictionary;
        }
    }
}
