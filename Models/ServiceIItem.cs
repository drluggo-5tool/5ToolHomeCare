using FiveToolHomeCare.Models.Enums;

namespace FiveToolHomeCare.Models;

public class ServiceItem
{
    public int ServiceItemId { get; set; }

    public int CustomerId { get; set; }

    public Customer Customer { get; set; } = null!;

    public AirFilterDetails? AirFilterDetails { get; set; }

    public ServiceItemType ItemType { get; set; }

    public string Location { get; set; } = "";

    public string? Description { get; set; }

    public bool IsActive { get; set; } = true;

    public string? Notes { get; set; }

    public DateTime DateCreatedUtc { get; set; } = DateTime.UtcNow;
}