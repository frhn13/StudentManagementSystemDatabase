using StudentDatabaseServer.Dtos;
using StudentDatabaseServer.Models;

namespace StudentDatabaseServer.DtoMapping
{
    public static class CourseMapping
    {
        public static CourseGetDto ToCourseGetDto(this CourseDetails course)
        {
            return new(
                course.Id,
                course.Name!,
                course.Course,
                course.Module,
                course.Semester
                );
        }
        public static CourseDetails ToCourseModel(this CoursePutDto updatedCourse, int id) 
        {
            return new CourseDetails()
            {
                Id = id,
                Name = updatedCourse.Name,
                Course = updatedCourse.Course,
                Module = updatedCourse.Module,
                Semester = updatedCourse.Semester
            };
        }
    }
}
