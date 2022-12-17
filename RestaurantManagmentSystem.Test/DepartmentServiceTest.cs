using Microsoft.EntityFrameworkCore;
using RestaurantManagmentSystem.Core.Contracts;
using RestaurantManagmentSystem.Core.Data;
using RestaurantManagmentSystem.Core.Models.Departments;
using RestaurantManagmentSystem.Core.Repository;
using RestaurantManagmentSystem.Core.Repository.Common;
using RestaurantManagmentSystem.Core.Services;

namespace RestaurantManagmentSystem.Test
{
    public class DepartmentServiceTest
    {
        private IRepository repo;
        private IDepartment departmentService;
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
        public async Task CreateDepartmentAsyncTest()
        {
            var repo = new Repository(applicationDbContext);

            departmentService = new DepartmentService(repo);

            var department = new Department()
            {
                Name = "Service",

            };

            await repo.AddAsync(department);

            await repo.SaveChangesAsync();

            var result = await repo.All<Department>().ToListAsync();

            var resultName = await repo.All<Department>().Where(x => x.Name == "Service").FirstAsync();

            Assert.That(1, Is.EqualTo(result.Count()));

            Assert.That("Service", Is.EqualTo(resultName.Name));
        }

        [Test]
        public async Task EditGetDepartmentAsyncTest()
        {
            var repo = new Repository(applicationDbContext);

            departmentService = new DepartmentService(repo);

            await repo.AddRangeAsync(new List<Department>()
            {
                new Department(){Id = 1, Name="Service"},
                new Department(){Id = 2, Name = "Administration"},
                new Department(){Id = 3, Name = "Delivery"},

            });

            await repo.SaveChangesAsync();

            var result = await departmentService.EditGetDepartmentAsync(1);

            Assert.That("Service", Is.EqualTo(result.Name));
        }

        [Test]
        public async Task EditPostDepartmentAsyncTest()
        {
            var repo = new Repository(applicationDbContext);

            departmentService = new DepartmentService(repo);

            await repo.AddRangeAsync(new List<Department>()
            {
                new Department(){Id = 1, Name="Service"},
                new Department(){Id = 2, Name = "Administration"},
                new Department(){Id = 3, Name = "Delivery"},

            });

            await repo.SaveChangesAsync();

            var model = await repo.All<Department>()
                .Where(x => x.Id == 1)
                .Select(x => new EditDepartmentViewModel()
                {
                    Id = x.Id,
                    Name = "SCM",
                    IsDeleted = x.IsDeleted
                }).FirstAsync();



            await departmentService.EditPostDepartmentAsync(model);

            var result = await repo.GetByIdAsync<Department>(1);

            Assert.That("SCM", Is.EqualTo(result.Name));
        }

        [Test]
        public async Task GetAllCategoriesAsyncTest()
        {
            var repo = new Repository(applicationDbContext);

            departmentService = new DepartmentService(repo);

            await repo.AddRangeAsync(new List<Department>()
            {
                new Department(){Id = 1, Name="Service"},
                new Department(){Id = 2, Name = "Administration"},
                new Department(){Id = 3, Name = "Delivery"},

            });

            await repo.SaveChangesAsync();

            var result = await departmentService.GetAllDepartmentsAsync();

            Assert.That(3, Is.EqualTo(result.Count()));
        }

        [Test]
        public async Task HasThisEntityAsyncTesTestt()
        {
            var repo = new Repository(applicationDbContext);

            departmentService = new DepartmentService(repo);

            await repo.AddRangeAsync(new List<Department>()
            {
                new Department(){Id = 1, Name = "Service"},
                new Department(){Id = 2, Name = "Administration"},
                new Department(){Id = 3, Name = "Delivery"},

            });

            await repo.SaveChangesAsync();

            var result = await departmentService.HasThisEntityAsync("Service");

            Assert.That(result, Is.EqualTo(true));

        }

        [Test]
        public async Task DeleteCategoryAsyncTest()
        {
            var repo = new Repository(applicationDbContext);

            departmentService = new DepartmentService(repo);

            await repo.AddRangeAsync(new List<Department>()
            {
                new Department(){Id = 1, Name = "Service", IsDeleted = false},
                new Department(){Id = 2, Name = "Administration", IsDeleted = false},
                new Department(){Id = 3, Name = "Delivery", IsDeleted = false},

            });

            await repo.SaveChangesAsync();

            await departmentService.DeleteDepartmentAsync(1);

            var result = repo.AllReadonly<Department>();

            var resultBool = await repo.GetByIdAsync<Department>(1);

            Assert.That(3, Is.EqualTo(result.Count()));

            Assert.That(true, Is.EqualTo(resultBool.IsDeleted == true));
        }

        [Test]
        public async Task GetDepartmentByIdTest()
        {
            var repo = new Repository(applicationDbContext);

            departmentService = new DepartmentService(repo);

            await repo.AddRangeAsync(new List<Department>()
            {
                new Department(){Id = 1, Name = "Service", IsDeleted = false},
                new Department(){Id = 2, Name = "Administration", IsDeleted = false},
                new Department(){Id = 3, Name = "Delivery", IsDeleted = false},

            });

            await repo.SaveChangesAsync();

            var result = await departmentService.GetDepartmentById(1);

            Assert.That("Service", Is.EqualTo(result.Name = "Service"));
        }

        [Test]
        public async Task RestoreDepartmentAsyncTest()
        {
            var repo = new Repository(applicationDbContext);

            departmentService = new DepartmentService(repo);

            await repo.AddRangeAsync(new List<Department>()
            {
                new Department(){Id = 1, Name = "Service", IsDeleted = true},
                new Department(){Id = 2, Name = "Administration", IsDeleted = false},
                new Department(){Id = 3, Name = "Delivery", IsDeleted = false},

            });

            await repo.SaveChangesAsync();

            await departmentService.RestoreDepartmentAsync(1);

            var result = await repo.GetByIdAsync<Department>(1);

            Assert.IsFalse(result.IsDeleted);
        }

        [Test]
        public async Task GetAllDeletedDepartmentsAsyncTest()
        {
            var repo = new Repository(applicationDbContext);

            departmentService = new DepartmentService(repo);

            await repo.AddRangeAsync(new List<Department>()
            {
                new Department(){Id = 1, Name = "Service", IsDeleted = true},
                new Department(){Id = 2, Name = "Administration", IsDeleted = false},
                new Department(){Id = 3, Name = "Delivery", IsDeleted = false},

            });

            await repo.SaveChangesAsync();

            var result = await departmentService.GetAllDeletedDepartmentsAsync();

            Assert.That(1, Is.EqualTo(result.Count()));
        }

        [TearDown]
        public void Teardown()
        {
            applicationDbContext.Dispose();
        }
    }
}
