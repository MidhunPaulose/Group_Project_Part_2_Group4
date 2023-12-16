namespace food;

/// <summary>
/// Interaction logic for OrderWindow.xaml
/// </summary>
public partial class OrderWindow : Window
{
    public Order Order { get; }

    public OrderWindow(PaymentMethod paymentMethod, Customer customer, IEnumerable<OrderItem> items)
    {
        InitializeComponent();

        Order = new()
        {
            Date = DateTime.Now,
            Method = paymentMethod,
            TotalPrice = 1.13m * items.Sum(item => item.Subtotal * item.Quantity),
            CustomerId = customer.Id,
            Items = items.ToList()
        };

        DateTextBox.Text = Order.Date.ToString();
        MethodTextBox.Text = Order.Method.ToString();
        TotalPriceTextBox.Text = Order.TotalPrice.ToString("C");
        CustomerTextBox.Text = $"{customer.FirstName} {customer.LastName}";
        ItemsDataGrid.ItemsSource = Order.Items;
    }

    private void ConfirmButton_Click(object sender, RoutedEventArgs e)
    {
        using DataContext context = new();
        var items = Order.Items;
        Order.Items = null;
        context.Add(Order);
        if (context.SaveChanges() > 0)
        {
            items.ForEach(item => { item.OrderId = Order.Id; item.ProductId = item.Product.Id; item.Product = null; });
            context.AddRange(items);
            if (context.SaveChanges() > 0)
            {
                var customer = context.Customers.FirstOrDefault(customer => customer.Id == Order.CustomerId);
                if (customer != null)
                {
                    customer.LoyaltyPoints += 10;
                    context.Customers.Update(customer);
                    context.SaveChanges();
                }

                MessageBox.Show("Order successfully placed.");
                Close();
            }
            else
            {
                MessageBox.Show("Could not place Order.");
            }
        }
        else
        {
            MessageBox.Show("Could not place Order.");
        }
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}
