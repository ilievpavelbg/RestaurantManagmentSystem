using RestaurantManagmentSystem.Core.Constrains.SubOrder;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantManagmentSystem.Core.Data
{
    public class SubOrder
    {
        public int Id { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsChecked { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreateOn { get; set; }
        public DateTime? CompletedOn { get; set; }

        [Required]
        [Range(typeof(decimal), SubOrderConstrains.PriceMinLenght, SubOrderConstrains.PriceMaxLenght)]
        public decimal CurrentTotalSum { get; set; }

        [ForeignKey(nameof(Order))]
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;

        public IEnumerable<Category> Categories { get; set; } = new List<Category>();
    }
}
