using GenericCrud.Db.Config;
using GenericCrud.Db.Entities;
using GenericCrud.Repositories.Base;
using GenericCrud.Repositories.Interfaces;

namespace GenericCrud.Repositories
{
    public class StudentRepository : BaseRepository<Student>, IStudentRepository
    {
        public StudentRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
