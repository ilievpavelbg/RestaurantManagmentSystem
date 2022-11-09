using Microsoft.EntityFrameworkCore;
using RestaurantManagmentSystem.Core.Contracts;
using RestaurantManagmentSystem.Core.Data;
using RestaurantManagmentSystem.Core.Models.Departments;
using RestaurantManagmentSystem.Core.Repository.Common;

namespace RestaurantManagmentSystem.Core.Services
{
    public class DepartmentService : IDepartment
    {
        private readonly IRepository repo;
        /// <summary>
        /// Initialize Repo in constructor
        /// </summary>
        /// <param name="_repo"></param>
        public DepartmentService(IRepository _repo)
        {
            repo = _repo;
        }
        /// <summary>
        /// Adding new Department to database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task AddDepartmentAsync(DepartmentViewModel model)
        {
            var department = new Department()
            {
                Name = model.Name
            };

            await repo.AddAsync<Department>(department);
            await repo.SaveChangesAsync();
        }
        /// <summary>
        /// Get the selected Department and render it to Edit page
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<EditDepartmentViewModel> EditGetDepartmentAsync(int Id)
        {
            var department = await repo.GetByIdAsync<Department>(Id);

            var model = new EditDepartmentViewModel()
            {
                Name = department.Name
            };

            return model;

        }
        /// <summary>
        /// Get the selected EditDepartmentViewModel from HHTPGet Edit, and save changes to DB
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task EditPostDepartmentAsync(EditDepartmentViewModel model)
        {
            var department = await repo.GetByIdAsync<Department>(model.Id);

            department.Name = model.Name;

            await repo.SaveChangesAsync();
        }
        /// <summary>
        /// Get all departments
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<EditDepartmentViewModel>> GetAllDepartmentAsync()
        {
            var allDepartments = await repo.All<Department>()
                .Where(x => x.IsDeleted == false)
                .Select(vm => new EditDepartmentViewModel
                {
                    Id = vm.Id,
                    Name = vm.Name
                })
                .ToListAsync();

            return allDepartments;
        }
        /// <summary>
        /// Check if in databes already exixt such entity
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool HasThisEntity(DepartmentViewModel model)
        {
            var entity = repo.All<Department>(x => x.Name == model.Name).FirstOrDefault();

            if (entity != null)
            {
                return true;
            }

            return false;
        }
        /// <summary>
        /// Delete Department as put the IsDeleted property to true, no physical detetion from DB
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task DeleteDepartmentAsync(int Id)
        {
            var user = repo.All<ApplicationUser>(x => x.DepartmentId == Id && x.IsDeleted == false);

            if (user.Any())
            {
                throw new ArgumentException("Have to delete all menuItems included in this category!");
            }

            var department = await repo.GetByIdAsync<Department>(Id);

            department.IsDeleted = true;

            await repo.SaveChangesAsync();
        }
        /// <summary>
        /// Get department by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<DepartmentViewModel> GetDepartmentById(int Id)
        {
            var department = await repo.GetByIdAsync<Department>(Id);

            var model = new DepartmentViewModel()
            {
                Name = department.Name
            };

            return model;
        }
        /// <summary>
        /// Restore Department as put the IsDeleted property to false
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task RestoreDepartmentAsync(int Id)
        {
            var users = repo.All<ApplicationUser>(x => x.DepartmentId == Id && x.IsDeleted == true);

            var department = await repo.GetByIdAsync<Department>(Id);

            department.IsDeleted = false;

            await repo.SaveChangesAsync();
        }
        /// <summary>
        /// Get All Deleted Departments
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<EditDepartmentViewModel>> GetAllDeletedDepartmentAsync()
        {
            var allDepts = await repo.All<Department>()
                .Where(x => x.IsDeleted == true)
                .Select(vm => new EditDepartmentViewModel
                {
                    Id = vm.Id,
                    Name = vm.Name
                })
                .ToListAsync();

            return allDepts;
        }
    }
}
