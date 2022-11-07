namespace Ski_Service_Management.Models
{
    /// <summary>
    /// Modelklasse Registration für DTO
    /// </summary>
    public class RegistrationModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }        
        public DateTime Created_Date { get; set; }
        public DateTime Pickup_Date { get; set; }

        public string Service { get; set; }
        public string Priority { get; set; }
       
        public string Status { get; set; }
    }
}
