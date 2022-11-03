namespace Ski_Service_Management.Models
{
    public class Priority
    {
        public int PriorityId { get; set; }
        public string PriorityName { get; set; }

        public List<Registration> PriorityRegistration { get; set; }
    }
}
