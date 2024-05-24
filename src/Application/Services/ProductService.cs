using multitenancy.Infra.Rest.Dtos.Product;
using multitenancy.Application.Models;
using multitenancy.Application.Repositories;

namespace multitenancy.Application.Services;

public class ProductService(IProductRepository repository) 
{
    public IEnumerable<Product> GetAllProducts()
    {
        return repository.GetAllProducts();
    }

    public Product CreateProduct(CreateProductRequest request)
    {
        Product product = new Product(request.Name, request.Description);
        return repository.CreateProduct(product);
    }

    public Product? GetProductById(int id)
    {
        if (id == 0) return null;
        return repository.GetProductById(id);
    }
    
    public bool DeleteProduct(int id)
    {
        var product = repository.GetProductById(id);
        if (product is null)
            throw new Exception("Product Not Found");
        return repository.DeleteProduct(product);
    }
}