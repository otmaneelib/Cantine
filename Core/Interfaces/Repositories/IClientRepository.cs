using Core.Entities;
using Core.Enums;

namespace Core.Interfaces.Repositories
{
    public interface IClientRepository : IRepositoryBase<Client>
    {
        Task<Client> GetClientByNameAsync(string name);
        IQueryable<Client> GetClientsByType(ClientType type);
    }
}
