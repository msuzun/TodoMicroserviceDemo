using FirstMicroservice.Todos.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstMicroservice.Todos.WebAPI.Context
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Todo> Todos { get; set; }
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
