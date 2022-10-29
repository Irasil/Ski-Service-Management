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
        public RegistrationController()
        {
        }
        
        [HttpGet]
        public ActionResult<List<Status>> GetAll() =>
        RegistrationService.GetAll();

        [HttpPost]
        public IActionResult Create(Registration registration)
        {
            RegistrationService.Add(registration);
            return CreatedAtAction(nameof(Create), new { id = registration.Id }, registration);
        }

        [HttpGet("{id}")]
        public ActionResult<Registration> Get(int id)
        {
            var registration = RegistrationService.Get(id);

            if (registration == null)
                return NotFound();

            return registration;
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Registration registration)
        {
            if (id != registration.Id)
                registration.Id = id;

            var existingRegistration = RegistrationService.Get(id);
            if (existingRegistration is null)
                return NotFound();

            RegistrationService.Update(registration);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var registration = RegistrationService.Get(id);

            if (registration is null)
                return NotFound();

            RegistrationService.Delete(id);

            return NoContent();
        }
    }
}
