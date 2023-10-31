using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs
{
    public class BudgetHistogramDTO
    {
        public Category Category { get; set; }
        public double MaxAmount { get; set; }
        public decimal AmountSpent { get; set; }
    }
}
