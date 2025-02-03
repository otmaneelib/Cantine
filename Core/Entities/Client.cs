using Core.Enums;

namespace Core.Entities
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Budget { get; set; }
        public ClientType Type { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

        public Client()
        {
            Tickets = new List<Ticket>();
        }
    }
}
