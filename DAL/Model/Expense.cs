using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Model
{
    public class Expense
    {
        public Guid ExpenseId { get; set; }
        public Guid UserId { get; set; }

        [Column( TypeName = "decimal(18, 2)" )]
        [Range( 0 , 999999999999999999.99 )]
        public decimal Amount { get; set; }

        [StringLength( 50 )]
        public string Receiver { get; set; }
        public DateTime DateStamp { get; set; }

        [StringLength( 800 )]
        public string? Comment { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public Guid? CategoryBudgetID { get; set; }

    }
}