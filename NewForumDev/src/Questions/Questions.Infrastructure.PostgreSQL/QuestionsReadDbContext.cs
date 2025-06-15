using Microsoft.EntityFrameworkCore;
using Questions.Application.Abstractions;
using Questions.Domain;

namespace Questions.Infrastructure.PostgreSQL;

public class QuestionsReadDbContext : DbContext, IQuestionsReadDbContext
{
    public DbSet<Question> Questions { get; set; }

    public IQueryable<Question> ReadQuestions => Questions.AsNoTracking().AsQueryable();
}