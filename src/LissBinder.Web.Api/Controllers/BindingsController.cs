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
    [Authorize]
    public class BindingsController : AuthController
    {
        private readonly IBindingsRepository _bindingRepository;

        public BindingsController(IBindingsRepository bindingRepository)
        {
            _bindingRepository = bindingRepository;
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
            //*** get it from context principal
            var pharmacyId = base.PharmacyClaim;

            binding.SetPharmacy(pharmacyId);

            await _bindingRepository.AddBindingAsync(binding);

            return Ok();
        }
    }
}
