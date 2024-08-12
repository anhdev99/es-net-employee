using Elasticsearch.Net;
using EmployeesESNET.Interfaces;
using EmployeesESNET.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;

namespace EmployeesESNET.Configuraitons;

public static class ESInstallers
{
    public static void AddESService(this IServiceCollection services, IConfiguration configuration)
    {
        var credentials = configuration.GetSection("Elastic");
        var settings = new ConnectionSettings(new Uri(credentials["host"]))
            .BasicAuthentication(credentials["username"], credentials["password"])
            .ServerCertificateValidationCallback(CertificateValidations.AllowAll)
            .DefaultIndex("employee");

        services.AddSingleton<IElasticClient>(new ElasticClient(settings));
        services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));
    }
}