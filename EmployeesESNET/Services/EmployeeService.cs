using AutoMapper;
using AutoMapper.QueryableExtensions;
using EmployeesESNET.Data;
using EmployeesESNET.ESModels;
using EmployeesESNET.Extensions;
using EmployeesESNET.Interfaces;
using EmployeesESNET.Models;
using EmployeesESNET.Queries;
using EmployeesESNET.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace EmployeesESNET.Services;

public class EmployeeService
{
    private readonly DataContext context;
    private readonly IMapper _mapper;
    private readonly IGenericRepo<ESEmployeeVM> repo;
    
    public EmployeeService(DataContext context, IMapper mapper,  IGenericRepo<ESEmployeeVM> repo)
    {
        this.context = context;
        _mapper = mapper;
        this.repo = repo;
    }
    
    public async Task<dynamic> GetEmployeesPaginationQuery(GetEmployeePaginationQuery query, CancellationToken cancellationToken)
    {
        var queryEntities = context.Employees.Where(x => x.Id != 0);

        if (!string.IsNullOrEmpty(query.Content))
        {
            queryEntities = queryEntities.Where(x => x.FirstName.ToLower().Contains(query.Content.ToLower()));
        }
        
        var result = await queryEntities.Skip((query.PageNumber - 1) * query.PageSize).Take(query.PageSize).ToListAsync(cancellationToken);
        return new PaginationResult<dynamic>()
        {
            Data = result
        };
    }
    
    public async Task<EmployeeVM> GetById(int id, CancellationToken cancellationToken)
    {
        return await context.Employees.Where(x => x.Id == id).ProjectTo<EmployeeVM>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
    }

    public async Task SyncDataToEs(CancellationToken cancellationToken)
    {
        int pageNumber = 1;
        int pageSize = 50;
        long totalItems = context.Employees.LongCount();

        var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

        for (int i = 1; i <= totalPages; i++)
        {
            var data = await context.Employees.ProjectTo<ESEmployeeVM>(_mapper.ConfigurationProvider).Skip((i - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

            await repo.Index(data);

            await Task.Delay(1000);
        }
    }
}