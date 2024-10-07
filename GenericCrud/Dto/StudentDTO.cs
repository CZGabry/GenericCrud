using GenericCrud.Dto.Base;

namespace GenericCrud.Dto
{
    public class StudentDTO: BaseDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public int ClassroomID { get; set; }
    }
}
