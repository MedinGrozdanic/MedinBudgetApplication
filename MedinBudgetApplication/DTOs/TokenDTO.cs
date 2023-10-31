namespace API.DTOs
{
    public class TokenDTO
    {
        public Guid userId { get; set; }
        public string UserInput { get; set; }
        public string Password { get; set; }
    }
}