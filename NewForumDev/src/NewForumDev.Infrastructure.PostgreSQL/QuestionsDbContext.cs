using Microsoft.EntityFrameworkCore;
using NewForumDev.Domain.Questions;

namespace NewForumDev.Infrastructure.PostgreSQL;

public class QuestionsDbContext : DbContext
{
    public DbSet<Question> Questions { get; set; }
}