using Ahura.Application.Contracts.Requests;
using Ahura.Application.Resources;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ahura.Application.FluentValidators;

public class AddUserDtoValidator : AbstractValidator<AddUserDto>
{
    public AddUserDtoValidator()
    {
        RuleFor(x => x.Username).NotEmpty().NotNull().WithMessage(BadRequestMessages.UserNameRequired);

        RuleFor(x => x.Username).MaximumLength(100).WithMessage(BadRequestMessages.UsernameMaxLenght);

        RuleFor(x => x.PhoneNumber).NotEmpty().NotNull().WithMessage(BadRequestMessages.UserNameRequired);
    }
}
