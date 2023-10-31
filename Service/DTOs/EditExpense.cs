using DAL.Model;

namespace API.DTOs
{
    public class EditExpense
    {
        public decimal Amount { get; set; }
        public string Receiver { get; set; }
        public DateTime TimeStamp { get; set; }
        public string? Comment { get; set; }

        public string CategoryName { get; set; }

    }
}