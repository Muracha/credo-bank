using credo_bank.Application.Common;
using credo_bank.Application.Settings;
using credo_bank.Infrastructure.Repositories.Implementation;
using credo_bank.Infrastructure.Repositories.Interfaces;
using credo_bank.Middleware.Events;

namespace credo_bank.Configuration;

internal static class ServiceCollectionExtension
{
    public static void AddServices(this IServiceCollection services)
    {
        //AutoMapper
        services.AddAutoMapper(typeof(IApplicationLayerAssemblyMarker).Assembly);
        
        //Mediator
        services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(IApplicationLayerAssemblyMarker).Assembly));
        
        //Repositories
        services.AddScoped<ILoanApplicationRepository, LoanApplicationRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        
        //SerilogSink
        services.AddScoped<EfCoreSink>();
    }
    public static void AddConfigurations(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection(typeof(JwtSettings).Name));
    }
}