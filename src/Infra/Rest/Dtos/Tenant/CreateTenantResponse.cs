using System.ComponentModel.DataAnnotations;

namespace multitenancy.Infra.Rest.Dtos.Tenant;

public class CreateTenantResponse
{
    [Required]
    public string Id { get; set; }
    [Required]
    public string Name { get; set; }
    public CreateTenantResponse(string id, string name)
    {
        Id = id;
        Name = name;
    }
}