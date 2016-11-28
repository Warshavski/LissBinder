using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Escyug.LissBinder.Web.Api.ViewModels
{
    public class UserInfo
    {
        public string UserName { get; set; }
        public string UserId { get; set; }
        public int PharmacyId { get; set; }

        public UserInfo(string userId,  string userName, int pharmacyId)
        {
            UserId = userId;
            UserName = userName;
            PharmacyId = pharmacyId;
        }

        public UserInfo(Models.User user)
            : this(user.Id, user.NameDescription, user.PharmacyId)
        {
            
        }
    }
}