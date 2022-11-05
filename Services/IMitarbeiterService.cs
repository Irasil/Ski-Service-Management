using Microsoft.AspNetCore.Mvc;
using Ski_Service_Management.Models;

namespace Ski_Service_Management.Services
{
    public interface IMitarbeiterService
    {
        public JsonResult? ProveUser(Mitarbeiter mitarbeiter);
    }
}
