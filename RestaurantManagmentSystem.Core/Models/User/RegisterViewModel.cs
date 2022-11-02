using RestaurantManagmentSystem.Core.Constrains.User;
using System.ComponentModel.DataAnnotations;


namespace RestaurantManagmentSystem.Core.Models.User
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(UserConstrains.FirstNameMaxLenght, MinimumLength = UserConstrains.FirstNameMinLenght)]
        public string FirstNane { get; set; } = null!;

        [Required]
        [StringLength(UserConstrains.LastNameMaxLenght, MinimumLength = UserConstrains.LastNameMinLenght)]
        public string LastNane { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]

        [StringLength(UserConstrains.PassMaxLenght, MinimumLength = UserConstrains.PassMinLenght)]
        public string Password { get; set; } = null!;

        [Required]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = null!;
    }
}
