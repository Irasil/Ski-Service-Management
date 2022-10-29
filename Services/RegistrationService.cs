using Ski_Service_Management.Models;
using static System.Net.Mime.MediaTypeNames;
using System.Text.Json;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Threading;
using System.Numerics;
using Microsoft.EntityFrameworkCore;

namespace Ski_Service_Management.Services
{
    public static class RegistrationService
    {

        static List<Registration> registrations { get; set; }
        static List<Status> status { get; set; }
        static RegistrationService()
        {

            var context = new ManagementContext();
            status = context.Status.Include("registrations").ToList();

        }

        public static List<Status> GetAll() => status;

        public static Registration? Get(int id) => registrations.FirstOrDefault(p => p.Id == id);


        public static void Add(Registration registration)
        {
            var context = new ManagementContext();
            registrations = context.Registrations.ToList();
            context.Add(registration);
            context.SaveChanges();
        }

        public static void Update(Registration registration)
        {
            var context = new ManagementContext();
            registrations = context.Registrations.ToList();
            var index = registrations.FindIndex(p => p.Id == registration.Id);
            if (index == -1)
                return;           

            context.Remove(registrations[index]);
            context.Add(registration);
            context.SaveChanges();
        }

        public static void Delete(int id)
        {
            var context = new ManagementContext();
            registrations = context.Registrations.ToList();
            var registration = Get(id);
            if (registration is null)
                return;

            context.Remove(registration);
            context.SaveChanges();
        }

    }
}

