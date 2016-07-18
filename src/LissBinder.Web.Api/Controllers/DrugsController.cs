using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Escyug.LissBinder.Web.Models.Drugs;
using Escyug.LissBinder.Web.Models.Repositories;


namespace Escyug.LissBinder.Web.Api.Controllers
{
    public class DrugsController : ApiController
    {
        private readonly IPharmacyDrugsRepository _pharmacyDrugsRepository;

        public DrugsController(IPharmacyDrugsRepository pharmacyDrugsRepository)
        {
            _pharmacyDrugsRepository = pharmacyDrugsRepository;
        }


        /**
         * GET: api/drugs/{pharmacyId}/{name}
         * 
         * Returns pharmacy drugs
         */
        [Route("api/drugs/{pharmacyId}/{name}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAsync(int pharmacyId, string name)
        {
            var drugsList = await _pharmacyDrugsRepository.GetDrugsByNameAsync(name, pharmacyId);
            if (drugsList != null && drugsList.Count() != 0)
            {
                return Ok(drugsList);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
