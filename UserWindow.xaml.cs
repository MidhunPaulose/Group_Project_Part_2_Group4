namespace food;

/// <summary>
/// Interaction logic for UserWindow.xaml
/// </summary>
public partial class UserWindow : Window
{
    public int? UserId { get; }
    public User User { get; }

    public UserWindow(int? userId = null)
    {
        InitializeComponent();

        UserId = userId;

        using DataContext context = new();
        User = UserId.HasValue ? context.Users.Find(UserId) : new();
        FirstNameTextBox.Text = User.FirstName;
        LastNameTextBox.Text = User.LastName;
        UsernameTextBox.Text = User.Username;
        PasswordPasswordBox.Password = User.Password;
        EmployeeRadioButton.IsChecked = User.Role == UserRole.Employee;
        OwnerRadioButton.IsChecked = User.Role == UserRole.Owner;
        ContactInfoTextBox.Text = User.ContactInfo;

        DeleteButton.IsEnabled = UserId.HasValue;
    }

    private void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        User.FirstName = FirstNameTextBox.Text;
        User.LastName = LastNameTextBox.Text;
        User.Username = UsernameTextBox.Text;
        User.Password = PasswordPasswordBox.Password;
        User.Role = EmployeeRadioButton.IsChecked == true ? UserRole.Employee : UserRole.Owner;
        User.ContactInfo = ContactInfoTextBox.Text;

        DataContext context = new();
        if (UserId.HasValue)
        {
            context.Users.Update(User);

            if (context.SaveChanges() > 0)
            {
                MessageBox.Show("User successfully updated.");
                Close();
            }
            else
            {
                MessageBox.Show("Could not update User.");
            }
        }
        else
        {
            context.Users.Add(User);

            if (context.SaveChanges() > 0)
            {
                MessageBox.Show("User successfully created.");
                Close();
            }
            else
            {
                MessageBox.Show("Could not create User.");
            }
        }
    }

    private void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        DataContext context = new();
        context.Users.Remove(User);
        if (context.SaveChanges() > 0)
        {
            MessageBox.Show("User successfully deleted.");
            Close();
        }
        else
        {
            MessageBox.Show("Could not delete User.");
        }
    }
}
