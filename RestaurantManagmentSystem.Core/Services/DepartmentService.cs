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
            var dept = await repo.GetByIdAsync<Department>(Id);

            var model = new EditDepartmentViewModel()
            {
                Id = Id,
                Name = dept.Name
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
            var dept = await repo.GetByIdAsync<Department>(model.Id);

            dept.Name = model.Name;

            await repo.SaveChangesAsync();
        }
        /// <summary>
        /// Get all departments
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Department> GetAllDepartments()
        {
            var allDepts = repo.All<Department>().ToList();

            return allDepts;
        }
        /// <summary>
        /// Delete Department as put the IsDeleted property to true, no physical detetion from DB
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task DeleteDepartmentAsync(int Id)
        {
                                   // ---------------------Check if is needed -----------------------

            //var menuItems = repo.All<MenuItem>(x => x.CategoryId == Id && x.IsDeleted == false);

            //if (menuItems.Any())
            //{
            //    throw new ArgumentException("Have to delete all menuItems included in this category!");
            //}

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
            var dept = await repo.GetByIdAsync<Department>(Id);

            var model = new DepartmentViewModel()
            {
                Name = dept.Name
            };

            return model;
        }
    }
}
