
namespace Escyug.LissBinder.Models.Services.Common
{
    public class ServiceToken
    {
        public string AccessToken {get; private set;}
        public string TokenType {get; private set;}
        public int ExpiresIn {get; private set;}

        public ServiceToken(string access_token, string token_type, int expires_in)
        {
            AccessToken = access_token;
            TokenType = token_type;
            ExpiresIn = expires_in;
        }
    }
}
