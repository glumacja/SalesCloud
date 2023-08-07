using System.ComponentModel.DataAnnotations;

namespace SalesCloud.Domain.Models
{
    public class Customer
    {
        [Key]
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public List<Account> Accounts { get; set; } = new();
    }
}