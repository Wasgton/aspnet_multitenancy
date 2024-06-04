using multitenancy.Application.Repositories;
using multitenancy.Application.Services;
using multitenancy.Infra.Config.Db;
using multitenancy.Infra.Config.Security;
using multitenancy.Infra.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddDbContext<TenantDbContext>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<TenantService>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<ITenantRepository, TenantRepository>();
builder.Services.AddScoped<ICurrentTenant, CurrentTenantService>();
builder.Services.AddSwaggerGen();
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();
app.UseMiddleware<TenantResolver>();
app.MapControllers();
app.Run();