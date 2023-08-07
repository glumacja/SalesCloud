using System.Linq.Expressions;
using SalesCloud.Domain.Models;

namespace SalesCloud.Data.Contracts
{
    public interface IPurchasedSoftwareRepository
    {
        Task<List<PurchasedSoftware>> GetAll();

        PurchasedSoftware? GetById(Guid id);

        List<PurchasedSoftware> GetByCondition(Expression<Func<PurchasedSoftware, bool>> expression);

        void AddEntity(PurchasedSoftware entity);

        void UpdateEntity(PurchasedSoftware entity);
    }
}