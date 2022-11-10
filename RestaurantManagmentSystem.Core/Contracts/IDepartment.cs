using RestaurantManagmentSystem.Core.Data;
using RestaurantManagmentSystem.Core.Models.Departments;

namespace RestaurantManagmentSystem.Core.Contracts
{
    public interface IDepartment
    {
        Task AddDepartmentAsync(DepartmentViewModel model);
        bool HasThisEntity(string name);
        Task<IEnumerable<EditDepartmentViewModel>> GetAllDepartmentAsync();
        Task<IEnumerable<EditDepartmentViewModel>> GetAllDeletedDepartmentAsync();
        Task DeleteDepartmentAsync(int Id);
        Task RestoreDepartmentAsync(int Id);
        Task EditPostDepartmentAsync(EditDepartmentViewModel model);
        Task<EditDepartmentViewModel> EditGetDepartmentAsync(int Id);
        Task<DepartmentViewModel> GetDepartmentById(int Id);
    }
}
