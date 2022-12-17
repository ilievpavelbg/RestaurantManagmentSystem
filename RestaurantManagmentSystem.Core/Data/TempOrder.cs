using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagmentSystem.Core.Data
{
    public class TempOrder
    {
        public int Id { get; set; }
        public int? ItemId { get; set; }
        public string? ItemName { get; set; } = null!;
        public decimal? Price { get; set; }
        public bool? ItemsForCooking { get; set; }
        public bool? IsChecked { get; set; }
        public IEnumerable<TempOrderMenuItemViewModel>? MenuItems { get; set; } = new List<TempOrderMenuItemViewModel>();

        [ForeignKey(nameof(Order))]
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;

    }
}
