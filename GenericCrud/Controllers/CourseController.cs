using GenericCrud.Controllers.Base;
using GenericCrud.Dto;
using GenericCrud.Services.Interfaces;

namespace GenericCrud.Controllers
{
    public class CourseController : BaseController<CourseDTO>
    {
        public CourseController(ICourseService service) : base(service)
        {
        }
    }
}
