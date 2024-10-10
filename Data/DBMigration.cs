using Microsoft.EntityFrameworkCore;

namespace StudentDatabaseServer.Data
{
    public static class DBMigration
    {
        public static async Task MigrateDbAsync(this WebApplication app) // Code for creating database if not already there
        {
            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<StudentInfoStudentContext>();
            await dbContext.Database.MigrateAsync();
        }
    }
}
