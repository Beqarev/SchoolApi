using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SchoolSystemApi.Dto;
using SchoolSystemApi.Models;

namespace SchoolSystemApi.Controllers;
[Route("api/[Controller]")]
[ApiController]
public class StudentController : ControllerBase
{
    private readonly IStudentRepository _studentRepository;
    private readonly IMapper _mapper;

    public StudentController(IStudentRepository studentRepository, IMapper mapper)
    {
        _studentRepository = studentRepository;
        _mapper = mapper;
    }
    
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(ICollection<Student>))]
    public IActionResult GetStudents()
    {
        var students = _mapper.Map<StudentDto>(_studentRepository.GetStudents());

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(students);
    }

    [HttpGet("{studentId}")]
    [ProducesResponseType(200, Type = typeof(Student))]
    public IActionResult GetStudent(int studentId)
    {
        var student = _mapper.Map<StudentDto>(_studentRepository.GetStudent(studentId));

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(student);
    }

    [HttpPost]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public IActionResult CreateStudent([FromBody] StudentDto studentCreate)
    {
        if (studentCreate == null)
            return BadRequest(ModelState);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var studentMap = _mapper.Map<Student>(studentCreate);
        
        if (!_studentRepository.CreateStudent(studentMap))
        {
            ModelState.AddModelError("", "Something went wrong creating student");
            return StatusCode(500, ModelState);
        }

        return Ok($"Created Successfully" + $" id = {studentMap.Id}");
    }

    [HttpPut("{studentId}")]
    [ProducesResponseType(400)]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public IActionResult UpdateStudent(int studentId, [FromBody] StudentDto updatedStudent)
    {
        if (updatedStudent == null)
            return BadRequest(ModelState);

        if (!ModelState.IsValid)
            return BadRequest();
        
        if (studentId != updatedStudent.Id)
            return BadRequest(ModelState);

        if (!_studentRepository.StudentExists(studentId))
            return NotFound();

        var studentMap = _mapper.Map<Student>(updatedStudent);

        if (!_studentRepository.UpdateStudent(studentMap))
        {
            ModelState.AddModelError("", "Something went wrong updating student");
            return StatusCode(500, ModelState);
        }

        return NoContent();
    }

    [HttpDelete("{studentId}")]
    [ProducesResponseType(400)]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public IActionResult DeleteStudent(int studentId)
    {
        if (!_studentRepository.StudentExists(studentId))
            return NotFound();
        
        if (!ModelState.IsValid)
            return BadRequest();

        var studentToDelete = _studentRepository.GetStudent(studentId);
        
        if (!_studentRepository.DeleteStudent(studentToDelete))
        {
            ModelState.AddModelError("", "Something went wrong deleting student");
            return StatusCode(500, ModelState);
        }

        return NoContent();
    }
}