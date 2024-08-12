using EmployeesESNET.ESModels;
using EmployeesESNET.Queries;
using EmployeesESNET.ViewModels;
using Nest;

namespace EmployeesESNET.Services;

public class ESService
{
    private readonly IElasticClient client;

    public ESService(IElasticClient client)
    {
        this.client = client;
    }

    public async Task<dynamic> Search(GetEmployeePaginationQuery query)
    {
        var searchResponse = await client.SearchAsync<ESEmployee>(s => s
            .Query(q => q
                .Fuzzy(f => f
                        .Field(f => f.FirstName)
                        .Value(query.Content)
                        .Fuzziness(Fuzziness.EditDistance(query.Fuzzy)) // Độ mờ
                )
            )
            .From((query.PageNumber - 1) * query.PageSize).Size(query.PageSize)
        );
        
        return new PaginationResult<dynamic>()
        {
            Data = searchResponse.Hits
        };
        // foreach (var hit in searchResponse.Hits)
        // {
        //     Console.WriteLine($"Id: {hit.Id}, FullName: {hit.Source.FullName}");
        // }

    }
}