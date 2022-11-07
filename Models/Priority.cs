namespace Ski_Service_Management.Models
{
    /// <summary>
    /// Inhalt der Tabelle Priority, mit FK zu Registration
    /// </summary>
    public class Priority
    {
        public int PriorityId { get; set; }
        public string PriorityName { get; set; }

        public List<Registration> PriorityRegistration { get; set; }
    }
}
