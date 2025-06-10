using System.Text.Json;
using Shared;

namespace NewForumDev.Application.Exceptions;

public class BadRequestException : Exception
{
    protected BadRequestException(Error[] errors) 
        : base(JsonSerializer.Serialize(errors))
    {
    }
}