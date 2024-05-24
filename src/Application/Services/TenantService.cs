using System.ComponentModel.DataAnnotations;
using multitenancy.Infra.Rest.Dtos.Tenant;
using multitenancy.Application.Models;
using multitenancy.Application.Repositories;

namespace multitenancy.Application.Services;

public class TenantService(ITenantRepository repository) 
{
    public IEnumerable<Tenant> GetAllTenants()
    {
        return repository.GetAllTenants();
    }
    public Tenant CreateTenant(CreateTenantRequest request)
    {
        Tenant tenant = new Tenant(request.Name);
        return repository.CreateTenant(tenant);
    }
    
    public Tenant? GetTenantById(string id)
    {
        return repository.GetTenantById(Guid.Parse(id));
    }

    public Tenant UpdateTenant(string id, UpdateTenantRequest request)
    {
        var tenant = repository.GetTenantById(Guid.Parse(id));
        if (tenant is null)
            throw new ValidationException("Tenant not Found!");
        if (tenant.DeletedAt is not null)
            throw new ValidationException("Tenant deleted, restore it first to update");
        tenant.Name = request.Name;
        return repository.UpdateTenant(tenant);
    }

    public void DeleteTenant(string id)
    {
        var tenant = repository.GetTenantById(Guid.Parse(id));
        if (tenant is null)
            throw new Exception("Tenant not Found");
        repository.DeleteTenant(tenant);
    }

    public Tenant RestoreTenant(string id)
    {
        var tenant = repository.GetTenantById(Guid.Parse(id));
        if (tenant is null)
            throw new ValidationException("Tenant not found");
        return repository.RestoreTenant(tenant);
    }
}