using SchoolSystemApi.Models;

namespace SchoolSystemApi.Dto;

public class CourseDto
{
    public int Id { get; set; }
    public string CourseName { get; set; }
    public int teacherId { get; set; }
}