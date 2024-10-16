using GenericCrud.Db.Base;

namespace GenericCrud.Db.Entities
{
    public class Classroom:BaseEntity
    {
        public string RoomNumber { get; set; }
        public int Capacity { get; set; }
        public string? RoomName { get; set; }
        public ICollection<Course> Courses { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}
