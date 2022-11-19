using RestaurantManagmentSystem.Core.Data;
using RestaurantManagmentSystem.Core.Models.ApplicationUser;
using RestaurantManagmentSystem.Core.Models.Employee;

namespace RestaurantManagmentSystem.Core.Contracts
{
    public interface IEmployee
    {
        Task CreateUserAsync(EmployeeViewModel model);
        bool CheckEmlploeeExistByEmail(string email);
        BecomeEmployee GetEmployeeByEmail(string email);
        Task ConnectUserWithEmployee(BecomeEmployee modelEmployee, ApplicationUser user);
    }
}
