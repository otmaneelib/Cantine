using Core.Entities;
using Core.Enums;
using Core.Interfaces.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repositories
{
    public class ClientRepository : RepositoryBase<Client>, IClientRepository
    {
        public ClientRepository(AppDbContext context, ILogger<RepositoryBase<Client>> logger) : base(context, logger)
        {
        }

        public async Task<Client> GetClientByNameAsync(string name)
        {
            return await _context.Clients
                .Include(c => c.Tickets)
                .FirstOrDefaultAsync(c => c.Name == name);
        }

        public IQueryable<Client> GetClientsByType(ClientType type)
        {
            return _context.Clients
                .Include(c => c.Tickets)
                .Where(c => c.Type == type)
                .AsQueryable();
        }
    }
}
