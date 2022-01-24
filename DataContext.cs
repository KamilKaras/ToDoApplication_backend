using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ToDoAplication
{
    public class DataContext : IdentityDbContext
    {
        protected readonly IConfiguration Configuration;

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public virtual DbSet<ToDoItem> ToDoItems { get; set; }
    }
}
