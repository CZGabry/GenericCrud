using AutoMapper;
using GenericCrud.Db.Entities;
using GenericCrud.Dto;

namespace GenericCrud.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Student, StudentDTO>();
            CreateMap<Course, CourseDTO>();
            CreateMap<Classroom, ClassroomDTO>();
            CreateMap<StudentDTO, Student>();
            CreateMap<CourseDTO, Course>();
            CreateMap<ClassroomDTO, Classroom>();
        }
    }
}
