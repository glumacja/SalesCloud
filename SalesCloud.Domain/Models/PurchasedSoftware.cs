using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SalesCloud.Domain.Enums;

namespace SalesCloud.Domain.Models
{
    public class PurchasedSoftware
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "ExternalProviderSoftwareId is a required field.")]
        public Guid ExternalProviderSoftwareId { get; set; }

        [Required(ErrorMessage = "Name is a required field.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Quantity is a required field.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "State is a required field.")]
        public PurchasedSoftwareState State { get; set; }

        [Required(ErrorMessage = "ValidTo is a required field.")]
        public DateTime ValidTo { get; set; }

        [Required(ErrorMessage = "AccountId is a required field.")]
        public Guid AccountId { get; set; }

        public Account? Account { get; set; }

        [NotMapped]
        public bool IsActive
        {
            get
            {
                return State.Equals(PurchasedSoftwareState.Active);
            }
        }
    }
}