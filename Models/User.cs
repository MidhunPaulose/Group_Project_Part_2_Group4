public class User
{
    public string FullName => $"{FirstName} {LastName}";

    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public UserRole Role { get; set; }
    public string ContactInfo { get; set; }
}