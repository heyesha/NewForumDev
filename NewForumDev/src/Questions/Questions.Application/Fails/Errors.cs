using Shared;

namespace Questions.Application.Fails;

public partial class Errors
{
    public static class General
    {
        public static Error NotFound(Guid id) =>
            Error.Failure("record.not.found", $"Запись по ID - {id} не найдена");
    }

    public static class Questions
    {
        public static Error TooManyQuestions() => 
            Error.Failure("question.too.many", "Пользователь не можеть открыть больше трех вопросов");

        public static Error NotEnoughRating()
            => Error.Failure("not.enough.rating", "Недостаточно рейтинга");
    }
}