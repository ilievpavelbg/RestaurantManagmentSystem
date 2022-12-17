using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagmentSystem.Core.Data
{
    public class TempOrderMenuItemViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; } = null!;

        public string? Description { get; set; } = null!;

        public int? OnStock { get; set; }
        public int? OrderedQty { get; set; }

        public decimal Price { get; set; }
        public string? ImageURL { get; set; } = null!;

        public bool? ItemsForCooking { get; set; }

        public bool? IsChecked { get; set; }

        public IEnumerable<TempOrder> TempOrders { get; set; } = new List<TempOrder>();

    }
}
