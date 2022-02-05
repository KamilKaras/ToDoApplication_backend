using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using ToDoAplication.Models;

namespace ToDoAplication.Tools
{
    public static class Mapper
    {
        public static UserToSend PrepareToSend(AplicationUser user) =>  new UserToSend
        {
               Email = user.Email,
               UserName = user.UserName,
               Id = user.Id,
               Tasks = new List<string>(),
               Created = user.Created
        };
    }
}