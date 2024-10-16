using GenericCrud.Dto.Base;

namespace GenericCrud.Dto
{
    public class CourseDTO : BaseDTO
    {
        public string? CourseName { get; set; }
        public string? TeacherName { get; set; }
        public int ClassroomID { get; set; }
        public ClassroomDTO Classroom { get; set; }
    }
}
