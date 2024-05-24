namespace multitenancy.Application.Repositories;

public interface ICurrentTenant
{
    Guid? TenantId { get; set; }
    public Task<bool> SetTenant(Guid tenant);
}