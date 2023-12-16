public class DataContext : DbContext
{
    private const string ConnectionString = "Data Source=MIDHUN\\SQLEXPRESS; Initial Catalog=coffeeshop;User ID=MIDHUN\\midhu; Integrated Security=True;";

    public DataContext()
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseMySql(ConnectionString, ServerVersion.AutoDetect(ConnectionString));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, FirstName = "Midhun", LastName = "Paulose", Username = "Midhun", Password = "1234", Role = UserRole.Owner },
            new User { Id = 2, FirstName = "Namiq", LastName = "Ismayilov", Username = "namiq", Password = "1234", Role = UserRole.Owner }
        );

        modelBuilder.Entity<FoodItem>().HasData(
            new FoodItem { Id = 1, Name = "BURGER", Description = "Yummy", ImageFilename = "BURGER.png", Price = 4.30m, StockLevel = 5 },
            new FoodItem { Id = 2, Name = "SALAD", Description = "Yummy", ImageFilename = "SALAD.png", Price = 2.50m, StockLevel = 3 },
            new FoodItem { Id = 3, Name = "FRIES", Description = "Yummy", ImageFilename = "FRIES.png", Price = 6.20m, StockLevel = 4 }
        );

        modelBuilder.Entity<Customer>().HasData(
            new Customer { Id = 1, FirstName = "Tom", LastName = "Hanks", Email = "hanks.tom@hotmail.com", Phone = "123-456-789", LoyaltyPoints = 10 },
            new Customer { Id = 2, FirstName = "John", LastName = "Wick", Email = "ilovemydog@gmail.com", Phone = "666-666-666", LoyaltyPoints = 100 },
            new Customer { Id = 3, FirstName = "Bruce", LastName = "Wayne", Email = "ilovemybat@outlook.com", Phone = "987-654-321", LoyaltyPoints = 0 }
        );
    }

    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    public DbSet<FoodItem> FoodItems => Set<FoodItem>(); // Changed from Product to FoodItem
    public DbSet<User> Users => Set<User>();
}
