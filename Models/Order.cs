public class Order
{
    public string ItemNames => Items != null ? string.Join(", ", Items.Select(item => item.Product?.Name ?? "")) : string.Empty;

    public int Id { get; set; }
    public DateTime Date { get; set; }
    public decimal TotalPrice { get; set; }
    public PaymentMethod Method { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
    public List<OrderItem> Items { get; set; } = new List<OrderItem>();
}