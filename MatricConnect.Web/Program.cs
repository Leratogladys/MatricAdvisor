// Programmer name : Lerato Molefe
// Project name    : Matric Connect
// Purpose         : Configures the MVC website, shared services,
//                   repositories, database, and request pipeline.

using MatricConnect.Core.Interfaces;
using MatricConnect.Data;
using MatricConnect.Infrastructure.Repositories;
using MatricConnect.Services;
using MatricConnect.Validation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Register MVC controllers and views.
builder.Services.AddControllersWithViews();

// Configure the same SQLitem database used by the console application
builder.Services.AddDbContext<MatricConnectContext>(options =>
   options.UseSqlite(DatabaseConfiguration.GetConnectionString()));

// Register repository interfaces and thier database implementations.
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IUniversityRepository, UniversityRepository>();
builder.Services.AddScoped<ISavedProgrammeRepository, SavedProgrammeRepository>();


// Register shared application services and validation.
builder.Services.AddScoped<StudentService>();
builder.Services.AddScoped<UniversityService>();
builder.Services.AddScoped<SavedProgrammeService>();
builder.Services.AddScoped<EligibilityService>();
builder.Services.AddScoped<StudentValidator>();

var app = builder.Build();  

// Apply existing migrations ans initialise data during local development.
if(app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();

    var context = scope.ServiceProvider.GetRequiredService<MatricConnectContext>();

    context.Database.Migrate();

    DbInitializer.Seed(context);
} // end if

// Configure production error handling and HTTPS security.
if(!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
} // end if

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();