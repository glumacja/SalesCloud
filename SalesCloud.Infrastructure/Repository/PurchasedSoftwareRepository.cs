using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SalesCloud.Data.Contracts;
using SalesCloud.Domain.Models;
using SalesCloud.Infrastructure.Context;

namespace SalesCloud.Infrastructure.Repository
{
    public class PurchasedSoftwareRepository : IPurchasedSoftwareRepository
    {
        private readonly ApplicationContext _dbContext;

        public PurchasedSoftwareRepository(ApplicationContext context)
        {
            _dbContext = context;
        }

        public async Task<List<PurchasedSoftware>> GetAll()
        {
            return await _dbContext.PurchasedLicenses.AsNoTracking().ToListAsync();
        }

        public PurchasedSoftware? GetById(Guid id)
        {
            return _dbContext.PurchasedLicenses.AsNoTracking().SingleOrDefault(ps => ps.Id.Equals(id));
        }

        public List<PurchasedSoftware> GetByCondition(Expression<Func<PurchasedSoftware, bool>> expression)
        {
            return _dbContext.PurchasedLicenses.Where(expression).AsNoTracking().ToList();
        }

        public void AddEntity(PurchasedSoftware entity)
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));

            if (entity.Id == Guid.Empty)
            {
                entity.Id = Guid.NewGuid();
            }

            _dbContext.PurchasedLicenses.AddAsync(entity);
        }

        public void UpdateEntity(PurchasedSoftware entity)
        {
            _dbContext.PurchasedLicenses.Update(entity);
        }
    }
}