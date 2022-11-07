namespace Ski_Service_Management.Models
{
    /// <summary>
    /// Inhalt der Tabelle Service, mit FK zu Registration
    /// </summary>
    public class Service
    {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }

        public List<Registration> ServiceRegistration { get; set; } = new List<Registration>();
    }
}
