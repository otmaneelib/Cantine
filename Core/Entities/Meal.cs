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
        public virtual ICollection<Supplement> Supplements { get; set; } = new List<Supplement>();

        // Navigation property for Tickets (one-to-many)
        public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

        // Constructor
        public Meal()
        {
            Supplements = new List<Supplement>();
            Tickets = new List<Ticket>();
        }

        // Override Equals and GetHashCode if necessary
        public override bool Equals(object obj)
        {
            if (obj is Meal meal)
            {
                return Id == meal.Id;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
