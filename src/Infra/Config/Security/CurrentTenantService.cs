using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using multitenancy.Application.Repositories;
using multitenancy.Infra.Config.Db;

namespace multitenancy.Infra.Config.Security;

public class CurrentTenantService : ICurrentTenant
{
    private TenantDbContext _context;
    public Guid? TenantId { get; set; }

    public CurrentTenantService(TenantDbContext context)
    {
        _context = context;
    }

    public async Task<bool> SetTenant(Guid tenant)
    {
        var tenantDb = await _context
            .Tenants
            .Where(x => x.Id == tenant)
            .Where(x=> x.DeletedAt == null)
            .FirstOrDefaultAsync();
        if (tenantDb is null)
            throw new ValidationException("Tenant not Found!");
        TenantId = tenantDb.Id;
        return true;
    }
}