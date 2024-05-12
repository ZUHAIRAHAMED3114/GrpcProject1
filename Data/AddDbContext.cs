using Microsoft.EntityFrameworkCore;
using ToDoGrpc.Models;

namespace ToDoGrpc.Data
{
    public class AddDbContext : DbContext
    {
        public AddDbContext(DbContextOptions<AddDbContext> options):base(options) 
        { 
        
        }
        public DbSet<TodoItem> TodoItems { get; set; }

    }
}
