using Cumulative1.Models;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class StudentAPIController : ControllerBase
{
    private readonly SchoolDbContext _context;

    public StudentAPIController(SchoolDbContext context)
    {
        _context = context;
    }

    [HttpGet("ListStudents")]
    public List<Student> ListStudents()
    {
        List<Student> students = new();
        using var conn = _context.AccessDatabase();
        conn.Open();

        var cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT * FROM students";

        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            students.Add(new Student
            {
                StudentId = Convert.ToInt32(reader["studentid"]),
                StudentFName = reader["studentfname"].ToString(),
                StudentLName = reader["studentlname"].ToString(),
                StudentNumber = reader["studentnumber"].ToString()
            });
        }

        return students;
    }
}
