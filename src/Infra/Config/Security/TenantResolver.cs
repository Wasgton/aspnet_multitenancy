using multitenancy.Application.Repositories;
using Newtonsoft.Json;

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
        if (string.IsNullOrEmpty(tenantFromHeader))
        {
            context.Response.Clear();
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync(JsonConvert.SerializeObject(new {Error = context.Response.StatusCode, Message = "Tenant not informed"}));
        }
        else
        {
            var parsedTenantId = Guid.Parse(tenantFromHeader!);
            await currentTenant.SetTenant(parsedTenantId);
            await _next(context);
        }
    }
}