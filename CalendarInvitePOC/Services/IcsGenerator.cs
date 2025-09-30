using System.Text;

namespace CalendarInvitePOC.Services
{
    public class IcsGenerator
    {
        public static string GenerateInvite(
            string subject,
            DateTime startIST,
            DateTime endIST,
            string location,
            string description,
            string[] attendees,
            string organizerEmail,
            bool isRecurring = false,
            bool saveToDisk = false)
        {
            var sb = new StringBuilder();
            sb.AppendLine("BEGIN:VCALENDAR");
            sb.AppendLine("VERSION:2.0");
            sb.AppendLine("PRODID:-//CalendarInvitePOC//EN");
            sb.AppendLine("BEGIN:VEVENT");

            string uid = Guid.NewGuid().ToString();
            sb.AppendLine($"UID:{uid}");
            sb.AppendLine($"DTSTAMP:{DateTime.UtcNow:yyyyMMddTHHmmssZ}");

            // Convert IST to UTC
            var startUTC = TimeZoneInfo.ConvertTimeToUtc(startIST, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
            var endUTC = TimeZoneInfo.ConvertTimeToUtc(endIST, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

            sb.AppendLine($"DTSTART:{startUTC:yyyyMMddTHHmmssZ}");
            sb.AppendLine($"DTEND:{endUTC:yyyyMMddTHHmmssZ}");
            sb.AppendLine($"SUMMARY:{subject}");
            sb.AppendLine($"LOCATION:{location}");
            sb.AppendLine($"DESCRIPTION:{description}");

            // Add organizer
            sb.AppendLine($"ORGANIZER;CN=CalendarInvitePOC:mailto:{organizerEmail}");

            // Add attendees
            foreach (var attendee in attendees)
            {
                sb.AppendLine($"ATTENDEE;CN={attendee}:mailto:{attendee}");
            }

            // Add recurrence
            if (isRecurring)
            {
                sb.AppendLine("RRULE:FREQ=WEEKLY;COUNT=5");
            }

            // Add visibility and status
            sb.AppendLine("STATUS:CONFIRMED");
            sb.AppendLine("TRANSP:OPAQUE");

            sb.AppendLine("END:VEVENT");
            sb.AppendLine("END:VCALENDAR");

            string icsContent = sb.ToString();

            // Log for audit
            Console.WriteLine("Generated ICS:");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Start (IST): {startIST}");
            Console.WriteLine($"End (IST): {endIST}");
            Console.WriteLine($"UID: {uid}");
            Console.WriteLine($"Organizer: {organizerEmail}");
            Console.WriteLine($"Attendees: {string.Join(", ", attendees)}");
            Console.WriteLine($"Recurring: {isRecurring}");
            Console.WriteLine($"Saved to disk: {saveToDisk}");

            // Save to disk if requested
            if (saveToDisk)
            {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Logs", $"invite_{uid}.ics");
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                File.WriteAllText(filePath, icsContent);
                Console.WriteLine($"ICS saved to: {filePath}");
            }

            return icsContent;
        }
    }
}
