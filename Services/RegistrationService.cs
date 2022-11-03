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
using AutoMapper;

namespace Ski_Service_Management.Services
{
    public class RegistrationService : IRegistrationsService
    {

        public List<Registration> registrations { get; set; }
        public List<Status> status { get; set; }

        private readonly ManagementContext _managementContext;
        public RegistrationService(ManagementContext context)
        {
            _managementContext = context;
        }


        //public static List<Registration> GetAll() => registrations;
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

        public Registration? GetId(int id)
        {
            registrations = _managementContext.Registrations.ToList();
            return registrations.FirstOrDefault(p => p.Id == id);
        }



        public RegistrationModel Get(int id)
        {
            List<RegistrationModel> t = GetAll();
            RegistrationModel r = t.Find(p => p.Id == id);
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

        public void Update(int id, RegistrationModel registration)
        {

            Registration reg = new Registration();
            reg = GetId(id);



            reg.Name = registration.Name;
            reg.Email = registration.Email;
            reg.Phone = registration.Phone;
            reg.Created_Date = registration.Created_Date;
            reg.Pickup_Date = registration.Pickup_Date;
            reg.Service = _managementContext.Services.FirstOrDefault(e => e.ServiceName == registration.Service);
            reg.Priority = _managementContext.Prioritys.FirstOrDefault(e => e.PriorityName == registration.Priority);
            reg.Status = _managementContext.Status.FirstOrDefault(e => e.StatusName == registration.Status);

            _managementContext.Entry(reg).State = EntityState.Modified;
            _managementContext.SaveChanges();
        }

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

