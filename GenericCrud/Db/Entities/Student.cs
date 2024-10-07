using GenericCrud.Db.Base;

namespace GenericCrud.Db.Entities
{
    public class Student: BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public int ClassroomID { get; set; }
        public Classroom Classroom { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}
