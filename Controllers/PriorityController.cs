using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ski_Service_Management.Models;
using Ski_Service_Management.Services;

namespace Ski_Service_Management.Controllers
{
    
    /// <summary>
    /// Kontroller für das Abrufen der Aufträge nach ihrer Priorität
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class PriorityController : Controller
    {
        private readonly ILogger<RegistrationController> _logger;
        private readonly IPriorityService _priorityService;
        public PriorityController(IPriorityService priorityService, ILogger<RegistrationController> logger)
        {
            _priorityService = priorityService;
            _logger = logger;   
        }

        /// <summary>
        /// Liste aller Registrationen geordnet nach derer Priorität
        /// </summary>
        /// <returns>Liste der Registrationen geordnet nach Priorität</returns>
        [HttpGet]
        public ActionResult<List<PriorityModel>> GetAll()
        {
            try
            {
                return _priorityService.GetAll();
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Warning --> {ex.Message}");
                return NotFound($"Warning --> {ex.Message}");
            }            
        }          
    }
}
