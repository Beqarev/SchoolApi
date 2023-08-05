namespace SchoolSystemApi.Models;

public class Course
{
    public int Id { get; set; }
    public string CourseName { get; set; }
    public Teacher Teacher { get; set; }
}