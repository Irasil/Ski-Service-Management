using Microsoft.EntityFrameworkCore;
using Ski_Service_Management.Models;
using Ski_Service_Management.Services;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ManagementContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("MovieDB")));

// Add services to the container.
builder.Services.AddScoped<IRegistrationsService, RegistrationService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
