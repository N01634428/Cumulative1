namespace Cumulative1.Models
{
    /// <summary>
    /// Represents a Teacher in the school database.
    /// </summary>
    public class Teacher
    {
        
        public int TeacherId { get; set; }

       
        public string TeacherFName { get; set; }

       
        public string TeacherLName { get; set; }
        
        public decimal Salary { get; set; }
    }
   
}