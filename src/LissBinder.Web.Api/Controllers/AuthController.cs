using System.Linq;
using System.Security.Claims;
using System.Web.Http;

namespace Escyug.LissBinder.Web.Api.Controllers
{
    [Authorize]
    public class AuthController : ApiController
    {
        protected int PharmacyClaim
        {
            get 
            {
                var pharmacyId = int.Parse(GetClaims().Claims.FirstOrDefault(x => x.Type == "id").Value);
                return pharmacyId;
            }
        }

        protected string UserName
        {
            get
            {
                var userName = GetClaims().Claims.FirstOrDefault(x => x.Type == "sub").Value;
                return userName;
            }
        }

        private ClaimsIdentity GetClaims()
        { 
            return User.Identity as ClaimsIdentity;
        }
    }
}
