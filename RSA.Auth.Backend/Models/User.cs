namespace RsaAuth.Backend.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateOnly DateOfBirth {  get; set; }
        public string Mobile  { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }

    public static class SessionStore
    {
        public static DateTime SessionExpiry;
    }

}
