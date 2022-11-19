namespace RestaurantManagmentSystem.Core.Models.Employee
{
    public class BecomeEmployee
    {
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string ApplicationUserId { get; set; } = null!;

    }
}
