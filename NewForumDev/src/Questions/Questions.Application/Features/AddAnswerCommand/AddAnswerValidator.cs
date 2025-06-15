﻿using FluentValidation;
using Questions.Contracts.Dtos;

namespace Questions.Application.Features.AddAnswerCommand;

public class AddAnswerValidator : AbstractValidator<AddAnswerDto>
{
    public AddAnswerValidator()
    {
        RuleFor(x => x.Text)
            .NotEmpty().MaximumLength(5000).WithMessage("Текст невалидный.");
    }
}