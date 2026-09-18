using FiveToolHomeCare.Models;
using FiveToolHomeCare.Models.Enums;
using FiveToolHomeCare.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FiveToolHomeCare.Pages.Customers;

public class CreateModel : PageModel
{
    private readonly CustomerService _customerService;

    public CreateModel(CustomerService customerService)
    {
        _customerService = customerService;
    }

    [BindProperty]
    public Customer Customer { get; set; } = new();

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        await _customerService.CreateCustomerAsync(Customer);

        TempData["SuccessMessage"] =
            $"Customer {Customer.FirstName} {Customer.LastName} was added.";

        return RedirectToPage();
    }
}