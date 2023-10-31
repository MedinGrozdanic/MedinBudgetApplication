using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DAL.Model
{
    [Index(nameof(Username), IsUnique = true)]
    [Index(nameof(Email), IsUnique = true)]
    public class User
    {
        public Guid UserId { get; set; }

        [StringLength( 50 , MinimumLength = 1 )]
        public string FirstName { get; set; }

        [StringLength(50, MinimumLength = 1)]
        public string LastName { get; set; }

        [StringLength(50, MinimumLength = 1)]
        public string Username { get; set; }

        [StringLength( 100 , MinimumLength = 1 )]
        public string Email { get; set; }

        [StringLength( 50 , MinimumLength = 1 )]
        public string Password { get; set; }
        public List<Expense>? Expenses { get; set; }
        public List<BudgetShell>? Budgets { get; set; }
    }
}