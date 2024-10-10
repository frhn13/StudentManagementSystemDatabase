using Microsoft.EntityFrameworkCore;

namespace StudentDatabaseServer.Endpoints
{
    public static class StudentsEndpoints
    {
        const string GetStudentEndpointName = "GetStudent";

        public static RouteGroupBuilder MapStudentsEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("students").WithParameterValidation(); // All endpoints of URL start with "students"

            return group;
        }
    }
}
