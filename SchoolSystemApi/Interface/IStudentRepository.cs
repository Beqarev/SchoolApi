using SchoolSystemApi.Data;
using SchoolSystemApi.Models;

namespace SchoolSystemApi;

public interface IStudentRepository
{
    ICollection<Student> GetStudents();
    Student GetStudent(int Id);
    bool StudentExists(int Id);
    bool CreateStudent(Student student);
    bool UpdateStudent(Student student);
    bool DeleteStudent(Student student);
    bool Save();
}