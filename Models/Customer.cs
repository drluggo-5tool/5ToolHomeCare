using FiveToolHomeCare.Models.Enums;

namespace FiveToolHomeCare.Models;

public class Customer
{
    public int CustomerId { get; set; }

    public string FirstName { get; set; } = "";

    public string LastName { get; set; } = "";

    public string Email { get; set; } = "";

    public string? Phone { get; set; }

    public string AddressLine1 { get; set; } = "";

    public string? AddressLine2 { get; set; }

    public string City { get; set; } = "";

    public string State { get; set; } = "";

    public string PostalCode { get; set; } = "";

    public AccessMethod? AccessMethod { get; set; }

    public string? DoorAccess { get; set; }

    public string? Notes { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime DateCreatedUtc { get; set; } = DateTime.UtcNow;

    public Lead? OriginalLead { get; set; }
    
    public ICollection<ServiceItem> ServiceItems { get; set; } = new List<ServiceItem>();
}