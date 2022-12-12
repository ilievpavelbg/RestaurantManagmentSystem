using Microsoft.AspNetCore.Mvc;
using RestaurantManagmentSystem.Core.Constrains.SubOrder;
using RestaurantManagmentSystem.Core.Data;
using RestaurantManagmentSystem.Core.Models.Categories;
using System.ComponentModel.DataAnnotations;

namespace RestaurantManagmentSystem.Core.Models.SubOrder
{
    public class SubOrderViewModel
    {
        public bool IsCompleted { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreateOn { get; set; }
        public DateTime? CompletedOn { get; set; }

        [Required]
        [Range(typeof(decimal), SubOrderConstrains.PriceMinLenght, SubOrderConstrains.PriceMaxLenght)]
        public decimal CurrentTotalSum { get; set; }
        public int CategoryId { get; set; }
      
        public IEnumerable<Category> Categories { get; set; } = new List<Category>();
    }
}
