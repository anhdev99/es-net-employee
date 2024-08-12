using System.Text.Json.Serialization;

namespace EmployeesESNET.ViewModels;

public class PaginationResult<T>
{
    [JsonIgnore]
    public long Total { get; set; }
    public T Data { get; set; }
}