using RestaurantManagmentSystem.Core.Constrains.Table;
using System.ComponentModel.DataAnnotations;

namespace RestaurantManagmentSystem.Core.Models.Tables
{
    public class CreateTableViewModel
    {

        [Required]
        [Range(TableConstrains.NumberMinLenght, TableConstrains.NumberMaxLenght)]
        public int Number { get; set; }

        [Required]
        [Range(TableConstrains.CapacityMinLenght, TableConstrains.CapacityMaxLenght)]
        public int Capacity { get; set; }

        [Required]
        public bool IsReserved { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

    }
}
