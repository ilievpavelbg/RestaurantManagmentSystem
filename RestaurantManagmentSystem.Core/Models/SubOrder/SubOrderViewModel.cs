using RestaurantManagmentSystem.Core.Constrains.SubOrder;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public int OrderId { get; set; }

        //public ICollection<Category> Categories { get; set; } = new List<Category>();
    }
}
