using GenericCrud.Controllers.Base;
using GenericCrud.Dto;
using GenericCrud.Services.Interfaces.Base;
using Microsoft.AspNetCore.Mvc;

namespace GenericCrud.Controllers
{
    public class StudentsController : BaseController<StudentDTO>
    {
        public StudentsController(IService<StudentDTO> service) : base(service)
        {
        }
    }
}
