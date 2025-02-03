using Core.Entities;
using Core.Enums;

namespace Core.Interfaces.Repositories
{
    public interface ISupplementRepository
    {
        IQueryable<Supplement> GetSupplementsByType(SupplementType type);
    }
}
