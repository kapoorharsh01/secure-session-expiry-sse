namespace RsaAuth.Backend.DTOs
{

    public class SignupDto
    {
        public string FullName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }


}
