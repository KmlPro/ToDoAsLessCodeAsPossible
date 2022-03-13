using Microsoft.Extensions.DependencyInjection;

namespace ToDoAsLessCodeAsPossible.Infrastructure.Persistence;

public static class DatabaseCreator
{
    public static void CreateDatabaseSchema(IServiceProvider services)
    {
        using (var scope = services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ToDoWriteDbContext>();
            dbContext.Database.EnsureCreated();
        }
    }
}