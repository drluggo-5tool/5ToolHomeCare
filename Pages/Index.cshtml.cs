using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FiveToolHomeCare.Services;
using FiveToolHomeCare.Models.InputModels;

namespace FiveToolHomeCare.Pages;

public class IndexModel : PageModel
{
    private readonly LeadService _leadService;
    private readonly EmailService _emailService;

    public IndexModel(
        LeadService leadService,
        EmailService emailService)
    {
        _leadService = leadService;
        _emailService = emailService;
    }

    [BindProperty]
    public LeadInputModel Input { get; set; } = new();

    [TempData]
    public string? SuccessMessage { get; set; }
    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var lead = await _leadService.CreateLeadAsync(
            Input.FirstName,
            Input.LastName,
            Input.Email,
            Input.Phone,
            Input.ServiceInterest,
            Input.Message
        );

        await _emailService.SendLeadNotificationAsync(
            lead.FirstName,
            lead.LastName,
            lead.Email,
            lead.Phone,
            lead.ServiceInterest,
            lead.Message,
            lead.LeadId
        );

        SuccessMessage =
            "Thank you! Your request has been received. We'll be in touch soon.";

        return Redirect("/#contact");

    }
}