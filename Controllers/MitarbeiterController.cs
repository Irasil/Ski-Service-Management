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
                string? auswertung = json.Value.ToString();
                bool gespert = false;
                gespert = auswertung.Contains("gespert");
                bool falsch = false;
                falsch = auswertung.Contains("Falsch");

                if (gespert == false && falsch == false)
                    return Ok(json);
                else if (gespert == true)
                {
                    return BadRequest("User ist blockiert");
                }
                else
                {
                    return BadRequest("User oder Passwort sind falsch");
                }                    
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Warning --> {ex.Message}");
                return NotFound($"Warning --> {ex.Message}");
            }            
        }

        /// <summary>
        /// Kontroller um Mitarbeiter freizuschalten
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Deblock( int id)
        {
            try
            {
                var mitarbeiter = _mitarbeiterService.Deblocker(id);
                if(mitarbeiter != null)
                {
                    return Ok("Mitarbeiter wurde wieder freigegeben");
                }
                return Ok("Mitarbeiter konnte nicht freigegeben werden oder er existiern nicht");
            }catch (Exception ex)
            {
                _logger.LogWarning($"Warning --> {ex.Message}");
                return NotFound($"Warning --> {ex.Message}");
            }
        }
    }
}
