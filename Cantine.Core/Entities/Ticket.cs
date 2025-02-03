namespace Cantine.Core.Entities
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
        public Client Client { get; set; }

        // Navigation property for Meal
        public Meal Meal { get; set; }
    }
}
