using Microsoft.AspNetCore.Identity;
using System;

namespace ToDoAplication.Models
{
    public class AplicationUser : IdentityUser
    {
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}
