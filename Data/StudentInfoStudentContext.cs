using Microsoft.EntityFrameworkCore;
using StudentDatabaseServer.Models;

namespace StudentDatabaseServer.Data
{
    public class StudentInfoStudentContext(DbContextOptions<StudentInfoStudentContext> options) : DbContext(options)
    {
        public DbSet<Student> Students => Set<Student>(); // Uses each set as a different table in DB
        public DbSet<Account> Accounts => Set<Account>();
        public DbSet<CourseDetails> CourseDetailsDB => Set<CourseDetails>();

    }
}
