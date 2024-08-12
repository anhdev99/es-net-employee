using EmployeesESNET.Helpers;
using EmployeesESNET.Queries;
using EmployeesESNET.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeesESNET.API;

[Route("api/v1/[controller]")]
public class EmployeeController: Controller
{
    private readonly EmployeeService _employeeService;
    public EmployeeController(EmployeeService employeeService)
    {
        _employeeService = employeeService;
    }
    
    [HttpPost]
    [Route("get-employees")]
    public async Task<IActionResult> GetEmployees([FromBody] GetEmployeePaginationQuery query, CancellationToken cancellationToken)
    {
        var response = await _employeeService.GetEmployeesPaginationQuery(query, cancellationToken);

        return Ok(response);
    }

    [HttpGet]
    [Route("get-by-id/{id}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _employeeService.GetById(id, cancellationToken);

            return Ok(
                new ResultResponse<dynamic>()
                    .WithData(response)
                    .WithCode(StatusCodes.Status200OK)
                    .WithMessage("Get data success.")
            );
        }
        catch (Exception ex)
        {
            return Ok(
                new ResultResponse<dynamic>()
                    .WithCode(StatusCodes.Status404NotFound)
                    .WithMessage("Data not found.")
            );
        }
    }

    [HttpGet]
    [Route("sync-data-to-es")]
    public async Task<IActionResult> SyncDataToES(CancellationToken cancellationToken)
    {
        try
        {
            await _employeeService.SyncDataToEs(cancellationToken);

            return Ok(
                new ResultResponse<dynamic>()
                    .WithCode(StatusCodes.Status200OK)
                    .WithMessage("Import Data Success.")
            );
        }
        catch (Exception ex)
        {
            return Ok(
                new ResultResponse<dynamic>()
                    .WithCode(StatusCodes.Status404NotFound)
                    .WithMessage("Error: " + ex.Message)
            );
        } 
    }
    
    [HttpGet]
    [Route("sync-data-salary-to-es")]
    public async Task<IActionResult> SyncDataSalaryToEs(CancellationToken cancellationToken)
    {
        try
        {
            await _employeeService.SyncDataSalaryToEs(cancellationToken);

            return Ok(
                new ResultResponse<dynamic>()
                    .WithCode(StatusCodes.Status200OK)
                    .WithMessage("Import Data Success.")
            );
        }
        catch (Exception ex)
        {
            return Ok(
                new ResultResponse<dynamic>()
                    .WithCode(StatusCodes.Status404NotFound)
                    .WithMessage("Error: " + ex.Message)
            );
        } 
    }
}