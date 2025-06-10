using Shared;

namespace NewForumDev.Application.Questions.Fails;

public partial class Errors
{
    public static class Questions
    {
        public static Error ToManyQuestions()
            => Error.Failure("question.too.many", "Пользователь не можеть открыть больше трех вопросов");
    }
}