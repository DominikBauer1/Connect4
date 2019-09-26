using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Connect4Web.Models
{
    public class User : IdentityUser
    {
        public string ConnectionId { get; set; }
    }
}
