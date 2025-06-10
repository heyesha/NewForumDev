using NewForumDev.Application.Exceptions;
using Shared;

namespace NewForumDev.Application.Questions.Fails.Exceptions;

public class TooManyQuestionsException : BadRequestException
{
    public TooManyQuestionsException() 
        : base([Errors.Questions.ToManyQuestions()])
    {
        
    }
}