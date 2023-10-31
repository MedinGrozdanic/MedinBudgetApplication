using DAL.Model;
using DAL.ModelExtensions;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class BudgetContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BudgetShell> Budgets { get; set; }

        public DbSet<BudgetPerCategory> CategoryBudgets { get; set; }


        //public DbSet<BudgetCategoryExpense> BudgetCategoryExpense { get; set; }k

        protected override void OnConfiguring( DbContextOptionsBuilder builder )
        {
            builder.EnableSensitiveDataLogging();

            {
                builder.UseSqlServer( @"Server = LocalHost\SQLEXPRESS; Database = MedinBudgetApp; Trusted_Connection = True;" );
            }
        }

        protected override void OnModelCreating( ModelBuilder modelBuilder )
        {
            //modelBuilder.ApplyConfiguration(new UserEntityConfig());
            //modelBuilder.ApplyConfiguration(new ExpenseEntityConfig());
            //modelBuilder.ApplyConfiguration(new CategoryEntityConfig());
            //modelBuilder.ApplyConfiguration(new BudgetCategoryExpenseEntityConfig());


            modelBuilder.Seed();
        }
    }
}