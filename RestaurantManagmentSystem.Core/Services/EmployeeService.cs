using Microsoft.EntityFrameworkCore;
using RestaurantManagmentSystem.Core.Contracts;
using RestaurantManagmentSystem.Core.Data;
using RestaurantManagmentSystem.Core.Models.ApplicationUser;
using RestaurantManagmentSystem.Core.Models.Employee;
using RestaurantManagmentSystem.Core.Repository.Common;
using System.Globalization;

namespace RestaurantManagmentSystem.Core.Services
{
    public class EmployeeService : IEmployee
    {
        private readonly IRepository repo;
        public EmployeeService(IRepository _repo)
        {
            repo = _repo;
        }
        public async Task CreateUserAsync(EmployeeViewModel model)
        {

            DateTime date ;

            DateTime.TryParseExact(model.HireDate, "yyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out date);

            var newUser = new Employee()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                HireDate = date,
                Salary = model.Salary,
                Phone = model.Phone,
                Address = model.Address,
                Town = model.Town,
                DepartmentId = model.DepartmentId
            };

            await repo.AddAsync(newUser);
            await repo.SaveChangesAsync();

        }

        public async Task<bool> EsistEmployeeByEmailAsync(string email)
        {
            return await repo.All<Employee>().AnyAsync(x => x.Email == email) ? true : false ;
        }

        public async Task<BecomeEmployee> GetEmployeeByEmailAsync(string email)
        {
            var employee = await repo.All<Employee>().FirstAsync(x => x.Email == email);

            var model = new BecomeEmployee()
            {
                Id = employee.Id,
                Email = employee.Email,
                ApplicationUserId = employee.ApplicationUserId = null!
            };

            return model;

        }


        public async Task ConnectUserWithEmployeeAsync(BecomeEmployee modelEmployee, ApplicationUser user)
        {
            var employee = repo.All<Employee>().First(x => x.Email == user.Email);

            var findUser = repo.All<ApplicationUser>().First(x => x.Email == modelEmployee.Email);

            employee.ApplicationUserId = user.Id;

            findUser.EmployeeId = modelEmployee.Id;

            await repo.SaveChangesAsync();

        }

        public async Task<IEnumerable<AllEmployeeViewModel>> GetAllEmployeesAsync()
        {
            var employees =
                await repo.All<Employee>()
                .Where(x => x.ApplicationUserId != null)
                .Select(e => new AllEmployeeViewModel()
                {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    UserName = e.ApplicationUser.UserName,
                    DepartmentId = e.DepartmentId,
                    AppUserId = e.ApplicationUserId

                }).ToListAsync();

            return employees;
        }
    }
}
