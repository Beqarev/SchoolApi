using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SchoolSystemApi.Dto;
using SchoolSystemApi.Models;

namespace SchoolSystemApi.Controllers;
[Route("api/[Controller]")]
[ApiController]
public class ReportCardController : ControllerBase
{
    private readonly IReportCardRepository _reportCardRepository;
    private readonly ICourseAndStudentRepository _courseAndStudentRepository;
    private readonly IMapper _mapper;

    public ReportCardController(IReportCardRepository reportCardRepository, ICourseAndStudentRepository courseAndStudentRepository, IMapper mapper)
    {
        _reportCardRepository = reportCardRepository;
        _courseAndStudentRepository = courseAndStudentRepository;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(ICollection<ReportCard>))]
    public IActionResult GetReportCard()
    {
        var reportcard = _mapper.Map<ReportCard>(_reportCardRepository.GetReportCard());
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        return Ok(reportcard);
    }

    [HttpGet("{reportcardId}")]
    [ProducesResponseType(200, Type = typeof(ReportCard))]
    [ProducesResponseType(400)]
    public IActionResult GetReportCardById(int reportcardId)
    {
        if (!_reportCardRepository.reportCardExists(reportcardId))
            return BadRequest(ModelState);

        var reportcarbyid = 
            _mapper.Map<ReportCardDto>(_reportCardRepository.GetReportCardById(reportcardId));
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        return Ok(reportcarbyid);
    }

    [HttpPost]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public IActionResult AddInReportCard([FromBody] SetReportCardDto addreportcard)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        //var reportCardMap = _mapper.Map<ReportCard>(addreportcard);
        var courseandstudent = _courseAndStudentRepository.GetCourseAndStudent(addreportcard.CourseAndStudentId);

        var reportCardMap = new ReportCard()
        {
            Id = addreportcard.Id,
            CourseAndStudent = courseandstudent,
            Mark = addreportcard.Mark
        };

        if (!_reportCardRepository.AddInReportCard(reportCardMap))
        {
            ModelState.AddModelError("", "Something went wrong adding reportcard");
            return StatusCode(500, ModelState);
        }

        return Ok("Successfully added");
    }

    [HttpPut("{reportcardId}")]
    [ProducesResponseType(400)]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public IActionResult UpdateReportCard(int reportcardId, [FromBody] SetReportCardDto updatedReportCard)
    {
        if (updatedReportCard == null)
            return BadRequest(ModelState);

        if (!ModelState.IsValid)
            return BadRequest();

        if (!_reportCardRepository.reportCardExists(reportcardId))
            return NotFound();

        if (reportcardId != updatedReportCard.Id)
            return BadRequest(ModelState);

        //var reportcardMap = _mapper.Map<ReportCard>(updatedReportCard);
        var courseandstudent = _courseAndStudentRepository.GetCourseAndStudent(updatedReportCard.CourseAndStudentId);
        var reportcardMap = new ReportCard()
        {
            Id = updatedReportCard.Id,
            CourseAndStudent = courseandstudent,
            Mark = updatedReportCard.Mark
        };

        if (!_reportCardRepository.UpdateReportCard(reportcardMap))
        {
            ModelState.AddModelError("", "Something went wrong updating reportcard");
            return StatusCode(500, ModelState);
        }

        return NoContent();
    }
    
    
    [HttpDelete("{reportcardId}")]
    [ProducesResponseType(400)]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public IActionResult DeleteCourseAndStudent(int reportcardId)
    {
        if (!_reportCardRepository.reportCardExists(reportcardId))
            return NotFound();
        
        if (!ModelState.IsValid)
            return BadRequest();

        var reportcardToDelete = 
            _reportCardRepository.GetReportCardById(reportcardId);

        if (!_reportCardRepository.DeleteReportCard(reportcardToDelete))
        {
            ModelState.AddModelError("", "Something went wrong deleting reportcard");
            return StatusCode(500, ModelState);
        }

        return NoContent();
    }
}