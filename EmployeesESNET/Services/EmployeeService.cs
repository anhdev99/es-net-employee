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
    private readonly IGenericRepo<ESSalaryVM> repoSalary;
    
    public EmployeeService(DataContext context, IMapper mapper, 
        IGenericRepo<ESEmployeeVM> repo,
        IGenericRepo<ESSalaryVM> repoSalary)
    {
        this.context = context;
        _mapper = mapper;
        this.repo = repo;
        this.repoSalary = repoSalary;
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

        int index = 0;
        for (int i = 1; i <= totalPages; i++)
        {
            var data = await context.Employees.Include(x => x.Salaries).OrderByDescending(x => x.Id).ProjectTo<ESEmployeeVM>(_mapper.ConfigurationProvider).Skip((i - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

            await repo.Index(data);

            if (index >= 100)
            {
                await Task.Delay(100);
                index = 0;
            }
            else
            {
                index++;
            }
        }
    }
    
    public async Task SyncDataSalaryToEs(CancellationToken cancellationToken)
    {
        int pageNumber = 1;
        int pageSize = 50;
        long totalItems = context.Salaries.LongCount();

        var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

        int index = 0;
        for (int i = 1; i <= totalPages; i++)
        {
            var data = await context.Salaries.OrderByDescending(x => x.EmployeeId).ProjectTo<ESSalaryVM>(_mapper.ConfigurationProvider).Skip((i - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

            await repoSalary.Index(data);

            if (index >= 100)
            {
                await Task.Delay(100);
                index = 0;
            }
            else
            {
                index++;
            }
        }
    }
}