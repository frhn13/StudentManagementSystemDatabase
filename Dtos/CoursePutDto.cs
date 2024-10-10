namespace StudentDatabaseServer.Dtos
{
    public record class CoursePutDto(
        string Name,
        string Course,
        string Module,
        int Semester
        );
}
