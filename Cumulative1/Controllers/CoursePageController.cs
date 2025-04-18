using Cumulative1.Models;
using Microsoft.AspNetCore.Mvc;

public class CoursePageController : Controller
{
    private readonly SchoolDbContext _context;

    public CoursePageController(SchoolDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult List()
    {
        var apiController = new CourseAPIController(_context);
        var courseList = apiController.ListCourses();
        return View(courseList);
    }
}
