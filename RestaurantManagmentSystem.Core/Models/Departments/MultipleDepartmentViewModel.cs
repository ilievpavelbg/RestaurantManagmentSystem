namespace RestaurantManagmentSystem.Core.Models.Departments
{
    public class MultipleDepartmentViewModel
    {
        public DepartmentViewModel DepartmentModel { get; set; } = null!;
        public IEnumerable<EditDepartmentViewModel> ActiveDepartments { get; set; } = new List<EditDepartmentViewModel>();
        public IEnumerable<EditDepartmentViewModel> DeletedDepartments { get; set; } = new List<EditDepartmentViewModel>();
    }
}
