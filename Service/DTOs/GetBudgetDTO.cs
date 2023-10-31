using DAL.Model;

namespace Service.DTOs
{
    public class GetBudgetDTO
    {
        public Guid BudgetId { get; set; }
        public string BudgetName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid UserId { get; set; }

        public List<CategoryBudgetDTO> CategoriesBudgetDTO { get; set; }
    }
}