using Microsoft.EntityFrameworkCore;
using RestaurantManagmentSystem.Core.Contracts;
using RestaurantManagmentSystem.Core.Data;
using RestaurantManagmentSystem.Core.Models.MenuItems;
using RestaurantManagmentSystem.Core.Repository;
using RestaurantManagmentSystem.Core.Repository.Common;
using RestaurantManagmentSystem.Core.Services;

namespace RestaurantManagmentSystem.Test
{
    public class MenuItemsServiceTest
    {
        private IRepository repo;
        private IMenuItem menuItemService;
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
        public async Task AddMenuItemAsyncTest()
        {
            var repo = new Repository(applicationDbContext);

            menuItemService = new MenuItemService(repo);

            var menuItem = new MenuItem()
            {
                Name = "Pizza",
                Description = "",
                Price = 10,
                ImageURL = "",
                ItemsForCooking = true,
                CategoryId = 1,
                OnStock = 10

            };

            await repo.AddAsync(menuItem);

            await repo.SaveChangesAsync();

            var result = await repo.All<MenuItem>().ToListAsync();

            var resultName = await repo.All<MenuItem>().Where(x => x.Name == "Pizza").FirstAsync();

            Assert.That(1, Is.EqualTo(result.Count()));

            Assert.That("Pizza", Is.EqualTo(resultName.Name));
        }

        [Test]
        public async Task EditPostMenuItemAsyncTest()
        {
            var repo = new Repository(applicationDbContext);

            menuItemService = new MenuItemService(repo);

            var menuItem = new MenuItem()
            {
                Name = "Pizza",
                Description = "",
                Price = 10,
                ImageURL = "",
                ItemsForCooking = true,
                CategoryId = 1,
                OnStock = 10

            };

            await repo.AddAsync(menuItem);

            await repo.SaveChangesAsync();

            var model = new EditMenuItemViewModel()
            {
                Id = 1,
                IsDeleted = false,
                Name = "Burger"
            };

           await menuItemService.EditPostMenuItemAsync(model);

            var result = await repo.All<MenuItem>().Where(x => x.Id == 1).FirstAsync();

            Assert.That("Burger", Is.EqualTo(result.Name));

        }

        [Test]
        public async Task EditGetMenuItemAsyncTest()
        {
            var repo = new Repository(applicationDbContext);

            menuItemService = new MenuItemService(repo);

            var menuItem = new MenuItem()
            {
                Id = 1,
                Name = "Pizza",
                Description = "",
                Price = 10,
                ImageURL = "",
                ItemsForCooking = true,
                CategoryId = 1,
                OnStock = 10

            };

            await repo.AddAsync(menuItem);

            await repo.SaveChangesAsync();

          

            var model = await menuItemService.EditGetMenuItemAsync(1);

            Assert.That("Pizza", Is.EqualTo(model.Name));

        }

        [TearDown]

        public void Teardown()
        {
            applicationDbContext.Dispose();
        }

    }
}
