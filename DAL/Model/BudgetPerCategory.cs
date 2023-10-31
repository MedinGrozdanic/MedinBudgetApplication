using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Model
{
    public class BudgetPerCategory    
    {
        [Key]
        public Guid CategoryBudgetId { get; set; }
        public Guid BudgetShellId { get; set; }
        public double MaxAmount { get; set; }
        public int CategoryId { get; set; }   
        public Category Category { get; set; }
    }
}