using GenericCrud.Controllers.Base;
using GenericCrud.Dto;
using GenericCrud.Filter;
using GenericCrud.Services.Interfaces;

namespace GenericCrud.Controllers
{
    public class CourseController : BaseController<CourseDTO, CourseFilter>
    {
        public CourseController(ICourseService service) : base(service)
        {
        }
    }
}
