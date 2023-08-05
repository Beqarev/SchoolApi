using SchoolSystemApi.Models;

namespace SchoolSystemApi.Dto;

public class CourseAndStudentDto
{
    public int Id { get; set; }
    public Course Course { get; set; }
    public Student Student { get; set; }
}