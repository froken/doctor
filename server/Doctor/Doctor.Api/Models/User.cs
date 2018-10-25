using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Doctor.Api.Models
{
    public class User : IdentityUser
    {
        public string Login { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
