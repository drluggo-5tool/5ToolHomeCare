namespace FiveToolHomeCare.Models;

public class ServiceVisitPhoto
{
    public int ServiceVisitPhotoId { get; set; }

    public int ServiceVisitId { get; set; }

    public string FileName { get; set; } = "";

    public string FilePath { get; set; } = "";

    public string? Caption { get; set; }

    public DateTime DateCreatedUtc { get; set; } = DateTime.UtcNow;

    public ServiceVisit ServiceVisit { get; set; } = null!;
}