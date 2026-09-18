using FiveToolHomeCare.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FiveToolHomeCare.Pages.Customers;

public class AddFilterModel : PageModel
{
    private readonly CustomerService _customerService;

    public AddFilterModel(CustomerService customerService)
    {
        _customerService = customerService;
    }

    [BindProperty]
    public int CustomerId { get; set; }

    [BindProperty]
    public string Location { get; set; } = "";

    [BindProperty]
    public decimal Width { get; set; }

    [BindProperty]
    public decimal Height { get; set; }

    [BindProperty]
    public decimal Depth { get; set; }

    [BindProperty]
    public int? MervRating { get; set; }

    [BindProperty]
    public string? Brand { get; set; }

    [BindProperty]
    public int ReplacementIntervalDays { get; set; }

    [BindProperty]
    public decimal FilterCost { get; set; }

    [BindProperty]
    public decimal FilterPrice { get; set; }

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

        await _customerService.AddAirFilterAsync(
            CustomerId,
            Location,
            Width,
            Height,
            Depth,
            MervRating,
            Brand,
            ReplacementIntervalDays,
            FilterCost,
            FilterPrice
        );

        TempData["SuccessMessage"] = "Air filter added.";

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