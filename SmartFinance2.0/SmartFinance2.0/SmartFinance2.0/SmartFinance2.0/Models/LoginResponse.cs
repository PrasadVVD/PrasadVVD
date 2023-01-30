namespace SmartFinance.Models
{
    public class LoginResponse
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public UserRole Role { get; set; }
        public int AppType { get; set; }
        public string Code { get; set; }

        public string Token { get; set; }
    }

    public enum UserRole
    {
        Admin = 1,
        Employee
    }
}
