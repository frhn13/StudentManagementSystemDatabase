using StudentDatabaseServer.Dtos;
using StudentDatabaseServer.Models;

namespace StudentDatabaseServer.DtoMapping
{
    public static class AccountMapping
    {
        public static AccountGetDto ToAccountGetDto(this Account account)
        {
            return new(
                account.Id,
                account.Name!,
                account.Username,
                account.Password,
                account.Role!
                );
        }
        public static Account ToAccountModel(this AccountPutDto updatedAccount, int id)
        {
            return new Account()
            {
                Id = id,
                Name = updatedAccount.Name,
                Username = updatedAccount.Username,
                Password = updatedAccount.Password,
                Role = updatedAccount.Role
            };
        }
    }
}
