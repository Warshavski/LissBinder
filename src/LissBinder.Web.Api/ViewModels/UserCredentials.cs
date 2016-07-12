using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Escyug.LissBinder.Web.Api.ViewModels
{
    public class UserCredentials
    {
        [Required]
        [MaxLength(50)]
        [DisplayName("Login")]
        public string Login { get; set; }

        [Required]
        [MaxLength(50)]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string Password { get; set; }
    }
}