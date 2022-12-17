using Microsoft.EntityFrameworkCore;
using RestaurantManagmentSystem.Core.Contracts;
using RestaurantManagmentSystem.Core.Data;
using RestaurantManagmentSystem.Core.Models.Categories;
using RestaurantManagmentSystem.Core.Repository;
using RestaurantManagmentSystem.Core.Repository.Common;
using RestaurantManagmentSystem.Core.Services;

namespace RestaurantManagmentSystem.Test
{
    public class CategoryServiceTest
    {
        private IRepository repo;
        private ICategory categoryService;
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
        public async Task EditGetCategoryAsyncTest()
        {
            var repo = new Repository(applicationDbContext);

            categoryService = new CategoryService(repo);

            await repo.AddRangeAsync(new List<Category>()
            {
                new Category(){Id = 1, Name="Soup"},
                new Category(){Id = 2, Name = "Meal"},
                new Category(){Id = 3, Name = "Desert"},
                new Category(){Id = 4, Name = "salad"}

            });

            await repo.SaveChangesAsync();

            var result = await categoryService.EditGetCategoryAsync(1);

            Assert.That("Soup", Is.EqualTo(result.Name));
        }

        [Test]
        public async Task EditPostCategoryAsyncTest()
        {
            var repo = new Repository(applicationDbContext);

            categoryService = new CategoryService(repo);

            await repo.AddRangeAsync(new List<Category>()
            {
                new Category(){Id = 1, Name="Soup"},
                new Category(){Id = 2, Name = "Meal"},
                new Category(){Id = 3, Name = "Desert"},
                new Category(){Id = 4, Name = "salad"}

            });

            await repo.SaveChangesAsync();

            var model = await repo.All<Category>()
                .Where(x => x.Id == 1)
                .Select(x => new EditCategoryViewModel()
            {
                Id = x.Id,
                Name = "IceCream",
                IsDeleted = x.IsDeleted
            }).FirstAsync();

            

            await categoryService.EditPostCategoryAsync(model);

            var result = await repo.GetByIdAsync<Category>(1);

            Assert.That("IceCream", Is.EqualTo(result.Name));
        }

        [Test]
        public async Task GetAllCategoriesAsync()
        {
            var repo = new Repository(applicationDbContext);

            categoryService = new CategoryService(repo);

            await repo.AddRangeAsync(new List<Category>()
            {
                new Category(){Id = 1, Name="Soup"},
                new Category(){Id = 2, Name = "Meal"},
                new Category(){Id = 3, Name = "Desert"},
                new Category(){Id = 4, Name = "salad"}

            });

            await repo.SaveChangesAsync();

            var result = await categoryService.GetAllCategoriesAsync();

            Assert.That(4, Is.EqualTo(result.Count()));
        }

        [Test]
        public async Task CreateCategoryAsyncTest()
        {
            var repo = new Repository(applicationDbContext);

            categoryService = new CategoryService(repo);

            var category = new Category()
            {
                Name = "Soup",

            };

            await repo.AddAsync(category);

            await repo.SaveChangesAsync();

            var result = await repo.All<Category>().ToListAsync();

            var resultName = await repo.All<Category>().Where(x => x.Name == "Soup").FirstAsync();

            Assert.That(1, Is.EqualTo(result.Count()));

            Assert.That("Soup", Is.EqualTo(resultName.Name));
        }

        [Test]
        public async Task CreateCategoryAsyncTest_1()
        {
            var repo = new Repository(applicationDbContext);

            categoryService = new CategoryService(repo);

            var categoryDTO = new CategoryViewModel()
            {
                Name = "Soup",

            };

            await categoryService.CreateCategoryAsync(categoryDTO);

            var result = await repo.All<Category>().Where(x => x.Name == "Soup").FirstAsync();

            Assert.That("Soup", Is.EqualTo(result.Name));

        }

        [Test]
        public async Task HasThisEntityAsyncTest()
        {
            var repo = new Repository(applicationDbContext);

            categoryService = new CategoryService(repo);

            await repo.AddRangeAsync(new List<Category>()
            {
                new Category(){Id = 1, Name="Soup"},
                new Category(){Id = 2, Name = "Meal"},
                new Category(){Id = 3, Name = "Desert"},
                new Category(){Id = 4, Name = "salad"}

            });

            await repo.SaveChangesAsync();

            var result = await categoryService.HasThisEntityAsync("Soup");

            Assert.That(result, Is.EqualTo(true));

        }

        [Test]
        public async Task DeleteCategoryAsyncTest()
        {
            var repo = new Repository(applicationDbContext);

            categoryService = new CategoryService(repo);

            await repo.AddRangeAsync(new List<Category>()
            {
                new Category(){Id = 1, Name="Soup", IsDeleted = false},
                new Category(){Id = 2, Name = "Meal", IsDeleted = false},
                new Category(){Id = 3, Name = "Desert", IsDeleted = false},
                new Category(){Id = 4, Name = "salad", IsDeleted = false}

            });

            await repo.SaveChangesAsync();

            await categoryService.DeleteCategoryAsync(1);

            var result =  repo.AllReadonly<Category>();

            var resultBool = await repo.GetByIdAsync<Category>(1);

            Assert.That(4, Is.EqualTo(result.Count()));

            Assert.That(true, Is.EqualTo(resultBool.IsDeleted == true));
        }

        [Test]
        public async Task GetCategoryByIdTest()
        {
            var repo = new Repository(applicationDbContext);

            categoryService = new CategoryService(repo);

            await repo.AddRangeAsync(new List<Category>()
            {
                new Category(){Id = 1, Name="Soup"},
                new Category(){Id = 2, Name = "Meal"},
                new Category(){Id = 3, Name = "Desert"},
                new Category(){Id = 4, Name = "salad"}

            });

            await repo.SaveChangesAsync();

            var result = await categoryService.GetCategoryById(1);

            Assert.That("Soup", Is.EqualTo(result.Name = "Soup"));
        }

        [Test]
        public async Task RestoreCategoryAsyncTest()
        {
            var repo = new Repository(applicationDbContext);

            categoryService = new CategoryService(repo);

            await repo.AddRangeAsync(new List<Category>()
            {
                new Category(){Id = 1, Name="Soup", IsDeleted = true},
                new Category(){Id = 2, Name = "Meal", IsDeleted = false},
                new Category(){Id = 3, Name = "Desert", IsDeleted = false},
                new Category(){Id = 4, Name = "salad", IsDeleted = false}

            });

            await repo.SaveChangesAsync();

            await categoryService.RestoreCategoryAsync(1);

            var result = await repo.GetByIdAsync<Category>(1);

            Assert.IsFalse(result.IsDeleted);
        }

        [Test]
        public async Task GetAllDeletedCategoriesAsyncTest()
        {
            var repo = new Repository(applicationDbContext);

            categoryService = new CategoryService(repo);

            await repo.AddRangeAsync(new List<Category>()
            {
                new Category(){Id = 1, Name="Soup", IsDeleted = true},
                new Category(){Id = 2, Name = "Meal", IsDeleted = true},
                new Category(){Id = 3, Name = "Desert", IsDeleted = false},

            });

            await repo.SaveChangesAsync();

            var result = await categoryService.GetAllDeletedCategoriesAsync();

            Assert.That(2, Is.EqualTo(result.Count()));
        }

        [Test]
        public async Task AddMenuItemsToCategoryTest()
        {
            var repo = new Repository(applicationDbContext);

            categoryService = new CategoryService(repo);

            await repo.AddRangeAsync(new List<Category>()
            {
                new Category(){Id = 1, Name="Soup", IsDeleted = false},
                new Category(){Id = 2, Name = "Meal", IsDeleted = false},
                new Category(){Id = 3, Name = "Desert", IsDeleted = false},
                new Category(){Id = 4, Name = "salad", IsDeleted = false}

            });

            await repo.AddRangeAsync(new List<MenuItem>()
            {
                new MenuItem(){Id = 11, Name="Kebapche", IsDeleted = false, CategoryId = 2, Description = "", ImageURL = ""},
                new MenuItem(){Id = 12, Name = "Topcheta", IsDeleted = false, CategoryId = 1, Description = "", ImageURL = ""},
                new MenuItem(){Id = 13, Name = "Icecream", IsDeleted = false, CategoryId = 3, Description = "", ImageURL = ""},
                new MenuItem(){Id = 14, Name = "Tarator", IsDeleted = false, CategoryId = 1, Description = "", ImageURL = ""}

            });

            await repo.SaveChangesAsync();

            //var catList = new List<Category>
            //{
            //    new Category(){Id = 1, Name="Soup", IsDeleted = true},
            //    new Category(){Id = 2, Name = "Meal", IsDeleted = false},
            //    new Category(){Id = 3, Name = "Desert", IsDeleted = false},
            //    new Category(){Id = 4, Name = "salad", IsDeleted = false}
            //};

            //var menuItemList = new List<MenuItem>()
            //{
            //    new MenuItem(){Id = 1, Name="Kebapche", IsDeleted = false},
            //    new MenuItem(){Id = 2, Name = "Topcheta", IsDeleted = false},
            //    new MenuItem(){Id = 2, Name = "Tarator", IsDeleted = false},
            //    new MenuItem(){Id = 3, Name = "Icecream", IsDeleted = false},
            //};

            var catList = await repo.AllReadonly<Category>().AsNoTracking().ToListAsync();
            var menuItemList = await repo.AllReadonly<MenuItem>().AsNoTracking().ToListAsync();

            await categoryService.AddMenuItemsToCategory(menuItemList, catList);

            var result = await repo.GetByIdAsync<Category>(1);

            Assert.That(result.MenuItems.Count(), Is.EqualTo(2));
        }


        [TearDown]
        public void Teardown()
        {
            applicationDbContext.Dispose();
        }
    }
}
