using Microsoft.EntityFrameworkCore;
using StudentDatabaseServer.Data;
using StudentDatabaseServer.DtoMapping;
using StudentDatabaseServer.Dtos;
using StudentDatabaseServer.Models;

namespace StudentDatabaseServer.Endpoints
{
    public static class AccountsEndpoints
    {
        const string GetAccountEndpointName = "GetAccount";
        public static RouteGroupBuilder MapAccountsEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("accounts").WithParameterValidation(); // All endpoints of URL start with "accounts"

            // GET /accounts
            group.MapGet("/", async (StudentInfoStudentContext dbContext) =>
            await dbContext.Accounts.Select(account => account.ToAccountGetDto()).AsNoTracking().ToListAsync());

            // GET /accounts/{id}
            group.MapGet("/{id}", async (int id, StudentInfoStudentContext dbContext) => // Starts with "accounts" in the endpoint, the id is just added onto that
            {
                Account? account = await dbContext.Accounts.FindAsync(id); // Returns specific student
                return account is null ? Results.NotFound() : Results.Ok(account.ToAccountGetDto()); // Returns result not found if no account found to be displayed
            }).WithName(GetAccountEndpointName); // Find route of endpoint, to find account request is looking for

            //Get /account for certain user
            group.MapGet("/userAccount/{username}", async (string username, StudentInfoStudentContext dbContext) =>
            {
                Account? account = await dbContext.Accounts.
                FromSql($"SELECT * From Accounts WHERE username = {username}").FirstAsync(); // SQL code to find the right account
                return account is null ? Results.NotFound() : Results.Ok(account.ToAccountGetDto());
            });

            // POST account
            group.MapPost("/", async (Account newAccount, StudentInfoStudentContext dbContext) =>
            {
                dbContext.Accounts.Add(newAccount);
                await dbContext.SaveChangesAsync();
                return Results.CreatedAtRoute(GetAccountEndpointName, // Gives name to this endpoint, so student can be found later
                    new { id = newAccount.Id }, newAccount.ToAccountGetDto());
            });

            // PUT /accounts
            group.MapPut("/{id}", async (int id, AccountPutDto updatedAccount, StudentInfoStudentContext dbContext) =>
            {
                var existingAccount = await dbContext.Accounts.FindAsync(id);
                if (existingAccount is null)
                    return Results.NotFound();

                dbContext.Entry(existingAccount).CurrentValues.SetValues(updatedAccount.ToAccountModel(id));
                await dbContext.SaveChangesAsync();

                return Results.NoContent();
            });

            // DELETE /accounts/{id}
            group.MapDelete("/{id}", async (int id, StudentInfoStudentContext dbContext) =>
            {
                await dbContext.Accounts.Where(account => account.Id == id).ExecuteDeleteAsync();
                return Results.NoContent();
            });

            // DELETE /account for user
            group.MapDelete("/userAccount/{name}", async (string name, StudentInfoStudentContext dbContext) =>
            {
                await dbContext.Accounts.Where(account => account.Name!.Equals(name)).ExecuteDeleteAsync();
                return Results.NoContent();
            });

            return group;
        }
    }
}
