using System.ComponentModel.DataAnnotations.Schema;

namespace FiveToolHomeCare.Models;

public class ServiceLog
{
    public int ServiceLogId { get; set; }

    public int ServiceItemId { get; set; }

    public DateTime ServiceDate { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal FilterCost { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal FilterPrice { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal ServicePrice { get; set; }

    public string? Notes { get; set; }

    public DateTime DateCreatedUtc { get; set; } = DateTime.UtcNow;

    public ServiceItem ServiceItem { get; set; } = null!;
}