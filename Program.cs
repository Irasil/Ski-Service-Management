using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Ski_Service_Management.Models;
using Ski_Service_Management.Services;
using System.Text;

internal class Program
{
   
    public static void Main(string[] args)
    {

       
        var builder = WebApplication.CreateBuilder(args);

        // Logger
        var loggerFromSettings = new LoggerConfiguration()
                       .ReadFrom.Configuration(builder.Configuration)
                       .Enrich.FromLogContext()
                       .CreateLogger();

        builder.Logging.ClearProviders();
        builder.Logging.AddSerilog(loggerFromSettings);

        // Entschlüsseln des vorgefertgten Passwort, funktioniert erst nach dem man es eingerichtet hat
        var key = builder.Configuration.GetValue<string>("Encryption:Key");
        var provider = DataProtectionProvider.Create(key);
        var protector = provider.CreateProtector(key);
        var pw = builder.Configuration.GetValue<string>("Encryption:Password");
        var con = builder.Configuration.GetConnectionString("MovieDB");
        

        // Vebindung zu dem SQL Server mithilfe von dem decodierten Passwort
        builder.Services.AddDbContext<ManagementContext>(options =>
                    options.UseSqlServer(string.Format(con, protector.Unprotect(pw))));

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

        builder.Services.AddDataProtection();

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
    }
}