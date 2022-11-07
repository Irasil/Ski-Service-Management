using Ski_Service_Management.Models;

namespace Ski_Service_Management.Services
{
    /// <summary>
    /// INteface für RegistrationsService
    /// </summary>
    public interface IRegistrationsService
    {


        public List<RegistrationModel> GetAll();


        //public Registration? Get(int id);
        public RegistrationModel? Get(int id);
        public Registration? GetId(int id);


        public void Add(RegistrationModel registration);


        public void Update(int id, RegistrationModel model);


        public void Delete(int id);

    }
}
