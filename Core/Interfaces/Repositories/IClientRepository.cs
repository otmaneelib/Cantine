using Core.Entities;

namespace Core.Interfaces.Repositories
{
    public interface IClientRepository : IRepositoryBase<Client>
    {
        Task<Client> GetClientByNameAsync(string name);
        Task<IEnumerable<Client>> GetClientsByTypeAsync(string type);
    }
}
