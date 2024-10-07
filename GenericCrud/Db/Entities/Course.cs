using GenericCrud.Db.Base;

namespace GenericCrud.Db.Entities
{
    public class Course : BaseEntity
    {
        public string CourseName { get; set; }
        public string TeacherName { get; set; }
        public int ClassroomID { get; set; }
        public Classroom Classroom { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}
