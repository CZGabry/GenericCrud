using GenericCrud.Controllers.Base;
using GenericCrud.Dto;
using GenericCrud.Services.Interfaces.Base;
using Microsoft.AspNetCore.Mvc;

namespace GenericCrud.Controllers
{
    public class ClassroomController : BaseController<ClassroomDTO>
    {
        public ClassroomController(IService<ClassroomDTO> service) : base(service)
        {
        }
    }
}
