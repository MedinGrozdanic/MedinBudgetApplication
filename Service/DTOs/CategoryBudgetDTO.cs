using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs
{
    public class CategoryBudgetDTO
    {
        
        public double Amount { get; set; }
        public Category Category { get; set; }
    }
}
