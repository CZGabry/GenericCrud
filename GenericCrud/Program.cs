using GenericCrud.Db.Config;
using GenericCrud.Repositories.Interfaces;
using GenericCrud.Repositories;
using GenericCrud.Services;
using Microsoft.EntityFrameworkCore;
using GenericCrud.Repositories.Interfaces.Base;
using GenericCrud.Repositories.Base;
using GenericCrud.AutoMapper;
using GenericCrud.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    });
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
builder.Services.AddScoped<IClassroomService, ClassroomService>();

builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<ICourseService, CourseService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontendOrigin", builder =>
        builder.WithOrigins("http://localhost:5173") // Adjust the port if necessary
               .AllowAnyMethod()
               .AllowAnyHeader()
               .WithExposedHeaders("X-Total-Count"));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseCors("AllowFrontendOrigin");
app.UseAuthorization();

app.MapControllers();

app.Run();
