using System.ComponentModel.DataAnnotations.Schema;

namespace FiveToolHomeCare.Models;

public class ServiceContract
{
    public int ServiceContractId { get; set; }

    public int CustomerId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public int ServiceIntervalMonths { get; set; } = 3;

    [Column(TypeName = "decimal(10,2)")]
    public decimal AnnualFilterCost { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal AnnualFilterPrice { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal AnnualServiceFee { get; set; }

    public bool IsActive { get; set; } = true;

    public string? Notes { get; set; }

    public DateTime DateCreatedUtc { get; set; } = DateTime.UtcNow;

    public Customer Customer { get; set; } = null!;
}