using FiveToolHomeCare.Models;
using Microsoft.EntityFrameworkCore;

namespace FiveToolHomeCare.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Lead> Leads { get; set; }

    public DbSet<Customer> Customers { get; set; }

    public DbSet<ServiceItem> ServiceItems { get; set; }

    public DbSet<AirFilterDetails> AirFilterDetails { get; set; }

    public DbSet<ServiceLog> ServiceLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ServiceItem -> AirFilterDetails relationship
        modelBuilder.Entity<ServiceItem>()
            .HasOne(s => s.AirFilterDetails)
            .WithOne(a => a.ServiceItem)
            .HasForeignKey<AirFilterDetails>(a => a.ServiceItemId);

        // Lead -> converted Customer relationship
        modelBuilder.Entity<Lead>()
            .HasOne(l => l.ConvertedCustomer)
            .WithOne(c => c.OriginalLead)
            .HasForeignKey<Lead>(l => l.ConvertedCustomerId);

        modelBuilder.Entity<ServiceVisit>()
            .HasOne(v => v.Customer)
            .WithMany()
            .HasForeignKey(v => v.CustomerId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<ServiceVisit>()
            .HasOne(v => v.ServiceContract)
            .WithMany()
            .HasForeignKey(v => v.ServiceContractId)
            .OnDelete(DeleteBehavior.NoAction);
    }

    public DbSet<ServiceContract> ServiceContracts { get; set; }

    public DbSet<ServiceVisit> ServiceVisits { get; set; }

    public DbSet<ServiceVisitPhoto> ServiceVisitPhotos { get; set; }
}