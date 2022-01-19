using Microsoft.EntityFrameworkCore;
using ToDoAsLessCodeAsPossible.Domain.Entity;
using ToDoAsLessCodeAsPossible.Infrastructure.Persistence.TableConfiguration;

namespace ToDoAsLessCodeAsPossible.Infrastructure.Persistence;

internal class ToDoWriteDbContext : DbContext
{
    public ToDoWriteDbContext(DbContextOptions options) : base(options){ }
 
    public DbSet<ToDo> ToDo { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ToDoConfiguration());
    }
}