using FiveToolHomeCare.Models;

namespace FiveToolHomeCare.Services;

public class CalendarService
{
    public string CreateServiceEvent(
        ServiceVisit serviceVisit,
        Customer customer)
    {
        var eventId = Guid.NewGuid();

        var calendar = $"""
BEGIN:VCALENDAR
VERSION:2.0
PRODID:-//5 Tool Home Care//Service Calendar//EN
BEGIN:VEVENT
UID:{eventId}@5toolhomecare.com
DTSTAMP:{DateTime.UtcNow:yyyyMMdd'T'HHmmss'Z'}
DTSTART;VALUE=DATE:{serviceVisit.NextServiceDate:yyyyMMdd}
SUMMARY:Filter Replace - {customer.FirstName} {customer.LastName}
DESCRIPTION:{serviceVisit.Notes ?? ""}
BEGIN:VALARM
TRIGGER:-P14D
ACTION:DISPLAY
DESCRIPTION:Upcoming 5 Tool Home Care service
END:VALARM
END:VEVENT
END:VCALENDAR
""";

        return calendar;
    }
}