using multitenancy.Application.Models;

namespace multitenancy.Application.Repositories;

public interface IProductRepository
{
    IEnumerable<Product> GetAllProducts();
    Product CreateProduct(Product product);
    Product? GetProductById(int id);
    bool DeleteProduct(Product product);
}