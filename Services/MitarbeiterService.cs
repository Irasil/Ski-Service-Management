using IdentityServer4.Services;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Ski_Service_Management.Models;
using Microsoft.AspNetCore.Authorization;




namespace Ski_Service_Management.Services
{
    public class MitarbeiterService : IMitarbeiterService
    {
        public List<Mitarbeiter> mitarbeiters;
        private readonly ManagementContext _managementContext;
        private readonly ITokenService _tokenService;
        public MitarbeiterService(ManagementContext context, ITokenService tokenService)
        {
            _managementContext = context;
            _tokenService = tokenService;

        }

        public JsonResult? ProveUser(Mitarbeiter mitarbeiter)
        {
            //var lol = 3;
            //var hey = lol / 0;
            mitarbeiters = _managementContext.Mitarbeiters.ToList();
            foreach(var m in mitarbeiters)
            {
                if (m.Name == mitarbeiter.Name && m.password == mitarbeiter.password)
                {
                    return new JsonResult(new { userName = mitarbeiter.Name, token = _tokenService.CreateToken(mitarbeiter.Name) });
                }                
            }
            return null;
        }
    }
    
}
