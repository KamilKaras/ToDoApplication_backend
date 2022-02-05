using System.Collections.Generic;

namespace ToDoAplication.Tools
{
    public class UserToSend
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Id { get; set; }
        public List<string> Tasks { get; set; }
    }
}