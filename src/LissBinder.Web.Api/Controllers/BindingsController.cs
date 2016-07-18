using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Escyug.LissBinder.Web.Models.Repositories;
using System.Threading.Tasks;

namespace Escyug.LissBinder.Web.Api.Controllers
{
    public class BindingsController : ApiController
    {
        private readonly IPharmacyDrugsRepository _pharmacyDrugRepository;

        public BindingsController(IPharmacyDrugsRepository pharmacyDrugRepository)
        {
            _pharmacyDrugRepository = pharmacyDrugRepository;
        }

        /**
         * POST: api/bind
         * 
         * Add binding to storage
         * 
         */
        [Route("api/bind")]
        [HttpPost]
        public async Task<IHttpActionResult>PostAsync([FromBody]Models.Binding binding)
        {
            var isAdded = await _pharmacyDrugRepository.AddBindingAsync(binding);

            if (isAdded)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
