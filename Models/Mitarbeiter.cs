using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
//using Newtonsoft.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Ski_Service_Management.Models
{
    /// <summary>
    /// Inhalt der Tabelle Mitarbeiter
    /// </summary>
    public class Mitarbeiter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public string password { get; set; }
        public int Counter{ get; set; }
    }
}
