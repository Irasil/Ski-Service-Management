using IdentityServer4.Services;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Ski_Service_Management.Models;
using Microsoft.AspNetCore.Authorization;




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
            
            mitarbeiters = _managementContext.Mitarbeiters.ToList();
            foreach(var m in mitarbeiters)
            {
                if (m.Name == mitarbeiter.Name && m.password == mitarbeiter.password)
                {
                    m.Counter = 0;
                    _managementContext.SaveChanges(mitarbeiter.Counter == m.Counter);
                    return new JsonResult(new { userName = mitarbeiter.Name, token = _tokenService.CreateToken(mitarbeiter.Name) });
                } else if (m.Name == mitarbeiter.Name && m.password != mitarbeiter.password)
                {
                    m.Counter += 1;
                    _managementContext.SaveChanges(mitarbeiter.Counter == m.Counter);
                    if (m.Counter >= 3)
                    {
                        JsonResult gespert = new JsonResult(new {gespert = m.Counter});
                        return gespert;
                    }
                }              
            } 
             JsonResult nu = new JsonResult(new { hey = "Hey" });
            return nu;
        }
    }
    
}
