using Core.Entities;

namespace Core.Interfaces.Repositories
{
    public interface ITicketRepository
    {
        Task<Ticket> GetTicketWithDetailsAsync(int id);

        IQueryable<Ticket> GetTicketsByClientId(int clientId);
    }
}
