namespace API.DTOs
{
    public class GetExpenseDTO
    {
        public Guid ExpenseId { get; set; }
        public decimal Amount { get; set; }
        public string Receiver { get; set; }
        public DateTime TimeStamp { get; set; }
        public string? Comment { get; set; }
        public List<string> CategoryName { get; set; }
    }
}