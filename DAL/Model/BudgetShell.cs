using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Model
{
    public class BudgetShell
    {
        [Key]
        public Guid BudgetShellId { get; set; }
        public string? BudgetName { get; set; }
        public Guid UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        
        public List<BudgetPerCategory> BudgetsPerCategory { get; set; }
     
 
    }
}