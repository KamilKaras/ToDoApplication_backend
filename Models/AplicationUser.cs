using Microsoft.AspNetCore.Identity;
using System;

namespace ToDoAplication.Models
{
    public class AplicationUser : IdentityUser
    {
        public string Created { get; set; } = DateTime.Now.ToString();
        public string Modified { get; set; } = DateTime.Now.ToString();

    }
}

