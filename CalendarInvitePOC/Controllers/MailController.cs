using Microsoft.AspNetCore.Mvc;
using CalendarInvitePOC.Services;


namespace CalendarInvitePOC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MailController : ControllerBase
    {
        private readonly EmailService emailService;

        public MailController()
        {
            emailService = new EmailService();
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendInvite([FromQuery] string toEmail)
        {
            await emailService.SendCalendarInviteEmail(toEmail, "Dakshitha");
            return Ok("Email with calendar invite sent!");
        }
    }
}
