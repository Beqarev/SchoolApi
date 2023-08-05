using SchoolSystemApi.Models;

namespace SchoolSystemApi;

public interface ICoursesRepository
{
    ICollection<Course> GetCourses();
    Course GetCourse(int Id);
    bool CourseExists(int Id);
    bool CreateCoures(Course course);
    bool UpdateCourse(Course course);
    bool DeleteCourse(Course course);
    bool Save();

}