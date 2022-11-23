using RestaurantManagmentSystem.Core.Data;
using RestaurantManagmentSystem.Core.Models.ApplicationUser;
using RestaurantManagmentSystem.Core.Models.Employee;

namespace RestaurantManagmentSystem.Core.Contracts
{
    public interface IEmployee
    {
        Task CreateUserAsync(EmployeeViewModel model);
        Task<IEnumerable<AllEmployeeViewModel>> GetAllEmployeesAsync();
        Task<bool> EsistEmployeeByEmailAsync(string email);
        Task<BecomeEmployee> GetEmployeeByEmailAsync(string email);
        Task ConnectUserWithEmployeeAsync(BecomeEmployee modelEmployee, ApplicationUser user);
    }
}
