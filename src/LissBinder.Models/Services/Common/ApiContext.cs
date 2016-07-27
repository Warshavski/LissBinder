
namespace Escyug.LissBinder.Models.Services.Common
{
    //*** too bad design : remove static
    internal static class ApiContext
    {
        public static string ApiUri { get; set; }
        public static ServiceToken Token { get; set; }
    }
}
