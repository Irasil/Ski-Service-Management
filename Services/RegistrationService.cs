using Ski_Service_Management.Models;
using static System.Net.Mime.MediaTypeNames;
using System.Text.Json;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Threading;
using System.Numerics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;


namespace Ski_Service_Management.Services
{
    /// <summary>
    /// Klasse für die Registrationen
    /// </summary>
    public class RegistrationService : IRegistrationsService
    {

        public List<Registration> registrations { get; set; }
        public List<Status> status { get; set; }

        private readonly ManagementContext _managementContext;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="context"></param>
        public RegistrationService(ManagementContext context)
        {
            _managementContext = context;
        }

        /// <summary>
        /// Gibt alle Registrationen als DTO zurück
        /// </summary>
        /// <returns>gistrationen DTO</returns>
        public List<RegistrationModel> GetAll()
        {
            List<RegistrationModel> registrationModels = new List<RegistrationModel>();
            registrations = _managementContext.Registrations.Include("Priority").Include("Status").Include("Service").ToList();
            registrations.ForEach(r => registrationModels.Add(new RegistrationModel()
            {
                Id = r.Id,
                Name = r.Name,
                Email = r.Email,
                Phone = r.Phone,
                Service = r.Service.ServiceName,
                Created_Date = r.Created_Date,
                Pickup_Date = r.Pickup_Date,
                Priority = r.Priority.PriorityName,
                Status = r.Status.StatusName

            }));
         
            return registrationModels;
        }

        /// <summary>
        /// Registration als Entity der gesuchten Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Registration als Entity</returns>
        public Registration? GetId(int id)
        {
            //So habe ich die Exceptions getestet
            //var lol = 3;
            //var hey = lol / 0;
            registrations = _managementContext.Registrations.ToList();
            return registrations.FirstOrDefault(p => p.Id == id);
        }


        /// <summary>
        /// Registrationen als DTO der gesuchten Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Registration als DTO</returns>
        public RegistrationModel Get(int id)
        {
            List<RegistrationModel> t = GetAll();
            RegistrationModel r = t.Find(p => p.Id == id);
            if (r == null)
                return r;
            return new RegistrationModel()
            {
                Id = r.Id,
                Name = r.Name,
                Phone = r.Phone,
                Pickup_Date = r.Pickup_Date,
                Created_Date = r.Created_Date,
                Email = r.Email,
                Service = r.Service,
                Priority = r.Priority,
                Status = r.Status,
            };           
        }

        /// <summary>
        /// Fügt einen neue Registration hinzu
        /// </summary>
        /// <param name="registration">DTO von Registration</param>
        public void Add(RegistrationModel registration)
        {

            Registration newreg = new Registration()
            {
                Name = registration.Name,
                Email = registration.Email,
                Phone = registration.Phone,
                Created_Date = registration.Created_Date,
                Pickup_Date = registration.Pickup_Date,
                Service = _managementContext.Services.FirstOrDefault(e => e.ServiceName == registration.Service),
                Priority = _managementContext.Prioritys.FirstOrDefault(e => e.PriorityName == registration.Priority),
                Status = _managementContext.Status.FirstOrDefault(e => e.StatusName == registration.Status)
            };       

            _managementContext.Add(newreg);
            _managementContext.SaveChanges();            
        }

        /// <summary>
        /// Updatet eine vorhandene Registration
        /// </summary>
        /// <param name="id"></param>
        /// <param name="registration"></param>
        public void Update(int id, RegistrationStatusModel registration)
        {

            Registration reg = new Registration();
            reg = GetId(id);
            reg.Status = _managementContext.Status.FirstOrDefault(e => e.StatusName == registration.Status);

            _managementContext.Entry(reg).State = EntityState.Modified;
            _managementContext.SaveChanges();
        }


        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        //Hier bin ich mir nicht sicher was verlangt wird. Muss man nur den Status ändern können oder den gesammten Datensatz?
        // Wenn nur der Status, dann ist es wie Oben mit RegistrationStatusModel und wenn man den gesamten Datensatz ändern können soll,
        //ist es mit Registrations Model wie unten auskommentiert ist. Natürlich müsste ich den Kontroller und IRegistration anpassen.
        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

        
        //public void Update(int id, RegistrationModel registration)
        //{

        //    Registration reg = new Registration();
        //    reg = GetId(id);

        //    reg.Name = registration.Name;
        //    reg.Email = registration.Email;
        //    reg.Phone = registration.Phone;
        //    reg.Created_Date = registration.Created_Date;
        //    reg.Pickup_Date = registration.Pickup_Date;
        //    reg.Service = _managementContext.Services.FirstOrDefault(e => e.ServiceName == registration.Service);
        //    reg.Priority = _managementContext.Prioritys.FirstOrDefault(e => e.PriorityName == registration.Priority);
        //    reg.Status = _managementContext.Status.FirstOrDefault(e => e.StatusName == registration.Status);

        //    _managementContext.Entry(reg).State = EntityState.Modified;
        //    _managementContext.SaveChanges();
        //}

        /// <summary>
        /// Löscht eine Registration anhand ihrer Id
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            var registration = GetId(id);
            if (registration is null)
                return;

            _managementContext.Remove(registration);
            _managementContext.SaveChanges();
        }

    }
}

