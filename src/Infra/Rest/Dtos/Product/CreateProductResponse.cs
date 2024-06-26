namespace multitenancy.Infra.Rest.Dtos.Product;

public class CreateProductResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public CreateProductResponse(int id, string name, string description )
    {
        Id = id;
        Name = name;
        Description = description;
    }
}