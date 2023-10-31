using DAL.Model;

namespace Service.DTOs
{
    public class CreateBudgetDTO
    {
        public string BudgetName { get; set; }
        public int Amount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public List<CategoryBudgetDTO> CategoryBudgetDTOs { get; set; }

        public Guid UserId { get; set; }
    }
}