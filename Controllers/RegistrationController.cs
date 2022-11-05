using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ski_Service_Management.Models;
using Ski_Service_Management.Services;

namespace Ski_Service_Management.Controllers
{
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


        [HttpPut("{id}")]
        public IActionResult Update(int id, RegistrationModel model)
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
