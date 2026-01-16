namespace ECom.Application.DTOs.Product;

public class UpdateProductDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string Image { get; set; }
    public int CategoryId { get; set; }
}
