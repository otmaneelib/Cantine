using Core.Entities;
using Core.Enums;
using Core.Interfaces.Repositories;
using Infrastructure.Data;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repositories
{
    public class SupplementRepository : RepositoryBase<Supplement>, ISupplementRepository
    {
        public SupplementRepository(AppDbContext context, ILogger<RepositoryBase<Supplement>> logger) : base(context, logger)
        {
        }

        public IQueryable<Supplement> GetSupplementsByType(SupplementType type)
        {
            return _context.Supplements
                .Where(s => s.Type == type)
                .AsQueryable();
        }
    }
}
