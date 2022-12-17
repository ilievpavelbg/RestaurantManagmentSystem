using RestaurantManagmentSystem.Core.Models.Categories;

namespace RestaurantManagmentSystem.Core.Models.SubOrder
{
    public class SubOrderViewModel
    {
        public int Id { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsChecked { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreateOn { get; set; }
        public DateTime? CompletedOn { get; set; }
        public decimal CurrentTotalSum { get; set; }

        public int OrderId { get; set; }

        public List<SubOrderCategoryViewModel> Categories { get; set; } = new List<SubOrderCategoryViewModel>();
    }
}
