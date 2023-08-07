using System.ComponentModel.DataAnnotations;

namespace SalesCloud.Domain.Models
{
    public class Account
    {
        [Key]
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public Guid? CustomerId { get; set; }
        public Customer? Customer { get; set; }

        public List<PurchasedSoftware> PurchasedSoftwares { get; set; } = new();
    }
}