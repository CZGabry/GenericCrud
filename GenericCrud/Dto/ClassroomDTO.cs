using GenericCrud.Dto.Base;

namespace GenericCrud.Dto
{
    public class ClassroomDTO: BaseDTO
    {
        public string? RoomNumber { get; set; }
        public int Capacity { get; set; }
    }
}
