using Cumulative1.Models;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class CourseAPIController : ControllerBase
{
    private readonly SchoolDbContext _context;

    public CourseAPIController(SchoolDbContext context)
    {
        _context = context;
    }

    [HttpGet("ListCourses")]
    public List<Course> ListCourses()
    {
        List<Course> courses = new();
        using var conn = _context.AccessDatabase();
        conn.Open();

        var cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT * FROM courses";

        using var result = cmd.ExecuteReader();
        while (result.Read())
        {
            courses.Add(new Course
            {
                CourseId = Convert.ToInt32(result["courseid"]),
                CourseCode = result["coursecode"].ToString(),
                CourseName = result["coursename"].ToString(),
                StartDate = Convert.ToDateTime(result["startdate"]),
                FinishDate = Convert.ToDateTime(result["finishdate"])
            });
        }

        return courses;
    }
}
