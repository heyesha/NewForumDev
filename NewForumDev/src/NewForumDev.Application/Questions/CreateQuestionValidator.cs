﻿using FluentValidation;
using NewForumDev.Contracts.Questions;

namespace NewForumDev.Application.Questions;

public class CreateQuestionValidator : AbstractValidator<CreateQuestionDto>
{
    public CreateQuestionValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(500).WithMessage("Заголовок невалидный.");

        RuleFor(x => x.Text).NotEmpty().MaximumLength(5000).WithMessage("Текст невалидный.");
        
        RuleFor(x => x.UserId).NotEmpty();
    }
}