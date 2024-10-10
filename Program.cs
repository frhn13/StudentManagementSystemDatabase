using StudentDatabaseServer.Endpoints;
using StudentDatabaseServer.Data;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("StudentDataStore"); // Gets connection string of DB from json file

// Commands needed to make DB
// 1) Install dotnet-ef through cmd (dotnet add package Microsoft.EntityFrameworkCore.Design --version 8.0.8)
// 2) dotnet ef migrations add InitialCreate (Name of migration) --output-dir Data\Migrations
// 3) dotnet ef database update
builder.Services.AddSqlite<StudentInfoStudentContext>(connString);

var app = builder.Build(); // Build the SQLite DB

app.MapStudentsEndpoints(); // All HTTP requests now process in another class

app.MapGet("/", () => "Hello World!");

await app.MigrateDbAsync(); // Creates DB if not already created (can replace the third step)

app.Run();
