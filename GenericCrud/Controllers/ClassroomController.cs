using GenericCrud.Controllers.Base;
using GenericCrud.Dto;
using GenericCrud.Filter;
using GenericCrud.Services.Interfaces;

namespace GenericCrud.Controllers
{
    public class ClassroomController : BaseController<ClassroomDTO, ClassroomFilter>
    {
        public ClassroomController(IClassroomService service) : base(service)
        {
        }
    }
}
