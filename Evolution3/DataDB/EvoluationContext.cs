using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        public DbSet<Setup> Setups { get; set; }
        public DbSet<RunResult> RunResults { get; set; }
        public DbSet<RunResultParam> RunResultParams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Configurations.Add<InputData>(new EntityTypeConfiguration<InputData>());
            //modelBuilder.Configurations.Add<Result>(new EntityTypeConfiguration<Result>());
            modelBuilder.Entity<InputData>().HasKey(p => new { p.IncomeIndexId, p.ParamIndexId });
            modelBuilder.Entity<InputData>().Property(p => p.Value).IsRequired();
            modelBuilder.Entity<Result>().HasKey(p => p.IncomeIndexId);
            modelBuilder.Entity<Result>()
                .Property(p => p.IncomeIndexId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            modelBuilder.Entity<Setup>().HasKey(p => p.Id);
            modelBuilder.Entity<Setup>().Property(p => p.MaxLevel).IsRequired();
            modelBuilder.Entity<Setup>().Property(p => p.TargetCorrelation).IsRequired();
            modelBuilder.Entity<Setup>().Property(p => p.CountParamIndex).IsRequired();
            modelBuilder.Entity<RunResult>().HasKey(p => p.Id);
            modelBuilder.Entity<RunResultParam>().HasKey(p => p.Id);
            modelBuilder.Entity<RunResult>().HasMany(p => p.Parameters).WithRequired(p => p.RunResult).HasForeignKey(p => p.RunResultId);

            base.OnModelCreating(modelBuilder);
        }

        #region Init

        /// <summary>
        /// Заполняем данными
        /// </summary>
        /// <param name="count">Кол-во наборов</param>
        public static void Init(int count)
        {
            using (EvoluationContext context = new EvoluationContext())
            {
                RecreateDB(context);

                //Заполняем по функции x*(x+y)
                Setup setup = new Setup() { Id = 0, CountParamIndex = 2, MaxLevel = 10, TargetCorrelation = 0.9999999 };
                context.Setups.Add(setup);

                for (int i = 0; i < count; i++)
                {
                    int x = i * 2;
                    int y = (i + 1) * 3;
                    int r = x * (x + y) / y;

                    InputData inputData = new InputData() { IncomeIndexId = i, ParamIndexId = 0, Value = x };
                    context.InputDatas.Add(inputData);
                    inputData = new InputData() { IncomeIndexId = i, ParamIndexId = 1, Value = y };
                    context.InputDatas.Add(inputData);

                    Result result = new Result() { IncomeIndexId = i, Value = r };
                    context.Results.Add(result);
                }
                context.SaveChanges();
            }
        }

        private static void RecreateDB(EvoluationContext context)
        {
            if (context.Database.Exists())
                context.Database.Delete();
            context.Database.Create();
        }

        #endregion
    }
}
