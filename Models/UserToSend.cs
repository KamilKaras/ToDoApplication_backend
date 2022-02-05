using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoAplication.Models
{
    public class UserToSend
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Id { get; set; }
        public List<string> Tasks { get; set; }
    }
}
