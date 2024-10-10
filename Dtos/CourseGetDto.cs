namespace StudentDatabaseServer.Dtos
{
    public record class CourseGetDto(
        int Id,
        string Name,
        string Course,
        string Module,
        int Semester
        );
}
