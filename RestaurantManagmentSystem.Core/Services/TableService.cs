using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RestaurantManagmentSystem.Core.Contracts;
using RestaurantManagmentSystem.Core.Data;
using RestaurantManagmentSystem.Core.Models.Tables;
using RestaurantManagmentSystem.Core.Repository.Common;

namespace RestaurantManagmentSystem.Core.Services
{
    public class TableService : ITable_1
    {
        
        private readonly IRepository repo;
        /// <summary>
        /// Initialize Repo in constructor
        /// </summary>
        /// <param name="_repo"></param>
        public TableService(IRepository _repo)
        {
            repo = _repo;
        }

        /// <summary>
        /// Create new Table to database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task CreateTableAsync(CreateTableViewModel model)
        {
            var table = new Table()
            {
                Number = model.Number,
                IsReserved = model.IsReserved,
                IsDeleted = model.IsDeleted,
                Capacity = model.Capacity

            };

            await repo.AddAsync<Table>(table);
            await repo.SaveChangesAsync();
        }

        /// <summary>
        /// Get all tables
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<TableViewModel>> GetAllTablesAsync()
        {
            var allTable = await repo.All<Table>()
                .Where(x => x.IsDeleted == false)
                .Select(vm => new TableViewModel
                {
                    Id = vm.Id,
                    IsReserved = vm.IsReserved,
                    UserId = vm.UserId
                    
                })
                .ToListAsync();

            return allTable;
        }

        public async Task<TableViewModel> GetTableByIdAsync(int Id)
        {
            var table = await repo.All<Table>().Where(x => x.Id == Id)
                .Select(x => new TableViewModel()
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    IsReserved=x.IsReserved
                })
                .SingleOrDefaultAsync();

            if (table == null)
            {
                throw new ArgumentNullException("Table not found");
            }

            return table;
        }

        public async Task<TableViewModel> ReleaseTableAsync(int Id, string userId)
        {
            var table = await repo.All<Table>().Where(x => x.Id == Id).FirstAsync();

            table.IsReserved = false;
            table.UserId = null;

            await repo.SaveChangesAsync();

            var model = new TableViewModel()
            {
                Id = table.Id,
                IsReserved = table.IsReserved,
                UserId = table.UserId
            };

            return model;
        }

        public async Task<TableViewModel> ReserveTableAsync(int Id, string userId)
        {
            var table = await repo.All<Table>().Where(x => x.Id == Id).FirstAsync();

            table.IsReserved = true;
            table.UserId = userId;

            await repo.SaveChangesAsync();

            var model = new TableViewModel()
            {
                Id = table.Id,
                IsReserved = table.IsReserved,
                UserId = table.UserId
            };

            return model;
        }
    }
}
