using credo_bank.Application.Settings;
using credo_bank.DAL.Repositories.Implementation;
using credo_bank.DAL.Repositories.Interfaces;

namespace credo_bank.Configuration;

internal static class ServiceCollectionExtension
{
    public static void AddServices(this IServiceCollection services)
    {
        //AutoMapper
        // services.AddAutoMapper(typeof(AutoMapperProfiler));
        // //FluentValidation
        // services.AddValidatorsFromAssemblyContaining<IApplicationLayerAssemblyMarker>();
        
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ILoanApplicationRepository, LoanApplicationRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
    }
    public static void AddConfigurations(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection(typeof(JwtSettings).Name));
    }
}