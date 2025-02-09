namespace Core.DTOs
{
    public class UserAddDTO
    {
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public int Age { get; set; }
        public string PhotoUrl { get; set; } = string.Empty;
    }
}
