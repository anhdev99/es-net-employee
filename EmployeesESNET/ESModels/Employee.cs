using System.Text.Json.Serialization;

namespace EmployeesESNET.ESModels;

public class ESEmployee
{
    public long Id { get; set; }
    
    [JsonIgnore]

    public DateOnly BirthDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;
    [JsonIgnore]
    public DateOnly HireDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    
    public string FullName => $"{FirstName} {LastName}";
}