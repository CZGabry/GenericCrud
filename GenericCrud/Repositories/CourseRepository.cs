using GenericCrud.Db.Config;
using GenericCrud.Db.Entities;
using GenericCrud.Repositories.Base;
using GenericCrud.Repositories.Interfaces;

namespace GenericCrud.Repositories
{
    public class CourseRepository : BaseRepository<Course>, ICourseRepository
    {
        public CourseRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
