using EmailApp.Model;
using EmailApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmailApp.Controllers
{
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [Route("api/[controller]")]
        [HttpPost]
        public async Task<IActionResult> SendEmail([FromBody] Mail body)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _emailService.SendEmailAsync(body);
            return Ok("Email has been sent");
        }
    }
}