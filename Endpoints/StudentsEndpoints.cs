using Microsoft.EntityFrameworkCore;
using StudentDatabaseServer.Data;
using StudentDatabaseServer.DtoMapping;
using StudentDatabaseServer.Dtos;
using StudentDatabaseServer.Models;

namespace StudentDatabaseServer.Endpoints
{
    public static class StudentsEndpoints
    {
        const string GetStudentEndpointName = "GetStudent";

        public static RouteGroupBuilder MapStudentsEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("students").WithParameterValidation(); // All endpoints of URL start with "students"

            // GET /students
            group.MapGet("/", async (StudentInfoStudentContext dbContext) => 
            await dbContext.Students.Select(student => student.ToStudentGetDto()).AsNoTracking().ToListAsync());

            // GET /students/{id}
            group.MapGet("/{id}", async (int id, StudentInfoStudentContext dbContext) => // Starts with "students" in the endpoint, the id is just added onto that
            {
                Student? student = await dbContext.Students.FindAsync(id); // Returns specific student
                return student is null ? Results.NotFound() : Results.Ok(student.ToStudentGetDto()); // Returns result not found if no student found to be displayed
            }).WithName(GetStudentEndpointName); // Find route of endpoint, to find student request is looking for

            //Get /student for certain user
            group.MapGet("/student/{name}", async (string name, StudentInfoStudentContext dbContext) =>
            {
                var student = await dbContext.CourseDetailsDB.FromSql($"SELECT * From Students WHERE name = {name}").FirstAsync(); // SQL code to find the right account
                return student;
            });

            // POST /students
            group.MapPost("/", async (Student newStudent, StudentInfoStudentContext dbContext) =>
            {
                dbContext.Students.Add(newStudent);
                await dbContext.SaveChangesAsync();
                return Results.CreatedAtRoute(GetStudentEndpointName, // Gives name to this endpoint, so student can be found later
                    new {id = newStudent.Id}, newStudent.ToStudentGetDto());
            });

            // PUT /students
            group.MapPut("/{id}", async (int id, StudentPutDto updatedStudent, StudentInfoStudentContext dbContext) =>
            {
                var existingStudent = await dbContext.Students.FindAsync(id);
                if (existingStudent is null)
                    return Results.NotFound();

                dbContext.Entry(existingStudent).CurrentValues.SetValues(updatedStudent.ToStudentModel(id));
                await dbContext.SaveChangesAsync();

                return Results.NoContent();
            });

            // DELETE /students/{id}
            group.MapDelete("/{id}", async (int id, StudentInfoStudentContext dbContext) =>
            {
                await dbContext.Students.Where(student => student.Id == id).ExecuteDeleteAsync();
                return Results.NoContent();
            });

            return group;
        }
    }
}
