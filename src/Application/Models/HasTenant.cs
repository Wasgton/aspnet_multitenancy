using System.ComponentModel.DataAnnotations.Schema;

namespace multitenancy.Application.Models;

public abstract class HasTenant
{
    [Column("tenant_id")]
    public Guid TenantId;
}