#  **CalendarInvitePOC**

A backend prototype for sending realistic calendar invites via email using **SendGrid** and **ICS attachments**. Built with **ASP.NET Core**, designed for enterprise-grade scheduling, modularity, and clarity.


##  **Features**

-  Secure SendGrid email integration using `.env`-based secrets  
-  ICS file generation with:
  - Organizer and multiple attendees
  - Recurrence support
  - Human-readable IST times
  - Save-to-disk option for auditability  
-  HTML email template with dynamic user name  
-  No secrets committed — GitHub Push Protection verified


##  **Tech Stack**

- ASP.NET Core  
- SendGrid API (For now)
- ICS (iCalendar) format  
- HTML templating  
- Environment variables via `.env` and `appsettings.json`



