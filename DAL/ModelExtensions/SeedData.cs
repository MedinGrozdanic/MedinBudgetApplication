using DAL.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;

namespace DAL.ModelExtensions
{
    public static class SeedData
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            //    modelBuilder.Entity<CategoryBudget>().HasOne<BudgetShell>().WithMany(bs => bs.CategoryBudgets).HasForeignKey(cb => cb.CategoryBudgetId);
            Random randomNumber = new Random();

            var users = new List<User>() {
                    new User { UserId = Guid.NewGuid(), Email = "mathias@gmail.com", FirstName = "Mathias", LastName = "Angelin", Username = "mathias1", Password = "abc"},
                    new User { UserId = Guid.NewGuid(), Email = "medin@gmail.com", FirstName = "Medin", LastName = "Grozdanic", Username = "Medin1", Password = "abc" },
            };

            modelBuilder.Entity<User>().HasData(users);


            var categories = new List<Category>() {
                    new Category {  CategoryId = 1, CategoryName = "Food"},
                    new Category {  CategoryId = 2, CategoryName = "Shopping"},
                    new Category {  CategoryId = 3, CategoryName = "Entertainment"},
                    new Category {  CategoryId = 4, CategoryName = "Housing"},
                    new Category {  CategoryId = 5, CategoryName = "Transportation"},
                    new Category {  CategoryId = 6, CategoryName = "Other"}
             };

            modelBuilder.Entity<Category>().HasData(categories);


            var expenses = GenerateExpense(200);
     

            List<Expense> GenerateExpense(int n)
            {
                var expenses = new List<Expense>();
                for (int i = 1; i <= n; i++)
                {
                    expenses.Add(new Expense()
                    {
                        ExpenseId = Guid.NewGuid(),
                        Amount = Math.Round(Convert.ToDecimal(randomNumber.Next(200, 2000)) / 10, 0) * 10,
                        Receiver = "Receiver" + i,
                        DateStamp = RandomDateTime(new DateTime(2022, 1, 1), DateTime.Today),
                        Comment = "Comment " + i,
                        UserId = users[randomNumber.Next(0, users.Count)].UserId,
                        CategoryId = categories[randomNumber.Next(0, categories.Count)].CategoryId
                    });
                }
                return expenses;
            }

            modelBuilder.Entity<Expense>().HasData(expenses);


            var budgets = GenerateBudget(10);

            List<BudgetShell> GenerateBudget(int nBudgets)
            {
                var budgets = new List<BudgetShell>();
                for (int i = 1; i <= nBudgets; i++)
                {
                    var startDate = RandomDateTime(new DateTime(2022, 1, 1), new DateTime(2022, 12, 31));
                    var endDate = startDate.AddDays(30);
                    budgets.Add(new BudgetShell()
                    {
                        BudgetShellId = Guid.NewGuid(),
                        BudgetName = "Budget " + i,
                        UserId = users[randomNumber.Next(1, users.Count)].UserId,
                        StartDate = startDate,
                        EndDate = endDate,

                    });
                }
                return budgets;
            }

            modelBuilder.Entity<BudgetShell>().HasData(budgets);


            List<BudgetPerCategory> categoryBudgets = new List<BudgetPerCategory>();
            for (int i = 0; i < 30; i++)
            {
                categoryBudgets.Add(
                    new BudgetPerCategory
                    {
                        CategoryBudgetId = Guid.NewGuid(),
                        BudgetShellId = budgets[randomNumber.Next(0, budgets.Count)].BudgetShellId,
                        MaxAmount = randomNumber.Next(0, 100) * 100,
                        CategoryId = categories[randomNumber.Next(0, categories.Count)].CategoryId
                    });
            }

            modelBuilder.Entity<BudgetPerCategory>().HasData(categoryBudgets);


            DateTime RandomDateTime(DateTime start, DateTime end)
            {
                Random random = new Random();
                int interval = (end - start).Days;
                return start.AddDays(random.Next(interval));
            }
        }
    }
}
