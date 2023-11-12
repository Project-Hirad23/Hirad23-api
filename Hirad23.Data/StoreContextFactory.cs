using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Hirad23.Data;

    public class StoreContextFactory : IDesignTimeDbContextFactory<StoreContext>
    {
        public StoreContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<StoreContext>();

            optionsBuilder.UseSqlite("Data Source=../store.db");

            return new StoreContext(optionsBuilder.Options);
        }
    }
