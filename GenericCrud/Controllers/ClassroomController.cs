using GenericCrud.Controllers.Base;
using GenericCrud.Dto;
using GenericCrud.Services.Interfaces;

namespace GenericCrud.Controllers
{
    public class ClassroomController : BaseController<ClassroomDTO>
    {
        public ClassroomController(IClassroomService service) : base(service)
        {
        }
    }
}
