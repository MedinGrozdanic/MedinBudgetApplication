using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DAL.Model
{
    [Index(nameof(CategoryName), IsUnique = true)]
    public class Category
    {
        [Key]
        public int CategoryId{ get; set; }
        public string CategoryName { get; set; }
        public List<Expense>? Expenses { get; set; }
    }
}