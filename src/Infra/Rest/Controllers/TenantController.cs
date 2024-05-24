using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using multitenancy.Infra.Rest.Dtos.Tenant;
using multitenancy.Application.Services;

namespace multitenancy.Infra.Rest.Controllers;

[ApiController]
[Route("api/[controller]/")]
public class TenantController : ControllerBase
{
    private TenantService _tenantService;
    public TenantController(TenantService service)
    {
        _tenantService = service;
    }
    
    [HttpGet]
    public ActionResult GetAllTenants()
    {
        try
        {
            var result = _tenantService.GetAllTenants();
            return Ok(result);
        }
        catch (ValidationException exception)
        {
            return BadRequest(exception.Message);
        }
        catch (Exception exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error while processing the operation");
        }
    }

    [HttpPost("create")]
    public ActionResult CreateTenant([FromBody] CreateTenantRequest tenantRequest)
    {
        var result = _tenantService.CreateTenant(tenantRequest);
        CreateTenantResponse output = new CreateTenantResponse(result.Id.ToString(), result.Name);
        return StatusCode(StatusCodes.Status201Created, output);
    }

    [HttpDelete("delete/{id}")]
    public ActionResult DeleteTenant(string id)
    {
        _tenantService.DeleteTenant(id);
        return Ok("Tenant Deleted");
    }
    
    [HttpPut("update/{id}")]
    public ActionResult UpdateTenant([FromBody] UpdateTenantRequest tenantRequest, string id)
    {
        try
        {
            var tenant = _tenantService.UpdateTenant(id, tenantRequest);
            return Ok(tenant);
        }
        catch (ValidationException exception)
        {
            return BadRequest(exception.Message);
        }
        catch (Exception exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error while processing the operation");
        }
    }
    
    [HttpPut("restore/{id}")]
    public ActionResult RestoreTenant(string id)
    {
        try
        {
            var tenant = _tenantService.RestoreTenant(id);
            return Ok(tenant);
        }
        catch (ValidationException exception)
        {
            return BadRequest(exception.Message);
        }
        catch (Exception exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error while processing the operation");
        }
    }
    
}