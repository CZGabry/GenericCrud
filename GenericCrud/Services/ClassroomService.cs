using AutoMapper;
using GenericCrud.Db.Entities;
using GenericCrud.Dto;
using GenericCrud.Filter;
using GenericCrud.Repositories.Interfaces.Base;
using GenericCrud.Services.Base;
using GenericCrud.Services.Interfaces;

namespace GenericCrud.Services
{
    public class ClassroomService : Service<ClassroomDTO, ClassroomFilter,  Classroom>, IClassroomService
    {
        public ClassroomService(IRepository<Classroom> repository, IMapper mapper)
            : base(repository, mapper)
        {
        }

    }
}
