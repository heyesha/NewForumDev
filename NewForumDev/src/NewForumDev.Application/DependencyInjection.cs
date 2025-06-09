using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using NewForumDev.Application.Questions;

namespace NewForumDev.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        services.AddScoped<IQuestionService, QuestionsService>();

        return services;
    }
}