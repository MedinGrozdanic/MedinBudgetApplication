using API.DTOs;
using DAL;
using DAL.Model;
using Microsoft.EntityFrameworkCore;
using Service.DTOs;
using Service.Services.ServiceInterfaces;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Service.Services
{
    public class ExpenseService : IExpenseService
    {

        public PieChartDTO GetSumPerCategory(Guid userId)
        {
            var expenses = ListExpensesWithCategory(userId);
            var pieChartDTO = new PieChartDTO();

            foreach (var expense in expenses)
            {
                switch (expense.Category.CategoryId)
                {
                    case 1:
                        pieChartDTO.FoodCategorySpent += expense.Amount;
                        break;

                    case 2:
                        pieChartDTO.ShoppingCategorySpent += expense.Amount;
                        break;

                    case 3:
                        pieChartDTO.EntertainmentCategorySpent += expense.Amount;
                        break;

                    case 4:
                        pieChartDTO.HousingCategorySpent += expense.Amount;
                        break;

                    case 5:
                        pieChartDTO.TransportationCategorySpent += expense.Amount;
                        break;

                    case 6:
                        pieChartDTO.OtherCategorySpent += expense.Amount;
                        break;
                }
            }
            return pieChartDTO;
        }

        public List<Expense> ListExpensesWithCategory(Guid userId)
        {
            using (var context = new BudgetContext())
            {
                var result = context.Expenses
                    .Where(u => u.UserId == userId).Include("Category")
                    .ToList();

                return result;
            }
        }



        public List<Expense> ListExpenses(Guid userId)
        {



            using (var context = new BudgetContext())
            {
                var result = context.Expenses
                    .Include(c => c.Category)
                    .Where(w => w.UserId == userId)
                    .OrderBy(e => e.DateStamp).Reverse()
                    .ToList();

                return result;
            }
        }
        public async Task<Expense> FindExpense(Guid guid)
        {
            using (var context = new BudgetContext())
            {
                try
                {
                    var result = await context.Expenses.
                         Where(g => g.ExpenseId == guid)
                         .FirstOrDefaultAsync();

                    return result;

                }
                catch
                {
                    return null;
                }
            }
        }


        public async Task DeleteExpense(Guid guid)
        {
            using var context = new BudgetContext();
            try
            {
                Expense expense = await context.Expenses.Where(b => b.ExpenseId == guid).FirstOrDefaultAsync();
                context.Expenses.Remove(expense);
                context.SaveChanges();
            }

            catch
            {
                throw;
            }
        }

        public async Task EditExpense(Guid oldExpense, EditExpense editedExpense)
        {
            var newCategoryId = CategoryIdSwapper(editedExpense);
            using (var context = new BudgetContext())
            {
                try
                {
                    var expense = await context.Expenses.Where(i => i.ExpenseId == oldExpense).FirstOrDefaultAsync();
                    expense.DateStamp = editedExpense.TimeStamp;
                    expense.Amount = editedExpense.Amount;
                    expense.Comment = editedExpense.Comment;
                    expense.Receiver = editedExpense.Receiver;
                    expense.CategoryId = newCategoryId;
                    context.SaveChanges();

                }
                catch
                {
                    throw;
                }
            }
        }

        public void CreateExpense(decimal InputAmount,
                                    string InputReceiver,
                                  DateTime InputTimeStamp,
                                    string InputComment,
                                       Guid InputUserId,
                                    string InputCategoryName)
        {
            using (var context = new BudgetContext())
            {

                var category = context.Categories.Include(e => e.Expenses).FirstOrDefault(c => c.CategoryName == InputCategoryName);
                var user = context.Users.Find(InputUserId);

                if (category == null)
                {
                    category = new Category()
                    {
                        CategoryName = InputCategoryName,

                    };
                    context.Categories.Add(category);


                }

                var expense = new Expense()
                {
                    Amount = InputAmount,
                    Receiver = InputReceiver,
                    DateStamp = InputTimeStamp,
                    Comment = InputComment,
                    UserId = InputUserId,
                    Category = category

                };

                try
                {
                    context.Expenses.Add(expense);
                    context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public SpentSumsAllCategoriesDTO GetSpentSums(Guid userId)
        {

            using (var context = new BudgetContext())
            {
                var ExpensesThisyear = context.Expenses.Where(e => e.UserId == userId);

                return new SpentSumsAllCategoriesDTO();
            }
        }

        public SpentThisYearAndMonthDTO GetSpentThisYearAndMonth(Guid userId)
        {
            var dateToday = DateTime.Today;



            var dateFirstThisMonth = dateToday.AddDays(-(DateTime.Now.Day - 1));

            var dateFirstThisYear = dateToday.AddDays(-(DateTime.Now.Day - 1)).AddMonths(-(DateTime.Now.Month - 1));

            var spentThisMonth = GetSumSpentByDate(new DateRangeDTO { UserId = userId, EndDate = dateFirstThisMonth.AddMonths(1).AddDays(-1), StartDate = dateFirstThisMonth });
            var spentThisYear = GetSumSpentByDate(new DateRangeDTO { UserId = userId, EndDate = dateFirstThisMonth.AddMonths(1).AddDays(-1), StartDate = dateFirstThisYear });
            return new SpentThisYearAndMonthDTO()
            {
                SpentThisYear = spentThisYear,
                SpentThisMonth = spentThisMonth
            };
        }

        public decimal GetSumSpentByDate(DateRangeDTO dateRangeDTO)
        {
            using (var context = new BudgetContext())
            {
                var AllExpenseAmounts = context.Expenses
                    .Where(e => e.UserId == dateRangeDTO.UserId
                    && e.DateStamp >= dateRangeDTO.StartDate &&
                    e.DateStamp <= dateRangeDTO.EndDate).Select(e => e.Amount);

                decimal SumSpent = 0;
                foreach (var amount in AllExpenseAmounts)
                {
                    SumSpent += amount;
                }
                return SumSpent;
            }

        }

        public SumsSpentPerMonthDTO GetSumsSpentPerMonthThisYear(Guid userId)
        {
            var dateFirstThisYear = DateTime.Today.AddDays(-(DateTime.Now.Day - 1)).AddMonths(-(DateTime.Now.Month - 1));
            var sumsSpent = new SumsSpentPerMonthDTO()
            {
                SumJanuary = GetSumSpentByDate(new DateRangeDTO { UserId = userId, StartDate = dateFirstThisYear, EndDate = dateFirstThisYear.AddMonths(1).AddDays(-1) }),
                SumFebruary = GetSumSpentByDate(new DateRangeDTO { UserId = userId, StartDate = dateFirstThisYear.AddMonths(1), EndDate = dateFirstThisYear.AddMonths(2).AddDays(-1) }),
                SumMarch = GetSumSpentByDate(new DateRangeDTO { UserId = userId, StartDate = dateFirstThisYear.AddMonths(2), EndDate = dateFirstThisYear.AddMonths(3).AddDays(-1) }),
                SumApril = GetSumSpentByDate(new DateRangeDTO { UserId = userId, StartDate = dateFirstThisYear.AddMonths(3), EndDate = dateFirstThisYear.AddMonths(4).AddDays(-1) }),
                SumMay = GetSumSpentByDate(new DateRangeDTO { UserId = userId, StartDate = dateFirstThisYear.AddMonths(4), EndDate = dateFirstThisYear.AddMonths(5).AddDays(-1) }),
                SumJune = GetSumSpentByDate(new DateRangeDTO { UserId = userId, StartDate = dateFirstThisYear.AddMonths(5), EndDate = dateFirstThisYear.AddMonths(6).AddDays(-1) }),
                SumJuly = GetSumSpentByDate(new DateRangeDTO { UserId = userId, StartDate = dateFirstThisYear.AddMonths(6), EndDate = dateFirstThisYear.AddMonths(7).AddDays(-1) }),
                SumAugust = GetSumSpentByDate(new DateRangeDTO { UserId = userId, StartDate = dateFirstThisYear.AddMonths(7), EndDate = dateFirstThisYear.AddMonths(8).AddDays(-1) }),
                SumSeptember = GetSumSpentByDate(new DateRangeDTO { UserId = userId, StartDate = dateFirstThisYear.AddMonths(8), EndDate = dateFirstThisYear.AddMonths(9).AddDays(-1) }),
                SumOctober = GetSumSpentByDate(new DateRangeDTO { UserId = userId, StartDate = dateFirstThisYear.AddMonths(9), EndDate = dateFirstThisYear.AddMonths(10).AddDays(-1) }),
                SumNovember = GetSumSpentByDate(new DateRangeDTO { UserId = userId, StartDate = dateFirstThisYear.AddMonths(10), EndDate = dateFirstThisYear.AddMonths(11).AddDays(-1) }),
                SumDecember = GetSumSpentByDate(new DateRangeDTO { UserId = userId, StartDate = dateFirstThisYear.AddMonths(11), EndDate = dateFirstThisYear.AddMonths(12).AddDays(-1) }),
            };

            return sumsSpent;
        }



        public int CategoryIdSwapper(EditExpense editexpense)
        {
            switch (editexpense.CategoryName)
            {
                case "Food":
                    return 1;
                case "Shopping":
                    return 2;
                case "Entertainment":
                    return 3;
                case "Housing":
                    return 4;
                case "Transportation":
                    return 5;
                case "Other":
                    return 6;
                default:
                    return 0;
            }
        }

        public List<Expense> GetExpensesByDate(DateRangeDTO dateRangeDTO)
        {
            using (var context = new BudgetContext())
            {
                var allExpensesBetweenDate = context.Expenses
                    .Where(e => e.UserId == dateRangeDTO.UserId
                    && e.DateStamp >= dateRangeDTO.StartDate &&
                    e.DateStamp <= dateRangeDTO.EndDate).Include("Category").ToList();

                return allExpensesBetweenDate;
            }
        }

    }
}