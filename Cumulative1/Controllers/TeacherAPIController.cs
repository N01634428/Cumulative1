using Cumulative1.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

[Route("api/[controller]")]
[ApiController]
public class TeacherAPIController : ControllerBase
{
    private readonly SchoolDbContext _context;

    public TeacherAPIController(SchoolDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Route("ListAllTeacher")]
    public List<Teacher> ListAllTeacher()
    {
        List<Teacher> teacherList = new List<Teacher>();

        using (MySqlConnection Connection = _context.AccessDatabase())
        {
            Connection.Open();
            MySqlCommand Command = Connection.CreateCommand();

            Command.CommandText = "SELECT * FROM teachers";

            using (MySqlDataReader ResultSet = Command.ExecuteReader())
            {
                while (ResultSet.Read())
                {
                    Teacher teacher = new Teacher
                    {
                        TeacherId = Convert.ToInt32(ResultSet["teacherid"]),
                        TeacherFName = ResultSet["teacherfname"].ToString(),
                        TeacherLName = ResultSet["teacherlname"].ToString()
                    };

                    teacherList.Add(teacher);
                }
            }
        }

        return teacherList;
    }

    [HttpGet("FindTeacherID/{id}")]
    public Teacher FindTeacherID(int id)
    {
        Teacher teacher = new Teacher();

        using (MySqlConnection Connection = _context.AccessDatabase())
        {
            Connection.Open();
            MySqlCommand Command = Connection.CreateCommand();
            Command.CommandText = "SELECT * FROM teachers WHERE teacherid=@id";
            Command.Parameters.AddWithValue("@id", id);

            using (MySqlDataReader ResultSet = Command.ExecuteReader())
            {
                if (ResultSet.Read())
                {
                    teacher.TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                    teacher.TeacherFName = ResultSet["teacherfname"].ToString();
                    teacher.TeacherLName = ResultSet["teacherlname"].ToString();
                }
            }
        }

        return teacher;
    }
}
