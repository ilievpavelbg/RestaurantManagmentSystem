using RestaurantManagmentSystem.Core.Models.Tables;

namespace RestaurantManagmentSystem.Core.Contracts
{
    public interface ITable_1
    {
        Task CreateTableAsync(CreateTableViewModel model);
        Task<TableViewModel> GetTableByIdAsync(int Id);
        Task<IEnumerable<TableViewModel>> GetAllTablesAsync();
        Task<TableViewModel> ReserveTableAsync(int Id, string userId);
        Task<TableViewModel> ReleaseTableAsync(int Id, string userId);
        Task SaveCurrentOrderIdToTable(int orderId, int tableId);
    }
}
