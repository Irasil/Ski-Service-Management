using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ski_Service_Management.Models;
using Ski_Service_Management.Services;

namespace Ski_Service_Management.Controllers
{

    /// <summary>
    /// Kontroller für alle CRUD Operationen für die Registrationen
    /// </summary>
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly ILogger<RegistrationController> _logger;
        private readonly IRegistrationsService _registrationsService;
        public RegistrationController(IRegistrationsService registrationsService, ILogger<RegistrationController> logger)
        {
            _registrationsService = registrationsService;
            _logger = logger;
        }

        /// <summary>
        /// Alle Registrationen
        /// </summary>
        /// <returns>Liste aller Registrationen</returns>
        [HttpGet]        
        public ActionResult<List<RegistrationModel>> GetAll()
        {
            try
            {
               return _registrationsService.GetAll();
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Warning --> {ex.Message}");
                return NotFound($"Warning --> {ex.Message}");
            }

        }
       
        /// <summary>
        /// Neuen Einträge für Registrationen, ohnen Autorisierung möglich
        /// </summary>
        /// <param name="registration"></param>
        /// <returns>Die Registration die gemacht wurde</returns>
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Create(RegistrationModel registration)
        {
            try
            {
                _registrationsService.Add(registration);
                return CreatedAtAction(nameof(Create), new { id = registration.Id }, registration);
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Warning --> {ex.Message}");
                return NotFound($"Warning --> {ex.Message}");
            }
        }

        /// <summary>
        /// Registration nach Id
        /// </summary>
        /// <param name="id">Id von der Registration</param>
        /// <returns>Die Registration mit der gesuchten Id</returns>
        [HttpGet("{id}")]
        public ActionResult<RegistrationModel> Get(int id)
        {
            try
            {
            if (_registrationsService.GetAll() == null)
                return NotFound();
            return _registrationsService.Get(id);
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Warning --> {ex.Message}");
                return NotFound($"Warning --> {ex.Message}");
            }

        }

        /// <summary>
        /// Registration updaten
        /// </summary>
        /// <param name="id">Id von der Registration</param>
        /// <param name="model">Änderungen für dieser Eintrag</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Update(int id, RegistrationStatusModel model)
        {

            try
            {
                _registrationsService.Update(id, model);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Warning --> {ex.Message}");
                return NotFound($"Warning --> {ex.Message}");
            }
        }    

        /// <summary>
        /// Löscht einen Eintrag
        /// </summary>
        /// <param name="id">Id des zu löschenden Eintrag</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var registration = _registrationsService.Get(id);

                if (registration is null)
                    return NotFound();

                _registrationsService.Delete(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Warning --> {ex.Message}");
                return NotFound($"Warning --> {ex.Message}");
            }

           
        }
    }
}
