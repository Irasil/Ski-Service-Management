using MessagePack;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ski_Service_Management.Models
{
    public class Status
    {        
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        
        public List<Registration> StatusRegistration { get; set; }      
        
    }
}
