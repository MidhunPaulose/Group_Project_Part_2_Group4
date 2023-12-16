public static class AuthenticationService
{
    public static User User { get; private set; }
#if DEBUG
        = new DataContext().Users.First();
#endif

    public static bool Login(string username, string password)
    {
        using DataContext context = new();

        User = context.Users.FirstOrDefault(staff => staff.Username == username && staff.Password == password);

        return User != null;
    }
}