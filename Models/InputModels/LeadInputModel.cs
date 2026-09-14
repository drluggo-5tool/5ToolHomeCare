using System.ComponentModel.DataAnnotations;

namespace FiveToolHomeCare.Models.InputModels;

public class LeadInputModel
{
    [Required]
    public string FirstName { get; set; } = "";

    [Required]
    public string LastName { get; set; } = "";

    [Required]
    [EmailAddress]
    public string Email { get; set; } = "";

    [Phone]
    public string? Phone { get; set; }

    [Required]
    public string ServiceInterest { get; set; } = "";

    public string? Message { get; set; }
}