using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Escyug.LissBinder.Models;
using Escyug.LissBinder.Models.Repositories;
using System.Threading.Tasks;

namespace Escyug.LissBinder.Web.Api.Controllers
{
    public class UserController : ApiController
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        //*** create view model for post
        [Route("api/user")]
        [HttpPost]
        public async Task<User> PostAsync([FromBody]string login)
        {
            var user = await _userRepository.GetUserByCredentialsAsync(login, "");

            return user;
        }

        //*** should be only post
        [Route("api/user/{login}/{password}")]
        [HttpGet]
        public async Task<User> GetAsync(string login, string password)
        {
            var user = await _userRepository.GetUserByCredentialsAsync(login, password);

            return user;
        }
    }
}
