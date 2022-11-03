using Ski_Service_Management.Models;

namespace Ski_Service_Management.Services
{
    public interface IRegistrationsService
    {


        public List<RegistrationModel> GetAll();


        //public Registration? Get(int id);
        public RegistrationModel? Get(int id);


        public void Add(RegistrationModel registration);


        public void Update(Registration registration);


        public void Delete(int id);

    }
}
