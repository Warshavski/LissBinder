using System.Threading.Tasks;
using System.Web.Http;

using Escyug.LissBinder.Models;
using Escyug.LissBinder.Models.Repositories;

using Escyug.LissBinder.Web.Api.ViewModels;

namespace Escyug.LissBinder.Web.Api.Controllers
{
    public class UserController : ApiController
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        /**
         * POST api/user/{user credentials}
         * 
         * Validate user
         */
        [Route("api/user")]
        [HttpPost]
        public async Task<IHttpActionResult> PostAsync([FromBody]UserCredentials credentials)
        {
            var login = credentials.Login;
            var password = credentials.Password;

            var user = await _userRepository.GetUserByCredentialsAsync(login, password);
            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
