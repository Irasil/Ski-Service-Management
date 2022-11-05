namespace Ski_Service_Management.Models
{
    public class PriorityModel
    {
        public string Priority { get; set; }
        
        public List<RegistrationModel> Registration { get; set; } = new List<RegistrationModel>();  
        

        
    }
}
