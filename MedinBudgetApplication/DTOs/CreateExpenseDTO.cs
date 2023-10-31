namespace API.DTOs
{
    public class CreateExpenseDTO
    {
        public decimal Amount { get; set; }
        public string Receiver { get; set; }
        public DateTime TimeStamp { get; set; }
        public string? Comment { get; set; }
        public Guid UserId { get; set; }
        public string CategoryName { get; set; }
    }
}