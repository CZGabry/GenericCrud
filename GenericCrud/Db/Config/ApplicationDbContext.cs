using GenericCrud.Db.Entities;
using Microsoft.EntityFrameworkCore;

namespace GenericCrud.Db.Config
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Classroom> Classroom { get; set; }
        public DbSet<Course> Course { get; set; }
        public DbSet<Student> Student { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Many-to-Many relationship between Student and Course
            modelBuilder.Entity<Student>()
                .HasMany(s => s.Courses)
                .WithMany(c => c.Students)
                .UsingEntity(j => j.ToTable("StudentCourses"));  // Junction table for many-to-many

            // One-to-Many relationship between Classroom and Course
            modelBuilder.Entity<Classroom>()
                .HasMany(c => c.Courses)
                .WithOne(e => e.Classroom)
                .HasForeignKey(e => e.Id);  // Corrected ForeignKey

            // One-to-Many relationship between Classroom and Student
            modelBuilder.Entity<Classroom>()
                .HasMany(c => c.Students)
                .WithOne(e => e.Classroom)
                .HasForeignKey(e => e.ClassroomID);  // Corrected ForeignKey

            modelBuilder.Entity<Course>()
            .HasOne(c => c.Classroom)
            .WithMany(cl => cl.Courses)
            .HasForeignKey(c => c.ClassroomID);modelBuilder.Entity<Course>();
        }

    }
}
