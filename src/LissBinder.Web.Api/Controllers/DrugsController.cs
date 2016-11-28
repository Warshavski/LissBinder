using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

using Escyug.LissBinder.Web.Models.Drugs;
using Escyug.LissBinder.Web.Models.Repositories;

namespace Escyug.LissBinder.Web.Api.Controllers
{
    [Authorize]
    public class DrugsController : AuthController
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
        [Route("api/drugs/{name}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAsync(string name)
        {
            //*** get it from context principal
            var pharmacyId = base.PharmacyClaim;
            
            try
            {
                var drugsList = await _pharmacyDrugsRepository.FindByNameAsync(pharmacyId, name);
                return Ok(drugsList);
            }
            catch(ArgumentNullException)
            {
                return NotFound();
            }
        }


        /**
         * POST: api/drugs/{list of drugs}
         * 
         * Add list of drugs to the storage
         */
        [Route("api/drugs/")]
        [HttpPost]
        public async Task<IHttpActionResult> PostAsync([FromBody]IEnumerable<PharmacyDrug> pharmacyDrugs)
        {
            //*** get it from context principal
            var pharmacyId = base.PharmacyClaim;

            var rowsCopied =
                await _pharmacyDrugsRepository.ImportAsync(pharmacyId, pharmacyDrugs);
            if (rowsCopied != -1)
            {
                return Ok(rowsCopied);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
