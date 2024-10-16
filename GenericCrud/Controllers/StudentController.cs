using GenericCrud.Controllers.Base;
using GenericCrud.Dto;
using GenericCrud.Filter;
using GenericCrud.Services.Interfaces;

namespace GenericCrud.Controllers
{
    public class StudentsController : BaseController<StudentDTO, StudentFilter>
    {
        public StudentsController(IStudentService service) : base(service)
        {
        }
    }
}
