using SalesCloud.Domain.Enums;

namespace SalesCloud.Common.Dtos
{
    public record PurchasedSoftwareDto(Guid Id, Guid ExternalProviderSoftwareId, string? Name, int Quantity, PurchasedSoftwareState State, DateTime ValidTo);
}