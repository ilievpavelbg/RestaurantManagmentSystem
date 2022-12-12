using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagmentSystem.Core.Models.Orders
{
    public class ListTest
    {
        public IEnumerable<Test> Tests { get; set; } = new List<Test>();
    }
}
