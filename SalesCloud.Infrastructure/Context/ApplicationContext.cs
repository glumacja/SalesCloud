using Microsoft.EntityFrameworkCore;
using SalesCloud.Domain.Models;

namespace SalesCloud.Infrastructure.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().ToTable("Account")
                .HasOne(x => x.Customer)
                .WithMany(x => x.Accounts)
                .HasForeignKey(x => x.CustomerId)
                .HasPrincipalKey(x => x.Id);

            modelBuilder.Entity<Account>().ToTable("Account")
                .HasMany(x => x.PurchasedSoftwares)
                .WithOne(x => x.Account)
                .HasForeignKey(x => x.AccountId)
                .HasPrincipalKey(x => x.Id);
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<PurchasedSoftware> PurchasedLicenses { get; set; }
    }
}