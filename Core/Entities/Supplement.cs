using Core.Enums;

namespace Core.Entities
{
    public class Supplement
    {
        public int Id { get; set; }
        public SupplementType Type { get; set; }
        public decimal Price { get; set; }

        // Navigation property for Meals (many-to-many)
        public ICollection<Meal> Meals { get; set; } = new List<Meal>();

        // Constructor
        public Supplement()
        {
            Meals = new List<Meal>();
        }

        // Override Equals and GetHashCode
        public override bool Equals(object obj)
        {
            if (obj is Supplement supplement)
            {
                return Id == supplement.Id;
            }
            return false;
        }

        // Override GetHashCode
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
