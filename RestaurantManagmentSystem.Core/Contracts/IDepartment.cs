using RestaurantManagmentSystem.Core.Models.Departments;

namespace RestaurantManagmentSystem.Core.Contracts
{
    public interface IDepartment
    {
        Task CreateDepartmentAsync(DepartmentViewModel model);
        Task<bool> HasThisEntityAsync(string name);
        Task<IEnumerable<EditDepartmentViewModel>> GetAllDepartmentsAsync();
        Task<IEnumerable<EditDepartmentViewModel>> GetAllDeletedDepartmentsAsync();
        Task DeleteDepartmentAsync(int Id);
        Task RestoreDepartmentAsync(int Id);
        Task EditPostDepartmentAsync(EditDepartmentViewModel model);
        Task<EditDepartmentViewModel> EditGetDepartmentAsync(int Id);
        Task<DepartmentViewModel> GetDepartmentById(int Id);
    }
}
