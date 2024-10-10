using Microsoft.EntityFrameworkCore;
using StudentDatabaseServer.Data;
using StudentDatabaseServer.DtoMapping;
using StudentDatabaseServer.Dtos;
using StudentDatabaseServer.Models;

namespace StudentDatabaseServer.Endpoints
{
    public static class CoursesEndpoints
    {
        const string GetCourseEndpointName = "GetCourse";

        public static RouteGroupBuilder MapCoursesEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("courses").WithParameterValidation(); // All endpoints of URL start with "courses"

            //GET /courses
            group.MapGet("/", async (StudentInfoStudentContext dbContext) =>
            await dbContext.CourseDetailsDB.Select(course => course.ToCourseGetDto()).AsNoTracking().ToListAsync());

            // GET /courses/{id}
            group.MapGet("/{id}", async (int id, StudentInfoStudentContext dbContext) => // Starts with "students" in the endpoint, the id is just added onto that
            {
                CourseDetails? course = await dbContext.CourseDetailsDB.FindAsync(id); // Returns specific student
                return course is null ? Results.NotFound() : Results.Ok(course.ToCourseGetDto()); // Returns result not found if no student found to be displayed
            }).WithName(GetCourseEndpointName); // Find route of endpoint, to find student request is looking for

            //Get /courses from certain user
            group.MapGet("/userCourses/{name}", async (string name, StudentInfoStudentContext dbContext) =>
            {
                var courses = await dbContext.CourseDetailsDB.FromSql($"SELECT * From CourseDetailsDB WHERE name = {name}").ToListAsync(); // SQL code to find the right name
                return courses;
            });

            // POST /courses
            group.MapPost("/", async (CourseDetails newCourse, StudentInfoStudentContext dbContext) =>
            {
                dbContext.CourseDetailsDB.Add(newCourse);
                await dbContext.SaveChangesAsync();
                return Results.CreatedAtRoute(GetCourseEndpointName, // Gives name to this endpoint, so course can be found later
                    new { id = newCourse.Id }, newCourse.ToCourseGetDto());
            });

            // PUT /courses
            group.MapPut("/{id}", async (int id, CoursePutDto updatedCourse, StudentInfoStudentContext dbContext) =>
            {
                var existingCourse = await dbContext.CourseDetailsDB.FindAsync(id);
                if (existingCourse is null)
                    return Results.NotFound();

                dbContext.Entry(existingCourse).CurrentValues.SetValues(updatedCourse.ToCourseModel(id));
                await dbContext.SaveChangesAsync();

                return Results.NoContent();
            });

            // DELETE /courses/{id}
            group.MapDelete("/{id}", async (int id, StudentInfoStudentContext dbContext) =>
            {
                await dbContext.CourseDetailsDB.Where(course => course.Id == id).ExecuteDeleteAsync();
                return Results.NoContent();
            });

            // DELETE /courses for user
            group.MapDelete("/userCourses/{name}", async (string name, StudentInfoStudentContext dbContext) =>
            {
                await dbContext.CourseDetailsDB.Where(course => course.Name!.Equals(name)).ExecuteDeleteAsync();
                return Results.NoContent();
            });
            return group;
        }
    }
}
