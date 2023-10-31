using Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.ServiceInterfaces
{
    public interface IBudgetService
    {
        public void CreateBudget(CreateBudgetDTO createBudgetDTO);

        public GetBudgetDTO GetBudget(Guid budgetID);

        public List<BudgetHistogramDTO> GetBudgetForHistogram(Guid userId);
    }
}
