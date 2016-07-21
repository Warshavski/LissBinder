using Escyug.LissBinder.Common.Utils;

using Microsoft.AspNet.Identity;

namespace Escyug.LissBinder.Web.Models
{
    public class User : IUser
    {
        public string Id { get; private set; }

        public string UserName { get; set; }

        public string Login { get; private set; }

        public byte[] PwdHash { get;  set; }

        public byte[] Salt { get;  set; }

        //public User(string id, string name)
        //{
        //    Id = id;
        //    UserName = name;
        //}

        //*** hmmm.... default hashing
        //public User(string id, string name, string password, string login)
        //    : this (id, name)
        //{
        //    Login = login;

        //    var byteSalt = Security.GenerateSalt();
        //    var bytePassword = System.Text.Encoding.UTF8.GetBytes(password);

        //    PwdHash = Security.GenerateSaltedHash(bytePassword, byteSalt);
        //    Salt = byteSalt;
        //}

        public User(string id, string name, string login, byte[] hash, byte[] salt)
        {
            Id = id;
            UserName = name;
            Login = login;
            PwdHash = hash;
            Salt = salt;
        }
    }
}
