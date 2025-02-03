namespace Core.Entities
{
    public class Supplement
    {
        public int Id { get; set; }
        public string Type { get; set; } // Boisson, Fromage, Pain, etc.
        public decimal Price { get; set; }

        // Navigation property for Meals (many-to-many)
        public ICollection<Meal> Meals { get; set; } = new List<Meal>();
    }
}
