using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDB
{
    public class EvoluationContext : DbContext
    {
        public EvoluationContext() :
            base("EvoluationContext")
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<InputData> InputDatas { get; set; }
        public DbSet<Result> Results { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Configurations.Add<InputData>(new EntityTypeConfiguration<InputData>());
            //modelBuilder.Configurations.Add<Result>(new EntityTypeConfiguration<Result>());
            modelBuilder.Entity<InputData>().HasKey(p => new { p.IncomeIndexId, p.ParamIndexId });
            modelBuilder.Entity<InputData>().Property(p => p.Value).IsRequired();
            modelBuilder.Entity<Result>().HasKey(p => p.IncomeIndexId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
