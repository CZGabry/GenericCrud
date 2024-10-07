using GenericCrud.Controllers.Base;
using GenericCrud.Dto;
using GenericCrud.Services.Interfaces.Base;
using Microsoft.AspNetCore.Mvc;

namespace GenericCrud.Controllers
{
    public class CourseController : BaseController<CourseDTO>
    {
        public CourseController(IService<CourseDTO> service) : base(service)
        {
        }
    }
}
