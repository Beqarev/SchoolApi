namespace SchoolSystemApi.Models;

public class CourseAndStudent
{
    public int Id { get; set; }
    public Course Course { get; set; }
    public Student Student { get; set; }
}