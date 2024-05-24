using multitenancy.Application.Models;

namespace multitenancy.Application.Repositories;

public interface ITenantRepository
{
    public IEnumerable<Tenant> GetAllTenants();
    public Tenant CreateTenant(Tenant tenant);
    public Tenant? GetTenantById(Guid id);
    public Tenant UpdateTenant(Tenant tenant);
    public void DeleteTenant(Tenant tenant);
    Tenant RestoreTenant(Tenant tenant);
}