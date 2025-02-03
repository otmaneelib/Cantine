namespace Core.Entities
{
    public class Meal
    {
        public int Id { get; set; }
        public string Entree { get; set; }
        public string Plat { get; set; }
        public string Dessert { get; set; }
        public string Pain { get; set; }

        // Navigation property for Supplements (many-to-many)
        public ICollection<Supplement> Supplements { get; set; } = new List<Supplement>();

        // Navigation property for Tickets (one-to-many)
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
