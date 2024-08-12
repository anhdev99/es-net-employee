using EmployeesESNET.ESModels;
using EmployeesESNET.Interfaces;
using EmployeesESNET.Queries;
using EmployeesESNET.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeesESNET.API;

[ApiController]
[Route("api/v1/[controller]")]
public class ESEmployeeController : Controller
{
    private readonly IGenericRepo<ESEmployee> repo;
    private readonly ESService esService;

    public ESEmployeeController(IGenericRepo<ESEmployee> repo, ESService esService)
    {
        this.repo = repo;
        this.esService = esService;
    }
    
    [HttpPost]
    public async Task<IEnumerable<string>> Create(IEnumerable<ESEmployee> employees)
        => await repo.Index(employees);

    [HttpGet]
    public async Task<ESEmployee> Read(string id)
        => await repo.Get(id);

    [HttpPut]
    public async Task<bool> Update(ESEmployee employee, string id)
        => await repo.Update(employee, id);

    [HttpDelete]
    public async Task<bool> Delete(string id)
        => await repo.Delete(id);

    [HttpPost]
    [Route("search")]
    public async Task<IActionResult> Search([FromBody] GetEmployeePaginationQuery query)
    {
        var result = await esService.Search(query);
        return Ok(result);
    }
}