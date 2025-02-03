namespace Cantine.Core.Entities
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Budget { get; set; }
        public string Type { get; set; } // Interne, Prestataire, VIP, Stagiaire, Visiteur

        // Navigation property for Tickets
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
