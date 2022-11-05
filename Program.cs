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


        var loggerFromSettings = new LoggerConfiguration()
                       .ReadFrom.Configuration(builder.Configuration)
                       .Enrich.FromLogContext()
                       .CreateLogger();

        builder.Logging.ClearProviders();
        builder.Logging.AddSerilog(loggerFromSettings);
       
        
        builder.Services.AddDbContext<ManagementContext>(options =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString("MovieDB")));
        var lol = builder.Configuration.GetConnectionString("MovieDB");
        var streamReader = new StreamReader("TextFile.txt");
        var line = streamReader.ReadLine();

        var streamReader1 = new StreamReader("TextFile1.txt");
        var line1 = streamReader1.ReadLine();

        var provider = DataProtectionProvider.Create(line);
        var protector = provider.CreateProtector(line);
        
        string deencrypted = protector.Unprotect(line1);
        var hey = string.Format(lol, deencrypted);
         
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