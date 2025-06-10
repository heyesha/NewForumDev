using NewForumDev.Application.Exceptions;
using Shared;

namespace NewForumDev.Application.Questions.Fails.Exceptions;

public class QuestionNotFoundException : NotFoundException
{
    public QuestionNotFoundException(Error[] errors) 
        : base(errors)
    {
        
    }
}