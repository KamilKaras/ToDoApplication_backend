using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoAplication.Models
{
    public class AplicationUser : IdentityUser
    {
        public DateTime CreatedTime { get; set; }
        public DateTime Modify { get; set; }
    }
}
