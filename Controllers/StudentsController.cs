
using Microsoft.AspNetCore.Mvc;
using StudentManagementApi.Models;

namespace StudentManagementApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private static readonly List<Student> _students =
    [
        new Student
        {
            Id = 1,
            Name = "Aarav Patel",
            Email = "aarav@example.com",
            Major = "Computer Science"
        },
        new Student
        {
            Id = 2,
            Name = "Maya Lin",
            Email = "maya@example.com",
            Major = "Data Science"
        }
    ];

    private static int _nextId = 3;

    // GET all students
    [HttpGet]
    public ActionResult<IEnumerable<Student>> GetAll()
    {
        return Ok(_students);
    }

    // GET student by ID
    [HttpGet("{id:int}")]
    public ActionResult<Student> GetById(int id)
    {
        var student = _students.FirstOrDefault(s => s.Id == id);

        if (student is null)
            return NotFound(new { message = "Student not found." });

        return Ok(student);
    }

    // POST create student
    [HttpPost]
    public ActionResult<Student> Create(Student newStudent)
    {
        newStudent.Id = _nextId++;
        _students.Add(newStudent);

        return CreatedAtAction(
            nameof(GetById),
            new { id = newStudent.Id },
            newStudent
        );
    }

    // PUT update student
    [HttpPut("{id:int}")]
    public IActionResult Update(int id, Student updatedStudent)
    {
        var existing = _students.FirstOrDefault(s => s.Id == id);

        if (existing is null)
            return NotFound(new { message = "Student not found." });

        existing.Name = updatedStudent.Name;
        existing.Email = updatedStudent.Email;
        existing.Major = updatedStudent.Major;

        return NoContent();
    }

    // DELETE student
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var student = _students.FirstOrDefault(s => s.Id == id);

        if (student is null)
            return NotFound(new { message = "Student not found." });

        _students.Remove(student);

        return NoContent();
    }
}
