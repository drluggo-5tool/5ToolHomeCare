using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FiveToolHomeCare.Models;

public class AirFilterDetails
{
    [Key]
    public int ServiceItemId { get; set; }

    [Column(TypeName = "decimal(6,2)")]
    public decimal Width { get; set; }

    [Column(TypeName = "decimal(6,2)")]
    public decimal Height { get; set; }

    [Column(TypeName = "decimal(6,2)")]
    public decimal Depth { get; set; }

    public int? MervRating { get; set; }

    public string? Brand { get; set; }

    public int ReplacementIntervalDays { get; set; }

    public DateTime? LastReplacedDate { get; set; }

    public DateTime? NextReplacementDate { get; set; }

    public ServiceItem ServiceItem { get; set; } = null!;
}