namespace SchoolSystemApi.Models;

public class ReportCard
{
    public int Id { get; set; }
    public CourseAndStudent CourseAndStudent { get; set; }
    public int Mark { get; set; }
}