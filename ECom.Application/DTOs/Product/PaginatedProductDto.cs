namespace ECom.Application.DTOs.Product;

public class PaginatedProductDto
{
    public IEnumerable<ProductDto> Products { get; set; }
    public int TotalCount { get; set; }
}
