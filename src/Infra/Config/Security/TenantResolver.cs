using multitenancy.Application.Repositories;

namespace multitenancy.Infra.Config.Security;

public class TenantResolver
{
    private readonly RequestDelegate _next;

    public TenantResolver(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ICurrentTenant currentTenant)
    {
        context.Request.Headers.TryGetValue("tenant", out var tenantFromHeader);
        if (string.IsNullOrEmpty(tenantFromHeader) == false)
        {
            var parsedTenantId = Guid.Parse(tenantFromHeader!);
            await currentTenant.SetTenant(parsedTenantId);
        }
        await _next(context);
    }
}