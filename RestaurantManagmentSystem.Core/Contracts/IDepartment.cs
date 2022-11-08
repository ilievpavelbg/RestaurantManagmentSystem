using RestaurantManagmentSystem.Core.Data;
using RestaurantManagmentSystem.Core.Models.Departments;

namespace RestaurantManagmentSystem.Core.Contracts
{
    public interface IDepartment
    {
        Task AddDepartmentAsync(DepartmentViewModel model);
        IEnumerable<Department> GetAllDepartments();
        Task DeleteDepartmentAsync(int Id);
        Task EditPostDepartmentAsync(EditDepartmentViewModel model);
        Task<EditDepartmentViewModel> EditGetDepartmentAsync(int Id);
        Task<DepartmentViewModel> GetDepartmentById(int Id);
    }
}
