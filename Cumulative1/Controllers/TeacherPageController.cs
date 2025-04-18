using Cumulative1.Models;
using Microsoft.AspNetCore.Mvc;

public class TeacherPageController : Controller
{
    private readonly TeacherAPIController _api;

    public TeacherPageController(TeacherAPIController api)
    {
        _api = api;
    }

    [HttpGet]
    public IActionResult List()
    {
        
        List<Teacher> teacherList = _api.ListAllTeacher(); 

        
        return View(teacherList);  
    }

    [HttpGet]
    public IActionResult Show(int id)
    {
        
        Teacher selectedTeacher = _api.FindTeacherID(id);

        
        return View(selectedTeacher); 
    }
}
