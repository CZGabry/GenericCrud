using GenericCrud.Controllers.Base;
using GenericCrud.Dto;
using GenericCrud.Services.Interfaces;

namespace GenericCrud.Controllers
{
    public class StudentsController : BaseController<StudentDTO>
    {
        public StudentsController(IStudentService service) : base(service)
        {
        }
    }
}
