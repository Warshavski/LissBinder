using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Escyug.LissBinder.Models.Drugs;
using Escyug.LissBinder.Models.Repositories;


namespace Escyug.LissBinder.Web.Api.Controllers
{
    public class DrugsController : ApiController
    {
        private readonly IPharmacyDrugsRepository _pharmacyDrugsRepository;

        public DrugsController(IPharmacyDrugsRepository pharmacyDrugsRepository)
        {
            _pharmacyDrugsRepository = pharmacyDrugsRepository;
        }

        [Route("api/drugs/{pharmacyID}/{name}")]
        [HttpGet]
        public async Task<IEnumerable<PharmacyDrug>> GetAsync(int pharmacyId, string name)
        {
            var drugsList = await _pharmacyDrugsRepository.GetDrugsByNameAsync(name, pharmacyId);
            return drugsList;
        }
    }
}
