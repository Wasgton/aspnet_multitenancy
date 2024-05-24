using System.ComponentModel.DataAnnotations;

namespace multitenancy.Infra.Rest.Dtos.Tenant;

public class CreateTenantRequest
{
    [Required]
    public string Name { get; set; }

    public CreateTenantRequest(string name)
    {
        Name = name;
    }
}