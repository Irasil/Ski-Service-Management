using IdentityServer4.Services;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Ski_Service_Management.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Ski_Service_Management.Services
{
    /// <summary>
    /// Klasse für die Verifikation der Mitarbeiter
    /// </summary>
    public class MitarbeiterService : IMitarbeiterService
    {
        public int counter;
        public List<Mitarbeiter> mitarbeiters;
        private readonly ManagementContext _managementContext;
        private readonly ITokenService _tokenService;
        public MitarbeiterService(ManagementContext context, ITokenService tokenService)
        {
            _managementContext = context;
            _tokenService = tokenService;

        }



        /// <summary>
        /// Mitarbeiter Autorisation,
        /// </summary>
        /// <param name="mitarbeiter"></param>
        /// <returns></returns>
        public JsonResult? ProveUser(Mitarbeiter mitarbeiter)
        {
            //var lol = 3;
            //var hey = lol / 0;

            //Hier ist es ein "Gebastel" ich wusste nicht wie ich es mit dem JsonResult besser machen könnte.
            
            mitarbeiters = _managementContext.Mitarbeiters.ToList();
            foreach(var m in mitarbeiters)
            {
                if (m.Name == mitarbeiter.Name && m.password == mitarbeiter.password)
                {
                    return new JsonResult(new { userName = mitarbeiter.Name, token = _tokenService.CreateToken(mitarbeiter.Name) });
                } else if (m.Name == mitarbeiter.Name && m.password != mitarbeiter.password)
                {
                    m.Counter += 1;
                    _managementContext.SaveChanges(mitarbeiter.Counter == m.Counter);
                    if (m.Counter >= 3)
                    {
                        return new JsonResult(new { gespert = m.Counter });
                    }
                }              
            } 
            return new JsonResult(new { falsch = "Falsch" });
        }

        /// <summary>
        /// Mitarbeiter Freigeben 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Mitarbeiter Deblocker(int id)
        {
           Mitarbeiter? mitarbeiter = _managementContext.Mitarbeiters.FirstOrDefault(e => e.Id == id);
            if (mitarbeiter != null)
            {
                mitarbeiter.Counter = 0;
                _managementContext.Entry(mitarbeiter).State = EntityState.Modified;
                _managementContext.SaveChanges();
                return mitarbeiter;
            }
            return null;
        }


        /// <summary>
        /// Ausgabe aller Mitarbeiter
        /// </summary>
        /// <returns></returns>
        public List<Mitarbeiter> AllMitarbeiter()
        {
            mitarbeiters = _managementContext.Mitarbeiters.ToList();
            return mitarbeiters;
        }
    }
    
}
