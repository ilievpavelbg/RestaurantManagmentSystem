using System.ComponentModel.DataAnnotations;

namespace RestaurantManagmentSystem.Core.Data
{
    public class Customer
    {
        [Key]
        public int? Id { get; set; }

        [StringLength(50)]
        public string? Name { get; set; } = null!;

        [StringLength(50)]
        public string? Phone { get; set; } = null!;

        [StringLength(50)]
        public string? Email { get; set; } = null!;

        public bool IsDeleted { get; set; }

        [StringLength(50)]
        public Table? Table { get; set; } = null!;
    }
}
