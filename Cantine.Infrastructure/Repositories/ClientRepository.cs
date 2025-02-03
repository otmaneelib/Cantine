using Cantine.Core.Entities;
using Cantine.Core.Interfaces.Repositories;
using Cantine.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Cantine.Infrastructure.Repositories
{
    public class ClientRepository : RepositoryBase<Client>, IClientRepository
    {
        public ClientRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Client> GetClientByNameAsync(string name)
        {
            return await _context.Clients
                .FirstOrDefaultAsync(c => c.Name == name);
        }

        public async Task<IEnumerable<Client>> GetClientsByTypeAsync(string type)
        {
            return await _context.Clients
                .Where(c => c.Type == type)
                .ToListAsync();
        }
    }
}
