using RestaurantManagmentSystem.Core.Models.SubOrder;

namespace RestaurantManagmentSystem.Core.Models.Orders
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }

        public DateTime CreatedOn { get; set; }

        public string OrderStatus { get; set; } = string.Empty;

        public int EmployeeId { get; set; }

        public int TableId { get; set; }

        public decimal TotalPrice { get; set; }

        public IEnumerable<SubOrderViewModel>? SubOrders { get; set; } = new List<SubOrderViewModel>();
    }
}
