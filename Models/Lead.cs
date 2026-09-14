using FiveToolHomeCare.Models.Enums;

namespace FiveToolHomeCare.Models;

public class Lead
{
    public int LeadId { get; set; }

    public string FirstName { get; set; } = "";

    public string LastName { get; set; } = "";

    public string Email { get; set; } = "";

    public string? Phone { get; set; }

    public string ServiceInterest { get; set; } = "";

    public string? Message { get; set; }

    public LeadStatus Status { get; set; } = LeadStatus.New;

    public DateTime DateSubmittedUtc { get; set; } = DateTime.UtcNow;

    public int? ConvertedCustomerId { get; set; }
    
    public DateTime? DateConvertedUtc { get; set; }

    public Customer? ConvertedCustomer { get; set; }
}