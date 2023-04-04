using Application.Data;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Validators
{
    public class RegisterUserValidator : AbstractValidator<RegisterUserDTO>
    {
        public RegisterUserValidator(IAppDbContext dbContext)
        {
            RuleFor(u => u.ConfirmPassowrd)
                .Equal(u => u.Password);

            RuleFor(u => u.Name)
                .MaximumLength(16);

            RuleFor(u => u.Name)
                .Custom((value, context) =>
                {
                    var nameInUse = dbContext.User.Any(u => u.Name == value);
                    if (nameInUse)
                    {
                        context.AddFailure("Name", "Name already taken.");
                    }
                });
        }
    }
}
