namespace StudentDatabaseServer.Dtos
{
    public record class AccountPutDto(
        string Name,
        string Username,
        string Password,
        string Role
        );
}
