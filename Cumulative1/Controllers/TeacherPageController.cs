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

   
    [HttpGet]
    public IActionResult Add()
    {
        return View(); 
    }

    
    [HttpPost]
    public IActionResult Add(Teacher teacher)
    {
        if (ModelState.IsValid)
        {
            _api.AddTeacher(teacher);  
            return RedirectToAction("List");  
        }
        return View(teacher); 
    }

    [HttpGet]
    public IActionResult DeleteConfirm(int id)
    {
        Teacher teacherToDelete = _api.FindTeacherID(id);  
        return View(teacherToDelete);  
    }
    [HttpPost]
    [ActionName("DeleteConfirm")]  
    public IActionResult DeleteTeacher(int id)
    {
        _api.DeleteTeacher(id);  
        return RedirectToAction("List");  
    }
    [HttpGet]
    public IActionResult Edit(int id)
    {
        Teacher teacherToEdit = _api.FindTeacherID(id);
        return View(teacherToEdit);
    }

    [HttpPost]
    public IActionResult Edit(int id, Teacher updatedTeacher)
    {
        _api.UpdateTeacher(id, updatedTeacher);
        return RedirectToAction("List");
    }

}
