
namespace Escyug.LissBinder.Models.Services.Common
{
    //*** too bad design : remove static
    public class ApiContext
    {
        public string ApiUri { get; private set; }
        public ServiceToken Token { get; set; }

        public ApiContext(string apiUri)
        {
            ApiUri = apiUri;
        }
    }
}
