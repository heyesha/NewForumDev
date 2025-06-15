using Microsoft.Extensions.DependencyInjection;
using NewForumDev.Application.Questions;
using NewForumDev.Application.Questions.Abstractions;
using NewForumDev.Infrastructure.PostgreSQL.Questions;
using NewForumDev.Infrastructure.PostgreSQL.Repositories;

namespace NewForumDev.Infrastructure.PostgreSQL;

public static class DependencyInjection
{
    public static IServiceCollection AddPostgresInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IQuestionsRepository, QuestionsEFCoreRepository>();

        services.AddDbContext<QuestionsReadDbContext>();

        return services;
    }
}