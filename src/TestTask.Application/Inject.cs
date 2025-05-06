using Microsoft.Extensions.DependencyInjection;
using TestTask.Application.Commands.SumMinNums;
using TestTask.Application.Commands.SumMinNumsLinq;
using TestTask.Application.Commands.SumMinNumsWithOverflow;

namespace TestTask.Application;

public static class Inject
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<SumMinNumsCommandHandler>();
        services.AddScoped<SumMinNumsWithOverflowCommandHandler>();
        services.AddScoped<SumMinNumsLinqCommandHandler>();


        return services;
    }
}