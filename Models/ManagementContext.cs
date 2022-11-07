using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;


namespace Ski_Service_Management.Models
{
    /// <summary>
    /// Erstellen der Tabellen und Verbindung zu denen 
    /// </summary>
    public class ManagementContext : DbContext
    {
        public DbSet<Mitarbeiter> Mitarbeiters { get; set; }

        public DbSet<Registration> Registrations { get; set; }

        public DbSet<Status> Status { get; set; }

        public DbSet<Priority> Prioritys { get; set; }
        public DbSet<Service> Services { get; set; }


        public ManagementContext()
        {
            
        }

        public ManagementContext(DbContextOptions<ManagementContext> options)
    : base(options)
        {
        }
    }
}
