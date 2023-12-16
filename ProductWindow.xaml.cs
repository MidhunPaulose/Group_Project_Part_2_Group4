namespace CoffeeShop;

/// <summary>
/// Interaction logic for ProductWindow.xaml
/// </summary>
public partial class ProductWindow : Window
{
    public int? ProductId { get; }
    public Product Product { get; }

    public ProductWindow(int? productId = null)
    {
        InitializeComponent();

        ProductId = productId;

        using DataContext context = new();
        Product = ProductId.HasValue ? context.Products.Find(ProductId) : new();
        NameTextBox.Text = Product.Name;
        DescriptionTextBox.Text = Product.Description;
        PriceTextBox.Text = Product.Price.ToString();
        ImageFilenameTextBox.Text = Product.ImageFilename;
        StockLevelTextBox.Text = Product.StockLevel.ToString();

        DeleteButton.IsEnabled = ProductId.HasValue;
    }

    private void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        if (!decimal.TryParse(PriceTextBox.Text, out var price))
        {
            MessageBox.Show("Invalid Price.");
        }
        else if (!int.TryParse(StockLevelTextBox.Text, out var stocklevel))
        {
            MessageBox.Show("Invalid Stock Level.");
        }
        else
        {
            Product.Name = NameTextBox.Text;
            Product.Description = DescriptionTextBox.Text;
            Product.Price = price;
            Product.ImageFilename = ImageFilenameTextBox.Text;
            Product.StockLevel = stocklevel;

            DataContext context = new();
            if (ProductId.HasValue)
            {
                context.Products.Update(Product);

                if (context.SaveChanges() > 0)
                {
                    MessageBox.Show("Product successfully updated.");
                    Close();
                }
                else
                {
                    MessageBox.Show("Could not update Product.");
                }
            }
            else
            {
                context.Products.Add(Product);

                if (context.SaveChanges() > 0)
                {
                    MessageBox.Show("Product successfully created.");
                    Close();
                }
                else
                {
                    MessageBox.Show("Could not create Product.");
                }
            }
        }
    }

    private void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        DataContext context = new();
        context.Products.Remove(Product);
        if (context.SaveChanges() > 0)
        {
            MessageBox.Show("Product successfully deleted.");
            Close();
        }
        else
        {
            MessageBox.Show("Could not delete Product.");
        }
    }
}