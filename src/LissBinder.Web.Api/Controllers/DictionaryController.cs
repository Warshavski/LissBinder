using System.Web.Http;

using System.Threading.Tasks;

using Escyug.LissBinder.Web.Models.Repositories;
using System;
using System.Text;


namespace Escyug.LissBinder.Web.Api.Controllers
{
    [Authorize]
    public class DictionaryController : ApiController
    {
        private readonly IDictionaryRepository _dictionaryRepository;

        public DictionaryController(IDictionaryRepository dictionaryRepository)
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
            try
            {
                var dictionary = await _dictionaryRepository.FindByNameAsync(name);

                return Ok(dictionary);
            }
            catch (ArgumentNullException)
            {
                return NotFound();
            }
        }
    }
}
