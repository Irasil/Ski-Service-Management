using Microsoft.EntityFrameworkCore;
using Ski_Service_Management.Models;

namespace Ski_Service_Management.Services
{
    /// <summary>
    /// Klasse für Priority
    /// </summary>
    public class PriorityService : IPriorityService
    {
        public List<PriorityModel> Priorities { get; set; }

        private readonly ManagementContext _managementContext;
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="context">DbContext</param>
        public PriorityService(ManagementContext context)
        {
            _managementContext = context;
        }
        
        /// <summary>
        /// Registrationen als DTO ausgeben, nach derer Priorität
        /// </summary>
        /// <returns></returns>
        public List<PriorityModel> GetAll()
        {
            List<Priority> priority = new List<Priority>();
            priority = _managementContext.Prioritys.Include("PriorityRegistration").Include("PriorityRegistration.Status").Include("PriorityRegistration.Service").ToList();

            List<PriorityModel> priorityModel = new List<PriorityModel>();

            foreach(var item in priority)
            {
                var prio = new PriorityModel();
                prio.Priority = item.PriorityName;
                foreach(var item2 in item.PriorityRegistration)
                {
                    RegistrationModel registration = new RegistrationModel();
                    registration.Id = item2.Id;
                    registration.Name = item2.Name;
                    registration.Email = item2.Email;
                    registration.Phone = item2.Phone;
                    registration.Created_Date = item2.Created_Date;
                    registration.Pickup_Date = item2.Pickup_Date;

                    registration.Priority = item.PriorityName;
                    registration.Service = item2.Service.ServiceName;
                    registration.Status = item2.Status.StatusName;

                    prio.Registration.Add(registration);
                }
                priorityModel.Add(prio);
            }
                       
            return priorityModel;

        }


    }
}
