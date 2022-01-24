using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoAplication.Models
{
    public abstract class AuthResult
    {
        public string Token { get; set; }
        public bool Sucssess { get; set; }
        public List<string> Errors { get; set; }
    }
}
