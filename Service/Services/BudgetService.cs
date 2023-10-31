using API.DTOs;
using DAL;
using DAL.Model;
using Microsoft.EntityFrameworkCore;
using Service.DTOs;
using Service.Services.ServiceInterfaces;
using System.Linq;
using static Service.DTOs.GetBudgetDTO;

namespace Service.Services
{
    public class BudgetService : IBudgetService
    {
        private IExpenseService _expenseService;
        private ICategoryService _categoryService;
        //private static BudgetService? _instance;

        //public static BudgetService Instance
        //{
        //    get
        //    {
        //        if (_instance == null)
        //        {
        //            _instance = new BudgetService();
        //        }
        //        return _instance;
        //    }
        //}
        public BudgetService(IExpenseService expenseService, ICategoryService categoryService)
        {
            _expenseService = expenseService;
            _categoryService = categoryService;
        }

        public void CreateBudget(CreateBudgetDTO createBudgetDTO)
        {
            using (var context = new BudgetContext())
            {
                var budget = new BudgetShell()
                {
                    BudgetName = createBudgetDTO.BudgetName,
                    StartDate = createBudgetDTO.StartDate,
                    EndDate = createBudgetDTO.EndDate,
                    UserId = createBudgetDTO.UserId,

                };
                context.Budgets.Add(budget);
                foreach (var item in createBudgetDTO.CategoryBudgetDTOs)
                {
                    BudgetPerCategory catBud = new BudgetPerCategory()
                    {
                        BudgetShellId = budget.BudgetShellId,
                        MaxAmount = item.Amount,
                        Category = item.Category
                    };

                    context.CategoryBudgets.Add(catBud);
                    budget.BudgetsPerCategory.Add(catBud);
                }



                context.SaveChanges();
            }
        }

        public GetBudgetDTO GetBudget(Guid budgetID)
        {
            using (var context = new BudgetContext())
            {
                var budget = context.Budgets.Include(b => b.BudgetsPerCategory)
                    .FirstOrDefault(b => b.BudgetShellId == budgetID);

                GetBudgetDTO getBudgetDTO = new GetBudgetDTO()
                {
                    BudgetId = budget.BudgetShellId,
                    BudgetName = budget.BudgetName,
                    EndDate = budget.EndDate,
                    StartDate = budget.StartDate,
                    UserId = budget.UserId,

                };

                foreach (var categoryBudget in budget.BudgetsPerCategory)
                {
                    CategoryBudgetDTO categoryBudgetDTO = new CategoryBudgetDTO()
                    {
                        Amount = categoryBudget.MaxAmount,
                        Category = categoryBudget.Category

                    };
                }

                return getBudgetDTO;
            }
        }

        public List<BudgetHistogramDTO> GetMaxAmountsInBudget(Guid userId, List<BudgetHistogramDTO> budgetHistogramDTOs)
        {
            using(var context = new BudgetContext())
            {
                var budget = context.Budgets.OrderBy(b => b.StartDate).Include(b => b.BudgetsPerCategory).Last(u => u.UserId == userId);

                budget.BudgetsPerCategory.OrderBy(b => b.CategoryId);
                var i = 0;
                foreach(var category in budget.BudgetsPerCategory)
                {
                    budgetHistogramDTOs.Find(b => b.Category.CategoryId == category.CategoryId).MaxAmount = category.MaxAmount;
                }
            }
            return budgetHistogramDTOs;
        }


        public List<BudgetHistogramDTO> GetBudgetForHistogram(Guid userId)
        {
            var dateToday = DateTime.Today;
            var dateFirstThisMonth = dateToday.AddDays(-(DateTime.Now.Day - 1));
            var dateLastThisMonth = dateFirstThisMonth.AddMonths(1);
            var expensesThisMonth = _expenseService.GetExpensesByDate(new DateRangeDTO { UserId = userId, StartDate = dateFirstThisMonth, EndDate = dateLastThisMonth });
            List<BudgetHistogramDTO> budgetHistogramDTOs = SortExpensesByCategory(expensesThisMonth);
            budgetHistogramDTOs = GetMaxAmountsInBudget(userId, budgetHistogramDTOs);
            
            return budgetHistogramDTOs;

        }

        public List<BudgetHistogramDTO> SortExpensesByCategory(List<Expense> expensesToSort)
        {
            var categories = _categoryService.ListCategories().OrderBy(c => c.CategoryId);
            List<BudgetHistogramDTO> budgetHistogramDTOs = new List<BudgetHistogramDTO>();

            foreach (var category in categories)
            {
                budgetHistogramDTOs.Add(new BudgetHistogramDTO() { Category = category, AmountSpent = 0, MaxAmount = 0 });
            }
            foreach (var expense in expensesToSort)
            {
                switch (expense.Category.CategoryId)
                {
                    case 1:
                        budgetHistogramDTOs[0].AmountSpent += expense.Amount;
                        break;

                    case 2:
                        budgetHistogramDTOs[1].AmountSpent += expense.Amount;
                        break;

                    case 3:
                        budgetHistogramDTOs[2].AmountSpent += expense.Amount;
                        break;

                    case 4:
                        budgetHistogramDTOs[3].AmountSpent += expense.Amount;
                        break;

                    case 5:
                        budgetHistogramDTOs[4].AmountSpent += expense.Amount;
                        break;

                    case 6:
                        budgetHistogramDTOs[5].AmountSpent += expense.Amount;
                        break;
                }
            }
            return budgetHistogramDTOs;
        }

        //public GetBudgetDTO GetBudget(int budgetId)
        //{
        //    // lägg till från/till datum
        //    using (var context = new BudgetContext())
        //    {
        //        var budget = context.Budgets.Include(b => b.Categories).ThenInclude(e => e.Expenses).FirstOrDefault(b => b.BudgetId == budgetId);
        //        var catSumDTOs = new List<CategorySumDTO>();
        //        decimal total = 0;
        //        foreach (var category in budget.Categories)
        //        {
        //            decimal catSum = 0;
        //            foreach (var expense in category.Expenses)
        //            {
        //                catSum += expense.Amount;
        //            }
        //            catSumDTOs.Add(new CategorySumDTO
        //            {
        //                budgetCategoryName = category.CategoryName,
        //                budgetCategorySum = catSum
        //            });
        //            total += catSum;
        //            catSum = 0;
        //        }
        //        var result = new GetBudgetDTO()
        //        {
        //            BudgetId = budget.BudgetId,
        //            BudgetName = budget.BudgetName,
        //            StartDate = budget.StartDate,
        //            EndDate = budget.EndDate,
        //            Amount = budget.Amount,
        //            TotalSpent = total,
        //            Balance = budget.Amount - total,
        //            CategorySums = catSumDTOs,
        //        };
        //        return result;
        //    }
        //}

        //public GetBudgetDTO GetBudget(int budgetId, DateTime startDate, DateTime endDate)
        //{
        //    // lägg till från/till datum
        //    using (var context = new BudgetContext())
        //    {
        //        var budget = context.Budgets.Include(b => b.Categories).ThenInclude(e => e.Expenses.Where(e => e.TimeStamp >= startDate && e.TimeStamp <= endDate)).FirstOrDefault(b => b.BudgetId == budgetId);
        //        var catSumDTOs = new List<CategorySumDTO>();
        //        decimal total = 0;
        //        foreach (var category in budget.Categories)
        //        {
        //            decimal catSum = 0;
        //            foreach (var expense in category.Expenses)
        //            {
        //                catSum += expense.Amount;
        //            }
        //            catSumDTOs.Add(new CategorySumDTO
        //            {
        //                budgetCategoryName = category.CategoryName,
        //                budgetCategorySum = catSum
        //            });
        //            total += catSum;
        //            catSum = 0;
        //        }
        //        var result = new GetBudgetDTO()
        //        {
        //            BudgetId = budget.BudgetId,
        //            BudgetName = budget.BudgetName,
        //            StartDate = budget.StartDate,
        //            EndDate = budget.EndDate,
        //            Amount = budget.Amount,
        //            TotalSpent = total,
        //            Balance = budget.Amount - total,
        //            CategorySums = catSumDTOs,
        //        };
        //        return result;
        //    }
        //}




        //public GetBudgetDTO GetBudget(int budgetId)
        //{
        //    // lägg till från/till datum
        //    using (var context = new BudgetContext())
        //    {
        //        var budget = context.Budgets.Include(b => b.Categories).ThenInclude(e => e.Expenses).FirstOrDefault(b => b.BudgetId == budgetId);
        //        var catSumDTOs = new List<CategorySumDTO>();
        //        decimal total = 0;
        //        foreach (var category in budget.Categories)
        //        {
        //            decimal catSum = 0;
        //            foreach (var expense in category.Expenses)
        //            {
        //                catSum += expense.Amount;
        //            }
        //            catSumDTOs.Add(new CategorySumDTO
        //            {
        //                budgetCategoryName = category.CategoryName,
        //                budgetCategorySum = catSum
        //            });
        //            total += catSum;
        //        }
        //        var result = new GetBudgetDTO()
        //        {
        //            BudgetId = budget.BudgetId,
        //            BudgetName = budget.BudgetName,
        //            Amount = budget.Amount,
        //            TotalSpent = total,
        //            Balance = budget.Amount - total,
        //            CategorySums = catSumDTOs,
        //        };
        //        return result;
        //    }
        //}


        //"returnera id + summa per categori"
        // tuple?

        //     var query = database.Posts    // your starting point - table in the "from" statement
        //          Join(database.Post_Metas, // the source table of the inner join
        //          post => post.ID,        // Select the primary key (the first part of the "on" clause in an sql "join" statement)
        //          meta => meta.Post_ID,   // Select the foreign key (the second part of the "on" clause)
        //          (post, meta) => new { Post = post, Meta = meta }) // selection
        //         .Where(postAndMeta => postAndMeta.Post.ID == id);    // where statement

        //var query = from emp in emps
        //            join prod in prods
        //on emp.ProductID equals prod.ProductID
        //            where emp.EmployeeID == 10
        //            select employee;
        //var result = query.Include(u => u.Departments)
        //var createBudgetDTO = new GetBudgetDTO()
        //                      {
        //                          Amount = budget.Amount,
        //                          StartDate = budget.StartDate,
        //                          EndDate = budget.EndDate,
        //                          Categories = budget.Categories,
        //                          User = budget.User,
        //                      };


    }
}