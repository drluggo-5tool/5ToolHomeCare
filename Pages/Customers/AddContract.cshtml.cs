using FiveToolHomeCare.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FiveToolHomeCare.Pages.Customers;

public class AddContractModel : PageModel
{
    private readonly CustomerService _customerService;

    public AddContractModel(CustomerService customerService)
    {
        _customerService = customerService;
    }

    [BindProperty]
    public int CustomerId { get; set; }

    [BindProperty]
    public DateTime StartDate { get; set; } = DateTime.Today;

    [BindProperty]
    public DateTime EndDate { get; set; } = DateTime.Today.AddYears(1).AddDays(-1);

    [BindProperty]
    public int ServiceIntervalMonths { get; set; } = 3;

    [BindProperty]
    public decimal AnnualFilterCost { get; set; }

    [BindProperty]
    public decimal AnnualFilterPrice { get; set; }

    [BindProperty]
    public decimal AnnualServiceFee { get; set; }

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

        await _customerService.CreateServiceContractAsync(
            CustomerId,
            StartDate,
            EndDate,
            ServiceIntervalMonths,
            AnnualFilterCost,
            AnnualFilterPrice,
            AnnualServiceFee,
            Notes
        );

        TempData["SuccessMessage"] = "Annual service contract added.";

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