using FiveToolHomeCare.Data;
using FiveToolHomeCare.Models;
using FiveToolHomeCare.Models.Enums;

namespace FiveToolHomeCare.Services;

public class LeadService
{
    private readonly AppDbContext _db;

    public LeadService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Lead> CreateLeadAsync(
        string firstName,
        string lastName,
        string email,
        string? phone,
        string serviceInterest,
        string? message)
    {
        var lead = new Lead
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Phone = phone,
            ServiceInterest = serviceInterest,
            Message = message,
            Status = LeadStatus.New
        };

        _db.Leads.Add(lead);

        await _db.SaveChangesAsync();

        return lead;
    }
}