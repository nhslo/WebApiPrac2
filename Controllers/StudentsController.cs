using Microsoft.AspNetCore.Mvc;
using WebApiPrac2.Models;

namespace WebApiPrac2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private static readonly List<Student> Students = new()
    {
        new Student { Id = 1, Name = "Alex", Group = "SE-301" },
        new Student { Id = 2, Name = "Anna", Group = "SE-302" },
        new Student { Id = 3, Name = "Max", Group = "SE-301" }
    };

    [HttpGet]
    public ActionResult<List<Student>> GetStudents() => Ok(Students);

    [HttpGet("{id:int}")]
    public ActionResult<Student> GetStudent(int id)
    {
        var student = Students.FirstOrDefault(item => item.Id == id);
        return student is null ? NotFound() : Ok(student);
    }

    [HttpPost]
    public ActionResult<Student> AddStudent(Student student)
    {
        if (student.Id <= 0) student.Id = Students.Count == 0 ? 1 : Students.Max(item => item.Id) + 1;
        if (Students.Any(item => item.Id == student.Id)) return Conflict("A student with this Id already exists.");
        Students.Add(student);
        return Ok(student);
    }
}
