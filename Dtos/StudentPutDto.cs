namespace StudentDatabaseServer.Dtos
{
    public record class StudentPutDto(
        string Name,
        int Age,
        int Year,
        string MobileNumber,
        DateOnly DOB
        );
}
