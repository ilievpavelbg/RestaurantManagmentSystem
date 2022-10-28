using RestaurantManagmentSystem.Core.Contracts;
using RestaurantManagmentSystem.Core.Data;
using RestaurantManagmentSystem.Core.Models;
using RestaurantManagmentSystem.Core.Repository;
using RestaurantManagmentSystem.Core.Repository.Common;

namespace RestaurantManagmentSystem.Core.Services
{
    public class MenuItemService : IMenuItem
    {
        private readonly IRepository repo;
        private readonly ApplicationDbContext context;

        public MenuItemService(IRepository _repo, ApplicationDbContext _context)
        {
            repo = _repo;
            context = _context;
        }

        public async Task AddMenuItemAsync(AddMenuItemViewModel model)
        {
            var menuItem = new MenuItem()
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                ImageURL = model.ImageURL,
                ItemsForCooking = model.ItemsForCooking,
                CategoryId = model.CategoryId
            };

            await repo.AddAsync<MenuItem>(menuItem);
            await repo.SaveChangesAsync();
        }

        public async Task DeleteMenuItemAsync(int Id)
        {
            var menuItem = await repo.GetByIdAsync<MenuItem>(Id);

            menuItem.IsDeleted = true;

            await repo.SaveChangesAsync();
        }

        public async Task EditMenuItemAsync(EditMenuItemViewModel model)
        {
            //context.MenuItems.Where(x => x.Id == model.Id).First();
            
            var menuItem = await repo.GetByIdAsync<MenuItem>(model.Id);

            menuItem.Name = model.Name;
            menuItem.Description = model.Description;
            menuItem.Price = model.Price;
            menuItem.ItemsForCooking = model.ItemsForCooking;
            menuItem.ImageURL = model.ImageURL;
            menuItem.CategoryId = model.CategoryId;
            menuItem.IsDeleted = model.IsDeleted;

            context.SaveChanges();
        }

        public async Task<EditMenuItemViewModel> EditMenuItemViewAsync(int Id)
        {
            //context.MenuItems.Where(x => x.Id == Id).FirstOrDefault();
            var menuItem = await repo.GetByIdAsync<MenuItem>(Id);

            var model = new EditMenuItemViewModel()
            {
                Id = Id,
                Name = menuItem.Name,
                Description = menuItem.Description,
                Price = menuItem.Price,
                ImageURL = menuItem.ImageURL,
                CategoryId = menuItem.CategoryId,
                ItemsForCooking = menuItem.ItemsForCooking
            };

            return model;
        }

        public IEnumerable<MenuItemViewModel> GetAllMenuItems()
        {
            var allMenuItem = repo.All<MenuItem>()
                .Select(mi => new MenuItemViewModel
                {
                    Id = mi.Id,
                    Name = mi.Name,
                    Description= mi.Description,
                    Price= mi.Price,
                    ImageURL= mi.ImageURL,
                    CategoryName = mi.Category.Name,
                    IsDeleted = mi.IsDeleted
                }).ToList();

            return allMenuItem;
        }
    }
}
