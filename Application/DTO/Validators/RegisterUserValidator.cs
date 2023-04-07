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

            RuleFor(u => u.Password)
                .MinimumLength(8)
                .MaximumLength(25);

            RuleFor(u => u.Name)
                .MinimumLength(5)
                .MaximumLength(16);

            RuleFor(u => u.Name)
                .Custom((value, context) =>
                {
                    var nameInUse = dbContext.Users.Any(u => u.Name == value);

                    if (nameInUse == true)
                    {
                        context.AddFailure("Name", "Name already taken.");
                    }
                });
        }
    }
}
