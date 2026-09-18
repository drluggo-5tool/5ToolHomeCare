namespace FiveToolHomeCare.Models;

public class ServiceVisit
{
    public int ServiceVisitId { get; set; }

    public int CustomerId { get; set; }

    public int ServiceContractId { get; set; }

    public DateTime ServiceDate { get; set; }

    public DateTime NextServiceDate { get; set; }

    public string? Notes { get; set; }

    public DateTime DateCreatedUtc { get; set; } = DateTime.UtcNow;

    public Customer Customer { get; set; } = null!;

    public ServiceContract ServiceContract { get; set; } = null!;

    public ICollection<ServiceVisitPhoto> Photos { get; set; }
        = new List<ServiceVisitPhoto>();
}