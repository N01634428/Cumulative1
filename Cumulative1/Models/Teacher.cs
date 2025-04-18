namespace Cumulative1.Models
{
    /// <summary>
    /// Represents a Teacher in the school database.
    /// </summary>
    public class Teacher
    {
        /// <summary>The unique ID of the teacher.</summary>
        public int TeacherId { get; set; }

        /// <summary>The first name of the teacher.</summary>
        public string TeacherFName { get; set; }

        /// <summary>The last name of the teacher.</summary>
        public string TeacherLName { get; set; }
    }
    public class Courses
    {
        public int CourseId { get; set; }
        public string? CourseCode { get; set; }
        public int TeacherId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public string? CourseName { get; set; }
    }
}