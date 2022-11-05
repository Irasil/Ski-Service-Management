using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Ski_Service_Management.Models;
using Ski_Service_Management.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


var loggerFromSettings = new LoggerConfiguration()
               .ReadFrom.Configuration(builder.Configuration)
               .Enrich.FromLogContext()
               .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(loggerFromSettings);


builder.Services.AddDbContext<ManagementContext>(options => 
            options.UseSqlServer(builder.Configuration.GetConnectionString("MovieDB")));

// Add services to the container.
builder.Services.AddScoped<IRegistrationsService, RegistrationService>();
builder.Services.AddScoped<IPriorityService, PriorityService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IMitarbeiterService, MitarbeiterService>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "JWT", Version = "v1" });
});

//
// JWT
//
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
