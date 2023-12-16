namespace CoffeeShop;

/// <summary>
/// Interaction logic for Login.xaml
/// </summary>
public partial class Login : Window
{
    public Login()
    {
        InitializeComponent();
    }

    private void LoginButton_Click(object sender, RoutedEventArgs e)
    {
        if (AuthenticationService.Login(UsernameTextBox.Text, PasswordPasswordBox.Password))
        {
            MessageBox.Show("Welcome to Coffe Shop.");

            Main main = new();
            main.Show();
            Close();
        }
        else
        {
            MessageBox.Show("Invalid Username or Password.");

            UsernameTextBox.Text = PasswordPasswordBox.Password = null;
        }
    }

    private void ExitButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}