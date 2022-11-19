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

        public bool CheckEmlploeeExistByEmail(string email)
        {
            return  repo.All<Employee>().Any(x => x.Email == email) ? true : false ;

        }

        public BecomeEmployee GetEmployeeByEmail(string email)
        {
            var employee = repo.All<Employee>().First(x => x.Email == email);

            var model = new BecomeEmployee()
            {
                Id = employee.Id,
                Email = employee.Email,
                ApplicationUserId = employee.ApplicationUserId = null!
            };

            return model;

        }


        public async Task ConnectUserWithEmployee(BecomeEmployee modelEmployee, ApplicationUser user)
        {
            var employee = repo.All<Employee>().First(x => x.Email == user.Email);

            var findUser = repo.All<ApplicationUser>().First(x => x.Email == modelEmployee.Email);

            employee.ApplicationUserId = user.Id;

            findUser.EmployeeId = modelEmployee.Id;

            await repo.SaveChangesAsync();

        }
    }
}
