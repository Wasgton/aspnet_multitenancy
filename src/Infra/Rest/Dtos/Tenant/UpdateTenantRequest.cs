using System.ComponentModel.DataAnnotations;

namespace multitenancy.Infra.Rest.Dtos.Tenant;

public class UpdateTenantRequest(string name)
{
    [Required]
    public string Name { get; set; } = name;
}