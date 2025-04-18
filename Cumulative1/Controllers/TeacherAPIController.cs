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

    /// <summary>
    /// Returns a list of all teachers in the system.
    /// </summary>
    /// <returns>List of Teacher objects</returns>
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

    /// <summary>
    /// Finds a teacher by their ID.
    /// </summary>
    /// <param name="id">Teacher ID</param>
    /// <returns>Teacher object</returns>
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

    /// <summary>
    /// Adds a new teacher to the database.
    /// </summary>
    /// <param name="newTeacher">The teacher object to add</param>
    /// <returns>A success or error message</returns>
    [HttpPost]
    [Route("AddTeacher")]
    public IActionResult AddTeacher([FromBody] Teacher newTeacher)
    {
        if (newTeacher == null || string.IsNullOrEmpty(newTeacher.TeacherFName) || string.IsNullOrEmpty(newTeacher.TeacherLName))
        {
            return BadRequest("Invalid teacher data.");
        }

        using (MySqlConnection connection = _context.AccessDatabase())
        {
            connection.Open();
            MySqlCommand command = connection.CreateCommand();

            command.CommandText = "INSERT INTO teachers (teacherfname, teacherlname) VALUES (@fname, @lname)";
            command.Parameters.AddWithValue("@fname", newTeacher.TeacherFName);
            command.Parameters.AddWithValue("@lname", newTeacher.TeacherLName);

            command.ExecuteNonQuery();
        }

        return Ok("Teacher added successfully.");
    }

    /// <summary>
    /// Deletes a teacher from the database by ID.
    /// </summary>
    /// <param name="id">The teacher ID to delete</param>
    /// <returns>A success or error message</returns>
    [HttpPost]
    [Route("DeleteTeacher/{id}")]
    public IActionResult DeleteTeacher(int id)
    {
        using (MySqlConnection connection = _context.AccessDatabase())
        {
            connection.Open();
            MySqlCommand command = connection.CreateCommand();

            command.CommandText = "DELETE FROM teachers WHERE teacherid = @id";
            command.Parameters.AddWithValue("@id", id);

            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                return Ok("Teacher deleted successfully.");
            }
            else
            {
                return NotFound("Teacher not found.");
            }
        }
    }
}
