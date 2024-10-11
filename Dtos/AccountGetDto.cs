namespace StudentDatabaseServer.Dtos
{
    public record class AccountGetDto(
        int Id,
        string Name,
        string Username,
        string Password,
        string Role
        );
}
