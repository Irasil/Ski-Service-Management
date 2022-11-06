﻿using IdentityServer4.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ski_Service_Management.Models;
using Ski_Service_Management.Services;

namespace Ski_Service_Management.Controllers
{
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

        [HttpPost]
        public IActionResult Login([FromBody] Mitarbeiter model)
        {
            try
            {
                JsonResult json = _mitarbeiterService.ProveUser(model);
                string lol = json.Value.ToString();
                bool hey = false;   
                hey = lol.Contains("gespert");


                if (json != null && hey == false)
                    return Ok(json);
                else if (json != null && hey == true)
                {
                    return BadRequest("user is blocked");
                }
                else
                {
                    return BadRequest("Invalid Credentials or user is blocked");
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
