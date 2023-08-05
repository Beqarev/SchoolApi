using SchoolSystemApi.Models;

namespace SchoolSystemApi;

public interface ITeacherRepository
{
    ICollection<Teacher> GetTeachers();
    Teacher GetTeacher(int Id);
    bool TeacherExists(int Id);
    bool CreateTeacher(Teacher teacher);
    bool UpdateTeacher(Teacher teacher);
    bool DeleteTeacher(Teacher teacher);
    bool Save();
}