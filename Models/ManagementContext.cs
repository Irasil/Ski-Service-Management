using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Ski_Service_Management.Models
{
    public class ManagementContext : DbContext
    {
        public DbSet<Mitarbeiter> Mitarbeiters { get; set; }

        public DbSet<Registration> Registrations { get; set; }

        public ManagementContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile($"appsettings.json").Build();

            //var config = configuration.Build();
            string lol = configuration.GetConnectionString("DB1");
            //optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-HM7PD66;Database=EFCoreCodeFirst;Trusted_Connection=True");
            optionsBuilder.UseSqlServer(lol);
        }

    }
}
