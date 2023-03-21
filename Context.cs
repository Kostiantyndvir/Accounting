using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Accounting
{
    public class Context : DbContext
    {

        public DbSet<CostCategory> CostCats { get; set; } = null!;
        public DbSet<ProfitCategory> ProfitCats { get; set; } = null!;
        public DbSet<Profit> Prfts { get; set; } = null!;
        public DbSet<Cost> Csts { get; set; } = null!;

        public Context()
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //stackoverflow.com/questions/72598734/entity-framework-core-in-net-maui-xamarin-forms
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "base.db");
            optionsBuilder.UseSqlite($"Filename={dbPath}");
            //optionsBuilder.UseSqlite("Data Source = D:\\Base.db");
        }
    }
}
