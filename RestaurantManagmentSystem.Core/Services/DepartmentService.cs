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

            if (department == null)
            {
                throw new ArgumentException("Department with this ID can not be found !");

            }

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
        /// Get all Departments
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<EditDepartmentViewModel>> GetAllDepartmentsAsync()
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
        public async Task<bool> HasThisEntityAsync(string name)
        {
            return await repo.All<Department>().AnyAsync(c => c.Name == name);
        }
        /// <summary>
        /// Delete Department as put the IsDeleted property to true, no physical detetion from DB
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task DeleteDepartmentAsync(int Id)
        {
            var appUsers = repo.All<Employee>(x => x.DepartmentId == Id && x.IsDeleted == false);

            var department = await repo.GetByIdAsync<Department>(Id);

            if (appUsers.Any())
            {
                throw new ArgumentException($"First have to delete all Users with department {department.Name}!");
            }

            department.IsDeleted = true;

            await repo.SaveChangesAsync();
        }
        /// <summary>
        /// Get Department by Id
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
           
            var department = await repo.GetByIdAsync<Department>(Id);

            department.IsDeleted = false;

            await repo.SaveChangesAsync();
        }
        /// <summary>
        /// Get All Deleted Departments
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<EditDepartmentViewModel>> GetAllDeletedDepartmentsAsync()
        {
            var allDepartments = await repo.All<Department>()
                .Where(x => x.IsDeleted == true)
                .Select(vm => new EditDepartmentViewModel()
                {
                    Id = vm.Id,
                    Name = vm.Name
                })
                .ToListAsync();

            return allDepartments;
        }
    }
}
