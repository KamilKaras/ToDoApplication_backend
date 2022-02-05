using Microsoft.AspNetCore.Identity;
using System;

namespace ToDoAplication.Models
{
    public class AplicationUser : IdentityUser
    {
        public DateTime CreatedTime { get; set; }
        public DateTime ModifiedTime { get; set; }
    }
}
