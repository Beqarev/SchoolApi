using AutoMapper;
using SchoolSystemApi.Dto;
using SchoolSystemApi.Models;

namespace SchoolSystemApi.Helper;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Course, CourseDto>();
        CreateMap<CourseDto, Course>();
        CreateMap<Student, StudentDto>();
        CreateMap<StudentDto, Student>();
        CreateMap<Teacher, TeacherDto>();
        CreateMap<TeacherDto, Teacher>();
        CreateMap<CourseAndStudentDto, CourseAndStudent>();
        CreateMap<CourseAndStudent, CourseAndStudentDto>();
        CreateMap<ReportCardDto, ReportCard>();
        CreateMap<ReportCard, ReportCardDto>();
        CreateMap<SetReportCardDto, ReportCard>();
    }
}