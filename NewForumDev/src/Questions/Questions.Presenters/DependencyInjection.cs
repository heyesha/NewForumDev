using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Questions.Application;
using Shared.Abstractions;

namespace Questions.Presenters;

public static class DependencyInjection
{
    public static IServiceCollection AddQuestionsModule(this IServiceCollection services)
    {
        services.AddQuestionsApplication();

        return services;
    }
}