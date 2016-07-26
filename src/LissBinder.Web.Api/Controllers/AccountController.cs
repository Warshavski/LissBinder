using System.Threading.Tasks;
using System.Web.Http;

using Microsoft.AspNet.Identity;

using Escyug.LissBinder.Web.Api.ViewModels;
using Escyug.LissBinder.Web.Models;
using System;

namespace Escyug.LissBinder.Web.Api.Controllers
{
    [Authorize]
    public class AccountController : AuthController
    {
        private readonly UserManager<User> _userManager;

        public AccountController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }


        //---------------------------------------------------------------------


        #region Public(interface) api methods

        /**
         * POST: api/Account/Register
         * 
         * Creates and adds new user
         */
        [AllowAnonymous]
        [Route("api/account/register")]
        [HttpPost]
        public async Task<IHttpActionResult> Register(UserModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new User(userModel.UserName, userModel.NameDescription, userModel.PharmacyId);

            IdentityResult result = await _userManager.CreateAsync(user, userModel.Password);

            IHttpActionResult errorResult = GetErrorResult(result);

            if (errorResult != null)
            {
                return errorResult;
            }

            return Ok();
        }

        /**
         * GET: api/account/{name}
         * 
         * Get ueser account info
         */
        [Route("api/account/info")]
        [HttpGet]
        public async Task<IHttpActionResult> Info()
        {
            try
            {
                var user = await _userManager.FindByNameAsync(base.UserName);

                var userInfo = new UserInfo(user);

                return Ok(userInfo);
            }
            catch (ArgumentNullException)
            {
                return NotFound();
            }
        }

        #endregion Public(interface) api methods


        //---------------------------------------------------------------------


        #region Helper methods

        /// <summary>
        /// Creates error description from...
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }

        #endregion Helper methods
    }
}
