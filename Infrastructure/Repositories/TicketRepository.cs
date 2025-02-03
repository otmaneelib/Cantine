using Core.Entities;
using Core.Interfaces.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repositories
{
    public class TicketRepository : RepositoryBase<Ticket>, ITicketRepository
    {
        public TicketRepository(AppDbContext context, ILogger<RepositoryBase<Ticket>> logger) : base(context, logger)
        {
        }

        public async Task<Ticket> GetTicketWithDetailsAsync(int id)
        {
            return await _context.Tickets
                .Include(t => t.Client)
                .Include(t => t.Meal)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public IQueryable<Ticket> GetTicketsByClientId(int clientId)
        {
            return _context.Tickets
                .Include(t => t.Client)
                .Include(t => t.Meal)
                .Where(t => t.ClientId == clientId)
                .AsQueryable();
        }
    }
}
