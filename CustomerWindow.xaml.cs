namespace food;

/// <summary>
/// Interaction logic for CustomerWindow.xaml
/// </summary>
public partial class CustomerWindow : Window
{
    public int? CustomerId { get; }
    public Customer Customer { get; }

    public CustomerWindow(int? customerId = null)
    {
        InitializeComponent();

        CustomerId = customerId;

        using DataContext context = new();
        Customer = CustomerId.HasValue ? context.Customers.Find(CustomerId) : new();
        FirstNameTextBox.Text = Customer.FirstName;
        LastNameTextBox.Text = Customer.LastName;
        EmailTextBox.Text = Customer.Email;
        PhoneTextBox.Text = Customer.Phone;
        LoyaltyPointsTextBox.Text = Customer.LoyaltyPoints.ToString();

        DeleteButton.IsEnabled = CustomerId.HasValue;
    }

    private void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        if (!int.TryParse(LoyaltyPointsTextBox.Text, out var loyaltyPoints))
        {
            MessageBox.Show("Invalid Loyalty Points.");
        }
        else
        {
            Customer.FirstName = FirstNameTextBox.Text;
            Customer.LastName = LastNameTextBox.Text;
            Customer.Email = EmailTextBox.Text;
            Customer.Phone = PhoneTextBox.Text;
            Customer.LoyaltyPoints = loyaltyPoints;

            DataContext context = new();
            if (CustomerId.HasValue)
            {
                context.Customers.Update(Customer);

                if (context.SaveChanges() > 0)
                {
                    MessageBox.Show("Customer successfully updated.");
                    Close();
                }
                else
                {
                    MessageBox.Show("Could not update Customer.");
                }
            }
            else
            {
                context.Customers.Add(Customer);

                if (context.SaveChanges() > 0)
                {
                    MessageBox.Show("Customer successfully created.");
                    Close();
                }
                else
                {
                    MessageBox.Show("Could not create Customer.");
                }
            }
        }
    }

    private void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        DataContext context = new();
        context.Customers.Remove(Customer);
        if (context.SaveChanges() > 0)
        {
            MessageBox.Show("Customer successfully deleted.");
            Close();
        }
        else
        {
            MessageBox.Show("Could not delete Customer.");
        }
    }
}
