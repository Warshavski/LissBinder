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
                var identity = User.Identity as ClaimsIdentity;
                var pharmacyId = int.Parse(identity.Claims.FirstOrDefault(x => x.Type == "id").Value);
                return pharmacyId;
            }
        }
    }
}
