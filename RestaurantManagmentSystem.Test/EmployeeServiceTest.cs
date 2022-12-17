using Microsoft.EntityFrameworkCore;
using RestaurantManagmentSystem.Core.Contracts;
using RestaurantManagmentSystem.Core.Data;
using RestaurantManagmentSystem.Core.Models.ApplicationUser;
using RestaurantManagmentSystem.Core.Models.Employee;
using RestaurantManagmentSystem.Core.Repository;
using RestaurantManagmentSystem.Core.Repository.Common;
using RestaurantManagmentSystem.Core.Services;

namespace RestaurantManagmentSystem.Test
{
    public class EmployeeServiceTest
    {

        private IRepository repo;
        private IEmployee employeeService;
        private ApplicationDbContext applicationDbContext;

        [SetUp]
        public void Setup()
        {
            var contextoptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("RMS_DB")
                .Options;

            applicationDbContext = new ApplicationDbContext(contextoptions);

            applicationDbContext.Database.EnsureDeleted();
            applicationDbContext.Database.EnsureCreated();

        }

        [Test]
        public async Task CreateUserAsync()
        {
            var repo = new Repository(applicationDbContext);

            employeeService = new EmployeeService(repo);

            var emplDTO = new EmployeeViewModel()
            {
                FirstName = "Pesho",
                LastName = "Petrov",
                Email = "",
                HireDate = "04/10/2008",
                Salary = 1000,
                Phone = "359886662323",
                Address = "",
                Town = "",
                DepartmentId = 1

            };

            await employeeService.CreateUserAsync(emplDTO);

            var result = await repo.All<Employee>().ToListAsync();

            var resultName = await repo.All<Employee>().Where(x => x.FirstName == "Pesho").FirstAsync();

            Assert.That(1, Is.EqualTo(result.Count()));

            Assert.That("Pesho", Is.EqualTo(resultName.FirstName));
        }

        [Test]
        public async Task ExistEmployeeByEmailAsyncTest()
        {
            var repo = new Repository(applicationDbContext);

            employeeService = new EmployeeService(repo);

            var employee = new Employee()
            {
                FirstName = "Pesho",
                LastName = "Petrov",
                Email = "abv@abv.bg",
                HireDate = DateTime.Now,
                Salary = 1000,
                Phone = "359886662323",
                Address = "",
                Town = "",
                DepartmentId = 1

            };

            await repo.AddAsync<Employee>(employee);

            await repo.SaveChangesAsync();

            var result = await employeeService.ExistEmployeeByEmailAsync("abv@abv.bg");

            Assert.That(true, Is.EqualTo(result));

        }

        [Test]
        public async Task GetEmployeeByEmailAsyncTest()
        {
            var repo = new Repository(applicationDbContext);

            employeeService = new EmployeeService(repo);

            var employee = new Employee()
            {
                FirstName = "Pesho",
                LastName = "Petrov",
                Email = "abv@abv.bg",
                HireDate = DateTime.Now,
                Salary = 1000,
                Phone = "359886662323",
                Address = "",
                Town = "",
                DepartmentId = 1

            };

            await repo.AddAsync<Employee>(employee);

            await repo.SaveChangesAsync();

            var result = await employeeService.GetEmployeeByEmailAsync("abv@abv.bg");

            Assert.That("abv@abv.bg", Is.EqualTo(result.Email));

        }

        [Test]
        public async Task ConnectUserWithEmployeeAsyncTest()
        {
            var repo = new Repository(applicationDbContext);

            employeeService = new EmployeeService(repo);

            var employee = new Employee()
            {
                Email = "abv@abv.bg",
                ApplicationUserId = "abc",
                Id = 13,
                FirstName = "",
                LastName = "",
                Phone = ""
            };

            var appUser = new ApplicationUser()
            {
                Email = "abv@abv.bg",
                Id = "abc",
                FirstName = "",
                LastName = ""

            };

            await repo.AddAsync<Employee>(employee);
            await repo.AddAsync<ApplicationUser>(appUser);

            await repo.SaveChangesAsync();

            var empl = await repo.GetByIdAsync<Employee>(13);

            var becomeEmpl = new BecomeEmployee()
            {
                ApplicationUserId = empl.ApplicationUserId,
                Email = empl.Email,
                Id = empl.Id
            };

            await employeeService.ConnectUserWithEmployeeAsync(becomeEmpl, appUser);

            Assert.That("abc", Is.EqualTo(becomeEmpl.ApplicationUserId));

        }

        [Test]
        public async Task GetAllEmployeesAsyncTest()
        {
            var repo = new Repository(applicationDbContext);

            employeeService = new EmployeeService(repo);

            var employee = new Employee()
            {
                FirstName = "Pesho",
                LastName = "Petrov",
                Email = "abv@abv.bg",
                HireDate = DateTime.Now,
                Salary = 1000,
                Phone = "359886662323",
                Address = "",
                Town = "",
                DepartmentId = 1

            };

            await repo.AddAsync<Employee>(employee);

            await repo.SaveChangesAsync();

            var result = await employeeService.GetAllEmployeesAsync();

            Assert.That(0, Is.EqualTo(result.Count()));

        }

        [Test]
        public async Task GetEmployeeByIdAsyncTest()
        {
            var repo = new Repository(applicationDbContext);

            employeeService = new EmployeeService(repo);

            var employee = new Employee()
            {
                Id = 13,
                FirstName = "Pesho",
                LastName = "Petrov",
                Email = "abv@abv.bg",
                HireDate = DateTime.Now,
                Salary = 1000,
                Phone = "359886662323",
                Address = "",
                Town = "",
                DepartmentId = 1

            };

            await repo.AddAsync<Employee>(employee);

            await repo.SaveChangesAsync();

            var empl = await repo.GetByIdAsync<Employee>(13);

            var model = new EmployeeDetailsViewModel()
            {
                Id = empl.Id,
                FirstName = empl.FirstName,
                LastName = empl.LastName,
                Email = empl.Email,
                HireDate = "17/12/2022",
                Salary = empl.Salary,
                Phone = empl.Phone,
                Address = empl.Address,
                Town= empl.Town,
            };

            var result = await employeeService.GetEmployeeByIdAsync(model.Id);

            Assert.That(13, Is.EqualTo(result.Id));

        }


        [TearDown]
        public void Teardown()
        {
            applicationDbContext.Dispose();
        }
    }



   
}
