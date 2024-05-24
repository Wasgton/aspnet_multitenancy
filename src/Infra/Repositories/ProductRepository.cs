using multitenancy.Application.Models;
using multitenancy.Application.Repositories;
using multitenancy.Infra.Config.Db;

namespace multitenancy.Infra.Repositories;

public class ProductRepository: IProductRepository
{
    private readonly ApplicationDbContext _context;
    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public IEnumerable<Product> GetAllProducts()
    {
        return _context.Products.ToList();
    }

    public Product CreateProduct(Product product)
    {
        _context.Products.Add(product);
        _context.SaveChanges();
        return product;
    }

    public Product? GetProductById(int id)
    {
        return _context.Products.Find(id);
    }
    
    public bool DeleteProduct(Product product)
    {
        _context.Remove(product);
        _context.SaveChanges();
        return true;
    }
}