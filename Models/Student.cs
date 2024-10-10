namespace StudentDatabaseServer.Models
{
    public class Student
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int Age { get; set; }
        public int Year { get; set; }
        public required string MobileNumber { get; set; }
        public DateOnly DOB { get; set; }
    }
}
