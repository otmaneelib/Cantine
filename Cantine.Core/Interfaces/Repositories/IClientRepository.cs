using Cantine.Core.Entities;

namespace Cantine.Core.Interfaces.Repositories
{
    public interface IClientRepository : IRepositoryBase<Client>
    {
        Task<Client> GetClientByNameAsync(string name);
        Task<IEnumerable<Client>> GetClientsByTypeAsync(string type);
    }
}
