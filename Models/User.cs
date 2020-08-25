using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Waka.Models
{
    public class User
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } // TODO: Hash Password
        public bool IsSignedIn { get; set; }
        public bool IsEnabled { get; set; }
    }
}
