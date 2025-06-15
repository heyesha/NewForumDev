using Microsoft.Extensions.DependencyInjection;
using Questions.Application.Abstractions;

namespace Questions.Infrastructure.PostgreSQL;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IQuestionsRepository, QuestionsEFCoreRepository>();

        services.AddDbContext<QuestionsReadDbContext>();

        return services;
    }
}