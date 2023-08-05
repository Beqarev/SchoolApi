using Microsoft.AspNetCore.Mvc;
using SchoolSystemApi.Models;
using AutoMapper;
using SchoolSystemApi.Dto;

namespace SchoolSystemApi.Controllers;
[Route("api/[Controller]")]
[ApiController]
public class CourseController : ControllerBase
{
    private readonly ICoursesRepository _coursesRepository;
    private readonly ITeacherRepository _teacherRepository;
    private readonly IMapper _mapper;

    public CourseController(ICoursesRepository coursesRepository, ITeacherRepository teacherRepository, IMapper mapper)
    {
        _coursesRepository = coursesRepository;
        _teacherRepository = teacherRepository;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(ICollection<Course>))]
    public IActionResult GetCourses()
    {
        var courses = _mapper.Map<CourseDto>(_coursesRepository.GetCourses().ToList());
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        return Ok(courses);
    }

    [HttpGet("{courseId}")]
    [ProducesResponseType(200, Type = typeof(Course))]
    [ProducesResponseType(400)]
    public IActionResult GetCourse(int courseId)
    {
        if (!_coursesRepository.CourseExists(courseId))
            return BadRequest(ModelState);
        
        var course = _mapper.Map<CourseDto>(_coursesRepository.GetCourse(courseId));
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(course);
    }

    [HttpPost]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public IActionResult CreateCourse(int teacherId, [FromBody] CourseDto courseCreate)
    {
        if (courseCreate == null)
            return BadRequest(ModelState);

        var course = _coursesRepository.GetCourses()
            .Where(c => c.CourseName.Trim().ToUpper() == courseCreate.CourseName.Trim().ToUpper())
            .FirstOrDefault();

        if (course != null)
        {
            ModelState.AddModelError("", "Course already exists");
            return StatusCode(422, ModelState);
        }

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        //var courseMap = _mapper.Map<Course>(courseCreate);
        var teacher = _teacherRepository.GetTeacher(teacherId);

        var courseMap = new Course()
        {
            CourseName = courseCreate.CourseName,
            Teacher = teacher
        };

        if (!_coursesRepository.CreateCoures(courseMap))
        {
            ModelState.AddModelError("","Something went wrong creating course");
            return StatusCode(500, ModelState);
        }

        return Ok("Successfully Created");
    }

    [HttpPut("{courseId}")]
    [ProducesResponseType(400)]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public IActionResult UpdateCourse(int courseId, [FromBody] CourseDto updatedCourse)
    {
        if (updatedCourse == null)
            return BadRequest(ModelState);
        
        if (!ModelState.IsValid)
            return BadRequest();

        if (!_coursesRepository.CourseExists(courseId))
            return NotFound();
        
        if (courseId != updatedCourse.Id)
            return BadRequest(ModelState);

        //var courseMap = _mapper.Map<Course>(updatedCourse);

        var teacher = _teacherRepository.GetTeacher(updatedCourse.teacherId);

        var courseMap = new Course()
        {
            Id = courseId,
            CourseName = updatedCourse.CourseName,
            Teacher = teacher
        };

        if (!_coursesRepository.UpdateCourse(courseMap))
        {
            ModelState.AddModelError("", "Something went wrong Updating course");
            return StatusCode(500, ModelState);
        }

        return NoContent();
    }

    [HttpDelete("{courseId}")]
    [ProducesResponseType(400)]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public IActionResult DeleteCourse(int courseId)
    {
        if (!_coursesRepository.CourseExists(courseId))
            return NotFound();
        
        if (!ModelState.IsValid)
            return BadRequest();

        var courseToDelete = _coursesRepository.GetCourse(courseId);

        if (!_coursesRepository.DeleteCourse(courseToDelete))
        {
            ModelState.AddModelError("", "Something went wrong deleting course");
            return StatusCode(500, ModelState);
        }

        return NoContent();
    }
}