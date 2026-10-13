using FiveToolHomeCare.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FiveToolHomeCare.Pages.Customers;

public class ViewPhotoModel : PageModel
{
    private readonly CustomerService _customerService;
    private readonly IWebHostEnvironment _environment;

    public ViewPhotoModel(
        CustomerService customerService,
        IWebHostEnvironment environment)
    {
        _customerService = customerService;
        _environment = environment;
    }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var photo = await _customerService.GetServiceVisitPhotoAsync(id);

        if (photo == null)
        {
            return NotFound();
        }

        var privateRoot = Path.GetFullPath(
            Path.Combine(
                _environment.ContentRootPath,
                "PrivateUploads"
            )
        );

        var filePath = Path.GetFullPath(
            Path.Combine(privateRoot, photo.FilePath)
        );

        if (!filePath.StartsWith(
                privateRoot + Path.DirectorySeparatorChar,
                StringComparison.OrdinalIgnoreCase))
        {
            return NotFound();
        }

        if (!System.IO.File.Exists(filePath))
        {
            return NotFound();
        }

        var extension = Path.GetExtension(filePath).ToLowerInvariant();

        var contentType = extension switch
        {
            ".jpg" or ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            ".gif" => "image/gif",
            ".webp" => "image/webp",
            _ => null
        };

        if (contentType == null)
        {
            return NotFound();
        }

        return PhysicalFile(filePath, contentType);

    }
}