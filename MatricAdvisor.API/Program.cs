using Microsoft.Extensions.Options;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MatricAdvisor.API.Data;



var builder = WebApplication.CreateBuilder(args);

// --- ADD SERVICES ---
builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(Options =>
      Options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
   options.AddPolicy("AllowReactApp", policy =>
   {
      policy.WithOrigins("http://localhost:3000") 
            .AllowAnyHeader()
            .AllowAnyMethod(); 
   });
});

//Swagger tool shows all API endpoints in browser
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// --- APP BUILD --- 

var app = builder.Build();

// --- Configuring Middleware ---

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowReactApp");
//app.UseAuthorization();
app.MapControllers();


app.Run();