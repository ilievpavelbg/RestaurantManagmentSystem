using RestaurantManagmentSystem.Core.Data;
using RestaurantManagmentSystem.Core.Models.ApplicationUser;
using RestaurantManagmentSystem.Core.Models.Employee;

namespace RestaurantManagmentSystem.Core.Contracts
{
    public interface IEmployee
    {
        Task CreateUserAsync(EmployeeViewModel model);
        Task<EmployeeDetailsViewModel> GetEmployeeByIdAsync(int Id);
        Task<IEnumerable<AllEmployeeViewModel>> GetAllEmployeesAsync();
        Task<bool> ExistEmployeeByEmailAsync(string email);
        Task<BecomeEmployee> GetEmployeeByEmailAsync(string email);
        Task ConnectUserWithEmployeeAsync(BecomeEmployee modelEmployee, ApplicationUser user);

        Task EditPostEmployeeAsync(EmployeeDetailsViewModel model);
    }
}
