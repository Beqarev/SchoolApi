using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SchoolSystemApi.Dto;
using SchoolSystemApi.Models;

namespace SchoolSystemApi.Controllers;
[Route("api/[Controller]")]
[ApiController]
public class TeacherController : ControllerBase
{
    private readonly ITeacherRepository _teacherRepository;
    private readonly IMapper _mapper;

    public TeacherController(ITeacherRepository teacherRepository, IMapper mapper)
    {
        _teacherRepository = teacherRepository;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(ICollection<Teacher>))]
    public IActionResult GetTeachers()
    {
        var teachers = _mapper.Map<TeacherDto>(_teacherRepository.GetTeachers());

        if (ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(teachers);
    }

    [HttpGet("{teacherId}")]
    [ProducesResponseType(200, Type = typeof(Teacher))]
    public IActionResult GetTeacher(int teacherId)
    {
        var teacher = _mapper.Map<TeacherDto>(_teacherRepository.GetTeacher(teacherId));

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(teacher);
    }

    [HttpPost]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public IActionResult CreateTeacher([FromBody] TeacherDto teacherCreate)
    {
        if (teacherCreate == null)
            return BadRequest(ModelState);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var teacherMap = _mapper.Map<Teacher>(teacherCreate);

        if (!_teacherRepository.CreateTeacher(teacherMap))
        {
            ModelState.AddModelError("", "Something went wrong creating teacher");
            return StatusCode(500, ModelState);
        }

        return Ok($"Successfully Created " + "TeacherId = {teacherMap.Id}");
    }


    [HttpPut("{teacherId}")]
    [ProducesResponseType(400)]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public IActionResult UpdateTeacher(int teacherId, [FromBody] TeacherDto updatedTeacher)
    {
        if (updatedTeacher == null)
            return BadRequest(ModelState);
        
        if (!ModelState.IsValid)
            return BadRequest();
        
        if(!_teacherRepository.TeacherExists(teacherId))
            return NotFound();

        if (teacherId != updatedTeacher.Id)
            return BadRequest(ModelState);

        var teacherMap = _mapper.Map<Teacher>(updatedTeacher);

        if (!_teacherRepository.UpdateTeacher(teacherMap))
        {
            ModelState.AddModelError("", "Something went wrong Updating teacher");
            return StatusCode(500, ModelState);
        }

        return NoContent();
    }

    [HttpDelete("{teacherId}")]
    [ProducesResponseType(400)]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public IActionResult DeleteTeacher(int teacherId)
    {
        if (!_teacherRepository.TeacherExists(teacherId))
            return NotFound();

        if (!ModelState.IsValid)
            return BadRequest();

        var teacherToDelete = _teacherRepository.GetTeacher(teacherId);

        if (!_teacherRepository.DeleteTeacher(teacherToDelete))
        {
            ModelState.AddModelError("", "Something went wrong deleting teacher");
            return StatusCode(500, ModelState);
        }

        return NoContent();
    }
}