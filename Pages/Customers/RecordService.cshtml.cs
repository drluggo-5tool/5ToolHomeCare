using FiveToolHomeCare.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FiveToolHomeCare.Pages.Customers;

public class RecordServiceModel : PageModel
{
    private readonly CustomerService _customerService;
    private readonly IWebHostEnvironment _environment;

    public RecordServiceModel(
        CustomerService customerService,
        IWebHostEnvironment environment)
    {
        _customerService = customerService;
        _environment = environment;
    }

    [BindProperty]
    public int CustomerId { get; set; }

    [BindProperty]
    public int ServiceContractId { get; set; }

    [BindProperty]
    public DateTime ServiceDate { get; set; } = DateTime.Today;

    [BindProperty]
    public string? Notes { get; set; }

    [BindProperty]
    public List<IFormFile> Photos { get; set; } = new();

    public List<SelectListItem> Customers { get; set; } = new();

    public async Task OnGetAsync()
    {
        await LoadCustomersAsync();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            await LoadCustomersAsync();
            return Page();
        }

        var contract =
            await _customerService.GetActiveServiceContractAsync(CustomerId);

        if (contract == null)
        {
            ModelState.AddModelError(
                "",
                "This customer does not have an active service contract."
            );

            await LoadCustomersAsync();
            return Page();
        }

        var serviceVisit =
            await _customerService.CreateServiceVisitAsync(
                CustomerId,
                contract.ServiceContractId,
                ServiceDate,
                Notes
            );

        var customer =
            await _customerService.GetCustomerAsync(CustomerId);

        var customerName = customer == null
            ? $"Customer-{CustomerId}"
            : $"{customer.LastName}-{customer.FirstName}";

        var folderName =
            $"{customerName}_{ServiceDate:yyyy-MM-dd}_Visit-{serviceVisit.ServiceVisitId}";

        var visitFolder = Path.Combine(
            _environment.WebRootPath,
            "uploads",
            "service-visits",
            folderName
        );

        if (Photos.Count > 0)
        {
            Directory.CreateDirectory(visitFolder);

            foreach (var photo in Photos)
            {
                if (photo.Length == 0)
                {
                    continue;
                }

                var fileName =
                    $"{Guid.NewGuid()}{Path.GetExtension(photo.FileName)}";

                var filePath = Path.Combine(
                    visitFolder,
                    fileName
                );

                using var stream =
                    new FileStream(filePath, FileMode.Create);

                await photo.CopyToAsync(stream);

                var relativePath =
                    $"/uploads/service-visits/{folderName}/{fileName}";

                await _customerService.AddServiceVisitPhotoAsync(
                    serviceVisit.ServiceVisitId,
                    fileName,
                    relativePath
                );
            }
        }

        TempData["SuccessMessage"] = "Service visit recorded.";

        return RedirectToPage();
    }

    private async Task LoadCustomersAsync()
    {
        var customers = await _customerService.GetCustomersAsync();

        Customers = customers
            .Select(c => new SelectListItem
            {
                Value = c.CustomerId.ToString(),
                Text = $"{c.LastName}, {c.FirstName}"
            })
            .ToList();
    }

}