using StudentDatabaseServer.Dtos;
using StudentDatabaseServer.Models;

namespace StudentDatabaseServer.DtoMapping
{
    public static class StudentMapping
    {
        public static StudentGetDto ToStudentGetDto(this Student student)
        {
            return new(
                student.Id,
                student.Name,
                student.Age,
                student.Year,
                student.MobileNumber,
                student.DOB
                );
        }
        public static Student ToStudentModel(this StudentPutDto updatedStudent, int id) 
        {
            return new Student()
            {
                Id = id,
                Name = updatedStudent.Name,
                Age = updatedStudent.Age,
                Year = updatedStudent.Year,
                MobileNumber = updatedStudent.MobileNumber,
                DOB = updatedStudent.DOB
            };
        }
    }
}
