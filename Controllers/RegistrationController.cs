using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ski_Service_Management.Models;
using Ski_Service_Management.Services;

namespace Ski_Service_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {

        private readonly IRegistrationsService _registrationsService;
        public RegistrationController(IRegistrationsService registrationsService)
        {
            _registrationsService = registrationsService;
        }
        
        [HttpGet]
        public ActionResult<List<RegistrationModel>> GetAll() =>
        _registrationsService.GetAll();

        [HttpPost]
        public ActionResult Create(RegistrationModel registration)
        {
           
            _registrationsService.Add(registration);
            return CreatedAtAction(nameof(Create), new { id = registration.Id }, registration);
            
        }


        [HttpGet("{id}")]
        public ActionResult<RegistrationModel> Get(int id)
        {
            if(_registrationsService.GetAll() == null)
                return NotFound();
            return _registrationsService.Get(id);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Registration registration)
        {
            if (id != registration.Id)
                registration.Id = id;

            var existingRegistration = _registrationsService.Get(id);
            if (existingRegistration is null)
                return NotFound();

            _registrationsService.Update(registration);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var registration = _registrationsService.Get(id);

            if (registration is null)
                return NotFound();

            _registrationsService.Delete(id);

            return NoContent();
        }
    }
}
