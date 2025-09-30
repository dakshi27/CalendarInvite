using SendGrid;
using SendGrid.Helpers.Mail;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CalendarInvitePOC.Services
{
    public class EmailService
    {
        private readonly string apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
        private readonly string senderEmail = Environment.GetEnvironmentVariable("SENDER_EMAIL");
        private readonly string senderName = Environment.GetEnvironmentVariable("SENDER_NAME");

        public async Task SendCalendarInviteEmail(string toEmail, string userName)
        {
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(senderEmail, senderName);
            var to = new EmailAddress(toEmail, userName);
            var subject = "Meeting Invite from CalendarInvitePOC";

            string templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Templates", "calendar_invite.html");
            string htmlContent = File.ReadAllText(templatePath).Replace("{{name}}", userName);
            var plainTextContent = $"Hi {userName}, here's your calendar invite.";

            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            var icsContent = IcsGenerator.GenerateInvite(
                subject: "Team Sync with Dakshitha",
                startIST: new DateTime(2025, 10, 1, 14, 30, 0),
                endIST: new DateTime(2025, 10, 1, 15, 30, 0),
                location: "Zoom",
                description: "Weekly sync-up for CalendarInvitePOC progress.",
                attendees: new[] { "dakshitha27.t@gmail.com", "mentor@example.com" },
                organizerEmail: senderEmail,
                isRecurring: true,
                saveToDisk: true
            );

            msg.AddAttachment("invite.ics", Convert.ToBase64String(Encoding.UTF8.GetBytes(icsContent)), "text/calendar");

            await client.SendEmailAsync(msg);
        }
    }
}

