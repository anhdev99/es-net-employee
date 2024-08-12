namespace EmployeesESNET.Queries;

public class GetEmployeePaginationQuery
{
    public string Content { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public int Fuzzy { get; set; } = 1;
}