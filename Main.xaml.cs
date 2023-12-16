namespace food;

public partial class Main : Window
{
    public bool IsOwner = AuthenticationService.User.Role == UserRole.Owner;
    private CustomerWindow _customerWindow;
    private ProductWindow _productWindow;
    private OrderWindow _orderWindow;
    private UserWindow _userWindow;

    public Main()
    {
        InitializeComponent();

        UserMenuItem.IsEnabled = IsOwner;

        using DataContext context = new();
        CustomerComboBox.ItemsSource = context.Customers.ToArray();
        OrderItemItemsControl.ItemsSource = context.Products.Select(product => new OrderItem { Product = product, ProductId = product.Id, Subtotal = product.Price }).ToArray();
    }
    private void HomepageMenuItem_Click(object sender, RoutedEventArgs e)
    {
        using DataContext context = new();
        CustomerComboBox.ItemsSource = context.Customers.ToArray();
        OrderItemItemsControl.ItemsSource = context.Products.Select(product => new OrderItem { Product = product, ProductId = product.Id, Subtotal = product.Price }).ToArray();

        HomepageGrid.Visibility = Visibility.Visible;
        CustomerGrid.Visibility = Visibility.Collapsed;
        ProductGrid.Visibility = Visibility.Collapsed;
        OrderGrid.Visibility = Visibility.Collapsed;
        UserGrid.Visibility = Visibility.Collapsed;
    }

    #region Customer
    private void CustomerMenuItem_Click(object sender, RoutedEventArgs e)
    {
        using DataContext context = new();
        CustomerDataGrid.ItemsSource = context.Customers.ToArray();

        HomepageGrid.Visibility = Visibility.Collapsed;
        CustomerGrid.Visibility = Visibility.Visible;
        ProductGrid.Visibility = Visibility.Collapsed;
        OrderGrid.Visibility = Visibility.Collapsed;
        UserGrid.Visibility = Visibility.Collapsed;
    }

    private void CustomerAddButton_Click(object sender, RoutedEventArgs e)
    {
        if (!_customerWindow?.IsVisible ?? true)
        {
            _customerWindow = new();
            _customerWindow.Title = "New Customer";
            _customerWindow.ShowDialog();

            using DataContext context = new();
            CustomerDataGrid.ItemsSource = context.Customers.ToArray();
        }
    }

    private void CustomerDataGrid_MouseUp(object sender, MouseButtonEventArgs e)
    {
        var source = (DependencyObject)e.OriginalSource;

        while (source != null && source is not DataGridCell && source is not DataGridColumnHeader)
        {
            source = VisualTreeHelper.GetParent(source);
        }

        if (source == null)
            return;

        if (source is DataGridCell)
        {
            if (!_customerWindow?.IsVisible ?? true)
            {
                var customer = (Customer)CustomerDataGrid.SelectedItem;
                if (customer != null)
                {
                    _customerWindow = new(customer.Id);
                    _customerWindow.Title = $"Customer {customer.FullName}";
                    _customerWindow.ShowDialog();
                    using DataContext context = new();
                    CustomerDataGrid.ItemsSource = context.Customers.ToArray();
                }
            }
        }
    }
    #endregion

    #region Product
    private void ProductMenuItem_Click(object sender, RoutedEventArgs e)
    {
        using DataContext context = new();
        ProductDataGrid.ItemsSource = context.Products.ToArray();

        HomepageGrid.Visibility = Visibility.Collapsed;
        CustomerGrid.Visibility = Visibility.Collapsed;
        ProductGrid.Visibility = Visibility.Visible;
        OrderGrid.Visibility = Visibility.Collapsed;
        UserGrid.Visibility = Visibility.Collapsed;
    }

    private void ProductAddButton_Click(object sender, RoutedEventArgs e)
    {
        if (!_productWindow?.IsVisible ?? true)
        {
            _productWindow = new();
            _productWindow.Title = "New Product";
            _productWindow.ShowDialog();

            using DataContext context = new();
            ProductDataGrid.ItemsSource = context.Products.ToArray();
        }
    }

    private void ProductDataGrid_MouseUp(object sender, MouseButtonEventArgs e)
    {
        var source = (DependencyObject)e.OriginalSource;

        while (source != null && source is not DataGridCell && source is not DataGridColumnHeader)
        {
            source = VisualTreeHelper.GetParent(source);
        }

        if (source == null)
            return;

        if (source is DataGridCell)
        {
            if (!_productWindow?.IsVisible ?? true)
            {
                var product = (Product)ProductDataGrid.SelectedItem;
                if (product != null)
                {
                    _productWindow = new(product.Id);
                    _productWindow.Title = $"Product {product.Name}";
                    _productWindow.ShowDialog();
                    using DataContext context = new();
                    ProductDataGrid.ItemsSource = context.Products.ToArray();
                }
            }
        }
    }
    #endregion

    #region Order
    private void OrderMenuItem_Click(object sender, RoutedEventArgs e)
    {
        using DataContext context = new();
        OrderDataGrid.ItemsSource = context.Orders.Include(order => order.Customer).Include(order => order.Items).ThenInclude(item => item.Product).ToArray();

        HomepageGrid.Visibility = Visibility.Collapsed;
        CustomerGrid.Visibility = Visibility.Collapsed;
        ProductGrid.Visibility = Visibility.Collapsed;
        OrderGrid.Visibility = Visibility.Visible;
        UserGrid.Visibility = Visibility.Collapsed;
    }

    private void OrderAddButton_Click(object sender, RoutedEventArgs e)
    {
        var orderItems = (IEnumerable<OrderItem>)OrderItemItemsControl.ItemsSource;
        orderItems = orderItems?.Where(item => item.Quantity > 0);
        if (!(orderItems?.Any() ?? false))
        {
            MessageBox.Show("Select at least one Item.");
        }
        else
        {
            var customer = (Customer)CustomerComboBox.SelectedItem;
            if (customer == null)
            {
                MessageBox.Show("Select a Customer.");
            }
            else
            {
                if (!_orderWindow?.IsVisible ?? true)
                {
                    var paymentMethod = CashRadioButton.IsChecked == true ? PaymentMethod.Cash : PaymentMethod.Card;
                    _orderWindow = new(paymentMethod, customer, orderItems);
                    _orderWindow.ShowDialog();

                    using DataContext context = new();
                    UserDataGrid.ItemsSource = context.Users.ToArray();
                    OrderItemItemsControl.ItemsSource = context.Products.Select(product => new OrderItem { Product = product, ProductId = product.Id, Subtotal = product.Price }).ToArray();
                }
            }
        }
    }
    #endregion

    #region User
    private void UserMenuItem_Click(object sender, RoutedEventArgs e)
    {
        using DataContext context = new();
        UserDataGrid.ItemsSource = context.Users.ToArray();

        HomepageGrid.Visibility = Visibility.Collapsed;
        CustomerGrid.Visibility = Visibility.Collapsed;
        ProductGrid.Visibility = Visibility.Collapsed;
        OrderGrid.Visibility = Visibility.Collapsed;
        UserGrid.Visibility = Visibility.Visible;
    }

    private void UserAddButton_Click(object sender, RoutedEventArgs e)
    {
        if (!_userWindow?.IsVisible ?? true)
        {
            _userWindow = new();
            _userWindow.Title = "New User";
            _userWindow.ShowDialog();

            using DataContext context = new();
            UserDataGrid.ItemsSource = context.Users.ToArray();
        }
    }

    private void UserDataGrid_MouseUp(object sender, MouseButtonEventArgs e)
    {
        var source = (DependencyObject)e.OriginalSource;

        while (source != null && source is not DataGridCell && source is not DataGridColumnHeader)
        {
            source = VisualTreeHelper.GetParent(source);
        }

        if (source == null)
            return;

        if (source is DataGridCell)
        {
            if (!_userWindow?.IsVisible ?? true)
            {
                var user = (User)UserDataGrid.SelectedItem;
                if (user != null)
                {
                    _userWindow = new(user.Id);
                    _userWindow.Title = $"User {user.FullName}";
                    _userWindow.ShowDialog();
                    using DataContext context = new();
                    UserDataGrid.ItemsSource = context.Users.ToArray();
                }
            }
        }
    }
    #endregion
}
