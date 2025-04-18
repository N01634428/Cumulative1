using Cumulative1.Models;
using Microsoft.AspNetCore.Mvc;

public class StudentPageController : Controller
{
    private readonly SchoolDbContext _context;

    public StudentPageController(SchoolDbContext context)
    {
        _context = context;
    }

    public IActionResult List()
    {
        // Manually create an instance of the API controller using the context
        var apiController = new StudentAPIController(_context);
        var students = apiController.ListStudents();
        return View(students);
    }
}
