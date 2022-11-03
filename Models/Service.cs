namespace Ski_Service_Management.Models
{
    public class Service
    {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }

        public List<Registration> ServiceRegistration { get; set; }
    }
}
