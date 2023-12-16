public class Product
{
    public string ImagePath => $"/Images/{ImageFilename}";

    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string ImageFilename { get; set; }
    public int StockLevel { get; set; }
}