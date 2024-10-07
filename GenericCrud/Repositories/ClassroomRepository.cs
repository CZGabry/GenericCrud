using GenericCrud.Db.Config;
using GenericCrud.Db.Entities;
using GenericCrud.Repositories.Base;
using GenericCrud.Repositories.Interfaces;

namespace GenericCrud.Repositories
{
    public class ClassroomRepository : BaseRepository<Classroom>, IClassroomRepository
    {
        public ClassroomRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
