namespace Core.Entities
{
    public class Ticket
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int MealId { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal EmployerCoverage { get; set; }
        public decimal AmountToPay { get; set; }
        public List<string> Products { get; set; } = new List<string>();

        // Navigation property for Client
        public virtual Client Client { get; set; }

        // Navigation property for Meal
        public Meal Meal { get; set; }

        // Constructor
        public Ticket()
        {
            Products = new List<string>();
        }

        // Override Equals and GetHashCode
        public override bool Equals(object obj)
        {
            if (obj is Ticket ticket)
            {
                return Id == ticket.Id;
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
