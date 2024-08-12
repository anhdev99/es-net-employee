using AutoMapper;
using EmployeesESNET.Mappings;
using EmployeesESNET.Models;

namespace EmployeesESNET.ViewModels;

public class EmployeeVM : IMapFrom<Employee>
{
    public DateOnly BirthDate { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;
    public string FullName { get; set; } = null!;

    public DateOnly HireDate { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Employee, EmployeeVM>()
            .ForMember(dest => dest.FullName, opt =>
                opt.MapFrom(src => $"{src.LastName} {src.FirstName}"));
    }
}