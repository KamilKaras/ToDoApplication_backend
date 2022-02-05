using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ToDoAplication.Models;

namespace ToDoAplication
{
    public class DataContext : IdentityDbContext<AplicationUser>
    {
        protected readonly IConfiguration Configuration;

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public virtual DbSet<ToDoItem> ToDoItems { get; set; }
    }
}
