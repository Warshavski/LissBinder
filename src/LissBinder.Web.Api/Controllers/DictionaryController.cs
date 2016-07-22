using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Escyug.LissBinder.Web.Models.Drugs;
using Escyug.LissBinder.Web.Models.Repositories;
using System.Threading.Tasks;

namespace Escyug.LissBinder.Web.Api.Controllers
{
    [Authorize]
    public class DictionaryController : ApiController
    {
        private readonly IDictionaryDrugsRepository _dictionaryRepository;

        public DictionaryController(IDictionaryDrugsRepository dictionaryRepository)
        {
            _dictionaryRepository = dictionaryRepository;
        }


        /**
         * GET: api/dictionary/{name}
         * 
         * Returns rls dictionary
         */
        [Route("api/dictionary/{name}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAsync(string name)
        {
            var dictionary = await _dictionaryRepository.GetDrugsByNameAsync(name);
            if (dictionary != null)
            {
                return Ok(dictionary);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
