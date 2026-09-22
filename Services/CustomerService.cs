using FiveToolHomeCare.Data;
using FiveToolHomeCare.Models;
using Microsoft.EntityFrameworkCore;

namespace FiveToolHomeCare.Services;

public class CustomerService
{
    private readonly AppDbContext _db;

    public CustomerService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Customer> CreateCustomerAsync(Customer customer)
    {
        _db.Customers.Add(customer);

        await _db.SaveChangesAsync();

        return customer;
    }

    public async Task<ServiceItem> AddAirFilterAsync(
        int customerId,
        string location,
        decimal width,
        decimal height,
        decimal depth,
        int? mervRating,
        string? brand,
        int replacementIntervalDays,
        decimal filterCost,
        decimal filterPrice)
    {
        var serviceItem = new ServiceItem
        {
            CustomerId = customerId,
            ItemType = Models.Enums.ServiceItemType.AirFilter,
            Location = location,
            AirFilterDetails = new AirFilterDetails
            {
                Width = width,
                Height = height,
                Depth = depth,
                MervRating = mervRating,
                Brand = brand,
                ReplacementIntervalDays = replacementIntervalDays,
                FilterCost = filterCost,
                FilterPrice = filterPrice
            }
        };

        _db.ServiceItems.Add(serviceItem);
        await _db.SaveChangesAsync();

        return serviceItem;
    }

    public async Task<List<Customer>> GetCustomersAsync()
    {
        return await _db.Customers
            .Where(c => c.IsActive)
            .OrderBy(c => c.LastName)
            .ThenBy(c => c.FirstName)
            .ToListAsync();
    }

    public async Task<List<ServiceItem>> GetAirFiltersAsync()
    {
        return await _db.ServiceItems
            .Include(s => s.Customer)
            .Include(s => s.AirFilterDetails)
            .Where(s =>
                s.IsActive &&
                s.ItemType == Models.Enums.ServiceItemType.AirFilter)
            .OrderBy(s => s.Customer.LastName)
            .ThenBy(s => s.Customer.FirstName)
            .ThenBy(s => s.Location)
            .ToListAsync();
    }

    public async Task<ServiceContract> CreateServiceContractAsync(
        int customerId,
        DateTime startDate,
        DateTime endDate,
        int serviceIntervalMonths,
        decimal annualFilterCost,
        decimal annualFilterPrice,
        decimal annualServiceFee,
        string? notes)
    {
        var contract = new ServiceContract
        {
            CustomerId = customerId,
            StartDate = startDate,
            EndDate = endDate,
            ServiceIntervalMonths = serviceIntervalMonths,
            AnnualFilterCost = annualFilterCost,
            AnnualFilterPrice = annualFilterPrice,
            AnnualServiceFee = annualServiceFee,
            Notes = notes
        };

        _db.ServiceContracts.Add(contract);
        await _db.SaveChangesAsync();

        return contract;
    }

    public async Task<ServiceVisit> CreateServiceVisitAsync(
        int customerId,
        int serviceContractId,
        DateTime serviceDate,
        string? notes)
    {
        var contract = await _db.ServiceContracts
            .FirstAsync(c =>
                c.ServiceContractId == serviceContractId &&
                c.CustomerId == customerId);

        var serviceVisit = new ServiceVisit
        {
            CustomerId = customerId,
            ServiceContractId = serviceContractId,
            ServiceDate = serviceDate,
            NextServiceDate = serviceDate.AddMonths(
                contract.ServiceIntervalMonths),
            Notes = notes
        };

        _db.ServiceVisits.Add(serviceVisit);
        await _db.SaveChangesAsync();

        return serviceVisit;
    }

    public async Task<ServiceContract?> GetActiveServiceContractAsync(
        int customerId)
    {
        return await _db.ServiceContracts
            .Where(c =>
                c.CustomerId == customerId &&
                c.IsActive)
            .OrderByDescending(c => c.StartDate)
            .FirstOrDefaultAsync();
    }

}