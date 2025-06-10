using NewForumDev.Application.Exceptions;
using Shared;

namespace NewForumDev.Application.Questions.Fails.Exceptions;

public class QuestionValidationException : BadRequestException
{
    public QuestionValidationException(Error[] errors) 
        : base(errors)
    {
        
    }
}