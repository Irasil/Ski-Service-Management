using IdentityServer4.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ski_Service_Management.Models;
using Ski_Service_Management.Services;

namespace Ski_Service_Management.Controllers
{
    /// <summary>
    /// Kontroller für die Verbindung zu der Tabelle Mitarbeiter
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class MitarbeiterController : ControllerBase
    {
        private readonly ILogger<RegistrationController> _logger;
        private readonly IMitarbeiterService _mitarbeiterService;
        public MitarbeiterController(IMitarbeiterService mitarbeiterService, ILogger<RegistrationController> logger)
        {

            _logger = logger;
            _mitarbeiterService = mitarbeiterService;
        }

        /// <summary>
        /// Methode um die Eingaben der Mitarbeiter an den Service weiter zu leiten
        /// </summary>
        /// <param name="model">Eingaben des Mitarbeiters</param>
        /// <returns>Ein JWT / Faschle eingaben / User ist blockiert</returns>
        [HttpPost]
        public IActionResult Login([FromBody] Mitarbeiter model)
        {
            try
            {
                
                JsonResult? json = _mitarbeiterService.ProveUser(model);
                string? lol = json.Value.ToString();
                bool hey = false;   
                hey = lol.Contains("gespert");
                bool nu = false;
                nu = lol.Contains("Hey");


                if (json != null && hey == false && nu == false)
                    return Ok(json);
                else if (json != null && hey == true)
                {
                    return BadRequest("user is blocked");
                }
                else
                {
                    return BadRequest("Invalid Credentials");
                }
                    
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Warning --> {ex.Message}");
                return NotFound($"Warning --> {ex.Message}");
            }
            
        }
    }
}
