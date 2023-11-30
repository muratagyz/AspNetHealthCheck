namespace HealthCheck.Api.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Stock { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedDate { get; set; }

    public Product()
    {
        
    }

    public Product(string name, int stock, decimal price)
    {
        Name = name;
        Stock = stock;
        Price = price;
        CreatedDate = DateTime.Now;
    }
}
