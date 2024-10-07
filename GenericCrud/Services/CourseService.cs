using AutoMapper;
using GenericCrud.Db.Entities;
using GenericCrud.Dto;
using GenericCrud.Repositories.Interfaces.Base;
using GenericCrud.Services.Base;
using GenericCrud.Services.Interfaces;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace GenericCrud.Services
{
    public class CourseService : Service<CourseDTO, Course>, ICourseService
    {
        public CourseService(IRepository<Course> repository, IMapper mapper)
            : base(repository, mapper)
        {
        }

    }
}
