using Sigma.Domain.Interfaces;
using Sigma.Repository.Repositories;
using Sigma.Service.Services;

namespace Sigma.API.Configurations;

public static class ServiceConfig
{
    public static void AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ICandidateRepository, CandidateRepository>();
        services.AddScoped<ICandidateService, CandidateService>();
    }
}
