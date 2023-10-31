using API.DTOs;
using DAL.Model;
using Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.ServiceInterfaces
{
    public interface IExpenseService
    {
        public PieChartDTO GetSumPerCategory(Guid userId);

        public List<Expense> ListExpensesWithCategory(Guid userId);

        public List<Expense> ListExpenses(Guid userId);

        public Task<Expense> FindExpense(Guid guid);

        public Task DeleteExpense(Guid guid);

        public Task EditExpense(Guid oldExpense, EditExpense editedExpense);

        public void CreateExpense(decimal InputAmount,
                                    string InputReceiver,
                                  DateTime InputTimeStamp,
                                    string InputComment,
                                       Guid InputUserId,
                                    string InputCategoryName);

        public SpentSumsAllCategoriesDTO GetSpentSums(Guid userId);

        public SpentThisYearAndMonthDTO GetSpentThisYearAndMonth(Guid userId);

        public decimal GetSumSpentByDate(DateRangeDTO dateRangeDTO);

        public SumsSpentPerMonthDTO GetSumsSpentPerMonthThisYear(Guid userId);

        public int CategoryIdSwapper(EditExpense editexpense);
        public List<Expense> GetExpensesByDate(DateRangeDTO dateRangeDTO);
    }
}
