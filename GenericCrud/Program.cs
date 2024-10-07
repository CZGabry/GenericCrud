using GenericCrud.Db.Config;
using GenericCrud.Repositories.Interfaces;
using GenericCrud.Repositories;
using GenericCrud.Services.Interfaces;
using GenericCrud.Services;
using Microsoft.EntityFrameworkCore;
using GenericCrud.Repositories.Interfaces.Base;
using GenericCrud.Repositories.Base;
using GenericCrud.AutoMapper;
using GenericCrud.Dto;
using GenericCrud.Services.Interfaces.Base;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("EntitiesTestDatabase");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(connectionString));

builder.Services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IStudentService, StudentService>();

builder.Services.AddScoped<IClassroomRepository, ClassroomRepository>();
builder.Services.AddScoped<IService<ClassroomDTO>, ClassroomService>();

builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<IService<CourseDTO>, CourseService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
