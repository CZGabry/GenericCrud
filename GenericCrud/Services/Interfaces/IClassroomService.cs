using GenericCrud.Dto;
using GenericCrud.Filter;
using GenericCrud.Services.Interfaces.Base;

namespace GenericCrud.Services.Interfaces
{
    public interface IClassroomService : IService<ClassroomDTO, ClassroomFilter>
    {
    }
}
