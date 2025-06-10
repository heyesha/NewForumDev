using Microsoft.Extensions.DependencyInjection;
using NewForumDev.Application.Questions;
using NewForumDev.Application.Questions.Interfaces;
using NewForumDev.Infrastructure.PostgreSQL.Repositories;

namespace NewForumDev.Infrastructure.PostgreSQL;

public static class DependencyInjection
{
    public static IServiceCollection AddPostgresInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IQuestionsRepository, QuestionsEFCoreRepository>();

        services.AddDbContext<QuestionsDbContext>();

        return services;
    }
}