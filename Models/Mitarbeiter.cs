using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;

namespace Ski_Service_Management.Models
{
    /// <summary>
    /// Inhalt der Tabelle Mitarbeiter
    /// </summary>
    public class Mitarbeiter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string password { get; set; }
        public int Counter{ get; set; }
    }
}
