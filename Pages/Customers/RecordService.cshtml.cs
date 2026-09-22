using FiveToolHomeCare.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FiveToolHomeCare.Pages.Customers;

public class RecordServiceModel : PageModel
{
    private readonly CustomerService _customerService;

    public RecordServiceModel(CustomerService customerService)
    {
        _customerService = customerService;
    }

    [BindProperty]
    public int CustomerId { get; set; }

    [BindProperty]
    public int ServiceContractId { get; set; }

    [BindProperty]
    public DateTime ServiceDate { get; set; } = DateTime.Today;

    [BindProperty]
    public string? Notes { get; set; }

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

        await _customerService.CreateServiceVisitAsync(
            CustomerId,
            contract.ServiceContractId,
            ServiceDate,
            Notes
        );

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