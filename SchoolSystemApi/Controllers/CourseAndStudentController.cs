using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SchoolSystemApi.Dto;
using SchoolSystemApi.Models;

namespace SchoolSystemApi.Controllers;
[Route("api/[Controller]")]
[ApiController]
public class CourseAndStudentController : ControllerBase
{
    private readonly ICourseAndStudentRepository _courseAndStudentRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly ICoursesRepository _coursesRepository;
    private readonly IMapper _mapper;

    public CourseAndStudentController(ICourseAndStudentRepository courseAndStudentRepository, IStudentRepository studentRepository, ICoursesRepository coursesRepository, IMapper mapper)
    {
        _courseAndStudentRepository = courseAndStudentRepository;
        _studentRepository = studentRepository;
        _coursesRepository = coursesRepository;
        _mapper = mapper;
    }


    [HttpGet]
    [ProducesResponseType(200, Type = typeof(ICollection<CourseAndStudent>))]
    public IActionResult GetCoursesAndStudents()
    {
        var coursesandstudents =
            _mapper.Map<CourseAndStudentDto>(_courseAndStudentRepository.GetCoursesAndStudents().ToList());
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        return Ok(coursesandstudents);
    }

    [HttpGet("{courseAndStudentId}")]
    [ProducesResponseType(200, Type = typeof(CourseAndStudent))]
    [ProducesResponseType(400)]
    public IActionResult GetCourseAndStudent(int courseAndStudentId)
    {
        if (!_courseAndStudentRepository.CourseAndStudentExists(courseAndStudentId))
            return BadRequest(ModelState);

        var courseandstudent =
            _mapper.Map<CourseAndStudentDto>(_courseAndStudentRepository.GetCourseAndStudent(courseAndStudentId));

        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        return Ok(courseandstudent);
    }

    [HttpPost]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public IActionResult CreateCourseAndStudent([FromBody] SetCourseAndStudentDto courseAndStudentCreate)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        if (courseAndStudentCreate == null)
            return BadRequest(ModelState);

        var courseandstudent = _courseAndStudentRepository.GetCoursesAndStudents()
            .FirstOrDefault(c => 
                c.Course.Id == courseAndStudentCreate.CourseId && c.Student.Id == courseAndStudentCreate.StudentId);

        if (courseandstudent != null)
        {
            ModelState.AddModelError("", "CourseAndStudent already exists");
            return StatusCode(422, ModelState);
        }

        var student = _studentRepository.GetStudent(courseAndStudentCreate.StudentId);
        var course = _coursesRepository.GetCourse(courseAndStudentCreate.CourseId);

        //var courseandstudentMap = _mapper.Map<CourseAndStudent>(courseAndStudentCreate);
        var courseandstudentMap = new CourseAndStudent()
        {
            Id = courseAndStudentCreate.Id,
            Course = course,
            Student = student
        };
        
        if (!_courseAndStudentRepository.CreateCourseAndStudents(courseandstudentMap))
        {
            ModelState.AddModelError("", "Something went wrong creating courseandstudent");
            return StatusCode(500, ModelState);
        }

        return Ok("Successfully Created");
    }

    [HttpPut("{courseandstudentId}")]
    [ProducesResponseType(400)]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public IActionResult UpdateCourseAndStudent(int courseandstudentId, [FromBody] CourseAndStudentDto updatedCourseAndStudent)
    {
        if (updatedCourseAndStudent == null)
            return BadRequest(ModelState);

        if (!ModelState.IsValid)
            return BadRequest();

        if (!_courseAndStudentRepository.CourseAndStudentExists(courseandstudentId))
            return NotFound();

        if (courseandstudentId != updatedCourseAndStudent.Id)
            return BadRequest(ModelState);

        var courseandstudentMap = _mapper.Map<CourseAndStudent>(updatedCourseAndStudent);

        if (!_courseAndStudentRepository.UpdateCourseAndStudents(courseandstudentMap))
        {
            ModelState.AddModelError("", "Something went wrong updating courseandstudent");
            return StatusCode(500, ModelState);
        }

        return NoContent();
    }


    [HttpDelete("{courseAndStudentId}")]
    [ProducesResponseType(400)]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public IActionResult DeleteCourseAndStudent(int courseAndStudentId)
    {
        if (!_courseAndStudentRepository.CourseAndStudentExists(courseAndStudentId))
            return NotFound();
        
        if (!ModelState.IsValid)
            return BadRequest();

        var courseandstudentToDelete = 
            _courseAndStudentRepository.GetCourseAndStudent(courseAndStudentId);

        if (!_courseAndStudentRepository.DeleteCourseAndStudents(courseandstudentToDelete))
        {
            ModelState.AddModelError("", "Something went wrong deleting courseandstundet");
            return StatusCode(500, ModelState);
        }

        return NoContent();
    }
}