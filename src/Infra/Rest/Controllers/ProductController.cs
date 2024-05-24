using Microsoft.AspNetCore.Mvc;
using multitenancy.Application.Models;
using multitenancy.Application.Services;
using multitenancy.Infra.Rest.Dtos.Product;

namespace multitenancy.Infra.Rest.Controllers;

[ApiController]
[Route("api/[controller]/")]
public class ProductController : ControllerBase
{
    private ProductService _productService;
    public ProductController(ProductService service)
    {
        _productService = service;
    }
    
    [HttpGet]
    public ActionResult GetAllProducts()
    {
        var result = _productService.GetAllProducts();
        return Ok(result);
    }

    [HttpPost("create")]
    public ActionResult CreateProduct([FromBody] CreateProductRequest productRequest)
    {
        var result = _productService.CreateProduct(productRequest);
        if (result is not Product) 
            return BadRequest("Error while registering the product");
        CreateProductResponse output = new CreateProductResponse(result.Id, result.Name, result.Description);
        return Ok(output);
    }

    [HttpDelete("delete/{id}")]
    public ActionResult DeleteProduct(int id)
    {
        _productService.DeleteProduct(id);
        return Ok("Product Deleted");
    }
}