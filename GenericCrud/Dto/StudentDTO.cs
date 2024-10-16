using GenericCrud.Dto.Base;
using GenericCrud.Filter.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenericCrud.Dto
{
    public class StudentDTO: BaseDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public int ClassroomID { get; set; }
        [NotMapped]
        public ClassroomDTO? Classroom { get; set; }

    }
}
