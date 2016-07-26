using System.Net.Http;
using System.Threading;
using System.Web.Http;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

using Escyug.LissBinder.Web.Models;
using Escyug.LissBinder.Web.Models.Services;
using System.Threading.Tasks;

namespace Escyug.LissBinder.Web.Api.Controllers
{
    public class BaseController : ApiController
    {
        //private User _member;

        //public UserManager<User> UserManager
        //{
        //    get { return Request.GetOwinContext().GetUserManager<UserService>(); }
        //}

        //public async Task<string> UserIdentityId
        //{
        //    get
        //    {
        //        var user = await UserManager.FindByName(User.Identity.Name);
        //        return user.Id;
        //    }
        //}

        //public User UserRecord
        //{
        //    get
        //    {
        //        if (_member != null)
        //        {
        //            return _member;
        //        }
        //        _member = UserManager.FindByName(Thread.CurrentPrincipal.Identity.Name);
        //        return _member;
        //    }
        //    set { _member = value; }
        //}

        //protected IHttpActionResult GetErrorResult(IdentityResult result)
        //{
        //    if (result == null)
        //    {
        //        return InternalServerError();
        //    }

        //    if (!result.Succeeded)
        //    {
        //        if (result.Errors != null)
        //        {
        //            foreach (string error in result.Errors)
        //            {
        //                ModelState.AddModelError("", error);
        //            }
        //        }

        //        if (ModelState.IsValid)
        //        {
        //            // No ModelState errors are available to send, so just return an empty BadRequest.
        //            return BadRequest();
        //        }

        //        return BadRequest(ModelState);
        //    }

        //    return null;
        //}
    }
}
