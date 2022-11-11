using Microsoft.AspNetCore.Mvc;
using Ski_Service_Management.Models;

namespace Ski_Service_Management.Services
{
    /// <summary>
    /// Interface für MitarbeiterService
    /// </summary>
    public interface IMitarbeiterService
    {
        public JsonResult? ProveUser(Mitarbeiter mitarbeiter);
        public Mitarbeiter Deblocker(int id);
        public List<Mitarbeiter> AllMitarbeiter();
    }
}
