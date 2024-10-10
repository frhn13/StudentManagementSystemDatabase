namespace StudentDatabaseServer.Dtos
{
    public record class StudentGetDto(
        int Id,
        string Name,
        int Age,
        int Year,
        string MobileNumber,
        DateOnly Date
        );
}
