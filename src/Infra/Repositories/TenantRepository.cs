using Microsoft.EntityFrameworkCore;
using multitenancy.Application.Models;
using multitenancy.Application.Repositories;
using multitenancy.Infra.Config.Db;

namespace multitenancy.Infra.Repositories;

public class TenantRepository: ITenantRepository
{
    private readonly TenantDbContext _context;
    public TenantRepository(TenantDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Tenant> GetAllTenants()
    {
        return _context.Tenants
            .Where(item => item.DeletedAt == null)
            .ToList();
    }

    public Tenant CreateTenant(Tenant tenant)
    {
        _context.Add(tenant);
        _context.SaveChanges();
        return tenant;
    }

    public Tenant UpdateTenant(Tenant tenant)
    {
        _context.Update(tenant);
        _context.SaveChanges();
        return tenant;
    }

    public Tenant? GetTenantById(Guid id)
    {
        return _context.Tenants.Find(id);
    }

    public void DeleteTenant(Tenant tenant)
    {
        tenant.DeletedAt = DateTime.Now;
        _context.Update(tenant);
        _context.SaveChanges();
    }

    public Tenant RestoreTenant(Tenant tenant)
    {
        tenant.DeletedAt = null;
        _context.Update(tenant);
        _context.SaveChanges();
        return tenant;
    }
}