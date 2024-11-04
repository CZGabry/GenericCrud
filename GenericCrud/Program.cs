using GenericCrud.Db.Config;
using GenericCrud.Repositories.Interfaces;
using GenericCrud.Repositories;
using GenericCrud.Services;
using Microsoft.EntityFrameworkCore;
using GenericCrud.Repositories.Interfaces.Base;
using GenericCrud.Repositories.Base;
using GenericCrud.AutoMapper;
using GenericCrud.Services.Interfaces;
using Newtonsoft.Json.Serialization;
using GenericCrud.ModelBinding;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

var snakeCaseContractResolver = new DefaultContractResolver
{ NamingStrategy = new SnakeCaseNamingStrategy() };


builder.Services.AddControllers(opt =>
{
    // Set a custom metadata binding for DTO filter object properties.
    opt.ModelMetadataDetailsProviders.Add(new SnakeCaseBindingMetadataProvider());

    // Set a custom model binding for DTO filter object.
    //opt.ModelBinderProviders.Insert(0, new FilterBinderProvider());
    //opt.ModelBinderProviders.Insert(0, new DateOnlyModelBinderProvider());

    //// Add a custom filter to manage some unhandled exceptions.
    //opt.Filters.Add(typeof(CustomExceptionFilter));

    // Disable the default value of  not treat null result values as 204 No Content.
    var noContentFormatter =
        opt.OutputFormatters.OfType<HttpNoContentOutputFormatter>().FirstOrDefault();
    if (noContentFormatter != null) noContentFormatter.TreatNullValueAsNoContent = false;

    // Set some profiles for HTTP caching configurations used in the controllers.
    opt.CacheProfiles.Add("15Minutes", new CacheProfile { Duration = 900 });
    opt.CacheProfiles.Add("2HoursProfile", new CacheProfile { Duration = 7200 });
})
    .AddNewtonsoftJson(options =>
    {
    options.SerializerSettings.ContractResolver = new DefaultContractResolver()
    {
        // Convert property names to snake_case when serializing JSON. Default was camelCase.
        NamingStrategy = new SnakeCaseNamingStrategy()
    };
    options.SerializerSettings.ContractResolver = snakeCaseContractResolver;

    // Serialize enum to their string value, instead of the numeric value.
    //options.SerializerSettings.Converters.Add(new StringEnumConverter());

    //options.SerializerSettings.Converters.Add(new NullableDateOnlyJsonConverter());
    //options.SerializerSettings.Converters.Add(new DateOnlyJsonConverter());

    //options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
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
