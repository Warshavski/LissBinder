using System;

using Microsoft.AspNet.Identity;

using Escyug.LissBinder.Common.Utils;

namespace Escyug.LissBinder.Web.Models
{
    public class User : IUser
    {
        public string Id { get; private set; }

        public int PharmacyId { get; private set; }

        public string UserName { get; set; }

        public string NameDescription { get; private set; }

        public string PasswordHash { get; private set; }

        public User(string id, string userName, string login, int pharmacyId)
            : this(userName, login, pharmacyId)
        {
            Id = id;
        }

        public User(string userName, string nameDescription, int pharmacyId)
        {
            UserName = userName;
            NameDescription = nameDescription;
            PharmacyId = pharmacyId;
        }

        public void SetPasswordHash(string passwordHash)
        {
            PasswordHash = passwordHash;
        }
    }
}
