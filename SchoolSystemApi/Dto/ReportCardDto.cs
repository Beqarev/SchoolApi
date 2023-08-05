using SchoolSystemApi.Models;

namespace SchoolSystemApi.Dto;

public class ReportCardDto
{
    public int Id { get; set; }
    public CourseAndStudent CourseAndStudent { get; set; }
    public int Mark { get; set; }
}