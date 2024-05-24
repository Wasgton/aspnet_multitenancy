using System.ComponentModel.DataAnnotations;

namespace multitenancy.Infra.Rest.Dtos.Product;

public class CreateProductRequest
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }

    public CreateProductRequest(string name, string description )
    {
        Name = name;
        Description = description;
    }
}