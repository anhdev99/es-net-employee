using AutoMapper;
using EmployeesESNET.Mappings;
using EmployeesESNET.Models;

namespace EmployeesESNET.ViewModels;

public class ESEmployeeVM : IMapFrom<Employee>
{
    public string UserId { get; set; }
    public DateOnly BirthDate { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;
    public string FullName { get; set; } = null!;

    public DateOnly HireDate { get; set; }
    
    public List<Salary> Salary { get; set; }
    public DateTime TimeTemp { get; set; } = DateTime.Now;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Employee, ESEmployeeVM>()
            .ForMember(dest => dest.FullName, opt =>
                opt.MapFrom(src => $"{src.LastName} {src.FirstName}"))
            .ForMember(dest => dest.UserId, opt => 
                opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Salary, opt =>
                opt.MapFrom(src => src.Salaries));
    }
}

public class ESEmployeeModel
{
    public string UserId { get; set; }
    public DateOnly BirthDate { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;
    public string FullName { get; set; } = null!;

    public DateOnly HireDate { get; set; }
}


public partial class ESSalaryVM
{
    public long EmployeeId { get; set; }

    public long Amount { get; set; }

    public DateOnly FromDate { get; set; }

    public DateOnly ToDate { get; set; }
}