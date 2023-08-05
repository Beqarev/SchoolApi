using SchoolSystemApi.Models;

namespace SchoolSystemApi;

public interface ICourseAndStudentRepository
{
    ICollection<CourseAndStudent> GetCoursesAndStudents();
    CourseAndStudent GetCourseAndStudent(int Id);
    bool CourseAndStudentExists(int Id);
    bool CreateCourseAndStudents(CourseAndStudent courseAndStudent);
    bool UpdateCourseAndStudents(CourseAndStudent courseAndStudent);
    bool DeleteCourseAndStudents(CourseAndStudent courseAndStudent);
    bool Save();
}