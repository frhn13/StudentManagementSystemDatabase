using System.ComponentModel.DataAnnotations;

namespace StudentDatabaseServer.Models
{
    public class CourseDetails
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public required string Course { get; set; }
        public required string Module { get; set; }
        public int Semester { get; set; }
    }
}
